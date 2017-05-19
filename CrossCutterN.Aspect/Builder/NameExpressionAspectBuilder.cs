/**
 * Description: name expression concern attribute aspect builder
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Concern;

    internal class NameExpressionAspectBuilder : AspectBuilderWithDefaultOptions, IWriteOnlyNameExpressionAspectBuilder
    {
        private readonly IDictionary<string, bool> _includes = new Dictionary<string, bool>();
        private readonly ISet<string> _excludes = new HashSet<string>(); 

        public NameExpressionAspectBuilder(ICollection<string> includes, ICollection<string> excludes)
        {
            if (includes == null || !includes.Any())
            {
                throw new ArgumentNullException("includes");
            }
            foreach (var pattern in includes)
            {
                if (String.IsNullOrWhiteSpace(pattern))
                {
                    throw new ArgumentException("Empty string can't serve as pattern", "includes");
                }
                if (_includes.ContainsKey(pattern))
                {
                    throw new ArgumentException(string.Format("Duplicated pattern: {0}", pattern), "includes");
                }
                _includes.Add(FormatPattern(pattern), ContainsWildCard(pattern));
            }
            if (excludes != null && excludes.Any())
            {
                foreach (var pattern in excludes)
                {
                    if (String.IsNullOrWhiteSpace(pattern))
                    {
                        throw new ArgumentException("Empty string can't serve as pattern", "excludes");
                    }
                    if (_excludes.Contains(pattern))
                    {
                        throw new ArgumentException(string.Format("Duplicated pattern: {0}", pattern), "excludes");
                    }
                    if (_includes.ContainsKey(pattern))
                    {
                        throw new ArgumentException(
                            string.Format("Pattern include/exclude confliction: {0}, no point to exclude and include the same pattern", pattern), "excludes");
                    }
                    _excludes.Add(FormatPattern(pattern));
                }
            }
        }

        public override IAspect GetAspect(IMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            ReadOnly.Assert(true);
            var aspect = AspectFactory.InitializeAspect();
            var fullName = string.Format("{0}.{1}", method.ClassFullName, method.MethodName);
            var joinPoints = Enum.GetValues(typeof (JoinPoint)).Cast<JoinPoint>().ToList();
            var match = PatternMatch(fullName);
            if((match == PatternMatchType.WildCard && ConcernMethod && MethodMatch(method)) ||
                match == PatternMatchType.Exact)
            {
                foreach (var joinPoint in joinPoints)
                {
                    if(ContainsJoinPoint(joinPoint))
                    {
                        aspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                    }
                }
            }
            return aspect.ToReadOnly();
        }

        public override IPropertyAspect GetAspect(IProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            ReadOnly.Assert(true);
            var getterAspect = AspectFactory.InitializeAspect();
            var setterAspect = AspectFactory.InitializeAspect();
            var fullName = string.Format("{0}.{1}", property.ClassFullName, property.PropertyName);
            var joinPoints = Enum.GetValues(typeof (JoinPoint)).Cast<JoinPoint>().ToList();
            var match = PatternMatch(fullName);
            if ((match == PatternMatchType.WildCard && ConcernPropertyGetter && PropertyGetterMatch(property)) ||
                match == PatternMatchType.Exact)
            {
                foreach (var joinPoint in joinPoints)
                {
                    if(ContainsJoinPoint(joinPoint))
                    {
                        getterAspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                    }
                }
            }

            if ((match == PatternMatchType.WildCard && ConcernPropertySetter && PropertySetterMatch(property)) ||
                match == PatternMatchType.Exact)
            {
                foreach (var joinPoint in joinPoints)
                {
                    if (ContainsJoinPoint(joinPoint))
                    {
                        setterAspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                    }
                }
            }
            return AspectFactory.InitializePropertyAspect(getterAspect.ToReadOnly(), setterAspect.ToReadOnly());
        }

        private bool MethodMatch(IMethod method)
        {
            if (method.IsConstructor && !ConcernConstructor)
            {
                return false;
            }
            if (method.IsInstance && !ConcernInstance)
            {
                return false;
            }
            if (!method.IsInstance && !ConcernStatic)
            {
                return false;
            }
            return AccessibilityMatch(method.Accessibility);
        }

        private bool PropertyGetterMatch(IProperty property)
        {
            return PropertyMatch(property) && property.GetterAccessibility.HasValue && 
                AccessibilityMatch(property.GetterAccessibility.Value);
        }

        private bool PropertySetterMatch(IProperty property)
        {
            return PropertyMatch(property) && property.SetterAccessibility.HasValue &&
                AccessibilityMatch(property.SetterAccessibility.Value);
        }

        private bool PropertyMatch(IProperty property)
        {
            if (property.IsInstance && !ConcernInstance)
            {
                return false;
            }
            if (!property.IsInstance == !ConcernStatic)
            {
                return false;
            }
            return true;
        }

        private bool AccessibilityMatch(Accessibility accessibility)
        {
            if (accessibility == Accessibility.Internal && !ConcernInternal)
            {
                return false;
            }
            if (accessibility == Accessibility.Private && !ConcernPrivate)
            {
                return false;
            }
            if (accessibility == Accessibility.Protected && !ConcernProtected)
            {
                return false;
            }
            if (accessibility == Accessibility.Public && !ConcernPublic)
            {
                return false;
            }
            return true;
        }

        private PatternMatchType PatternMatch(string name)
        {
            var result = PatternMatchType.No;
            foreach (var pattern in _includes)
            {
                if (Regex.IsMatch(name, pattern.Key))
                {
                    if (pattern.Value)
                    {
                        result = PatternMatchType.WildCard;
                    }
                    else
                    {
                        result = PatternMatchType.Exact;
                        break;
                    }
                }
            }
            // exact match overwrites exclusion
            if (result == PatternMatchType.WildCard)
            {
                // exclusion overwrites wildcard match
                if (_excludes.Any(pattern => Regex.IsMatch(name, pattern)))
                {
                    result = PatternMatchType.No;
                }
            }
            return result;
        }

        private static bool ContainsWildCard(string pattern)
        {
            return pattern.Contains('*');
        }

        private static string FormatPattern(string pattern)
        {
            const string validation = "[a-zA-Z_\\*][a-zA-Z0-9\\*_]*";
            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Empty name expression is not acceptable");
            }
            var sections = pattern.Split('.');
            foreach (var str in sections)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    throw new ArgumentException("Invalid input pattern for name expression");
                }
                if (!Regex.IsMatch(str, validation))
                {
                    throw new ArgumentException("Invalid input pattern for name expression");
                }
                if (str.Contains("**"))
                {
                    throw new ArgumentException("Invalid input pattern for name expression");
                }
            }
            return string.Join("\\.", sections).Replace("*", ".*");
        }

        private enum PatternMatchType { No, Exact, WildCard }
    }
}

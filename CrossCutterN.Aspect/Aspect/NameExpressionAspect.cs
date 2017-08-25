// <copyright file="NameExpressionAspect.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using CrossCutterN.Aspect.Metadata;

    /// <summary>
    /// Aspect that identifies target methods and properties by method/property name expression.
    /// </summary>
    internal sealed class NameExpressionAspect : SwitchableAspectWithDefaultOptions
    {
        private readonly IDictionary<string, bool> includes = new Dictionary<string, bool>();
        private readonly ISet<string> excludes = new HashSet<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NameExpressionAspect"/> class.
        /// </summary>
        /// <param name="includes">Patterns used to include target methods/properties.</param>
        /// <param name="excludes">Patterns used to exclude target methods/properties.</param>
        public NameExpressionAspect(ICollection<string> includes, ICollection<string> excludes)
        {
            if (includes == null || !includes.Any())
            {
                throw new ArgumentNullException("includes");
            }

            foreach (var pattern in includes)
            {
                if (string.IsNullOrWhiteSpace(pattern))
                {
                    throw new ArgumentException("Empty string can't serve as pattern", "includes");
                }

                if (this.includes.ContainsKey(pattern))
                {
                    throw new ArgumentException($"Duplicated pattern: {pattern}", "includes");
                }

                this.includes.Add(FormatPattern(pattern), ContainsWildCard(pattern));
            }

            if (excludes != null && excludes.Any())
            {
                foreach (var pattern in excludes)
                {
                    if (string.IsNullOrWhiteSpace(pattern))
                    {
                        throw new ArgumentException("Empty string can't serve as pattern", "excludes");
                    }

                    if (this.excludes.Contains(pattern))
                    {
                        throw new ArgumentException($"Duplicated pattern: {pattern}", "excludes");
                    }

                    if (this.includes.ContainsKey(pattern))
                    {
                        throw new ArgumentException($"Pattern include/exclude confliction: {pattern}, no point to exclude and include the same pattern", "excludes");
                    }

                    this.excludes.Add(FormatPattern(pattern));
                }
            }
        }

        private enum PatternMatchType
        {
            No,
            Exact,
            WildCard,
        }

        /// <inheritdoc/>
        public override bool CanApplyTo(IMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            ReadOnly.Assert(true);
            var fullName = $"{method.ClassFullName}.{method.MethodName}";
            var joinPoints = Enum.GetValues(typeof(JoinPoint)).Cast<JoinPoint>().ToList();
            var match = PatternMatch(fullName);
            return (match == PatternMatchType.WildCard && ConcernMethod && MethodMatch(method)) || match == PatternMatchType.Exact;
        }

        /// <inheritdoc/>
        public override PropertyConcern CanApplyTo(IProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            ReadOnly.Assert(true);
            var fullName = $"{property.ClassFullName}.{property.PropertyName}";
            var joinPoints = Enum.GetValues(typeof(JoinPoint)).Cast<JoinPoint>().ToList();
            var match = PatternMatch(fullName);
            var concern = PropertyConcern.None;
            if ((match == PatternMatchType.WildCard && ConcernPropertyGetter && PropertyGetterMatch(property)) ||
                match == PatternMatchType.Exact)
            {
                concern = concern.ConcernGetter();
            }

            if ((match == PatternMatchType.WildCard && ConcernPropertySetter && PropertySetterMatch(property)) ||
                match == PatternMatchType.Exact)
            {
                concern = concern.ConcernSetter();
            }

            return concern;
        }

        private static bool ContainsWildCard(string pattern) => pattern.Contains('*');

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
            foreach (var pattern in includes)
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
                if (excludes.Any(pattern => Regex.IsMatch(name, pattern)))
                {
                    result = PatternMatchType.No;
                }
            }

            return result;
        }
    }
}

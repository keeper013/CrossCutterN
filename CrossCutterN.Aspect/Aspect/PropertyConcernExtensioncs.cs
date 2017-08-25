// <copyright file="PropertyConcernExtensioncs.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    /// <summary>
    /// Extension of <see cref="PropertyConcern"/>.
    /// </summary>
    public static class PropertyConcernExtensioncs
    {
        /// <summary>
        /// Gets a value indicating whether the property conern concerns getter method.
        /// </summary>
        /// <param name="concern">The property concern.</param>
        /// <returns>True if the property concern concerns getter, false elsewise.</returns>
        public static bool IsGetterConcerned(this PropertyConcern concern)
        {
            return (concern & PropertyConcern.Getter) == PropertyConcern.Getter;
        }

        /// <summary>
        /// Gets a value indicating whether the property conern concerns setter method.
        /// </summary>
        /// <param name="concern">The property concern.</param>
        /// <returns>True if the property concern concerns setter, false elsewise.</returns>
        public static bool IsSetterConcerned(this PropertyConcern concern)
        {
            return (concern & PropertyConcern.Setter) == PropertyConcern.Setter;
        }

        /// <summary>
        /// Apply concern getter to a <see cref="PropertyConcern"/>
        /// </summary>
        /// <param name="concern">The property concern.</param>
        /// <returns>The new value with concern getter method value applied.</returns>
        public static PropertyConcern ConcernGetter(this PropertyConcern concern)
        {
            return concern | PropertyConcern.Getter;
        }

        /// <summary>
        /// Apply concern setter to a <see cref="PropertyConcern"/>
        /// </summary>
        /// <param name="concern">The property concern.</param>
        /// <returns>The new value with concern setter method value applied.</returns>
        public static PropertyConcern ConcernSetter(this PropertyConcern concern)
        {
            return concern | PropertyConcern.Setter;
        }

        /// <summary>
        /// Gets a value indicating whether the concern value concerns at least one method in the property.
        /// </summary>
        /// <param name="concern">The concern value.</param>
        /// <returns>True if the concern value concerns at least one of the methods in the property, false elsewise.</returns>
        public static bool IsConcerned(this PropertyConcern concern)
        {
            return concern != PropertyConcern.None;
        }
    }
}

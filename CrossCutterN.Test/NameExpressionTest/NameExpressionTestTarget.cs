// <copyright file="NameExpressionTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.NameExpressionTest
{
    /// <summary>
    /// Name expression test target.
    /// </summary>
    public static class NameExpressionTestTarget
    {
        private static int value;

        /// <summary>
        /// Gets or sets some property to be concerned.
        /// </summary>
        public static int PropertyToBeConcerned
        {
            get => value;

            set { NameExpressionTestTarget.value = value; }
        }

        /// <summary>
        /// Gets or sets some property not to be concerned.
        /// </summary>
        public static int PropertyNotToBeConcerned
        {
            get => value;

            set { NameExpressionTestTarget.value = value; }
        }

        /// <summary>
        /// Gets or sets some property not mentioned.
        /// </summary>
        public static int PropertyNotMentioned
        {
            get { return value; }
            set { NameExpressionTestTarget.value = value; }
        }

        /// <summary>
        /// Some method to be concerned.
        /// </summary>
        public static void MethodToBeConcerned()
        {
        }

        /// <summary>
        /// A method not to be concerned.
        /// </summary>
        public static void MethodNotToBeConcerned()
        {
        }

        /// <summary>
        /// Some method not to be concerned.
        /// </summary>
        public static void MethodNotMentioned()
        {
            ToBeMatchedButIsPrivate();
        }

        /// <summary>
        /// Some method exluded by not exact match by included by exact match.
        /// </summary>
        public static void NotMentionedToTestExactOverwrite()
        {
            NotToBeMatchedButMatchedByExact();
        }

        private static void ToBeMatchedButIsPrivate()
        {
        }

        private static void NotToBeMatchedButMatchedByExact()
        {
        }
    }
}

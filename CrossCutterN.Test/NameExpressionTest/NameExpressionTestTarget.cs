/**
 * Description: name expression test target
 * Author: David Cui
 */

namespace CrossCutterN.Test.NameExpressionTest
{
    public class NameExpressionTestTarget
    {
        private static int _value;
        public static void MethodToBeConcerned()
        {
            
        }

        public static int PropertyToBeConcerned
        {
            get { return _value; }
            set { _value = value; }
        }

        public static void MethodNotToBeConcerned()
        {
            
        }

        public static int PropertyNotToBeConcerned
        {
            get { return _value; }
            set { _value = value; }
        }

        public static void MethodNotMentioned()
        {
            ToBeMatchedButIsPrivate();
        }

        public static int PropertyNotMentioned
        {
            get { return _value; }
            set { _value = value; }
        }

        private static void ToBeMatchedButIsPrivate()
        {
            
        }

        public static void NotMentionedToTestExactOverwrite()
        {
            NotToBeMatchedButMatchedByExact();
        }

        private static void NotToBeMatchedButMatchedByExact()
        {
            
        }
    }
}

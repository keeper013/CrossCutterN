/**
 * Description: Options Test Targets
 * Author: David Cui
 */

using System.Globalization;

namespace CrossCutterN.Test.OptionsTest
{
    using System;
    using System.Text;

    [OptionsConcernClass]
    internal abstract class OptionsTestTargetBase
    {
        private static string _str1;
        private readonly StringBuilder _stringBuilder = new StringBuilder("OptionsTestTargetBase");

        public static int PublicStaticInt { get { return 1; } }
        protected string ProtectedInstanceString { set { _str1 = value; } }
        internal Func<int, int> InternalInstanceFunc { get; set; }
        private StringBuilder StringBuilder { get { return _stringBuilder; } }

        static OptionsTestTargetBase()
        {
            _str1 = "x";
        }

        protected OptionsTestTargetBase()
        {
            InternalInstanceFunc = Square;
        }

        public StringBuilder PublicReturnObj(int x, string y)
        {
            Console.Out.WriteLine("{0} {1}", x, y);
            var strb = StringBuilder;
            strb.Append(x);
            strb.Append(y + 1);
            strb.Append(_str1);
            return new StringBuilder(strb.ToString());
        }

        protected static string ProtectedReturnString()
        {
            return PublicStaticInt.ToString(CultureInfo.InvariantCulture);
        }

        internal Func<int, int> InternalMultipleParameter(ref StringBuilder strb, int x, string y, Action<int> action)
        {
            if(strb != null)
            {
                strb.Append(x).Append(y);
            }
            if (action != null)
            {
                action(x);
            }
            return InternalInstanceFunc;
        }

        private static int Square(int i)
        {
            return i * i;
        }

        public abstract void TestAbstract(int i);
    }

    internal class OptionsTestTargetDerived : OptionsTestTargetBase
    {
        public static int StaticInt { get; set; }

        public OptionsTestTargetDerived()
        {
            StaticInt = 100;
        }

        public int Square(int i)
        {
            return i * i;
        }

        public override void TestAbstract(int i)
        {
            var str = ProtectedReturnString();
            var x = i + StaticInt;
            Console.Out.WriteLine(str + Square(x));
        }
    }
}

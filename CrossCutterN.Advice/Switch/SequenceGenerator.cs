/**
* Description: Advice switch implementation
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    internal class SequenceGenerator
    {
        private int _value;

        public int Next()
        {
            _value++;
            return _value;
        }
    }
}

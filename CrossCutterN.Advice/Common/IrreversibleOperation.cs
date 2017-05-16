/**
 * Description: Irreversible operation helper
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Common
{
    using System;

    public sealed class IrreversibleOperation
    {
        private bool _applied;

        public IrreversibleOperation()
        {
            _applied = false;
        }

        public void Assert(bool applied)
        {
            if(_applied != applied)
            {
                throw new InvalidOperationException(
                    string.Format("Irreversible operation validation failed, expecting {0}, got {1}", _applied, applied));
            }
        }

        public void Apply()
        {
            Assert(false);
            _applied = true;
        }
    }
}

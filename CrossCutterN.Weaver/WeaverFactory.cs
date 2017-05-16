/**
* Description: weaver factory
* Author: David Cui
*/

namespace CrossCutterN.Weaver
{
    public static class WeaverFactory
    {
        public static ICanAddAspectBuilder InitializeWeaver()
        {
            return new Weaver();
        }
    }
}

/**
 * Description: Method weaving factory
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using Mono.Cecil;

    internal static class WeavingFactory
    {
        public static IWeavingContext InitializeMethodWeavingContext(ModuleDefinition module)
        {
            return new WeavingContext(module);
        }

        public static IlHandler InitializeIlHandler(MethodDefinition method, IWeavingContext context)
        {
            return new IlHandler(method, context);
        }

        public static IlHandler InitializeStaticConstructorIlHandler(TypeDefinition type, IWeavingContext context)
        {
            return new IlHandler(type, context);
        }
    }
}

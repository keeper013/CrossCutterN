/**
 * Description: No trace attribute
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Concern
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class NoConcernAttribute : Attribute
    {
    }
}

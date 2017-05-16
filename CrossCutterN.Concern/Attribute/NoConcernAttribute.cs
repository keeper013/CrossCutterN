/**
 * Description: No trace attribute
 * Author: David Cui
 */

namespace CrossCutterN.Concern.Attribute
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class NoConcernAttribute : Attribute
    {
    }
}

/**
 * Description: advice switch interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Reflection;

    public interface IAdviceSwitch
    {
        int Switch(Type type);
        int Switch(MethodInfo method);
        int Switch(PropertyInfo property);
        int Switch(string aspect);
        int Switch(Type type, string aspect);
        int Switch(MethodInfo method, string aspect);
        int Switch(PropertyInfo property, string aspect);

        int SwitchOn(Type type);
        int SwitchOn(MethodInfo method);
        int SwitchOn(PropertyInfo property);
        int SwitchOn(string aspect);
        int SwitchOn(Type type, string aspect);
        int SwitchOn(MethodInfo method, string aspect);
        int SwitchOn(PropertyInfo property, string aspect);

        int SwitchOff(Type type);
        int SwitchOff(MethodInfo method);
        int SwitchOff(PropertyInfo property);
        int SwitchOff(string aspect);
        int SwitchOff(Type type, string aspect);
        int SwitchOff(MethodInfo method, string aspect);
        int SwitchOff(PropertyInfo property, string aspect);

        /// <summary>
        /// Without clear requirements, this version doesn't support massive switch status lookup
        /// This is the only lookup interface for user to check switch status for an aspect in a method
        /// </summary>
        bool? GetSwitchStatus(MethodInfo method, string aspect);
    }
}

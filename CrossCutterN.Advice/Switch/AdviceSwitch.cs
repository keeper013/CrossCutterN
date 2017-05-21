/**
* Description: Advice switch implementation
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class AdviceSwitch : IAdviceSwitch, IAdviceSwitchBuildUp, IAdviceSwitchLookUp
    {
        private IDictionary<int, bool> _switchDictionary = new Dictionary<int, bool>();

        public void Complete(string clazz)
        {
            throw new NotImplementedException();
        }

        public bool IsOn(int id)
        {
            if(!_switchDictionary.ContainsKey(id))
            {
                throw new InvalidOperationException(string.Format("Switch for id {0} is not found", id));
            }
            return _switchDictionary[id];
        }

        public void RegisterSwitch(int id, string clazz, string property, string method, string aspect, bool value)
        {
            throw new NotImplementedException();
        }

        public int Switch(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public int Switch(string aspect)
        {
            throw new NotImplementedException();
        }

        public int Switch(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public int Switch(Type type)
        {
            throw new NotImplementedException();
        }

        public int Switch(PropertyInfo property, string aspect)
        {
            throw new NotImplementedException();
        }

        public int Switch(MethodInfo method, string aspect)
        {
            throw new NotImplementedException();
        }

        public int Switch(Type type, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(Type type)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(PropertyInfo property, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(MethodInfo method, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOff(Type type, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(Type type)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(PropertyInfo property, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(MethodInfo method, string aspect)
        {
            throw new NotImplementedException();
        }

        public int SwitchOn(Type type, string aspect)
        {
            throw new NotImplementedException();
        }
    }
}

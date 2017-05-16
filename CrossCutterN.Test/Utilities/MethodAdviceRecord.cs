/**
 * Description: method advice calling record
 * Author: David Cui
 */

namespace CrossCutterN.Test.Utilities
{
    using System;
    using Advice.Parameter;

    internal class MethodAdviceRecord
    {
        public MethodAdviceRecord(string name, IExecution execution, Exception exception, IReturn rReturn, bool? hasException)
        {
            Name = name;
            Execution = execution;
            Exception = exception;
            Return = rReturn;
            HasException = hasException;
        }

        public string Name { get; private set; }
        public IExecution Execution { get; private set; }
        public Exception Exception { get; private set; }
        public IReturn Return { get; private set; }
        public bool? HasException { get; private set; }
    }
}

// <copyright file="MethodAdviceRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.Utilities
{
    using System;
    using CrossCutterN.Base.Metadata;

    /// <summary>
    /// Advice calling record.
    /// </summary>
    internal sealed class MethodAdviceRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodAdviceRecord"/> class.
        /// </summary>
        /// <param name="name">Name of the advice call.</param>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="exception">System.Exception parameter.</param>
        /// <param name="rReturn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public MethodAdviceRecord(string name, IExecutionContext context, IExecution execution, Exception exception, IReturn rReturn, bool? hasException)
        {
            Name = name;
            Context = context;
            Execution = execution;
            Exception = exception;
            Return = rReturn;
            HasException = hasException;
        }

        /// <summary>
        /// Gets name of the advice call.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets <see cref="IExecutionContext"/> parameter.
        /// </summary>
        public IExecutionContext Context { get; private set; }

        /// <summary>
        /// Gets <see cref="IExecution"/> parameter.
        /// </summary>
        public IExecution Execution { get; private set; }

        /// <summary>
        /// Gets System.Exception parameter.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets <see cref="IReturn"/> parameter.
        /// </summary>
        public IReturn Return { get; private set; }

        /// <summary>
        /// Gets HasException parameter.
        /// </summary>
        public bool? HasException { get; private set; }
    }
}

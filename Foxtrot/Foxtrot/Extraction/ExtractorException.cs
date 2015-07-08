using System;
using System.Runtime.Serialization;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Application-level exceptions thrown by this component.
    /// </summary>
    public class ExtractorException : Exception
    {
        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public ExtractorException()
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public ExtractorException(string s) : base(s)
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public ExtractorException(string s, Exception inner) : base(s, inner)
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public ExtractorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
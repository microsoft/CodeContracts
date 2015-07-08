using System;
using System.Runtime.Serialization;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Application-level exceptions thrown by this component.
    /// </summary>
    public class RewriteException : Exception
    {
        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public RewriteException()
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public RewriteException(string s) : base(s)
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public RewriteException(string s, Exception inner) : base(s, inner)
        {
        }

        /// <summary>
        /// Exception specific to an error occurring in the contract rewriter
        /// </summary>
        public RewriteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
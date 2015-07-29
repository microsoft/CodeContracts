using System;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot.Utils
{
    internal static class ParameterExtensions
    {
        /// <summary>
        /// Gets the metadata name of the type. For generic types the arity is appended, such
        /// as <c>Func`2</c> for <see cref="Func{TResult}"/>.
        /// </summary>
        public static string GetMetadataName(this TypeNode parameter)
        {
            Contract.Requires(parameter != null);

            return HelperMethods.Unspecialize(parameter).Name.Name;
        }
    }
}
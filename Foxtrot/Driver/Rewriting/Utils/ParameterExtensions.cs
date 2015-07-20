using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot.Utils
{
    internal static class ParameterExtensions
    {
        /// <summary>
        /// Returns underlying parameter type.
        /// For generic types it would be like "Func`2".
        /// </summary>
        public static string GetGenericTypeName(this Parameter parameter)
        {
            Contract.Requires(parameter != null);
            Contract.Requires(parameter.Type != null);

            return HelperMethods.Unspecialize(parameter.Type).Name.Name;
        }
    }
}
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot.Utils
{
    internal static class TypeNodeExtensions
    {
        public static int TemplateArgumentsCount(this TypeNode typeNode)
        {
            Contract.Requires(typeNode != null);

            return typeNode.TemplateArguments == null ? 0 : typeNode.TemplateArguments.Count;
        }

        public static int CountOrDefault(this TypeNodeList list)
        {
            return list == null ? 0 : list.Count;
        }
    }
}
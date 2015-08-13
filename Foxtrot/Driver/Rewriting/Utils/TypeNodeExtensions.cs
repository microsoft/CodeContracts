using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot.Utils
{
    internal static class TypeNodeExtensions
    {
        // TODO: retire this method after moving to C# 6.0
        public static int TemplateArgumentsCount(this TypeNode typeNode)
        {
            Contract.Requires(typeNode != null);

            return typeNode.TemplateArguments == null ? 0 : typeNode.TemplateArguments.Count;
        }
        
        // TODO: retire this method after moving to C# 6.0
        public static int TemplateArgumentsCount(this Method method)
        {
            Contract.Requires(method != null);

            return method.TemplateArguments == null ? 0 : method.TemplateArguments.Count;
        }

        // TODO: retire this method ater moving to C# 6.0
        public static int CountOrDefault(this TypeNodeList list)
        {
            return list == null ? 0 : list.Count;
        }
    }
}
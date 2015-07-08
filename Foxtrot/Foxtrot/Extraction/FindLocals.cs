using System.Compiler;
using Microsoft.Research.DataStructures;

namespace Microsoft.Contracts.Foxtrot
{
    internal class FindLocals : Inspector
    {
        private Set<Local> locals = new Set<Local>();

        private FindLocals()
        {
        }

        public static Set<Local> Get(Expression e)
        {
            var n = new FindLocals();
            n.Visit(e);
            return n.locals;
        }

        public override void VisitLocal(Local local)
        {
            this.locals.Add(local);
        }
    }
}
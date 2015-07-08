using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Scans code and flags an error if any local is found that
    /// is already in the static hashtable GatherLocals.Locals
    /// Each instance of the class starts out with the flag off
    /// and turns it on if a local is found that is in the
    /// hashtable. Create a new instance for each check.
    /// </summary>
    internal class CheckLocals : Inspector
    {
        internal Local reUseOfExistingLocal = null;
        private GatherLocals gatherLocals;

        public CheckLocals(GatherLocals gatherLocals)
        {
            this.gatherLocals = gatherLocals;
        }

        /// <summary>
        /// check for reused local.
        /// HACK: ignore VB cached boolean locals used in tests. VB does this seemingly for debuggability.
        /// These are always assigned before use in the same scope, so we ignore them.
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        public override void VisitLocal(Local local)
        {
            if (gatherLocals.Locals[local.UniqueKey] != null)
            {
                if (local.Name != null && local.Name.Name.StartsWith("VB$CG$"))
                {
                    // ignore VB compiler generated locals.
                }
                else
                {
                    reUseOfExistingLocal = local;
                }
            }

            base.VisitLocal(local);
        }
    }
}
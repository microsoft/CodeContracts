// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Cci.MutableCodeModel;
using Microsoft.Research.CodeAnalysis;
using System.Linq;

namespace Microsoft.Cci.Analysis
{
    /// <summary>
    /// Given a set of root classes, this class visits an assembly and returns a list of types that
    /// are defined in the same assembly and are reachable from the metadata, code, and attributes
    /// of the roots.
    /// </summary>
    public class FindReachable : MetadataVisitor
    {
        readonly private UnitIdentity _unitIdentity;
        readonly internal ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice;
        readonly private IMetadataHost _host;
        readonly private MetadataTraverser _traverser;
        private uint _lastOffset;
        private bool _visitingContract;
        readonly private Dictionary<IMethodDefinition, uint> _contractOffsets;
        readonly private HashSet<object> _thingsToKeep;
        readonly private HashSet<uint> _methodsWhoseBodiesShouldBeKept;

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.slice != null);
            Contract.Invariant(_host != null);
            Contract.Invariant(_traverser != null);
            Contract.Invariant(_contractOffsets != null);
        }


        internal static void Reachable(
          IMetadataHost host,
          ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice,
          HashSet<object> thingsToKeep,
          HashSet<uint> methodsWhoseBodiesShouldBeKept,
          out Dictionary<IMethodDefinition, uint> contractOffsets
          )
        {
            Contract.Requires(host != null);
            Contract.Requires(slice != null);
            Contract.Requires(thingsToKeep != null);
            Contract.Requires(methodsWhoseBodiesShouldBeKept != null);

            var traverser = new MetadataTraverser();
            var me = new FindReachable(host, traverser, slice, thingsToKeep, methodsWhoseBodiesShouldBeKept);
            traverser.TraverseIntoMethodBodies = true;
            traverser.PreorderVisitor = me;

            var methodsToTraverse = slice.Methods;
            foreach (var m in methodsToTraverse)
            {
                var methodDefinition = m.reference.ResolvedMethod;
                traverser.Traverse(methodDefinition);
            }

            foreach (var c in slice.Chains)
            {
                VisitChain(c, traverser);
            }
            contractOffsets = me._contractOffsets;
            return;
        }

        private FindReachable(
          IMetadataHost host,
          MetadataTraverser myTraverser, ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice,
          HashSet<object> thingsToKeep,
          HashSet<uint> methodsWhoseBodiesShouldBeKept
          )
        {
            Contract.Requires(host != null);
            Contract.Requires(myTraverser != null);
            Contract.Requires(slice != null);
            Contract.Requires(thingsToKeep != null);
            Contract.Requires(methodsWhoseBodiesShouldBeKept != null);

            _host = host;
            _traverser = myTraverser;
            _unitIdentity = slice.ContainingAssembly.UnitIdentity;
            this.slice = slice;
            _thingsToKeep = thingsToKeep;
            _methodsWhoseBodiesShouldBeKept = methodsWhoseBodiesShouldBeKept;
            _contractOffsets = new Dictionary<IMethodDefinition, uint>();
        }

        private static void VisitChain(IChain<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor> c, MetadataTraverser mt)
        {
            Contract.Requires(c != null);
            Contract.Requires(mt != null);
            switch (c.Tag)
            {
                case ChainTag.Type:
                    {
                        Contract.Assume(c.Children != null, "The code makes such an assumption");
                        foreach (var child in c.Children)
                            VisitChain(child, mt);
                    }
                    break;
                case ChainTag.Field:
                    {
                        Contract.Assume(c.Field.reference != null);
                        mt.Traverse(c.Field.reference);
                    }
                    break;
                case ChainTag.Method:
                    {
                        Contract.Assume(c.Method.reference != null);
                        mt.Traverse(c.Method.reference);
                    }
                    break;
                default:
                    Contract.Assert(false, "switch should be exhaustive");
                    break;
            }
        }

        public override void Visit(ITypeReference typeReference)
        {
            var gtpr = typeReference as IGenericTypeParameterReference;
            if (gtpr != null) goto JustVisit;
            var gmpr = typeReference as IGenericMethodParameterReference;
            if (gmpr != null) goto JustVisit;
            var td = typeReference.ResolvedType;
            var namedTypeDefinition = td as INamedTypeDefinition;
            if (namedTypeDefinition != null)
            {
                var tdUnit = TypeHelper.GetDefiningUnit(td);
                if (tdUnit.UnitIdentity.Equals(_unitIdentity))
                {
                    _thingsToKeep.Add(td);
                    foreach (var invariantMethod in Microsoft.Cci.MutableContracts.ContractHelper.GetInvariantMethods(td))
                    {
                        // they have to be defined in this unit
                        _thingsToKeep.Add(invariantMethod);
                        _methodsWhoseBodiesShouldBeKept.Add(invariantMethod.InternedKey);
                        _traverser.Traverse(invariantMethod.Body);
                    }
                }
            }
            var gtir = typeReference as IGenericTypeInstanceReference;
            if (gtir != null)
            {
                var gt = gtir.GenericType;
                var tdUnit = TypeHelper.GetDefiningUnitReference(gt);
                if (tdUnit.UnitIdentity.Equals(_unitIdentity))
                {
                    _thingsToKeep.Add(gt);
                    var invariantMethods = Microsoft.Cci.MutableContracts.ContractHelper.GetInvariantMethods(gt.ResolvedType);
                    Contract.Assume(invariantMethods != null);
                    foreach (var invariantMethod in invariantMethods)
                    {
                        // they have to be defined in this unit
                        _thingsToKeep.Add(invariantMethod);
                        _methodsWhoseBodiesShouldBeKept.Add(invariantMethod.InternedKey);
                        _traverser.Traverse(invariantMethod.Body);
                    }
                }
            }

        JustVisit:
            base.Visit(typeReference);
        }

        public override void Visit(IFieldReference fieldReference)
        {
            var du = TypeHelper.GetDefiningUnitReference(fieldReference.ContainingType);
            if (du.UnitIdentity.Equals(_unitIdentity))
                _thingsToKeep.Add(fieldReference);
            base.Visit(fieldReference);
        }

        public override void Visit(IMethodDefinition method)
        {
            var unspec = MemberHelper.UninstantiateAndUnspecialize(method);
            var du = TypeHelper.GetDefiningUnitReference(unspec.ContainingType);
            if (du.UnitIdentity.Equals(_unitIdentity))
            {
                _thingsToKeep.Add(unspec);

                if (IsGetter(method) || IsSetter(method))
                {
                    _methodsWhoseBodiesShouldBeKept.Add(method.InternedKey);
                }

                // need to visit its whole body since the whole thing will be in the sliced assembly
                // so anything it mentions also needs to potentially be added to the slice.
                // since we're just a visitor, need to get our traverser to do it.
                // REVIEW: should this type just be a traverser then?
                var resolvedMethod = unspec.ResolvedMethod;
                if (!resolvedMethod.IsAbstract && !resolvedMethod.IsExternal && !_contractOffsets.ContainsKey(resolvedMethod))
                { // don't do this more than once
                    var methodBody = resolvedMethod.Body;
                    var ops = new List<IOperation>(methodBody.Operations);
                    _contractOffsets.Add(resolvedMethod, uint.MaxValue); // depends on using uint.MaxValue as flag that it was done
                    _lastOffset = uint.MaxValue;
                    _traverser.Traverse(ops);
                }

                base.Visit(method);
            }
        }
        public override void Visit(IMethodReference methodReference)
        {
            var unspec = MemberHelper.UninstantiateAndUnspecialize(methodReference);
            var du = TypeHelper.GetDefiningUnitReference(unspec.ContainingType);
            if (du.UnitIdentity.Equals(_unitIdentity))
            {
                _thingsToKeep.Add(unspec);

                // for most methods, need to visit *only* its contract and its attributes: that is all that
                // will be included in the sliced assembly so anything those mention also needs to potentially
                // be added to the slice. if the entire body is visited, then extra stuff will get dragged into
                // the slice that shouldn't be there.
                //
                // since we're just a visitor, need to get our traverser to do it.
                // REVIEW: should this type just be a traverser then?
                var resolvedMethod = unspec.ResolvedMethod;

                // however, if it is a getter/setter, then need to keep its body *and* visit its entire body
                // that is done by the Visit for method definitions, so just send it there
                if (IsGetter(resolvedMethod) || IsSetter(resolvedMethod))
                {
                    this.Visit(resolvedMethod);
                    return;
                }

                _traverser.Traverse(resolvedMethod.Attributes);
                var contractMethod = Microsoft.Cci.MutableContracts.ContractHelper.GetMethodFromContractClass(_host, resolvedMethod);
                if (contractMethod != null)
                {
                    if (TypeHelper.GetDefiningUnit(contractMethod.ContainingTypeDefinition).UnitIdentity.Equals(_unitIdentity))
                    {
                        _traverser.Traverse((IMethodReference)contractMethod); // visit it as a ref, not a def so that its contract is found. (alternative, visit it as a def, and add it to the method defs to be kept)
                        _methodsWhoseBodiesShouldBeKept.Add(contractMethod.InternedKey);
                    }
                }
                if (!resolvedMethod.IsAbstract && !resolvedMethod.IsExternal && !_contractOffsets.ContainsKey(resolvedMethod))
                { // don't do this more than once
                    uint lastOffset;
                    var methodBody = resolvedMethod.Body;
                    var ops = new List<IOperation>(methodBody.Operations);
                    var found = TryGetOffsetOfLastContractCall(ops, out lastOffset);
                    _contractOffsets.Add(resolvedMethod, lastOffset); // depends on using uint.MaxValue as flag that it was done, but no contracts found
                    if (found)
                    {
                        var savedVisitingContract = _visitingContract;
                        _visitingContract = true;
                        _lastOffset = lastOffset;
                        _traverser.Traverse(ops);
                        _visitingContract = savedVisitingContract;
                        _traverser.StopTraversal = false; // gets turned off because of the visit to an operation that is beyond the contract.
                    }
                }

                base.Visit(methodReference); // don't resolve so its body doesn't get visited!
            }
        }

        public override void Visit(IOperation operation)
        {
            if (!_visitingContract || operation.Offset <= _lastOffset)
                base.Visit(operation);
            else
                _traverser.StopTraversal = true;
        }

        private bool TryGetOffsetOfLastContractCall(List<IOperation>/*?*/ instrs, out uint offset)
        {
            Contract.Ensures(Contract.Result<bool>() || Contract.ValueAtReturn(out offset) == uint.MaxValue);
            offset = uint.MaxValue;
            if (instrs == null) return false; // not found
            for (int i = instrs.Count - 1; 0 <= i; i--)
            {
                IOperation op = instrs[i];
                Contract.Assume(op != null);
                if (op.OperationCode != OperationCode.Call) continue;
                IMethodReference method = op.Value as IMethodReference;
                if (method == null) continue;
                if (Microsoft.Cci.MutableContracts.ContractHelper.IsValidatorOrAbbreviator(method))
                {
                    offset = op.Offset;
                    return true;
                }
                var methodName = method.Name.Value;
                if (TypeHelper.TypesAreEquivalent(method.ContainingType, _host.PlatformType.SystemDiagnosticsContractsContract)
                  && IsNameOfPublicContractMethod(methodName)
                  )
                {
                    offset = op.Offset;
                    return true;
                }
            }
            return false; // not found
        }

        private bool IsNameOfPublicContractMethod(string methodName)
        {
            switch (methodName)
            {
                case "Requires":
                case "RequiresAlways":
                case "Ensures":
                case "EnsuresOnThrow":
                case "Invariant":
                case "EndContractBlock":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsGetter(IMethodDefinition methodDefinition)
        {
            Contract.Requires(methodDefinition != null);

            return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
        }

        private static bool IsSetter(IMethodDefinition methodDefinition)
        {
            Contract.Requires(methodDefinition != null);
            return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
        }
    }
}


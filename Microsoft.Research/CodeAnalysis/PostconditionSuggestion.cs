// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    public struct Details
    {
        public bool HasVariables;
        public bool HasCompoundExp;
        public bool HasOldVariable;
        public bool HasReturnVariable;
        public bool IsRootedInThis;
        public bool IsThis;

        public bool HasInterestingVariables
        {
            get
            {
                return this.HasVariables || this.HasOldVariable || this.HasReturnVariable || this.IsThis;
            }
        }

        public void AllFalse()
        {
            this.HasVariables = false;
            this.HasCompoundExp = false;
            this.HasOldVariable = false;
            this.HasReturnVariable = false;
            this.IsRootedInThis = false;
            this.IsThis = false;
        }
    }

    public class BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Typ, Variable, Expression, Attribute, Assembly>
      where Typ : IEquatable<Typ>
    {
        private readonly IExpressionContext<Local, Parameter, Method, Field, Typ, Expression, Variable> context;
        private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> metaDataDecoder;
        private readonly Optional<Variable> returnVariable;
        private readonly BoxedExpression returnExpression;
        private readonly IDisjunctiveExpressionRefiner<Variable, BoxedExpression> extendedRefiner;

        public BoxedExpressionReader(IExpressionContext<Local, Parameter, Method, Field, Typ, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> metaDataDecoder,
          IDisjunctiveExpressionRefiner<Variable, BoxedExpression> extendedRefiner = null)
        {
            Contract.Requires(context != null);
            Contract.Requires(metaDataDecoder != null);

            this.context = context;
            this.metaDataDecoder = metaDataDecoder;
            this.extendedRefiner = extendedRefiner;

            Variable retVar;
            if (!this.metaDataDecoder.IsVoidMethod(this.context.MethodContext.CurrentMethod)
              && this.context.ValueContext.TryResultValue(this.context.MethodContext.CFG.NormalExit, out retVar))
            {
                returnVariable = new Optional<Variable>(retVar);
                Typ type = this.metaDataDecoder.ReturnType(this.context.MethodContext.CurrentMethod);
                returnExpression = BoxedExpression.Result(type);
            }
            else
            {
                returnVariable = new Optional<Variable>();
                returnExpression = null;
            }
        }

        /// <summary>
        /// Try to read the boxed expression <code>exp</code> in the exit state. 
        /// If it fails, it returns null
        /// </summary>
        /// <param name="hasVariables">true iff the return value is != null and the expression contains at least one variable</param>
        /// <returns>The expression read in the exit state of the method. If it cannot read it, it returns null</returns>
        public BoxedExpression ExpressionInPostState(BoxedExpression exp, bool replaceReturnValue, out Details details)
        {
            Contract.Requires(exp != null);

            details = new Details();

            return ExpressionInPostStateHelper(exp, replaceReturnValue, false, true, ref details);
        }

        public BoxedExpression ExpressionInPostState(BoxedExpression exp, bool replaceReturnValue, bool overrideAccessModifiers, bool allowReturnValue, out Details details)
        {
            Contract.Requires(exp != null);

            details = new Details();

            return ExpressionInPostStateHelper(exp, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
        }

        private BoxedExpression ExpressionInPostStateHelper(BoxedExpression exp, bool replaceReturnValue, bool overrideAccessModifiers, bool allowReturnValue, ref Details details)
        {
            if (exp.IsConstant || exp.IsSizeOf /*|| exp.IsNull*/)
            { // Nothing to do
                return exp;
            }
            if (exp.IsNull)
            {
                return BoxedExpression.Const(null, metaDataDecoder.System_Object, metaDataDecoder);
            }
            if (exp.IsVariable)
            {
                if (exp.IsVariableBoundedInQuantifier)
                {
                    return exp;
                }

                // F: this check is there as there may be slack variables form subpolyhedra

                Variable var;
                if (!exp.TryGetFrameworkVariable(out var))
                {
                    return null;
                }

                // We want to replace the return variable with the marker for the return value
                if (replaceReturnValue && returnVariable.IsValid && var.Equals(returnVariable.Value))
                {
                    details.HasVariables = true;
                    details.HasReturnVariable = true;

                    return returnExpression;
                }

                #region First try: Read it in the post-state
                var accessPath =
                  context.ValueContext.VisibleAccessPathListFromPost(context.MethodContext.CFG.NormalExit, var, allowReturnValue);

#if false
                // F: change!!!!

                // Console.WriteLine("Access paths for sv = {0}", var);
                foreach (var ap in context.ValueContext.AccessPaths(context.MethodContext.CFG.NormalExit, var, AccessPathFilter<Method>.FromPostcondition(context.MethodContext.CurrentMethod)))
                {
                    // Console.WriteLine(ap);
                    if (context.ValueContext.PathSuitableInEnsures(context.MethodContext.CFG.NormalExit, ap))
                    {
                        // we found our accesspath
                        accessPath = ap;
                        break;
                    }
                }

#endif
                if (accessPath != null)
                {
                    details.HasVariables = true;

                    details.HasReturnVariable = accessPath.Head.IsReturnValue;

                    if (accessPath.Length() == 1 && accessPath.ToString() == "this")
                    {
                        details.IsThis = true;
                    }

                    if (accessPath.Head.ToString() == "this")
                    {
                        details.IsRootedInThis = true;
                    }

                    // TODO: check possible bug here: this should be var not exp.UnderlyingVariable
                    return BoxedExpression.Var(var, accessPath);
                }
                #endregion

                #region Second try: Read it in the pre-state
                accessPath =
                  // context.ValueContext.VisibleAccessPathListFromPre(context.MethodContext.CFG.EntryAfterRequires, var);
                  context.ValueContext.AccessPathList(context.MethodContext.CFG.EntryAfterRequires, var, false, false);

                if (accessPath != null)
                {
                    details.HasVariables = true;

                    var type = context.ValueContext.GetType(context.MethodContext.CFG.NormalExit, var);

                    if (type.IsNormal)
                    {
                        details.HasOldVariable = true;

                        return BoxedExpression.Old(BoxedExpression.Var(var, accessPath), type.Value);
                    }
                    else
                    {
                        return null;
                    }
                }
                #endregion

                #region Third try: use the extended refined, if any

                if (extendedRefiner != null)
                {
                    BoxedExpression refined;
                    if (extendedRefiner.TryRefineExpression(context.MethodContext.CFG.NormalExit, var, out refined)
                      && refined.IsArrayIndex) // F: it seems we generate some recursive expression which causes a stack overflow. Should investigate!!!!
                    {
                        return ExpressionInPostStateHelper(refined, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                    }
                }

                #endregion

                // giving up
                return null;
            }
            if (exp.IsUnary)
            {
                var rec = ExpressionInPostStateHelper(exp.UnaryArgument, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                if (rec == null)
                    return null;

                // Avoid copying the expression (optimization?)
                if (rec == exp.UnaryArgument)
                    return exp;
                else
                    return BoxedExpression.Unary(exp.UnaryOp, rec);
            }
            if (exp.IsBinary)
            {
                if (!exp.BinaryOp.IsComparisonBinaryOperator())
                {
                    details.HasCompoundExp = true;
                }
                var recLeft = ExpressionInPostStateHelper(exp.BinaryLeft, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                if (recLeft == null)
                    return null;

                var recRight = ExpressionInPostStateHelper(exp.BinaryRight, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                if (recRight == null)
                    return null;

                // Heuristics to rule out trivial expressions arr.Length >= 0 or 0 <= arr.Length or this.Count >= 0 or 0 <= this.Count 
                int value;
                if ((exp.BinaryOp == BinaryOperator.Cge && recLeft.IsArrayLengthOrThisDotCount(context.MethodContext.CFG.NormalExit, context, metaDataDecoder) && recRight.IsConstantInt(out value) && value == 0)
                  ||
                (exp.BinaryOp == BinaryOperator.Cle && recLeft.IsConstantInt(out value) && value == 0 && recRight.IsArrayLengthOrThisDotCount(context.MethodContext.CFG.NormalExit, context, metaDataDecoder)))
                {
                    return null;
                }

                if (recLeft == exp.BinaryLeft && recRight == exp.BinaryRight)
                {
                    return exp;
                }
                else
                {
                    return BoxedExpression.Binary(exp.BinaryOp, recLeft, recRight);
                }
            }
            object t;
            BoxedExpression array, index;
            if (exp.IsArrayIndexExpression(out array, out index, out t) && t is Typ)
            {
                // Sanity check. We have a case with post-inference where the array and the index get swapped by the underlying framework
                if (array.UnderlyingVariable is Variable)
                {
                    var type = context.ValueContext.GetType(context.MethodContext.CFG.NormalExit, (Variable)array.UnderlyingVariable);
                    if (type.IsNormal && metaDataDecoder.IsArray(type.Value))
                    {
                        var arrayRef = ExpressionInPostStateHelper(array, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                        if (arrayRef != null)
                        {
                            var indexRef = ExpressionInPostStateHelper(index, replaceReturnValue, overrideAccessModifiers, allowReturnValue, ref details);
                            if (indexRef != null)
                            {
                                return new BoxedExpression.ArrayIndexExpression<Typ>(arrayRef, indexRef, (Typ)t);
                            }
                        }
                    }
                }

                return null;
            }

            // Unreachable?
            return null;
        }

        private bool IsRootedInStaticField(FList<PathElement> accessPath)
        {
            return accessPath != null && accessPath.Head.IsStatic;
        }
    }
}



// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis.Expressions;

namespace Microsoft.Research.CodeAnalysis
{
    public enum CodeFixKind
    {
        ArrayInitialization,
        ArrayOffByOne,
        ConstantInitialization,
        ExpressionInitialization,
        FloatingPointCast,
        MethodCallResult,
        MethodCallResultNoCode,
        MethodShouldBePure,
        NonOverflowingExpression,
        OffByOne,
        RemoveConstructor,
        StrengthenTest,
        Test,
        AssumeMethodResult,
        AssumeLocalInitialization,
        BaselineAssume,
    }

    public interface ICodeFix
    {
        APC PC { get; }

        ProofObligation Obligation { get; }

        /// <returns>A string in the form "Code Fix: (some English explanation). Fix: [Add | Remove constructor] (some code) </returns>
        string Suggest();
        string SuggestCode();

        string GetMessageForSourceObligation();

        CodeFixKind Kind { get; }
    }

    public struct ParametersFixMethodCallReturnValue<Variable, ArgList, Method>
      where ArgList : IIndexable<Variable>
    {
        private readonly IDecodeMethods<Method> mdDecoder;
        public readonly ProofObligation obl;
        public readonly APC pc;
        public readonly Func<APC> pcWithSourceContext;
        public readonly Variable dest;
        public readonly ArgList args;
        public readonly BoxedExpression condition;
        public readonly List<BoxedExpression> premises;
        public readonly Func<APC, Variable, FList<PathElement>> AccessPath;
        public readonly Func<FList<PathElement>, bool> IsRootedInParameter;
        public readonly Func<Variable, FList<PathElement>, BoxedExpression> MakeMethodCall;
        public readonly Func<Variable, BoxedExpression> MakeEqualZero;
        public readonly Method method;
        public readonly Func<APC, BoxedExpression, bool, BoxedExpression> ReadAt;
        public readonly ICFG cfg;

        #region Constructor
        public ParametersFixMethodCallReturnValue(ProofObligation obl, APC pc, Func<APC> pcWithSourceContext, Variable dest, ArgList args,
          BoxedExpression condition, List<BoxedExpression> premises,
          Method method,
          IDecodeMethods<Method> mdDecoder,
          ICFG cfg,
          Func<APC, BoxedExpression, bool, BoxedExpression> ReadAt,
          Func<APC, Variable, FList<PathElement>> AccessPath,
          Func<FList<PathElement>, bool> IsRootedInParameter,
          Func<Variable, FList<PathElement>, BoxedExpression> MakeMethodCall, Func<Variable, BoxedExpression> MakeEqualZero
        )
        {
            Contract.Requires(mdDecoder != null);

            this.obl = obl;
            this.pc = pc;
            this.pcWithSourceContext = pcWithSourceContext;
            this.dest = dest;
            this.args = args;
            this.condition = condition;
            this.premises = premises;
            this.mdDecoder = mdDecoder;
            this.cfg = cfg;
            this.method = method;
            this.ReadAt = ReadAt;
            this.AccessPath = AccessPath;
            this.IsRootedInParameter = IsRootedInParameter;
            this.MakeMethodCall = MakeMethodCall;
            this.MakeEqualZero = MakeEqualZero;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("Condition = {0}", condition);
        }

        public bool CalleeIsProperty()
        {
            Contract.Assume(mdDecoder != null);
            return mdDecoder.IsPropertyGetter(this.method) || mdDecoder.IsPropertySetter(this.method);
        }

        public bool CalleeIsStaticMethod()
        {
            Contract.Assume(mdDecoder != null);
            return mdDecoder.IsStatic(this.method);
        }

        public string MethodFullName()
        {
            Contract.Assume(mdDecoder != null);
            return this.method != null ? mdDecoder.FullName(this.method) : null;
        }
        public string MethodName()
        {
            Contract.Assume(mdDecoder != null);
            return this.method != null ? mdDecoder.Name(this.method) : null;
        }
    }

    public struct ParametersSuggestNonOverflowingExpression<Variable>
    {
        public readonly APC pc;
        public readonly APC pcWithSourceContext;
        public readonly BoxedExpression exp;
        public readonly IFactQuery<BoxedExpression, Variable> factQuery;
        public readonly Func<APC, BoxedExpression, bool, BoxedExpression> Simplificator;
        public readonly Func<Variable, IntervalStruct> TypeRange;

        #region Constructor
        public ParametersSuggestNonOverflowingExpression(APC pc, Func<APC, APC> pcWithSourceContext, BoxedExpression exp, IFactQuery<BoxedExpression, Variable> factQuery, Func<APC, BoxedExpression, bool, BoxedExpression> Simplificator, Func<Variable, IntervalStruct> TypeRange)
        {
            Contract.Requires(pcWithSourceContext != null);

            this.pc = pc;
            this.pcWithSourceContext = pcWithSourceContext(pc);
            this.exp = exp;
            this.factQuery = factQuery;
            this.Simplificator = Simplificator;
            this.TypeRange = TypeRange;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("Condition = {0}", this.exp);
        }
    }

    public struct ParametersSuggestOffByOneFix<Variable>
    {
        public readonly ProofObligation obl;
        public readonly APC pc;
        public readonly APC pcWithSourceContext;
        public readonly bool isArrayAccess;
        public readonly BoxedExpression exp;
        public readonly Func<Variable, FList<PathElement>> AccessPath;
        public readonly Func<BoxedExpression, bool> IsArrayLength;
        public readonly IFactQuery<BoxedExpression, Variable> factQuery;

        #region Constructor
        public ParametersSuggestOffByOneFix(ProofObligation obl, APC pc, Func<APC, APC> pcWithSourceContext, bool isArrayAccess, BoxedExpression exp, Func<Variable, FList<PathElement>> AccessPath, Func<BoxedExpression, bool> IsArrayLength, IFactQuery<BoxedExpression, Variable> factQuery)
        {
            Contract.Requires(pcWithSourceContext != null);

            this.obl = obl;
            this.pc = pc;
            this.pcWithSourceContext = pcWithSourceContext(pc);
            this.isArrayAccess = isArrayAccess;
            this.exp = exp;
            this.AccessPath = AccessPath;
            this.IsArrayLength = IsArrayLength;
            this.factQuery = factQuery;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("Condition = {0}", exp);
        }
    }

    public struct InitializationFix<Variable>
    {
        public readonly ProofObligation obl;
        public readonly APC pc;
        private readonly BoxedExpression sourceExp;
        public readonly Set<Variable> varsInSourceExp;
        public readonly Variable dest;
        private Optional<BoxedExpression> cached_SourceExp;

        public InitializationFix(ProofObligation obl, APC pc, Variable dest, BoxedExpression sourceExp, Set<Variable> varsInSourceExp)
        {
            Contract.Requires(obl != null);
            Contract.Requires(sourceExp != null);
            Contract.Requires(varsInSourceExp != null);

            this.obl = obl;
            this.pc = pc;
            this.dest = dest;
            this.sourceExp = sourceExp;
            this.varsInSourceExp = varsInSourceExp;
            cached_SourceExp = default(Optional<BoxedExpression>);
        }

        public BoxedExpression SourceExp
        {
            get
            {
                if (!cached_SourceExp.IsValid)
                {
                    var checkIsSourceLevelReadable = new SourceLevelReadableExpression<Variable>(null);
                    cached_SourceExp = checkIsSourceLevelReadable.Visit(sourceExp, new Void());
                }

                return cached_SourceExp.Value;
            }
        }

        public override string ToString()
        {
            return string.Format("fix of {0} init under condition {1}", dest, sourceExp);
        }
    }

    public interface ICodeFixesManager
    {
        int SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Exp, Variable, ILogOptions>(
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
              where Variable : IEquatable<Variable>
              where Type : IEquatable<Type>
              where ILogOptions : IFrameworkLogOptions;

        bool TrySuggestInitializationFix<Variable>(ref InitializationFix<Variable> parameters);

        bool TrySuggestConstantInititalizationFix(ProofObligation obl, APC pc, BoxedExpression dest, BoxedExpression oldInitialization, BoxedExpression newInitialization, BoxedExpression constraint);

        bool TrySuggestConstantInitializationFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(
          ProofObligation obl,
          Func<APC> pc, BoxedExpression failingCondition, BoxedExpression falseCondition,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<Variable, BoxedExpression> VariableValueBeforeRenaming, Func<Variable, BoxedExpression> VariableName);

        bool TrySuggestFixForMethodCallReturnValue<Variable, ArgList, Method>(ref ParametersFixMethodCallReturnValue<Variable, ArgList, Method> context)
          where ArgList : IIndexable<Variable>;

        bool TrySuggestLargerAllocation<Variable>(ProofObligation obl, Func<APC> definitionPC, APC failingConditionPC, BoxedExpression failingCondition, Variable array, Variable length, Func<Variable, BoxedExpression> Converter, IFactQuery<BoxedExpression, Variable> factQuery);

        bool TrySuggestTestFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          ProofObligation obl,
          Func<APC> pc, BoxedExpression guard, BoxedExpression failingCondition, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<BoxedExpression, bool> IsArrayLength);

        bool TrySuggestTestStrengthening(
          ProofObligation obl,
          APC pc, BoxedExpression additionalGuard, bool strengthenNullCheck);

        bool TrySuggestFloatingPointComparisonFix(
          ProofObligation obl,
          APC pc, BoxedExpression left, BoxedExpression right, ConcreteFloat leftType, ConcreteFloat rightType);

        bool TrySuggestFixingConstructor(APC pc, string name, bool isConstructor, IEnumerable<MinimalProofObligation> obligations);

        bool TrySuggestNonOverflowingExpression<Variable>(ref ParametersSuggestNonOverflowingExpression<Variable> context);

        bool TrySuggestOffByOneFix<Variable>(ref ParametersSuggestOffByOneFix<Variable> context);

        bool IsEnabled { get; }
    }

    /// <summary>
    /// A code fix to embed an assumption
    /// </summary>
    public class AssumeCodeFix : ICodeFix
    {
        private readonly ProofObligation obl;

        public AssumeCodeFix(ProofObligation obl)
        {
            Contract.Requires(obl != null);

            this.obl = obl;
        }

        public string Suggest()
        {
            return string.Format("Entry point assumption. Fix: Add {0}", this.SuggestCode());
        }

        public string SuggestCode()
        {
            return string.Format("Contract.Assume({0});", obl.Condition);
        }

        public string GetMessageForSourceObligation()
        {
            return string.Format("The error arises when the following assumption does *not* hold on entry: {0}", obl.Condition);
        }

        public CodeFixKind Kind
        {
            get { return CodeFixKind.AssumeMethodResult; }
        }

        public APC PC
        {
            get { return obl.PC; }
        }

        public ProofObligation Obligation
        {
            get { return obl; }
        }
    }

    public class CodeFixesProfiler
      : ICodeFixesManager
    {
        #region Statics for profiling

        [ThreadStatic]
        private static int InitializationFixes;
        [ThreadStatic]
        private static int ReturnValuesFixes;
        [ThreadStatic]
        private static int AllocationFixes;
        [ThreadStatic]
        private static int TestFalseFixes;
        [ThreadStatic]
        private static int TestStrenghteningFixes;
        [ThreadStatic]
        private static int FloatComparisonMismatchesFixes;
        [ThreadStatic]
        private static int RemoveConstructorFixes;
        [ThreadStatic]
        private static int OverflowingExpressionFixes;
        [ThreadStatic]
        private static int OffByOneFixes;

        private static int TotalFixesSuggested { get { return InitializationFixes + ReturnValuesFixes + AllocationFixes + TestFalseFixes + TestStrenghteningFixes + FloatComparisonMismatchesFixes + RemoveConstructorFixes + OverflowingExpressionFixes + OffByOneFixes; } }


        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            output.WriteLine("Detected {0} code fixes", TotalFixesSuggested);
            output.WriteLine("Proof obligations with a code fix: {0}", ProofObligation.ProofObligationsWithCodeFix);
        }

        #endregion

        #region Object invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(inner != null);
        }

        #endregion

        #region privates

        readonly private ICodeFixesManager inner;
        readonly private bool trace;

        #endregion

        #region Constructor

        public CodeFixesProfiler(ICodeFixesManager inner, bool trace)
        {
            Contract.Requires(inner != null);
            this.inner = inner;
            this.trace = trace;
        }

        #endregion

        #region Proxies
        public int SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Exp, Variable, ILogOptions>(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
            where Variable : IEquatable<Variable>
            where Type : IEquatable<Type>
            where ILogOptions : IFrameworkLogOptions
        {
            return inner.SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(mdriver);
        }

        public bool TrySuggestInitializationFix<Variable>(ref InitializationFix<Variable> parameters)
        {
            return ProfileAndTrace("initialization", inner.TrySuggestInitializationFix(ref parameters), ref InitializationFixes);
        }

        public bool TrySuggestConstantInititalizationFix(ProofObligation obl, APC pc, BoxedExpression dest, BoxedExpression oldInitialization, BoxedExpression newInitialization, BoxedExpression constraint)
        {
            return ProfileAndTrace("Constant initialization", inner.TrySuggestConstantInititalizationFix(obl, pc, dest, oldInitialization, newInitialization, constraint), ref InitializationFixes);
        }

        public bool TrySuggestConstantInitializationFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(ProofObligation obl, Func<APC> pc, BoxedExpression failingCondition, BoxedExpression falseCondition, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<Variable, BoxedExpression> VariableValueBeforeRenaming, Func<Variable, BoxedExpression> VariableName)
        {
            return ProfileAndTrace("Constant initialization", inner.TrySuggestConstantInitializationFix(obl, pc, failingCondition, falseCondition, metaDataDecoder, VariableValueBeforeRenaming, VariableName), ref InitializationFixes);
        }

        public bool TrySuggestFixForMethodCallReturnValue<Variable, ArgList, Method>(ref ParametersFixMethodCallReturnValue<Variable, ArgList, Method> context) where ArgList : IIndexable<Variable>
        {
            return ProfileAndTrace("Method call return value", inner.TrySuggestFixForMethodCallReturnValue(ref context), ref ReturnValuesFixes);
        }

        public bool TrySuggestLargerAllocation<Variable>(ProofObligation obl, Func<APC> pc, APC failingConditionPC, BoxedExpression failingCondition, Variable array, Variable length, Func<Variable, BoxedExpression> Converter, IFactQuery<BoxedExpression, Variable> factQuery)
        {
            return ProfileAndTrace("Larger allocation", inner.TrySuggestLargerAllocation(obl, pc, failingConditionPC, failingCondition, array, length, Converter, factQuery), ref AllocationFixes);
        }

        public bool TrySuggestTestFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(ProofObligation obl, Func<APC> pc, BoxedExpression guard, BoxedExpression failingCondition, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<BoxedExpression, bool> IsArrayLength)
        {
            return ProfileAndTrace("Condition in a test", inner.TrySuggestTestFix(obl, pc, guard, failingCondition, metaDataDecoder, IsArrayLength), ref TestFalseFixes);
        }

        public bool TrySuggestTestStrengthening(ProofObligation obl, APC pc, BoxedExpression additionalGuard, bool strengthenNullCheck)
        {
            return ProfileAndTrace("Test strenghtening", inner.TrySuggestTestStrengthening(obl, pc, additionalGuard, strengthenNullCheck), ref TestStrenghteningFixes);
        }

        public bool TrySuggestFloatingPointComparisonFix(ProofObligation obl, APC pc, BoxedExpression left, BoxedExpression right, ConcreteFloat leftType, ConcreteFloat rightType)
        {
            return ProfileAndTrace("Floating point comparison", inner.TrySuggestFloatingPointComparisonFix(obl, pc, left, right, leftType, rightType), ref FloatComparisonMismatchesFixes);
        }

        public bool TrySuggestFixingConstructor(APC pc, string name, bool isConstructor, IEnumerable<MinimalProofObligation> obligations)
        {
            return ProfileAndTrace("Object constructor", inner.TrySuggestFixingConstructor(pc, name, isConstructor, obligations), ref RemoveConstructorFixes);
        }

        public bool TrySuggestNonOverflowingExpression<Variable>(ref ParametersSuggestNonOverflowingExpression<Variable> context)
        {
            return ProfileAndTrace("Non-overflowing expression", inner.TrySuggestNonOverflowingExpression(ref context), ref OverflowingExpressionFixes);
        }

        public bool TrySuggestOffByOneFix<Variable>(ref ParametersSuggestOffByOneFix<Variable> context)
        {
            return ProfileAndTrace("Off by one", inner.TrySuggestOffByOneFix(ref context), ref OffByOneFixes);
        }
        #endregion

        #region Private

        public bool ProfileAndTrace(string codefix, bool b, ref int value)
        {
            if (trace)
            {
                if (b)
                {
                    Console.WriteLine("  Found a code fix for {0}", codefix);
                }
                else
                {
                    Console.WriteLine("  Code fix inference for {0} failed", codefix);
                }
            }

            return IncrementIfTrue(b, ref value);
        }

        private static bool IncrementIfTrue(bool b, ref int value)
        {
            if (b) value++;

            return b;
        }

        #endregion

        public bool IsEnabled
        {
            get { return inner.IsEnabled; }
        }
    }

    [ContractVerification(false)]
    public class DummyCodeFixesManager
      : ICodeFixesManager
    {
        public int SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Exp, Variable, ILogOptions>(
           IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
              where Variable : IEquatable<Variable>
              where Type : IEquatable<Type>
              where ILogOptions : IFrameworkLogOptions
        {
            return 0;
        }

        public bool TrySuggestInitializationFix<Variable>(ref InitializationFix<Variable> parameters)
        {
            return false;
        }

        public bool TrySuggestConstantInititalizationFix(ProofObligation obl, APC pc, BoxedExpression dest, BoxedExpression oldInitialization, BoxedExpression newInitialization, BoxedExpression constraint)
        {
            return false;
        }

        public bool TrySuggestConstantInitializationFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(ProofObligation obl, Func<APC> pc, BoxedExpression failingCondition, BoxedExpression falseCondition, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<Variable, BoxedExpression> VariableValueBeforeRenaming, Func<Variable, BoxedExpression> VariableName)
        {
            return false;
        }

        public bool TrySuggestFixForMethodCallReturnValue<Variable, ArgList, Method>(ref ParametersFixMethodCallReturnValue<Variable, ArgList, Method> context) where ArgList : IIndexable<Variable>
        {
            return false;
        }

        public bool TrySuggestTestFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(ProofObligation obl, Func<APC> pc, BoxedExpression guard, BoxedExpression failingCondition, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder, Func<BoxedExpression, bool> IsArrayLength)
        {
            return false;
        }

        public bool TrySuggestTestStrengthening(ProofObligation obl, APC pc, BoxedExpression additionalGuard, bool strengthenNullCheck)
        {
            return false;
        }

        public bool TrySuggestFloatingPointComparisonFix(ProofObligation obl, APC pc, BoxedExpression left, BoxedExpression right, ConcreteFloat leftType, ConcreteFloat rightType)
        {
            return false;
        }

        public bool TrySuggestFixingConstructor(APC pc, string name, bool isConstructor, IEnumerable<MinimalProofObligation> obligations)
        {
            return false;
        }

        public bool TrySuggestNonOverflowingExpression<Variable>(ref ParametersSuggestNonOverflowingExpression<Variable> context)
        {
            return false;
        }

        public bool TrySuggestOffByOneFix<Variable>(ref ParametersSuggestOffByOneFix<Variable> context)
        {
            return false;
        }


        public bool TrySuggestLargerAllocation<Variable>(ProofObligation obl, Func<APC> definitionPC, APC failingConditionPC, BoxedExpression failingCondition, Variable array, Variable length, Func<Variable, BoxedExpression> Converter, IFactQuery<BoxedExpression, Variable> factQuery)
        {
            return false;
        }


        public bool IsEnabled
        {
            get { return false; }
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if false
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.IO;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    class BenigniMain
    {
        internal const string BLOCKPREFIX = "b";
        internal const string SEPPARAM = ", ";
        internal const string TRANS = " -> ";
        internal const string EMPTY = " ";
        internal const string INITIAL = "(INITIAL {0})";
        internal const string VARS = "(VAR {0})";
        internal const string BEGIN_RULES = "(RULES ";
        internal const string END_RULES = ")";

        public static void EmitTheEquations(EquationBlock entryPoint, List<EquationBlock> equations, TextWriter where, InvariantQuery<APC> invariantDB)
        {
            Set<string> vars = new Set<string>();
            foreach (EquationBlock b in equations)
            {
                vars.AddRange(b.FormalParameters);
            }
            List<string> allVars = new List<string>(vars);
            allVars.Sort();

            where.WriteLine(Vars(allVars));

            where.WriteLine(Initial(entryPoint));


            where.WriteLine(BEGIN_RULES);
            foreach (EquationBlock b in equations)
            {
                b.EmitEquations(where, invariantDB);
            }
            where.WriteLine(END_RULES);
        }

        private static string Initial(EquationBlock entry)
        {
            return SanitizeString(String.Format(INITIAL, entry.Head()));
        }

        private static string Vars(List<string> vars)
        {
            return SanitizeString(String.Format(VARS, FormalParametersAsString(vars)));
        }

        public static string FormalParametersAsString(List<string> listOfVariables)
        {
            StringBuilder tmp = new StringBuilder();

            foreach (string p in listOfVariables)
            {
                tmp.Append(p + SEPPARAM);
            }

            if (tmp.Length > SEPPARAM.Length)
            {
                tmp.Remove(tmp.Length - SEPPARAM.Length, SEPPARAM.Length);  // remove the last ", "
            }
            else
            {
                tmp.Append(BenigniMain.EMPTY);
            }

            return tmp.ToString();
        }
        /// <summary>
        /// Replace "svXXX (YYY)" with "svXXX_(YYY)"
        /// </summary>
        public static string SanitizeString(string s)
        {
            char[] sAsChar = s.ToCharArray();
            char[] result = new char[sAsChar.Length];

            for (int i = 0, j = 0; i < sAsChar.Length;)
            {
                if (i < sAsChar.Length - 1 && sAsChar[i] == 's' && sAsChar[i + 1] == 'v')
                {
                    result[j++] = sAsChar[i++]; // copy 's'
                    result[j++] = sAsChar[i++]; // copy 'v'
                    while (Char.IsDigit(sAsChar[i]))
                    {
                        result[j++] = sAsChar[i++];
                    }
                    while (Char.IsWhiteSpace(sAsChar[i]))
                    {
                        result[j++] = '_';
                        i++;
                    }
                    if (sAsChar[i] == '(')   // Skip left parenthesis
                    {
                        i++;
                    }
                    while (Char.IsDigit(sAsChar[i]))
                    {
                        result[j++] = sAsChar[i++];
                    }
                    if (sAsChar[i] == ')')
                    {
                        i++;
                    }
                }

                result[j++] = sAsChar[i++];
            }

            return new String(result);
        }

    }

    /// <summary>
    /// Represent the equations for a block 
    /// </summary>
    class EquationBlock
    {
        private CFGBlock id;
        private List<string> formalParameters; // We want to keep it sorted...
        private List<EquationBody> bodies;

        private string/*?*/ condition;


        public List<string> FormalParameters
        {
            get
            {
                Contract.Ensures(Contract.Result<List<string>>() != null);

                if (formalParameters.Count == 0)
                {
                    this.InferParameters(new Set<EquationBlock>());
                }

                return formalParameters;
            }
        }

        public CFGBlock Block
        {
            get
            {
                return id;
            }
        }

        public EquationBlock(CFGBlock ID)
        {
            id = ID;
            formalParameters = new List<string>();
            bodies = new List<EquationBody>();
            condition = null;
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(formalParameters != null);
        }

        public void AddBody(EquationBody toAdd)
        {
            bodies.Add(toAdd);
        }

        public void AddParameters(Set<string> vars)
        {
            formalParameters.AddRange(vars);
            formalParameters.Sort(); // we keep the parameters sorted
        }

        public void EmitEquations(TextWriter where, InvariantQuery<APC> invariantDB)
        {
            foreach (EquationBody body in bodies)
            {
                body.EmitEquation(where, invariantDB);
            }
        }

        public string Condition
        {
            set
            {
                if (value != null && value.Length != 0)
                    condition = value;
                else
                    condition = null;
            }
            get
            {
                return condition;
            }
        }

        internal string Head()
        {
            return string.Format("{0}{1} ({2})", BenigniMain.BLOCKPREFIX, id.Index, BenigniMain.FormalParametersAsString(this.FormalParameters));
        }

        private void InferParameters(Set<EquationBlock> visited)
        {
            // Console.WriteLine("Visiting {0}", this.Block.Index);
            if (visited.Contains(this))
                return;

            visited.Add(this);

            var newParameters = new Set<string>(formalParameters);
            foreach (EquationBody succ in bodies)
            {
                succ.To.InferParameters(visited);
                newParameters.AddRange(succ.To.formalParameters);
            }

            List<string> asList = new List<string>(newParameters);
            asList.Sort();
            formalParameters = asList;
        }
    }

    class EquationBody
    {
        private EquationBlock parent;
        private EquationBlock to;
        private IFunctionalMap<string, string> renamings;

        internal EquationBlock To
        {
            get
            {
                return to;
            }
        }

        public EquationBody(EquationBlock parent, EquationBlock to, IFunctionalMap<string, string> renamings)
        {
            this.parent = parent;
            this.to = to;
            this.renamings = renamings;
        }

        public void EmitEquation(TextWriter where, InvariantQuery<APC> invariantDB)
        {
            string head = parent.Head(); // bn(... )

            StringBuilder bodyAsString = new StringBuilder();

            foreach (string s in to.FormalParameters)
            {
                string actualParameter = renamings[s];

                if (actualParameter == null)
                    actualParameter = s;  // if it is not there, it means that it is not renamed, i.e. it is the identity

                bodyAsString.Append(actualParameter + BenigniMain.SEPPARAM);
            }

            if (bodyAsString.Length > BenigniMain.SEPPARAM.Length)
            {
                bodyAsString.Remove(bodyAsString.Length - BenigniMain.SEPPARAM.Length, BenigniMain.SEPPARAM.Length);  // remove the last ", "
            }
            else if (parent.Condition != null)
            { // no successors, so I just write down the parameters, 
                bodyAsString.Append(BenigniMain.FormalParametersAsString(parent.FormalParameters));
            }
            else
            {
                bodyAsString.Append(BenigniMain.EMPTY);
            }

            string succ = BenigniMain.BLOCKPREFIX + this.To.Block.Index;

            string result = String.Format("{0} {1} {2}( {3} ) ", head, BenigniMain.TRANS, succ, bodyAsString.ToString());

            string postState = invariantDB(this.To.Block.First);

            if (parent.Condition != null)  // Is there a condition on the rewriting rule?
            {
                result = String.Format("{0} | {1} -> 1", result, parent.Condition);

                if (postState != null && postState.Length > 0)
                {
                    result += string.Format(", {0}", postState);
                }
            }
            else if (postState != null && postState.Length > 0)
            {
                result = String.Format("{0} | {1}", result, postState);
            }

            result = BenigniMain.SanitizeString(result);

            // Here we output the equation
            where.WriteLine(result);
        }
    }

    #region Expression visitors
    /// <summary>
    /// The crawler used by Benigni to find which variables are used in a block
    /// </summary>
    public class BenigniVariableCrawler
    {
        class Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>
          : IVisitValueExprIL<ExternalExpression<Label, SymbolicValue>, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue, Set<string>, Set<string>>
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
        {
            IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder;
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

            public Decoder(
              IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
            )
            {
                this.decoder = decoder;
                this.mdDecoder = mdDecoder;
            }

            public Set<string> Variables(ExternalExpression<Label, SymbolicValue> expr)
            {
                Set<string> emptySet = new Set<string>();
                return Recurse(emptySet, expr);
            }

            #region IVisitExprIL<Label,Type,ExternalExpression,SymbolicValue,StringBuilder,Unit> Members

            Set<string> Recurse(Set<string> input, ExternalExpression<Label, SymbolicValue> expr)
            {
                return decoder.ExpressionContext.Decode<Set<string>, Set<string>, Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>>(expr, this, input);
            }

            public Set<string> Binary(ExternalExpression<Label, SymbolicValue> pc, BinaryOperator op, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> s1, ExternalExpression<Label, SymbolicValue> s2, Set<string> data)
            {

                data = Recurse(data, s1);
                data = Recurse(data, s2);

                return data;
            }

            public Set<string> Isinst(ExternalExpression<Label, SymbolicValue> pc, Type type, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> obj, Set<string> data)
            {
                return Recurse(data, obj);
            }

            public Set<string> Ldconst(ExternalExpression<Label, SymbolicValue> pc, object constant, Type type, SymbolicValue dest, Set<string> data)
            {
                return data;
            }

            public Set<string> Ldnull(ExternalExpression<Label, SymbolicValue> pc, SymbolicValue dest, Set<string> data)
            {
                return data;
            }

            public Set<string> Sizeof(ExternalExpression<Label, SymbolicValue> pc, Type type, SymbolicValue dest, Set<string> data)
            {
                return data;
            }

            public Set<string> Unary(ExternalExpression<Label, SymbolicValue> pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> source, Set<string> data)
            {
                return Recurse(data, source);
            }

            public Set<string> SymbolicConstant(ExternalExpression<Label, SymbolicValue> pc, SymbolicValue symbol, Set<string> data)
            {
                data.Add(symbol.ToString());
                return data;
            }

            #endregion
        }

        public static Converter<ExternalExpression<Label, SymbolicValue>, Set<string>> Crawler<Label, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue>(
          IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
        {
            Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly> expr = new Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>(decoder, mdDecoder);
            return expr.Variables;
        }
    }

    /// <summary>
    /// The printer for expressions used by Benigni
    /// </summary>
    public class BenigniExprPrinter
    {
        class Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>
          : IVisitValueExprIL<ExternalExpression<Label, SymbolicValue>, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue, StringBuilder, Unit>
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
        {
            IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder;
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;

            public Decoder(
              IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
            )
            {
                this.decoder = decoder;
                this.mdDecoder = mdDecoder;
            }

            public string ToString(ExternalExpression<Label, SymbolicValue> expr)
            {
                StringBuilder sb = new StringBuilder();
                Recurse(sb, expr);
                return sb.ToString();
            }

            #region IVisitExprIL<Label,Type,ExternalExpression,SymbolicValue,StringBuilder,Unit> Members

            void Recurse(StringBuilder tw, ExternalExpression<Label, SymbolicValue> expr)
            {
                decoder.ExpressionContext.Decode<StringBuilder, Unit, Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>>(expr, this, tw);
            }

            public Unit Binary(ExternalExpression<Label, SymbolicValue> pc, BinaryOperator op, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> s1, ExternalExpression<Label, SymbolicValue> s2, StringBuilder data)
            {
                data.Append(op.ToString());
                data.Append('(');
                Recurse(data, s1);
                data.Append(", ");
                Recurse(data, s2);
                data.Append(')');
                return Unit.Value;
            }

            public Unit Isinst(ExternalExpression<Label, SymbolicValue> pc, Type type, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> obj, StringBuilder data)
            {
                data.AppendFormat("IsInst({0}) ", mdDecoder.FullName(type));
                Recurse(data, obj);
                return Unit.Value;
            }

            public Unit Ldconst(ExternalExpression<Label, SymbolicValue> pc, object constant, Type type, SymbolicValue dest, StringBuilder data)
            {
                data.Append(constant.ToString());
                return Unit.Value;
            }

            public Unit Ldnull(ExternalExpression<Label, SymbolicValue> pc, SymbolicValue dest, StringBuilder data)
            {
                data.Append("null");
                return Unit.Value;
            }

            public Unit Sizeof(ExternalExpression<Label, SymbolicValue> pc, Type type, SymbolicValue dest, StringBuilder data)
            {
                data.AppendFormat("sizeof({0})", mdDecoder.FullName(type));
                return Unit.Value;
            }

            public Unit Unary(ExternalExpression<Label, SymbolicValue> pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, ExternalExpression<Label, SymbolicValue> source, StringBuilder data)
            {
                data.AppendFormat("{0}(", op.ToString());
                Recurse(data, source);
                data.AppendFormat(")");
                return Unit.Value;
            }

            public Unit SymbolicConstant(ExternalExpression<Label, SymbolicValue> pc, SymbolicValue symbol, StringBuilder data)
            {
                data.Append(symbol.ToString());
                return Unit.Value;
            }

            #endregion
        }

        public static Converter<ExternalExpression<Label, SymbolicValue>, string> Printer<Label, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue>(
          IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<Label, SymbolicValue>, SymbolicValue> decoder,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
        {
            var exprPrinter = new Decoder<Label, Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly>(decoder, mdDecoder);
            return exprPrinter.ToString;
        }
    }
    #endregion

    #region Instruction visitor
    public static class BenigniConditionCrawler
    {

        public static AssumptionFinder<Label> Create<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>(
          IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          Converter<Source, string> sourceName,
          Converter<Dest, string> destName
        )
        {
            return new SearchAssumptions<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>(ilDecoder, mdDecoder, sourceName, destName).SearchAssumptionAt;
        }

        private class SearchAssumptions<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData> :
          IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, string, string>
        {
            IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder;
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
            Converter<Source, string> sourceName;
            Converter<Dest, string> destName;

            public SearchAssumptions(
              IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> ilDecoder,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
              Converter<Source, string> sourceName,
              Converter<Dest, string> destName
            )
            {
                this.ilDecoder = ilDecoder;
                this.mdDecoder = mdDecoder;
                this.sourceName = sourceName;
                this.destName = destName;
            }

            public string SearchAssumptionAt(Label label)
            {
                return ilDecoder.ForwardDecode<string, string, SearchAssumptions<Label, Local, Parameter, Method, Field, Property, Event, Type, Source, Dest, Context, Attribute, Assembly, EdgeData>>(label, this, null);
            }

            #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Source,Dest,string,string> Members

            private string/*?*/ SourceName(Source src)
            {
                if (sourceName != null) { return sourceName(src); }
                return null;
            }

            private string/*?*/ DestName(Dest dest)
            {
                if (destName != null) { return destName(dest); }
                return null;
            }

            public string Binary(Label pc, BinaryOperator op, Dest dest, Source s1, Source s2, string data)
            {
                return data;
            }

            public string Assert(Label pc, string tag, Source s, string data)
            {
                //       tw.WriteLine("{0}assert({1}) {2}", prefix, tag, SourceName(s));
                return data;
            }

            public string Assume(Label pc, string tag, Source s, string data)
            {
                //        tw.WriteLine("{0}assume({1}) {2}", prefix, tag, SourceName(s));

                if (data != null)
                    return null;
                else if (tag.Equals("true"))
                    return SourceName(s);
                else
                    return "not(" + SourceName(s) + ")";
            }

            public string Arglist(Label pc, Dest d, string data)
            {
                return data;
            }

            public string BranchCond(Label pc, Label target, BranchOperator bop, Source value1, Source value2, string data)
            {
                return data;
            }

            public string BranchTrue(Label pc, Label target, Source cond, string data)
            {
                return data;
            }

            public string BranchFalse(Label pc, Label target, Source cond, string data)
            {
                return data;
            }

            public string Branch(Label pc, Label target, bool leave, string data)
            {
                return data;
            }

            public string Break(Label pc, string data)
            {
                return data;
            }

            public string Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Dest dest, ArgList args, string data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                return data;
            }

            public string Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Dest dest, Source fp, ArgList args, string data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                return data;
            }

            public string Ckfinite(Label pc, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Unary(Label pc, UnaryOperator op, bool ovf, bool unsigned, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Cpblk(Label pc, bool @volatile, Source destaddr, Source srcaddr, Source len, string data)
            {
                return data;
            }

            public string Endfilter(Label pc, Source condition, string data)
            {
                return data;
            }

            public string Endfinally(Label pc, string data)
            {
                return data;
            }

            public string Entry(Label pc, Method method, string data)
            {
                return data;
            }

            public string Initblk(Label pc, bool @volatile, Source destaddr, Source value, Source len, string data)
            {
                return data;
            }

            public string Jmp(Label pc, Method method, string data)
            {
                return data;
            }

            public string Ldarg(Label pc, Parameter argument, bool isOld, Dest dest, string data)
            {
                return data;
            }

            public string Ldarga(Label pc, Parameter argument, bool isOld, Dest dest, string data)
            {
                return data;
            }

            public string Ldconst(Label pc, object constant, Type type, Dest dest, string data)
            {
                return data;
            }

            public string Ldnull(Label pc, Dest dest, string data)
            {
                return data;
            }

            public string Ldftn(Label pc, Method method, Dest dest, string data)
            {
                return data;
            }

            public string Ldind(Label pc, Type type, bool @volatile, Dest dest, Source ptr, string data)
            {
                return data;
            }

            public string Ldloc(Label pc, Local local, Dest dest, string data)
            {
                return data;
            }

            public string Ldloca(Label pc, Local local, Dest dest, string data)
            {
                return data;
            }

            public string Ldresult(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Ldstack(Label pc, int offset, Dest dest, Source s, bool isOld, string data)
            {
                return data;
            }

            public string Ldstacka(Label pc, int offset, Dest dest, Source s, Type type, bool isOld, string data)
            {
                return data;
            }

            public string Localloc(Label pc, Dest dest, Source size, string data)
            {
                return data;
            }


            public string Nop(Label pc, string data)
            {
                return data;
            }

            public string Pop(Label pc, Source source, string data)
            {
                return data;
            }

            public string Return(Label pc, Source source, string data)
            {
                return data;
            }

            public string Starg(Label pc, Parameter argument, Source source, string data)
            {
                return data;
            }

            public string Stind(Label pc, Type type, bool @volatile, Source ptr, Source value, string data)
            {
                return data;
            }

            public string Stloc(Label pc, Local local, Source value, string data)
            {
                return data;
            }

            public string Switch(Label pc, Type type, IEnumerable<Pair<object, Label>> cases, Source value, string data)
            {
                return data;
            }

            public string Box(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Dest dest, ArgList args, string data)
              where TypeList : IIndexable<Type>
              where ArgList : IIndexable<Source>
            {
                return data;
            }

            public string Castclass(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Cpobj(Label pc, Type type, Source destptr, Source srcptr, string data)
            {
                return data;
            }

            public string Initobj(Label pc, Type type, Source ptr, string data)
            {
                return data;
            }

            public string Isinst(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Ldelem(Label pc, Type type, Dest dest, Source array, Source index, string data)
            {
                return data;
            }

            public string Ldelema(Label pc, Type type, bool @readonly, Dest dest, Source array, Source index, string data)
            {
                return data;
            }

            public string Ldfld(Label pc, Field field, bool @volatile, Dest dest, Source ptr, string data)
            {
                return data;
            }

            public string Ldflda(Label pc, Field field, Dest dest, Source ptr, string data)
            {
                return data;
            }

            public string Ldlen(Label pc, Dest dest, Source array, string data)
            {
                return data;
            }

            public string Ldsfld(Label pc, Field field, bool @volatile, Dest dest, string data)
            {
                return data;
            }

            public string Ldsflda(Label pc, Field field, Dest dest, string data)
            {
                return data;
            }

            public string Ldtypetoken(Label pc, Type type, Dest dest, string data)
            {
                return data;
            }

            public string Ldfieldtoken(Label pc, Field field, Dest dest, string data)
            {
                return data;
            }

            public string Ldmethodtoken(Label pc, Method method, Dest dest, string data)
            {
                return data;
            }

            public string Ldvirtftn(Label pc, Method method, Dest dest, Source obj, string data)
            {
                return data;
            }

            public string Mkrefany(Label pc, Type type, Dest dest, Source value, string data)
            {
                return data;
            }

            public string Newarray<ArgList>(Label pc, Type type, Dest dest, ArgList lengths, string data)
              where ArgList : IIndexable<Source>
            {
                return data;
            }

            public string Newobj<ArgList>(Label pc, Method ctor, Dest dest, ArgList args, string data)
              where ArgList : IIndexable<Source>
            {
                return data;
            }

            public string Refanytype(Label pc, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Refanyval(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Rethrow(Label pc, string data)
            {
                return data;
            }

            public string Sizeof(Label pc, Type type, Dest dest, string data)
            {
                return data;
            }

            public string Stelem(Label pc, Type type, Source array, Source index, Source value, string data)
            {
                return data;
            }

            public string Stfld(Label pc, Field field, bool @volatile, Source ptr, Source value, string data)
            {
                return data;
            }

            public string Stsfld(Label pc, Field field, bool @volatile, Source value, string data)
            {
                return data;
            }

            public string Throw(Label pc, Source value, string data)
            {
                return data;
            }

            public string Unbox(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            public string Unboxany(Label pc, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            #endregion

            #region IVisitSynthIL<Label,Method,Source,Dest,TextWriter,Unit> Members


            public string BeginOld(Label pc, Label matchingEnd, string data)
            {
                return data;
            }

            public string EndOld(Label pc, Label matchingBegin, Type type, Dest dest, Source source, string data)
            {
                return data;
            }

            #endregion
        }

    }

    #endregion
}
#endif
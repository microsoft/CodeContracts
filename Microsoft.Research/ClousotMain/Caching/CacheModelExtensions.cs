// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  using Provenance = IEnumerable<ProofObligation>;
  using Microsoft.Research.CodeAnalysis.Caching.Models;

    /// <summary>
    /// We box the inferred pre, post, oi so that serialization is faster and smaller
    /// </summary>
    [Serializable]
    struct InferredExpr<Field, Method>
    {
        public Pair<BoxedExpression, Provenance>[] PreConditions;
        public Pair<BoxedExpression, Provenance>[] PostConditions;
        public Pair<BoxedExpression, Provenance>[] ObjectInvariants;
        public Pair<Field, BoxedExpression>[] NonNullFields;
        public ConditionList<Method> EntryAssumes;
        public ConditionList<Method> CalleeAssumes; // Method: For each assume, which method is it a postcondition for? 
        public bool MayReturnNull;

        public override string ToString()
        {
            return String.Format("Preconditions:\n{0}\nPostconditions:\n{1}\nObjectInvariants:\n{2}\nEntryAssumes:{3}\nCalleeAssumes:{4}MayReturnNull{5}",
              JoinWithDefaultIfNull("\n", this.PreConditions),
              JoinWithDefaultIfNull("\n", this.PostConditions),
              JoinWithDefaultIfNull("\n", this.ObjectInvariants),
              JoinWithDefaultIfNull("\n", this.NonNullFields),
              JoinWithDefaultIfNull("\n", this.EntryAssumes),
              JoinWithDefaultIfNull("\n", this.CalleeAssumes),
              this.MayReturnNull);
          // TODO: print Provenance properly
        }

        /// <summary>
        /// exclude the entry and callee assumes from the hash as they might fail to deserialize we don't need an exact match.
        /// </summary>
        /// <returns></returns>
        public string ToHashString()
        {
          return String.Format("Preconditions:\n{0}\nPostconditions:\n{1}\nObjectInvariants:\n{2}\n",
            JoinWithDefaultIfNull("\n", this.PreConditions),
            JoinWithDefaultIfNull("\n", this.PostConditions),
            JoinWithDefaultIfNull("\n", this.ObjectInvariants));
          // TODO: print Provenance properly
        }

        private string JoinWithDefaultIfNull<T>(string separator, IEnumerable<T> sequence)
        {
          if (sequence == null)
            return "";
          else
            return String.Join(separator, sequence);
        }

        public bool HasAssumes
        {
          get
          {
            return this.EntryAssumes.Any() || this.CalleeAssumes.Any();
          }
        }

        public int AssumeCount
        {
          get
          {
            return 
              this.EntryAssumes.Count() + this.CalleeAssumes.Count();
          }
        }
    }

    /// <summary>
    /// These functions are only needed as long as we do not know how to serialize Provenance
    /// </summary>
    public static class ProvenanceExtensions
    {
      public static ConditionList<M> ToConditionList<M>(this IEnumerable<Pair<BoxedExpression, Provenance>> enumerable)
      {
        return new ConditionList<M>(enumerable.Select(p => Pair.For(p.One, default(M))));
      }
      public static ConditionList<M> ToConditionList<M>(this IEnumerable<STuple<BoxedExpression, Provenance, M>> enumerable)
      {
        return new ConditionList<M>(enumerable.Select(p => Pair.For(p.One, p.Three)));
      }
      public static IEnumerable<Pair<T, Provenance>> RemoveProvenance<T>(this IEnumerable<Pair<T, Provenance>> enumerable)
      {
        Contract.Ensures(Contract.Result<IEnumerable<Pair<T, Provenance>>>() != null);

        return enumerable.Select(p => Pair.For(p.One, (Provenance)null));
      }
      public static IEnumerable<STuple<T1, Provenance, T2>> RemoveProvenance<T1, T2>(this IEnumerable<STuple<T1, Provenance, T2>> enumerable)
      {
        return enumerable.Select(t => STuple.For(t.One, (Provenance)null, t.Three));
      }
    }

    internal static class CacheModelExtensions
    {
      #region ContextEdgeModel

      public static STuple<CFGBlock, CFGBlock, string> GetContextEdge(this ContextEdge @this, BijectiveMap<Subroutine, int> subroutineLocalIds)
        {
            return new STuple<CFGBlock, CFGBlock, string>(
              subroutineLocalIds.KeyForValue(@this.Block1SubroutineLocalId).Blocks.First(b => b.Index == @this.Block1Index),
              subroutineLocalIds.KeyForValue(@this.Block2SubroutineLocalId).Blocks.First(b => b.Index == @this.Block2Index),
              @this.Tag);
        }

        public static void SetContextEdge(this ContextEdge @this, BijectiveMap<Subroutine, int> subroutineLocalIds, STuple<CFGBlock, CFGBlock, string> edge)
        {
            @this.Block1SubroutineLocalId = subroutineLocalIds[edge.One.Subroutine];
            @this.Block1Index = edge.One.Index;
            @this.Block2SubroutineLocalId = subroutineLocalIds[edge.Two.Subroutine];
            @this.Block2Index = edge.Two.Index;
            @this.Tag = edge.Three;
        }

        #endregion

        #region OutcomeOrSuggestionModel

        public static APC GetAPC(this OutcomeOrSuggestion @this, BijectiveMap<Subroutine, int> subroutineLocalIds)
        {
            return new APC(
              subroutineLocalIds.KeyForValue(@this.SubroutineLocalId).Blocks.First(b => b.Index == @this.BlockIndex),
              @this.ApcIndex,
              @this.ContextEdges
                  .OrderBy(edge => edge.Rank)
                  .Select(edge => edge.GetContextEdge(subroutineLocalIds))
                  .Aggregate((FList<STuple<CFGBlock, CFGBlock, string>>)null, (accu, edge) => accu.Cons(edge))
                  .Reverse());
        }

        /// <summary>
        /// Must be called only once !
        /// </summary>
        public static void SetAPC(this IClousotCache @this, OutcomeOrSuggestion that, APC apc, BijectiveMap<Subroutine, int> subroutineLocalIds)
        {
            if (@that.ContextEdges.Any())
                throw new InvalidOperationException();
            @that.SubroutineLocalId = subroutineLocalIds[apc.Block.Subroutine];
            @that.BlockIndex = apc.Block.Index;
            @that.ApcIndex = apc.Index;
            int rank = 0;
            foreach (var edge in apc.SubroutineContext.GetEnumerable())
            {
              var added = @this.AddNewContextEdge(that, rank);
              added.SetContextEdge(subroutineLocalIds, edge);
              rank++;
            }
        }

        /// <summary>
        /// Must be called only once !
        /// </summary>
        public static void SetWitness(this IClousotCache @this, Outcome outcome, Witness witness, BijectiveMap<Subroutine, int> subroutineLocalIds)
        {
            if (@outcome.OutcomeContexts.Any())
                throw new InvalidOperationException();
            @outcome.ProofOutcome = witness.Outcome;
            @outcome.WarningType = witness.Warning;
            @this.SetAPC(outcome, witness.PC, subroutineLocalIds);
            foreach (var c in witness.Context)
            {
              @this.AddNewOutcomeContext(outcome, c);
            }
        }

        public static Witness GetWitness(this Outcome @this, BijectiveMap<Subroutine, int> subroutineLocalIds)
        {
            Contract.Ensures(Contract.Result<Witness>() != null);
 
            return new Witness(
              null, // F: The proof obligation ID is lost in the serialization/deserialization
              @this.WarningType,
              @this.ProofOutcome,
              @this.GetAPC(subroutineLocalIds),
              new Set<WarningContext>(@this.OutcomeContexts.Select(c => c.WarningContext)));
        }

        #endregion
      
        #region pre/post conditions getter/deserialization

        public static void SetInferredExpr<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this Microsoft.Research.CodeAnalysis.Caching.Models.Method @this,
          InferredExpr<Field, Method> value,
          Method currentMethod,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
          bool trace,
          bool traceHashing)
        {
            // To make the serialization happy
            var state = StreamingContextStates.File | StreamingContextStates.Persistence;
            var streamingContext = new StreamingContext(state);

            // We do not want to serialize CCI types directly. We want the Serialization to call us and ask if we have a better way of serializing
            var surrogateSelector = Serializers.SurrogateSelectorFor(streamingContext, currentMethod, mdDecoder, contractDecoder, trace);

            // Create the serializer
            var formatter = new BinaryFormatter(surrogateSelector, streamingContext);
            var stream = new System.IO.MemoryStream();
            try
            {            
              // Now do the work!
              formatter.Serialize(stream, value);
            }
            catch (SerializationException)
            {
                throw;
            }
            catch (NotImplementedException e)
            {
                throw new SerializationException("Some serialization implementation is missing, see inner exception", e);
            }
            catch (Exception e)
            {
                throw new SerializationException("Random exception while serializing, see inner exception", e);
            }
            @this.InferredExpr = stream.ToArray();
            @this.InferredExprHash = DummyExpressionHasher.Hash(value, traceHashing);
            if (trace)
            {
              @this.InferredExprString = value.ToString();
            }
        }

        public static InferredExpr<Field, Method> GetInferredExpr<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
           this Microsoft.Research.CodeAnalysis.Caching.Models.Method @this, 
           Method currentMethod,
           IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
           IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
           bool trace
        )
        {
            if (@this.InferredExpr == null)
              return default(InferredExpr<Field, Method>);
            if (@this.InferredExprHash == null)
                throw new SerializationException("Cannot check deserialization, hash null");

            var state = StreamingContextStates.File | StreamingContextStates.Persistence;
            var streamingContext = new StreamingContext(state);
            var surrogateSelector = Serializers.SurrogateSelectorFor(streamingContext, currentMethod, mdDecoder, contractDecoder, trace);
            var formatter = new BinaryFormatter(surrogateSelector, streamingContext);
            var stream = new System.IO.MemoryStream(@this.InferredExpr);
            try
            {
              // Fix for SerializationException below. Somehow deserialization does not find certain assemblies, so we help out
              // the resolver to find currently loaded assemblies.
              AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(DeserializeAssemblyResolve);
              var result = (InferredExpr<Field, Method>)formatter.Deserialize(stream);
              var newHash = DummyExpressionHasher.Hash(result, trace);
              if (@this.InferredExprHash.ContentEquals(newHash))
              {
                  return result;
              }
              throw new SerializationException(String.Format("Deserialization failed: hash differs. Expected '{0}', got '{1}'.", @this.InferredExprString, result.ToString()));
            }
            catch (SerializationException)
            {
                // "Unable to find assembly 'ClousotMain, Version=1.1.1.1, Culture=neutral, PublicKeyToken=188286aac86319f9'." just means the Clousot has been recompiled (its version number should have changed)
                // must not happen, please, please, please! ('cause the method hasn't changed)

                // this happens because there are some types in ClousotMain that get serialized. But when we build an installer, these types are actually in cccheck.exe
                // so a cache populated using Clousot does not interact well with a cache used with cccheck.
                throw;
            }
            catch (NotImplementedException e)
            {
                throw new SerializationException("Some deserialization implementation is missing, see inner exception", e);
            }
            catch (Exception e)
            {
                // ArgumentException "Object of type '<type>' cannot be converted to type '<type>'." just means the type has changed
                throw new SerializationException("Random exception while deserializing, see inner exception", e);
            }
            finally
            {
              AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(DeserializeAssemblyResolve);
            }

        }

        /// <summary>
        /// We use this weird fix to make sure during deserialization that we bind to the currently running assemblies. Somehow the 
        /// deserialization is buggy and fails occasionally to bind properly. Looks like this happens only once, then future deserializations
        /// work.
        /// </summary>
        static private System.Reflection.Assembly DeserializeAssemblyResolve(object sender, ResolveEventArgs args)
        {
          var name = args.Name.Split(',')[0];
          var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
          var result = FindInCurrentAssemblies(name, currentAssemblies);
          return result;
        }

        private static System.Reflection.Assembly FindInCurrentAssemblies(string name, System.Reflection.Assembly[] currentAssemblies)
        {
          System.Reflection.Assembly result = null;
          for (int i = 0; i < currentAssemblies.Length; i++)
          {
            var assembly = currentAssemblies[i];
            if (name == assembly.FullName.Split(',')[0])
            {
              result = assembly;
              break;
            }
          }
          return result;
        }
        #endregion

    }
}

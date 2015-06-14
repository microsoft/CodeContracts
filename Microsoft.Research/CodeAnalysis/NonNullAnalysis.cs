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
using System.Text;

using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public class NonNullAnalysis<Label, Local, Parameter, Method, Field, Type, SymbolicValue> where SymbolicValue:IEquatable<SymbolicValue>
  {
    public class Domain : IAbstractValue<Domain>
    {
      private Domain(IFunctionalSet<SymbolicValue> underlying)
      {
        this.NonNullValues = underlying;
      }

      /// <summary>
      /// Used as a set
      /// </summary>
      IFunctionalSet<SymbolicValue> NonNullValues;

      public Domain Rename(IFunctionalMap<SymbolicValue,SymbolicValue> substitution)
      {
        foreach (SymbolicValue v in NonNullValues.Elements) {
          if (substitution.Contains(v)) {
            NonNullValues = NonNullValues.Remove(v).Add(substitution[v]);
          }
        }
        return this;
      }

      public Domain ConstrainEqual(SymbolicValue target, SymbolicValue source)
      {
        if (NonNullValues.Contains(source)) {
          NonNullValues = NonNullValues.Add(target);
        }
        return this;
      }

      #region IAbstractValue<Domain> Members

      static Converter<SymbolicValue/*!*/, int> KeyNumber;

      public static Domain InitialValue(Converter<SymbolicValue/*!*/, int> keyNumber)
      {
        KeyNumber = keyNumber;
        return new Domain(FunctionalIntKeySet<SymbolicValue>.Empty(keyNumber));
      }

      public Domain Top
      {
        get { return new Domain(FunctionalIntKeySet<SymbolicValue>.Empty(KeyNumber)); }
      }

      private static Domain BottomValue;

      public Domain Bottom
      {
        get {
          if (BottomValue == null) {
            BottomValue = new Domain(FunctionalIntKeySet<SymbolicValue>.Empty(KeyNumber));
          }
          return BottomValue;
        }
      }

      public bool IsTop
      {
        get { return this.NonNullValues.Count == 0; }
      }

      public bool IsBottom
      {
        get { return this == BottomValue; }
      }

      public Domain ImmutableVersion()
      {
        return this;
      }

      public Domain Clone()
      {
        return new Domain(this.NonNullValues);
      }

      public Domain Join(Domain newState, out bool weaker)
      {
        IFunctionalSet<SymbolicValue> result = this.NonNullValues.Intersect(newState.NonNullValues);
        weaker = result.Count<this.NonNullValues.Count;
        return new Domain(result);
      }

      public Domain Meet(Domain b)
      {
        // stupid but easy
        return this;
      }

      public Domain Widen(Domain newState, out bool weaker)
      {
        return Join(newState, out weaker);
      }

      public bool LessEqual(Domain that)
      {
        return that.NonNullValues.Contained(this.NonNullValues);
      }

      public void Dump(System.IO.TextWriter tw)
      {
        this.NonNullValues.Dump(tw);
      }

      #endregion
    }

    class Driver : IValueAnalysisDriver<Label, Domain, IVisitMSIL<Label, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>,SymbolicValue, SymbolicValue, IStackContext<Label,Method>>
    {
      #region IValueAnalysisDriver<Label,Domain,IVisitMSIL<Label,Local,Parameter,Method,Field,Type,SymbolicValue,SymbolicValue,Domain,Domain>,SymbolicValue> Members

      public Domain ParallelAssign(Pair<Label,Label> edge, IFunctionalMap<SymbolicValue,FList<SymbolicValue>> substitution, Domain state)
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IAnalysisDriver<Label,Domain,IVisitMSIL<Label,Local,Parameter,Method,Field,Type,SymbolicValue,SymbolicValue,Domain,Domain>> Members

      public EdgeConverter<Label, Domain> EdgeConverter()
      {
        return delegate(Label from, Label next, bool joinPoint, Domain state) { return state; };
      }

      public Joiner<Label, Domain> Joiner()
      {
        return delegate(Pair<Label, Label> edge, Domain newState, Domain prevState, out bool weaker, bool widen)
        {
          return prevState.Join(newState, out weaker);
        };
      }

      public IVisitMSIL<Label, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain> Visitor(IStackContext<Label,Method> context)
      {
        return new Decoder();
      }

      StateLookup<Label, Domain> preStateLookup;
      StateLookup<Label, Domain> postStateLookup;

      public bool CacheStates(StateLookup<Label, Domain> preState, StateLookup<Label, Domain> postState)
      {
        this.preStateLookup = preState;
        this.postStateLookup = postState;
        return false;
      }

      #endregion
    }

    class Decoder : MSILVisitor<Label, Local,Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>
    {
      protected override Domain Default(Label pc, Domain data)
      {
        return data;
      }
    }
  }
}
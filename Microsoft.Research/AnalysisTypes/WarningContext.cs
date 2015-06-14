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
namespace Microsoft.Research.CodeAnalysis
{
  public struct WarningContext
  {
    [Flags]
    public enum CalleeInfo
    {
      Nothing = 0x0,
      IsAbstract = 0x1,
      IsAutoProperty = 0x2,
      IsCompilerGenerated = 0x4,
      IsConstructor = 0x8,
      IsDispose = 0x10,
      IsExtern = 0x20,
      IsFinalizer = 0x40,
      IsGeneric = 0x80,
      IsImplicitImplementation = 0x100,
      IsInternal = 0x200,
      IsNewSlot = 0x400,
      IsOverride = 0x800,
      IsPrivate = 0x1000,
      IsPropertyGetterOrSetter = 0x2000,
      IsProtected = 0x4000,
      IsPublic = 0x8000,
      IsSealed = 0x10000,
      IsStatic = 0x20000,
      IsVirtual = 0x4000,
      IsVoidMethod = 0x8000,
      ReturnPrimitiveValue = 0x100000,
      ReturnReferenceType = 0x200000,
      ReturnStructValue = 0x400000,
      DeclaredInTheSameType = 0x800000, // We want to give credit to those
      DeclaredInADifferentAssembly = 0x1000000, // we have low confindence in those, or not... depending on the cmd line switch
      DeclaredInAFrameworkAssembly = 0x2000000
    }

    // The contexts
    public enum ContextType
    {
      NonNullAccessPath,

      InferredPrecondition,
      InferredObjectInvariant,
      InferredEntryAssume,
      InferredCalleeAssume,

      InferredConditionIsSufficient,
      InferredConditionContainsDisjunction,
      InferredCalleeAssumeContainsDisjunction,

      ViaCast,
      ViaOutParameter,
      ViaPureMethodReturn,
      ViaOldValue,
      ViaCallThisHavoc,

      ViaArray,
      ViaField,
      ViaMethodCall,
      ViaParameterUnmodified,

      ViaMethodCallThatMayReturnNull,

      ContainsReadOnly,
      MayContainForAllOrADisjunction,
      ContainsIsInst,

      OffByOne,

      CodeRepairLikelyFixingABug,
      CodeRepairLikelyFoundAWeakenessInClousot,

      AssertMayBeTurnedIntoAssume,

      WPReachedMethodEntry,
      WPReachedMaxPathSize,
      WPReachedLoop,
      WPSkippedBecauseAdaptiveAnalysis,

      InheritedEnsures,
      ObjectInvariantNeededForEnsures,

      InsideUnsafeMethod,
      StringComparedAgainstNullInSwitchStatement,

      InterproceduralPathToError,
      IsVarNotEqNullCheck,
      CodeRepairThinksWrongInitialization,
      IsPureMethodCallNotEqNullCheck,

      ConditionContainsValueAtReturn,
    }

    public readonly ContextType Type;
    public readonly int AssociatedInfo;
    public readonly string Name;

    public WarningContext(WarningContext.ContextType type, int associatedNumericalinfo)
    {
      this.Type = type;
      this.AssociatedInfo = associatedNumericalinfo;
      this.Name = null;
    }
    public WarningContext(WarningContext.ContextType type, string varName)
      : this(type, 0)
    {
      this.Name = varName;
    }

    public WarningContext(WarningContext.ContextType type)
      : this(type, 0)
    {
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (obj is WarningContext)
      {
        var that = (WarningContext)obj;

        return this.Type == that.Type && this.AssociatedInfo == that.AssociatedInfo;
      }
      else
      {
        return false;
      }
    }

    public override int GetHashCode()
    {
      return this.Type.GetHashCode() + this.AssociatedInfo;
    }

    public override string ToString()
    {
 
     if (this.Type != ContextType.InferredCalleeAssume)
      {
        return string.Format("{0}{1}", this.Type.ToString(), this.AssociatedInfo > 1 ? " (x" + this.AssociatedInfo.ToString() + ")" : null);
      }
      else
      {
        return string.Format("{0} ({1})", this.Type, ((CalleeInfo) this.AssociatedInfo).ToString());
      }
    }

  }

}
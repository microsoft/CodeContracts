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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Xml;
using System.Reflection;
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{
  public class WarningScoresManager
  {

    #region Thresholds for the levels

    // low:       [LOWSCORE ... 1.0] 
    // mediumlow: [MEDIUMLOWSCORE ... 1]
    // medium:    [MEDIUMSCORE ... 1]
    // full:      all

    public double LOWSCORE { get { return this.ConfidenceThresholdLowWarnings; } }
    public double MEDIUMLOWSCORE { get { return this.ConfidenceThreshouldMediumLowWarning; } }
    public double MEDIUMSCORE { get { return this.ConfidenceThresholdMediumWarning; } }

    private double ConfidenceThresholdLowWarnings;
    private double ConfidenceThreshouldMediumLowWarning;
    private double ConfidenceThresholdMediumWarning;

    #endregion

    #region Scores for the outcomes

    private double ScoreBottom;
    private double ScoreFalse;
    private double ScoreTrue;
    private double ScoreTop;

    #endregion

    #region Score for the warning types

    private double ScoreArithmeticDivisionByZero;
    private double ScoreArithmeticDivisionOverflow;
    private double ScoreArithmeticOverflow;
    private double ScoreArithmeticMinValueNegation;
    private double ScoreArithmeticFloatEqualityPrecisionMismatch;
    private double ScoreArrayCreation;
    private double ScoreArrayLowerBound;
    private double ScoreArrayUpperBound;

    private double ScoreArrayPurity;

    private double ScoreContractAssert;
    private double ScoreContractEnsures;
    private double ScoreContractInvariant;
    private double ScoreContractRequires;

    private double ScoreEnumRange;

    private double ScoreMissingPrecondition;
    private double ScoreMissingPreconditionInvolvingReadonly;
    private double ScoreSuggestion;

    private double ScoreNonnullCall;
    private double ScoreNonnullArray;
    private double ScoreNonnullField;
    private double ScoreNonnullUnbox;

    private double ScoreUnsafeCreation;
    private double ScoreUnsafeLowerBound;
    private double ScoreUnsafeUpperBound;

    private double ScoreUnreachedCodeAfterPrecondition;
    private double ScoreTestAlwaysEvaluatingToAConstant;
    private double ScoreFalseEnsures;
    private double ScoreFalseRequires;

    private double ScoreOffByOne;
    private double ScoreCodeRepairLikelyFixingABug;
    private double ScoreCodeRepairLikelyFoundAWeakenessInClousot;
    private double ScoreCodeRepairThinksWrongInitialization;

    private double ScoreClousotCacheNotAvailable;

    #endregion

    #region Scores for the various contexts

    private double ScoreViaMethodCall;
    private double ScoreViaArray;

    private double ScoreViaPureMethodReturn;
    private double ScoreViaOldValue;
    private double ScoreViaCast;
    private double ScoreViaOutParameter;
    private double ScoreViaField;
    private double ScoreViaCallThisHavoc;
    private double ScoreViaParameterUnmodified;

    private double ScoreViaMethodCallThatMayReturnNull;

    private double ScoreContainsReadOnlyField;
    private double ScoreMayContainForAllOrDisjunction;
    private double ScoreContainsIsInst;

    private double ScoreInferredPrecondition;
    private double ScoreInferredObjectInvariant;
    private double ScoreInferredCalleeAssume;
    private double ScoreInferredEntryAssume;

    private double ScoreAssertMayBeTurnedIntoAssume;

    private double ScoreInferredConditionIsSufficient;
    private double ScoreInferredConditionContainsDisjunction;
    private double ScoreInferredCalleeAssumeContainsDisjunction;

    private double ScoreInsideUnsafeMethod;

    private double ScoreInterproceduralPathToError;
    
    private double ScoreStringComparedAgainstNullInSwitchStatement;

    private double ScoreIsInheritedEnsures;
    private double ScoreObjectInvariantNeededForEnsures;

    private double ScoreIsVarNotEqNullCheck;
    private double ScoreIsPureMethodCallNotEqNullCheck;

    private double ScoreWPReachedMaxPathSize;
    private double ScoreWPReachedMethodEntry;
    private double ScoreWPReachedLoop;
    private double ScoreWPSkippedBecauseAdaptiveAnalysis;

    #endregion

    #region Scores for relational information

    private double ScoreRelationalIsAssertAndVarNullCheck;
    private double ScoreRelationalIsMissingPreconditionAndPureMethodNullCheck;

    #endregion

    #region Scores specific for the Calle assume info

    private const string ScoreCalleeAssumeExtraInfo = "ScoreCalleeAssumeExtraInfo"; // Important!!! This should be the prefix of the fields before, because we use reflection to get to them

#pragma warning disable 0414 // We use reflection to read those fields

    private double ScoreCalleeAssumeExtraInfoNothing;
    private double ScoreCalleeAssumeExtraInfoIsAbstract;
    private double ScoreCalleeAssumeExtraInfoIsAutoProperty;
    private double ScoreCalleeAssumeExtraInfoIsCompilerGenerated;
    private double ScoreCalleeAssumeExtraInfoIsConstructor;
    private double ScoreCalleeAssumeExtraInfoIsDispose;
    private double ScoreCalleeAssumeExtraInfoIsExtern;
    private double ScoreCalleeAssumeExtraInfoIsFinalizer;
    private double ScoreCalleeAssumeExtraInfoIsGeneric;
    private double ScoreCalleeAssumeExtraInfoIsImplicitImplementation;
    private double ScoreCalleeAssumeExtraInfoIsInternal;
    private double ScoreCalleeAssumeExtraInfoIsNewSlot;
    private double ScoreCalleeAssumeExtraInfoIsOverride;
    private double ScoreCalleeAssumeExtraInfoIsPrivate;
    private double ScoreCalleeAssumeExtraInfoIsPropertyGetterOrSetter;
    private double ScoreCalleeAssumeExtraInfoIsProtected;
    private double ScoreCalleeAssumeExtraInfoIsPublic;
    private double ScoreCalleeAssumeExtraInfoIsSealed;
    private double ScoreCalleeAssumeExtraInfoIsStatic;
    private double ScoreCalleeAssumeExtraInfoIsVirtual;
    private double ScoreCalleeAssumeExtraInfoIsVoidMethod;
    private double ScoreCalleeAssumeExtraInfoReturnPrimitiveValue;
    private double ScoreCalleeAssumeExtraInfoReturnReferenceType;
    private double ScoreCalleeAssumeExtraInfoReturnStructValue;
    private double ScoreCalleeAssumeExtraInfoDeclaredInTheSameType;
    private double ScoreCalleeAssumeExtraInfoDeclaredInADifferentAssembly;
    private double ScoreCalleeAssumeExtraInfoDeclaredInAFrameworkAssembly;

#pragma warning restore 0414

    #endregion

    #region PC based Scores

    private double ScoreNoSourceContext;

    #endregion

    #region Constructors

    /// <summary>
    /// construct the warning scores with the default values
    /// </summary>
    public WarningScoresManager(bool lowScoreForExternalAPI)
    {
      InitializeDefaultValuesForWarningThresholds();
      InitializeDefaultValuesForOutcomes();
      InitializeDefaultValuesForWarningTypes();
      InitializeDefaultValuesForContexts(lowScoreForExternalAPI);
      InitializeDefaultValuesForSourceContext();
    }

    /// <summary>
    /// If filename is null, we use the default scores
    /// </summary>
    /// <param name="filename"></param>
    public WarningScoresManager(bool lowScoreForExternalAPI, string filename)
      : this(lowScoreForExternalAPI)
    {
      if (filename != null)
      {
        InitializeValuesFromAFile(filename);
      }
    }

    #endregion

    #region Initialize the fields to the default values

    private void InitializeDefaultValuesForWarningThresholds()
    {
      ConfidenceThresholdLowWarnings = 0.99;
      ConfidenceThreshouldMediumLowWarning = 0.98;
      ConfidenceThresholdMediumWarning = 0.5;
      // full is implicitely 0
    }

    private void InitializeDefaultValuesForOutcomes()
    {
      ScoreBottom = 10.0;
      ScoreFalse = 1000.0;
      ScoreTop = 1.0;
      ScoreTrue = 0.0;
    }

    private void InitializeDefaultValuesForWarningTypes()
    {
      ScoreArithmeticDivisionByZero = 60;
      ScoreArithmeticDivisionOverflow = 50;         // This is likely to appear each time we have x/y
      ScoreArithmeticOverflow = 50;
      ScoreArithmeticMinValueNegation = 50;          // This is likely to appear each time we have "-x", so we give a low score
      ScoreArithmeticFloatEqualityPrecisionMismatch = 200;  // When we get one of those (e.g. local == o.f) we are very likely to have a possible bug

      ScoreArrayCreation = 54;                       // Usually those are quite precise, so if we get a warning is likely to be a bug
      ScoreArrayLowerBound = ScoreArrayCreation;      // As above
      ScoreArrayUpperBound = ScoreArrayCreation - 2;  // Upper bounds can be wrong because of imprecisions in the analysis

      ScoreArrayPurity = 60;

      ScoreContractAssert = 120;                     // A violated assertion may be because it's some knowledge is missing
      ScoreContractEnsures = 200;                    // A violated postcondition is bad
      ScoreContractInvariant = 150;                  // A violated invariant is not good
      ScoreContractRequires = 200;                   // A violated precondition is very bad...

      ScoreOffByOne = 1000;                           // An off-by-one is likely to be a bug
      ScoreCodeRepairLikelyFixingABug = 1000;        // We think we have a fix exposing a bug, let's push the corresponding proof obligation up

      ScoreCodeRepairLikelyFoundAWeakenessInClousot = 1; // Let us keep them the warning as it is. If it is a weakeness in Clousot, we want to fix it in the future

      ScoreEnumRange = 200;                         // Enum out of range is something we want to be very careful about, let's tread it as a precondition

      ScoreMissingPrecondition = 1000;              // Missing a precondition, in particular in public methods, is very bad
      ScoreMissingPreconditionInvolvingReadonly = ScoreMissingPrecondition / 2;

      ScoreNonnullCall = 100;                        // Calling without being sure it's not null is bad
      ScoreNonnullArray = ScoreNonnullCall + 1;       // Likely to be a bug
      ScoreNonnullField = ScoreNonnullCall - 10;      // Likely to miss an object invariant
      ScoreNonnullUnbox = ScoreNonnullCall - 1;

      ScoreUnsafeCreation = ScoreArrayCreation + 10;
      ScoreUnsafeLowerBound = ScoreArrayLowerBound + 10;
      ScoreUnsafeUpperBound = ScoreArrayUpperBound + 10;

      ScoreUnreachedCodeAfterPrecondition = ScoreTestAlwaysEvaluatingToAConstant = 1000;
      ScoreFalseEnsures = 900;
      ScoreFalseRequires = ScoreSuggestion = ScoreMissingPrecondition;

      ScoreCodeRepairThinksWrongInitialization = 1000;

      ScoreClousotCacheNotAvailable = 10000; // We abort clousot

    }

    private void InitializeDefaultValuesForContexts(bool lowScoreForExternalAPI)
    {
      // Atomic Scores

      ScoreViaMethodCall = .08;                   // missing postcondition?
      ScoreViaPureMethodReturn = .09;             // missing postcondition?
      ScoreViaOldValue = .02;                     // Old may be hard to prove

      ScoreViaField = .01;                        // low... 
      ScoreViaArray = .01;                         // low...
      ScoreViaCast = .1;                           // Have we lost some information?

      ScoreViaParameterUnmodified = .1;
      ScoreViaOutParameter = 0.1;                 // Missing some postcondition?
      ScoreViaCallThisHavoc = 0.1;                // We havoced a field

      ScoreViaMethodCallThatMayReturnNull = 10.0;  // Let's make it very high

      ScoreInferredPrecondition = .001;     
      ScoreInferredObjectInvariant = .001;
      ScoreInferredEntryAssume = .001;

      ScoreAssertMayBeTurnedIntoAssume = 1.5;     // Let's bump it a little bit

      ScoreInferredConditionIsSufficient = 1.0;      // By default, has no effect.

      ScoreInferredConditionContainsDisjunction = 1.1; // Let's bump it a little bit, as it may be a complex warning
      ScoreInferredCalleeAssumeContainsDisjunction = .01; // Let's make it very small

      ScoreInferredCalleeAssume = .8;          // Let's lower it just a little bit, as the extra info will give extra score
      InitializeDefaultValuesForContextsOfCalleeAssumeCanDischarge(lowScoreForExternalAPI);

      ScoreContainsReadOnlyField = .0001;          // If it contains a readonly field, then we can put it very low
      ScoreMayContainForAllOrDisjunction = .0001;                 // If it contains a ForAll expression, then we put it very low
      ScoreContainsIsInst = .0005;

      ScoreIsInheritedEnsures = 1.1;
      ScoreObjectInvariantNeededForEnsures = 1.2;

      ScoreStringComparedAgainstNullInSwitchStatement = ScoreInsideUnsafeMethod = 0.005;

      ScoreWPReachedMaxPathSize = 0.9; // it may be our bad because the exploration does not go so deep 
      ScoreWPReachedMethodEntry = 1.1; // bump a little bit, we inferred a sufficient condition?
      ScoreWPReachedLoop = 1.0;        // do nothing by default
      ScoreWPSkippedBecauseAdaptiveAnalysis = 0.9;

      ScoreInterproceduralPathToError = 2.0;

      ScoreIsVarNotEqNullCheck = 1.5; // little bump

      ScoreIsPureMethodCallNotEqNullCheck = 1.0; // lower the score

      // Relational scores
      ScoreRelationalIsAssertAndVarNullCheck = 1000.0;
      ScoreRelationalIsMissingPreconditionAndPureMethodNullCheck = 0.25;
    }

    private void InitializeDefaultValuesForContextsOfCalleeAssumeCanDischarge(bool lowScoreForExternalAPI)
    {
      ScoreCalleeAssumeExtraInfoIsAbstract = .8;
      ScoreCalleeAssumeExtraInfoIsVirtual = .8;

      ScoreCalleeAssumeExtraInfoIsCompilerGenerated = 000001;
      ScoreCalleeAssumeExtraInfoIsExtern = .000001;

      ScoreCalleeAssumeExtraInfoDeclaredInTheSameType = 1.5; // We want to fix it, as it's in our code!
      ScoreCalleeAssumeExtraInfoDeclaredInAFrameworkAssembly = .05;
      ScoreCalleeAssumeExtraInfoDeclaredInADifferentAssembly = lowScoreForExternalAPI ?
        ScoreCalleeAssumeExtraInfoDeclaredInAFrameworkAssembly
        : 100.0;


      #region By default, we make those neutral

      ScoreCalleeAssumeExtraInfoNothing = 1.0;
      ScoreCalleeAssumeExtraInfoIsAutoProperty = 1.0;
      ScoreCalleeAssumeExtraInfoIsConstructor = 1.0;
      ScoreCalleeAssumeExtraInfoIsDispose = 1.0;
      ScoreCalleeAssumeExtraInfoIsFinalizer = 1.0;
      ScoreCalleeAssumeExtraInfoIsGeneric = 1.0;
      ScoreCalleeAssumeExtraInfoIsImplicitImplementation = 1.0;
      ScoreCalleeAssumeExtraInfoIsNewSlot = 1.0;
      ScoreCalleeAssumeExtraInfoIsPrivate = 1.0;
      ScoreCalleeAssumeExtraInfoIsPropertyGetterOrSetter = 1.0;
      ScoreCalleeAssumeExtraInfoIsProtected = 1.0;
      ScoreCalleeAssumeExtraInfoIsPublic = 1.0;
      ScoreCalleeAssumeExtraInfoIsSealed = 1.0;
      ScoreCalleeAssumeExtraInfoIsVoidMethod = 1.0;
      ScoreCalleeAssumeExtraInfoReturnPrimitiveValue = 1.0;
      ScoreCalleeAssumeExtraInfoReturnReferenceType = 1.0;
      ScoreCalleeAssumeExtraInfoReturnStructValue = 1.0;
      ScoreCalleeAssumeExtraInfoIsOverride = 1.0;
      ScoreCalleeAssumeExtraInfoIsInternal = 1.0;
      ScoreCalleeAssumeExtraInfoIsStatic = 1.0;

      #endregion
    }

    private void InitializeDefaultValuesForSourceContext()
    {
      ScoreNoSourceContext = .2;
    }

    private void InitializeValuesFromAFile(string filename)
    {
      Contract.Requires(filename != null);

      try
      {
        filename = filename.TrimStart('@');
        var str = new StringReader(File.ReadAllText(filename));
        var reader = new XmlTextReader(str);

        var state = -1;
        var name = String.Empty;

        while (reader.Read())
        {
          switch (reader.NodeType)
          {
            case XmlNodeType.Element:
              {
                state = 0;
                name = reader.Name;
              }
              break;

            case XmlNodeType.Text:
              {
                if (state == 0)
                {
                  InitializeField(name, reader.Value);
                }
                state = -1;

              }
              break;

            case XmlNodeType.EndElement:
              {
                state = -1;
                name = String.Empty;
              }
              break;

            default:
              break;
          }
        }
      }
      catch (Exception e)
      {
        Console.Error.WriteLine("Cannot open the configuration file {0}", filename);
        Console.Error.WriteLine("  Reason: {0}", e.ToString());
      }
    }

    private void InitializeField(string fieldName, string fieldValue)
    {
      double value;
      if (Double.TryParse(fieldValue, out value))
      {
        // Get the field
        var field = typeof(WarningScoresManager).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        // Let's be sure it's a double
        if (field.GetValue(this) is double)
        {
          field.SetValue(this, value);

#if DEBUG
          Console.WriteLine("The field {0} is set to value {1}", field, value);
#endif

          return;
        }

        Console.Error.WriteLine("Cannot set the field {0} to the value {1}", fieldName, value);
      }
      else
      {
        Console.Error.WriteLine("Cannot parse the value {0} = {1}", fieldName, fieldValue);
      }
    }
    #endregion

    #region Score Logic


    /// <summary>
    /// We normalize the score to return a value in the interval [0, 1]
    /// </summary>
    /// <param name="witness"></param>
    /// <returns></returns>
    [Pure]
    [ContractVerification(true)]
    public Tuple<double, List<string>> GetScore(Witness witness)
    {
      Contract.Requires(witness != null);

      Contract.Ensures(0.0 <= Contract.Result<Tuple<double, List<string>>>().Item1);
      Contract.Ensures(Contract.Result<Tuple<double, List<string>>>().Item1 <= 2.0);
      Contract.Ensures(Contract.Result<Tuple<double, List<string>>>().Item2 != null);

      List<string> why;
      var ScoreForOutcome = this.GetScoreForOutcome(witness.Outcome);
      var ScoreForWarning = this.GetScoreForWarningType(witness.Warning);
      var ScoreForContext = this.GetScoreForWarningContexts(witness.Context, out why);
      var ScoreForPC = this.GetScoreForPC(witness.PC);
      var ScoreForRelationalInformation = this.GetScoreForRelationalInformation(witness);

      var result = ScoreForOutcome * ScoreForWarning * ScoreForContext * ScoreForPC * ScoreForRelationalInformation;

      Contract.Assume(!double.IsNaN(result));
      var score = NormalizeScore(result);

      return new Tuple<double, List<string>>(score, why);
    }


    [Pure]
    [ContractVerification(true)]
    private double NormalizeScore(double r)
    {
      Contract.Requires(!Double.IsNaN(r));
      Contract.Ensures(0.0 <= Contract.Result<double>());
      Contract.Ensures(Contract.Result<double>() <= 1.0);

      if (1.0 < r)
      {
        return (2 - 1 / r) / 2;
      }

      // We abstract all the scores under 1 to epsilon
      if (0.0 <= r /*&& r <= 1.0*/)
      {
        return r / 2;
      }

      Contract.Assume(false, "Should be unreachable");
      return 0;
    }

    [Pure]
    [ContractVerification(true)]
    private double GetScoreForOutcome(ProofOutcome proofOutcome)
    {
      switch (proofOutcome)
      {
        #region All the cases
        case ProofOutcome.Bottom:
          return this.ScoreBottom;

        case ProofOutcome.False:
          return this.ScoreFalse;

        case ProofOutcome.Top:
          return this.ScoreTop;

        case ProofOutcome.True:
          return this.ScoreTrue;

        default:
          // should be unreachable
#if DEBUG
          Contract.Assert(false);
#endif

          return 0.0;
        #endregion
      }
    }

    [Pure]
    [ContractVerification(true)]
    private double GetScoreForWarningType(WarningType warningType)
    {
      switch (warningType)
      {
        #region All the cases ...
        case WarningType.ArithmeticDivisionByZero:
          return ScoreArithmeticDivisionByZero;

        case WarningType.ArithmeticDivisionOverflow:
          return ScoreArithmeticDivisionOverflow;

        case WarningType.ArithmeticMinValueNegation:
          return ScoreArithmeticMinValueNegation;

        case WarningType.ArithmeticFloatEqualityPrecisionMismatch:
          return ScoreArithmeticFloatEqualityPrecisionMismatch;

        case WarningType.ArithmeticOverflow:
        case WarningType.ArithmeticUnderflow:
          return ScoreArithmeticOverflow;

        case WarningType.ArrayCreation:
          return ScoreArrayCreation;

        case WarningType.ArrayLowerBound:
          return ScoreArrayLowerBound;

        case WarningType.ArrayUpperBound:
          return ScoreArrayUpperBound;

        case WarningType.ArrayPurity:
          return ScoreArrayPurity;

        case WarningType.ClousotCacheNotAvailable:
          return ScoreClousotCacheNotAvailable;

        case WarningType.ContractAssert:
          return ScoreContractAssert;

        case WarningType.ContractEnsures:
          return ScoreContractEnsures;

        case WarningType.ContractInvariant:
          return ScoreContractInvariant;

        case WarningType.ContractRequires:
          return ScoreContractRequires;

        case WarningType.EnumRange:
          return ScoreEnumRange;

        case WarningType.MissingPrecondition:
          return ScoreMissingPrecondition;

        case WarningType.MissingPreconditionInvolvingReadonly:
          return ScoreMissingPreconditionInvolvingReadonly;

        case WarningType.Suggestion:
          return ScoreSuggestion;

        case WarningType.NonnullArray:
          return ScoreNonnullArray;

        case WarningType.NonnullCall:
          return ScoreNonnullCall;

        case WarningType.NonnullField:
          return ScoreNonnullField;

        case WarningType.NonnullUnbox:
          return ScoreNonnullUnbox;

        case WarningType.UnsafeCreation:
          return ScoreUnsafeCreation;

        case WarningType.UnsafeLowerBound:
          return ScoreUnsafeLowerBound;

        case WarningType.UnsafeUpperBound:
          return ScoreUnsafeUpperBound;

        case WarningType.UnreachedCodeAfterPrecondition:
          return ScoreUnreachedCodeAfterPrecondition;

        case WarningType.FalseEnsures:
          return ScoreFalseEnsures;

        case WarningType.FalseRequires:
          return ScoreFalseRequires;

        case WarningType.TestAlwaysEvaluatingToAConstant:
          return ScoreTestAlwaysEvaluatingToAConstant;

        default:
          // should be unreachable
#if DEBUG
          Contract.Assume(false);
#endif

          return 1.0;
        #endregion
      }
    }

    [Pure]
    [ContractVerification(true)]
    private double GetScoreForWarningContexts(Set<WarningContext> contexts, out List<string> why)
    {
      Contract.Ensures(Contract.ValueAtReturn(out why) != null);

      var score = 1.0;
      why = new List<string>();

      if (contexts == null || contexts.Count == 0)
      {
        why.Add("no context");
        return score;
      }

      foreach (var context in contexts)
      {
        why.Add(string.Format("{0}", context.ToString()));

        #region All the cases
        switch (context.Type)
        {
          case WarningContext.ContextType.NonNullAccessPath:
            {
              score *= context.AssociatedInfo > 1 ? (1.0 / (context.AssociatedInfo - 1)) : 1.0;
            }
            break;

          case WarningContext.ContextType.ViaParameterUnmodified:
            {
              var exp = Math.Max(1, context.AssociatedInfo);
              score *= ScoreViaParameterUnmodified / Math.Pow(2.0, exp);
            }
            break;

          case WarningContext.ContextType.InferredPrecondition:
            {
              // we do not take div here, because we assume that one precondition is enough
              // Also at the point we get this info, it may be the case we have not run the full simplification (because of performance) so there may be fewer "real" preconditions
              score *= ScoreInferredPrecondition;
            }
            break;

          case WarningContext.ContextType.InferredObjectInvariant:
            {
              var div = (double)Math.Max(1, context.AssociatedInfo);
              score *= ScoreInferredObjectInvariant / div;
            }
            break;

          case WarningContext.ContextType.InferredCalleeAssume:
            {
              score *= ComputeScoreForInferredCalleeAssume((WarningContext.CalleeInfo)context.AssociatedInfo);
            }
            break;

          case WarningContext.ContextType.AssertMayBeTurnedIntoAssume:
            {
              score *= ScoreAssertMayBeTurnedIntoAssume;
            }
            break;

          case WarningContext.ContextType.InferredEntryAssume:
            {
              var div = (double)Math.Max(1, context.AssociatedInfo);
              score *= ScoreInferredEntryAssume / div;
            }
            break;
          case WarningContext.ContextType.ViaArray:
            {
              score *= ScoreViaArray * (1.0 / (context.AssociatedInfo != 0 ? context.AssociatedInfo : 1.0));
            }
            break;

          case WarningContext.ContextType.ViaField:
            {
              score *= ScoreViaField * (1.0 / (context.AssociatedInfo != 0 ? context.AssociatedInfo : 1.0));
            }
            break;

          case WarningContext.ContextType.ViaMethodCall:
            {
              score *= ScoreViaMethodCall * (1.0 / (context.AssociatedInfo != 0 ? context.AssociatedInfo : 1.0));
            }
            break;

          case WarningContext.ContextType.ViaMethodCallThatMayReturnNull:
            {
              // Ignore associated info
              score *= ScoreViaMethodCallThatMayReturnNull;
            }
            break;

          case WarningContext.ContextType.ContainsReadOnly:
            {
              score *= ScoreContainsReadOnlyField;
            }
            break;

          case WarningContext.ContextType.MayContainForAllOrADisjunction:
            {
              score *= ScoreMayContainForAllOrDisjunction;
            }
            break;

          case WarningContext.ContextType.IsVarNotEqNullCheck:
            {
              score *= ScoreIsVarNotEqNullCheck;
            }
            break;

          case WarningContext.ContextType.IsPureMethodCallNotEqNullCheck:
            {
              score *= ScoreIsPureMethodCallNotEqNullCheck;
            }
            break;

          case WarningContext.ContextType.ContainsIsInst:
            {
              score *= ScoreContainsIsInst;
            }
            break;

          case WarningContext.ContextType.OffByOne:
            {
              score *= ScoreOffByOne;
            }
            break;

          case WarningContext.ContextType.CodeRepairLikelyFixingABug:
            {
              score *= ScoreCodeRepairLikelyFixingABug;
            }
            break;

          case WarningContext.ContextType.CodeRepairLikelyFoundAWeakenessInClousot:
            {
              score *= ScoreCodeRepairLikelyFoundAWeakenessInClousot;
            }
            break;

          case WarningContext.ContextType.CodeRepairThinksWrongInitialization:
            {
              score *= ScoreCodeRepairThinksWrongInitialization;
            }
            break;

          case WarningContext.ContextType.ViaPureMethodReturn:
            {
              score *= ScoreViaPureMethodReturn * Math.Pow(2.0, 1-context.AssociatedInfo);
            }
            break;

          case WarningContext.ContextType.ViaCast:
            {
              score *= ScoreViaCast;
            }
            break;

          case WarningContext.ContextType.ViaOutParameter:
            {
              score *= ScoreViaOutParameter;
            }
            break;

          case WarningContext.ContextType.ViaCallThisHavoc:
            {
              score *= ScoreViaCallThisHavoc;
            }
            break;

          case WarningContext.ContextType.ViaOldValue:
          case WarningContext.ContextType.ConditionContainsValueAtReturn:
            {
              score *= ScoreViaOldValue;
            }
            break;

          case WarningContext.ContextType.InferredConditionIsSufficient:
            {
              score *= ScoreInferredConditionIsSufficient;
            }
            break;

          case WarningContext.ContextType.InferredConditionContainsDisjunction:
            {
              score *= ScoreInferredConditionContainsDisjunction;
            }
            break;

          case WarningContext.ContextType.InferredCalleeAssumeContainsDisjunction:
            {
              score *= ScoreInferredCalleeAssumeContainsDisjunction;
            }
            break;

          case WarningContext.ContextType.InheritedEnsures:
            {
              score *= ScoreIsInheritedEnsures;
            }
            break;

          case WarningContext.ContextType.InsideUnsafeMethod:
            {
              score *= ScoreInsideUnsafeMethod;
            }
            break;

          case WarningContext.ContextType.InterproceduralPathToError:
            {
              score *= (ScoreInterproceduralPathToError + (1.0 * context.AssociatedInfo));
            }
            break;

          case WarningContext.ContextType.ObjectInvariantNeededForEnsures:
            {
              score *= ScoreObjectInvariantNeededForEnsures;
            }
            break;

          case WarningContext.ContextType.StringComparedAgainstNullInSwitchStatement:
            {
              score *= ScoreStringComparedAgainstNullInSwitchStatement;
            }
            break;

          case WarningContext.ContextType.WPReachedMaxPathSize:
            {
              score *= ScoreWPReachedMaxPathSize;
            }
            break;

          case WarningContext.ContextType.WPReachedMethodEntry:
            {
              score *= ScoreWPReachedMethodEntry;
            }
            break;

          case WarningContext.ContextType.WPReachedLoop:
            {
              score *= ScoreWPReachedLoop;
            }
            break;

          case WarningContext.ContextType.WPSkippedBecauseAdaptiveAnalysis:
            {
              score *= ScoreWPSkippedBecauseAdaptiveAnalysis;
            }
            break;

          default:
            Contract.Assert(false, "Should be unreached, we forgot one case? " + context.Type);
            // do nothing
            break;

        #endregion
        }
      }

      return score;
    }

    [Pure]
    private double GetScoreForRelationalInformation(Witness witness)
    {
      // assert (x != null)
      if(witness.WarningKind == WarningKind.Assert && ContainsWarningContext(witness, WarningContext.ContextType.IsVarNotEqNullCheck))
      {
        return ScoreRelationalIsAssertAndVarNullCheck;
      }

      // Requires(a.Property != null)
      if(witness.WarningKind == WarningKind.MissingPrecondition && ContainsWarningContext(witness, WarningContext.ContextType.IsPureMethodCallNotEqNullCheck))
      {
        return ScoreRelationalIsMissingPreconditionAndPureMethodNullCheck;
      }
      return 1.0;
    }

    [Pure]
    static private bool ContainsWarningContext(Witness witness, WarningContext.ContextType what)
    {
      return witness.Context.Any(wc => wc.Type == what);
    }

    [Pure]
    private double ComputeScoreForInferredCalleeAssume(WarningContext.CalleeInfo calleeInfo)
    {
      var result = ScoreInferredCalleeAssume;

      // Using reflection, there are too many cases (26?) to consider, and we may add more in the future...

      var thisFields = typeof(WarningScoresManager).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
        .Where(f => f.Name.StartsWith(ScoreCalleeAssumeExtraInfo))
        .ToArray(); // Use ToArray to force the evaluation

      foreach (var enumFlag in Enum.GetValues(typeof(WarningContext.CalleeInfo)).Cast<WarningContext.CalleeInfo>())
      {
        if (calleeInfo.HasFlag(enumFlag))
        {
          var fieldName = ScoreCalleeAssumeExtraInfo + enumFlag.ToString();
          var field = thisFields.Where(f => f.Name == fieldName).FirstOrDefault();
          if (field != null) // Sanity check, should never happen
          {
            var score = (double)field.GetValue(this);
            result *= score;
          }
          else
          {
            Contract.Assume(false, "Should never happen");
          }
        }
      }

      return result;
    }

    [Pure]
    [ContractVerification(true)]
    private double GetScoreForPC(APC pc)
    {
      var Score = 1.0;
      if (pc.Block != null && !pc.PrimaryMethodLocation().HasRealSourceContext)
      {
        Score *= ScoreNoSourceContext;
      }
      return Score;
    }

    #endregion

    #region Tracing

    public static bool Tracing = false;

    #endregion

    #region Dump the scores
    public void DumpScores(Action<string, object[]> writeline)
    {
      Contract.Requires(writeline != null);

      var empty = new object[0];
      writeline("<ClousotScores>", empty);
      foreach (var f in typeof(WarningScoresManager).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        writeline("    <{0}>{1}</{0}>", new object[] { f.Name, f.GetValue(this).ToString() });
      }
      writeline("</ClousotScores>", empty);
    }
    #endregion
  }
}

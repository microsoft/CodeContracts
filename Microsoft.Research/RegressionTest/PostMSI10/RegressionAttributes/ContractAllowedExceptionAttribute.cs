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

// #define PEX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if PEX
using Microsoft.Pex.Framework.Validation;
using Microsoft.ExtendedReflection.Utilities;
using Microsoft.Pex.Framework.Packages;
using Microsoft.ExtendedReflection.ComponentModel;
using Microsoft.Pex.Engine.Exceptions;
using Microsoft.Pex.Engine.Goals;
using Microsoft.ExtendedReflection.Utilities.Safe;
using Microsoft.Pex.Framework.Generated;
#endif

#if PEX
namespace Contracts.Regression
{
  [__DoNotInstrument]
  public class ContractAllowedExceptionAttribute : PexExplorationPackageAttributeBase
  {
    PexRuntimeContractsFlags expectedKind;
    int expectedCount;

    public ContractAllowedExceptionAttribute(PexRuntimeContractsFlags expected, int expectedCount)
    {
      this.expectedKind = expected;
      this.expectedCount = expectedCount;
    }

    class Validator : ExplorationComponentElementBase, IPexExplorationGoal, IPexExceptionValidator
    {
      readonly PexRuntimeContractsFlags expectedKind;
      readonly int expectedCount;
      int actualCount;

      public Validator(Microsoft.Pex.Engine.ComponentModel.IPexExplorationComponent host, PexRuntimeContractsFlags expectedKind, int expectedCount)
        : base(host)
      {
        this.expectedKind = expectedKind;
        this.expectedCount = expectedCount;
      }

      public PexExceptionState Validate(Exception exception, out string message, out string wikiTopic)
      {
        message = null;
        wikiTopic = null;

        if (exception == null) return PexExceptionState.Unknown;

        ContractsMetadata.ContractFailureKind kind;

        if (ContractsMetadata.TryGetContractExceptionKind(exception, out kind)&& kind == expectedKind)
        {
          actualCount++;
          if (actualCount <= expectedCount) return PexExceptionState.Expected;
        }
        return PexExceptionState.Unknown;
      }

#region IPexExplorationGoal Members

      public string GetOutcome()
      {
        return SafeString.Format("Contracts expected {0} {1} outcomes, got {2}", this.expectedCount, this.expectedKind, this.actualCount);
      }

      bool IPexExplorationGoal.IsReached(bool explorationFinished)
      {
        if (explorationFinished)
        {
          if (expectedCount != actualCount)
          {
            return false;
          }
          else
          {
            return true;
          }
        }
        return false;
      }

      string IPexExplorationGoal.Kind
      {
        get { return GetOutcome(); }
      }

      #endregion

#region IPexExceptionValidator Members

      string IPexExceptionValidator.Description
      {
        get { return "MaF"; }
      }

      void IPexExceptionValidator.MarkTest(Exception exception, Microsoft.ExtendedReflection.Metadata.Builders.MethodDefinitionBuilder testMethod)
      {
      }

      #endregion
    }

    protected override object BeforeExploration(Microsoft.Pex.Engine.ComponentModel.IPexExplorationComponent host)
    {
      var validator = new Validator(host, this.expectedKind, this.expectedCount);
      host.ExplorationServices.ExceptionManager.AddExceptionValidator(validator);
      host.ExplorationServices.GoalManager.AddGoal(validator);
      return null;
    }

  }
}
#endif
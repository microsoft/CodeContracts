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


namespace Microsoft.Research.CodeAnalysis
{

  public static class ContractDensityAnalyzer
  {
    public static ContractDensity GetContractDensity<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver
    )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.GetContractDensity(driver);
    }

    public static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      internal static ContractDensity GetContractDensity(
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver)
      {
        var analysis = new DensityAnalysis();

        return analysis.DoIt(driver);
      }

      private class DensityAnalysis
        : CodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Unit, Unit, Attribute, Assembly, IMethodContext<Field, Method>, Unit>
      {
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
        }

        ulong methodInstructions;
        ulong contractInstructions;
        ulong contracts;

        public DensityAnalysis()
        {
          methodInstructions = 0;
          contractInstructions = 0;
          contracts = 0;
        }

        public ContractDensity DoIt(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver)
        {
          base.Run(driver.RawLayer, null);

          return new ContractDensity(this.methodInstructions, this.contractInstructions, this.contracts);
        }

        #region Visit of the instructions

        public override bool Join(DataStructures.Pair<APC, APC> edge, bool newState, bool prevState, out bool weaker, bool widen)
        {
          weaker = false;
          return true;
        }

        /// <summary>
        /// By default, count instruction
        /// </summary>
        protected override bool Default(APC pc, bool data)
        {
          if (pc.InsideContract)
          {
            contractInstructions++;
          }
          else
          {
            methodInstructions++;
          }
          return data;
        }

        public override bool Assert(APC pc, string tag, Unit condition, object provenance, bool data)
        {
          base.Assert(pc, tag, condition, provenance, data);
          if (!pc.InsideContract)
          {
            // shift some instructions from method to contract
            var delta = Math.Min(contractInstructions / (contracts + 1), 4);
            if (methodInstructions > delta)
            {
              methodInstructions -= delta;
            }
            contractInstructions += delta;
          }
          contracts++;
          return true;
        }

        public override bool Assume(APC pc, string tag, Unit condition, object provenance, bool data)
        {
          base.Assume(pc, tag, condition, provenance, data);
          if (tag == "true" || tag == "false") return true;
          if (!pc.InsideContract)
          {
            // shift some instructions from method to contract
            var delta = Math.Min(contractInstructions / (contracts + 1), 4);
            if (methodInstructions > delta)
            {
              methodInstructions -= delta;
            }
            contractInstructions += delta;
          }
          contracts++;
          return true;
        }

        public override bool Nop(APC pc, bool data)
        {
          // not counted as instruction!
          return data;
        }

        #endregion
      }
    }
  }
}

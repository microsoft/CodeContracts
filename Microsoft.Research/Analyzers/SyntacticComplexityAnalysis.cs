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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using MethodName = System.String;

namespace Microsoft.Research.CodeAnalysis
{
  public static class SyntacticComplexityInfo
  {
    public static SyntacticComplexity GetSyntacticComplexity<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver
    )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.GetSyntacticComplexity(driver);
    }

    public static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      internal static SyntacticComplexity GetSyntacticComplexity(
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver)
      {
        var analysis = new SyntacticComplexityAnalysis();

        return analysis.DoIt(driver);
      }

      private class SyntacticComplexityAnalysis
        : CodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Unit, Unit, Attribute, Assembly, IMethodContext<Field,Method>, Unit>
      {
        #region Object invariant
       
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(joinpoints >= loops);
        }
        
        #endregion

        #region Private state

        uint loops;
        uint instructions;
        uint joinpoints;

        #endregion
      
        public SyntacticComplexityAnalysis()
        {
          loops = 0;
          instructions = 0;
          joinpoints = 0;
        }

        public SyntacticComplexity DoIt(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver)
        {
          base.Run(driver.RawLayer);

          return new SyntacticComplexity(this.joinpoints, this.instructions, this.loops);
        }

        #region Visit of the instructions

        public override bool Join(DataStructures.Pair<APC, APC> edge, bool newState, bool prevState, out bool weaker, bool widen)
        {
          if (widen)
          {
            loops++;
          }

          joinpoints++;

          return base.Join(edge, newState, prevState, out weaker, widen);
        }

        /// <summary>
        /// By default, count instruction
        /// </summary>
        protected override bool Default(APC pc, bool data)
        {
          /*
          var handlers = this.Context.MethodContext.CFG.ExceptionHandlers<int, int>(pc, 0, null);
          foreach(var h in handlers)
          {
            if (!pc.Block.Subroutine.ExceptionExit.Equals(h.Block))
            {
              Console.WriteLine("found!");
            }
          }
          Console.WriteLine();
          */
          instructions++;
          return data;
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

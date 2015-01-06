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
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class BoxedVariable<Variable>
    : IEquatable<BoxedVariable<Variable>>
  {
    enum VariableKind { Framework, NoFramework, Slack }

    readonly Variable var;
    readonly InnerVariable innerVar;
    readonly VariableKind varKind;

    public BoxedVariable(Variable var)
    {
      if (var != null)
      {
        this.var = var;
        this.innerVar = default(InnerVariable);
        this.varKind = VariableKind.Framework;
      }
      else
      {
        this.var = default(Variable);
        this.innerVar = new InnerVariable();
        this.varKind = VariableKind.Slack;
      }
    }

    public BoxedVariable(bool slack)
    {
      this.var = default(Variable);
      this.innerVar = new InnerVariable();
      this.varKind = slack ? VariableKind.Slack: VariableKind.NoFramework;
    }


    public bool IsSlackVariable
    {
      get
      {
        return this.varKind == VariableKind.Slack;
      }
    }

    public bool IsFrameworkVariable
    {
      get
      {
        return this.varKind == VariableKind.Framework;
      }
    }

    public bool TryUnpackVariable(out Variable v)
    {
      if (this.innerVar != null)
      {
        v = default(Variable);
        return false;
      }
      else
      {
        v = this.var;
        return true;
      }
    }

    public static void ResetFreshVariableCounter()
    {
      InnerVariable.ResetFreshVariableCounter();
    }

    public bool Equals(BoxedVariable<Variable> other)
    {
      Contract.Assume(other != null);

      if (this.innerVar != null)
      {
        if (other.innerVar != null)
        {
          return this.innerVar.Equals(other.innerVar);
        }
      }
      else if (other.innerVar == null)
      {
        return this.var.Equals(other.var);
      }

      return false;
    }

    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      var that = obj as BoxedVariable<Variable>;

      if (that != null)
      {
        if (this.innerVar != null)
        {
          if (that.innerVar != null)
          {
            return this.innerVar.Equals(that.innerVar);
          }

          return false;
        }

        if (this.var != null)
        {
          return this.var.Equals(that.var);
        }
      }

      if (obj is Variable && this.innerVar == null)
      {
        return obj.Equals(this.var);
      }

      return false;
    }

    public override int GetHashCode()
    {
      if (this.innerVar != null)
        return innerVar.GetHashCode();
      else
        return var.GetHashCode();
    }

    public override string ToString()
    {
      if (this.innerVar != null)
      {
        if (this.IsSlackVariable)
        {
          Debug.Assert(this.varKind == VariableKind.Slack);
          return "s" + innerVar.ToString();
        }
        else
        {
          Contract.Assume(this.varKind == VariableKind.NoFramework);
          return innerVar.ToString();
        }
      }
      else
      {
        Contract.Assume(this.varKind == VariableKind.Framework);
        return var.ToString();
      }
    }

    class InnerVariable
    {
#if DEBUG
      [ThreadStatic]
#endif
      private static int count;

#if DEBUG
      [ThreadStatic]
      private static int startCount;
#endif

      private readonly int id;
#if DEBUG
      private readonly int startId;
#endif

      public InnerVariable()
      {
        this.id = count++;
#if DEBUG
        this.startId = startCount;
#endif
      }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;

        var that = obj as InnerVariable;

        if (that != null)
        {
          return this.id == that.id;
        }

        return false;
      }

      public override int GetHashCode()
      {
        return this.id;
      }

      public override string ToString()
      {
#if DEBUG
        return "iv" + (this.id - this.startId).ToString();
#else
        return "iv" + this.id.ToString();
#endif
      }

      [Conditional("DEBUG")]
      public static void ResetFreshVariableCounter()
      {
#if DEBUG
        startCount = count; 
#endif
      }
    }
  }
}
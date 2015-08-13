// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    public class BoxedVariable<Variable>
      : IEquatable<BoxedVariable<Variable>>
    {
        private enum VariableKind { Framework, NoFramework, Slack }

        private readonly Variable var;
        private readonly InnerVariable innerVar;
        private readonly VariableKind varKind;

        public BoxedVariable(Variable var)
        {
            if (var != null)
            {
                this.var = var;
                innerVar = default(InnerVariable);
                varKind = VariableKind.Framework;
            }
            else
            {
                this.var = default(Variable);
                innerVar = new InnerVariable();
                varKind = VariableKind.Slack;
            }
        }

        public BoxedVariable(bool slack)
        {
            var = default(Variable);
            innerVar = new InnerVariable();
            varKind = slack ? VariableKind.Slack : VariableKind.NoFramework;
        }


        public bool IsSlackVariable
        {
            get
            {
                return varKind == VariableKind.Slack;
            }
        }

        public bool IsFrameworkVariable
        {
            get
            {
                return varKind == VariableKind.Framework;
            }
        }

        public bool TryUnpackVariable(out Variable v)
        {
            if (innerVar != null)
            {
                v = default(Variable);
                return false;
            }
            else
            {
                v = var;
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

            if (innerVar != null)
            {
                if (other.innerVar != null)
                {
                    return innerVar.Equals(other.innerVar);
                }
            }
            else if (other.innerVar == null)
            {
                return var.Equals(other.var);
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
                if (innerVar != null)
                {
                    if (that.innerVar != null)
                    {
                        return innerVar.Equals(that.innerVar);
                    }

                    return false;
                }

                if (var != null)
                {
                    return var.Equals(that.var);
                }
            }

            if (obj is Variable && innerVar == null)
            {
                return obj.Equals(var);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (innerVar != null)
                return innerVar.GetHashCode();
            else
                return var.GetHashCode();
        }

        public override string ToString()
        {
            if (innerVar != null)
            {
                if (this.IsSlackVariable)
                {
                    Debug.Assert(varKind == VariableKind.Slack);
                    return "s" + innerVar.ToString();
                }
                else
                {
                    Contract.Assume(varKind == VariableKind.NoFramework);
                    return innerVar.ToString();
                }
            }
            else
            {
                Contract.Assume(varKind == VariableKind.Framework);
                return var.ToString();
            }
        }

        private class InnerVariable
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
                id = count++;
#if DEBUG
                startId = startCount;
#endif
            }

            public override bool Equals(object obj)
            {
                if (this == obj)
                    return true;

                var that = obj as InnerVariable;

                if (that != null)
                {
                    return id == that.id;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return id;
            }

            public override string ToString()
            {
#if DEBUG
                return "iv" + (id - startId).ToString();
#else
                return "iv" + id.ToString();
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
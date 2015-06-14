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

// File System.Reflection.Emit.OpCodes.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Reflection.Emit
{
  public partial class OpCodes
  {
    #region Methods and constructors
    internal OpCodes()
    {
    }

    public static bool TakesSingleByteArgument(OpCode inst)
    {
      return default(bool);
    }
    #endregion

    #region Fields
    public readonly static OpCode Add;
    public readonly static OpCode Add_Ovf;
    public readonly static OpCode Add_Ovf_Un;
    public readonly static OpCode And;
    public readonly static OpCode Arglist;
    public readonly static OpCode Beq;
    public readonly static OpCode Beq_S;
    public readonly static OpCode Bge;
    public readonly static OpCode Bge_S;
    public readonly static OpCode Bge_Un;
    public readonly static OpCode Bge_Un_S;
    public readonly static OpCode Bgt;
    public readonly static OpCode Bgt_S;
    public readonly static OpCode Bgt_Un;
    public readonly static OpCode Bgt_Un_S;
    public readonly static OpCode Ble;
    public readonly static OpCode Ble_S;
    public readonly static OpCode Ble_Un;
    public readonly static OpCode Ble_Un_S;
    public readonly static OpCode Blt;
    public readonly static OpCode Blt_S;
    public readonly static OpCode Blt_Un;
    public readonly static OpCode Blt_Un_S;
    public readonly static OpCode Bne_Un;
    public readonly static OpCode Bne_Un_S;
    public readonly static OpCode Box;
    public readonly static OpCode Br;
    public readonly static OpCode Br_S;
    public readonly static OpCode Break;
    public readonly static OpCode Brfalse;
    public readonly static OpCode Brfalse_S;
    public readonly static OpCode Brtrue;
    public readonly static OpCode Brtrue_S;
    public readonly static OpCode Call;
    public readonly static OpCode Calli;
    public readonly static OpCode Callvirt;
    public readonly static OpCode Castclass;
    public readonly static OpCode Ceq;
    public readonly static OpCode Cgt;
    public readonly static OpCode Cgt_Un;
    public readonly static OpCode Ckfinite;
    public readonly static OpCode Clt;
    public readonly static OpCode Clt_Un;
    public readonly static OpCode Constrained;
    public readonly static OpCode Conv_I;
    public readonly static OpCode Conv_I1;
    public readonly static OpCode Conv_I2;
    public readonly static OpCode Conv_I4;
    public readonly static OpCode Conv_I8;
    public readonly static OpCode Conv_Ovf_I;
    public readonly static OpCode Conv_Ovf_I_Un;
    public readonly static OpCode Conv_Ovf_I1;
    public readonly static OpCode Conv_Ovf_I1_Un;
    public readonly static OpCode Conv_Ovf_I2;
    public readonly static OpCode Conv_Ovf_I2_Un;
    public readonly static OpCode Conv_Ovf_I4;
    public readonly static OpCode Conv_Ovf_I4_Un;
    public readonly static OpCode Conv_Ovf_I8;
    public readonly static OpCode Conv_Ovf_I8_Un;
    public readonly static OpCode Conv_Ovf_U;
    public readonly static OpCode Conv_Ovf_U_Un;
    public readonly static OpCode Conv_Ovf_U1;
    public readonly static OpCode Conv_Ovf_U1_Un;
    public readonly static OpCode Conv_Ovf_U2;
    public readonly static OpCode Conv_Ovf_U2_Un;
    public readonly static OpCode Conv_Ovf_U4;
    public readonly static OpCode Conv_Ovf_U4_Un;
    public readonly static OpCode Conv_Ovf_U8;
    public readonly static OpCode Conv_Ovf_U8_Un;
    public readonly static OpCode Conv_R_Un;
    public readonly static OpCode Conv_R4;
    public readonly static OpCode Conv_R8;
    public readonly static OpCode Conv_U;
    public readonly static OpCode Conv_U1;
    public readonly static OpCode Conv_U2;
    public readonly static OpCode Conv_U4;
    public readonly static OpCode Conv_U8;
    public readonly static OpCode Cpblk;
    public readonly static OpCode Cpobj;
    public readonly static OpCode Div;
    public readonly static OpCode Div_Un;
    public readonly static OpCode Dup;
    public readonly static OpCode Endfilter;
    public readonly static OpCode Endfinally;
    public readonly static OpCode Initblk;
    public readonly static OpCode Initobj;
    public readonly static OpCode Isinst;
    public readonly static OpCode Jmp;
    public readonly static OpCode Ldarg;
    public readonly static OpCode Ldarg_0;
    public readonly static OpCode Ldarg_1;
    public readonly static OpCode Ldarg_2;
    public readonly static OpCode Ldarg_3;
    public readonly static OpCode Ldarg_S;
    public readonly static OpCode Ldarga;
    public readonly static OpCode Ldarga_S;
    public readonly static OpCode Ldc_I4;
    public readonly static OpCode Ldc_I4_0;
    public readonly static OpCode Ldc_I4_1;
    public readonly static OpCode Ldc_I4_2;
    public readonly static OpCode Ldc_I4_3;
    public readonly static OpCode Ldc_I4_4;
    public readonly static OpCode Ldc_I4_5;
    public readonly static OpCode Ldc_I4_6;
    public readonly static OpCode Ldc_I4_7;
    public readonly static OpCode Ldc_I4_8;
    public readonly static OpCode Ldc_I4_M1;
    public readonly static OpCode Ldc_I4_S;
    public readonly static OpCode Ldc_I8;
    public readonly static OpCode Ldc_R4;
    public readonly static OpCode Ldc_R8;
    public readonly static OpCode Ldelem;
    public readonly static OpCode Ldelem_I;
    public readonly static OpCode Ldelem_I1;
    public readonly static OpCode Ldelem_I2;
    public readonly static OpCode Ldelem_I4;
    public readonly static OpCode Ldelem_I8;
    public readonly static OpCode Ldelem_R4;
    public readonly static OpCode Ldelem_R8;
    public readonly static OpCode Ldelem_Ref;
    public readonly static OpCode Ldelem_U1;
    public readonly static OpCode Ldelem_U2;
    public readonly static OpCode Ldelem_U4;
    public readonly static OpCode Ldelema;
    public readonly static OpCode Ldfld;
    public readonly static OpCode Ldflda;
    public readonly static OpCode Ldftn;
    public readonly static OpCode Ldind_I;
    public readonly static OpCode Ldind_I1;
    public readonly static OpCode Ldind_I2;
    public readonly static OpCode Ldind_I4;
    public readonly static OpCode Ldind_I8;
    public readonly static OpCode Ldind_R4;
    public readonly static OpCode Ldind_R8;
    public readonly static OpCode Ldind_Ref;
    public readonly static OpCode Ldind_U1;
    public readonly static OpCode Ldind_U2;
    public readonly static OpCode Ldind_U4;
    public readonly static OpCode Ldlen;
    public readonly static OpCode Ldloc;
    public readonly static OpCode Ldloc_0;
    public readonly static OpCode Ldloc_1;
    public readonly static OpCode Ldloc_2;
    public readonly static OpCode Ldloc_3;
    public readonly static OpCode Ldloc_S;
    public readonly static OpCode Ldloca;
    public readonly static OpCode Ldloca_S;
    public readonly static OpCode Ldnull;
    public readonly static OpCode Ldobj;
    public readonly static OpCode Ldsfld;
    public readonly static OpCode Ldsflda;
    public readonly static OpCode Ldstr;
    public readonly static OpCode Ldtoken;
    public readonly static OpCode Ldvirtftn;
    public readonly static OpCode Leave;
    public readonly static OpCode Leave_S;
    public readonly static OpCode Localloc;
    public readonly static OpCode Mkrefany;
    public readonly static OpCode Mul;
    public readonly static OpCode Mul_Ovf;
    public readonly static OpCode Mul_Ovf_Un;
    public readonly static OpCode Neg;
    public readonly static OpCode Newarr;
    public readonly static OpCode Newobj;
    public readonly static OpCode Nop;
    public readonly static OpCode Not;
    public readonly static OpCode Or;
    public readonly static OpCode Pop;
    public readonly static OpCode Prefix1;
    public readonly static OpCode Prefix2;
    public readonly static OpCode Prefix3;
    public readonly static OpCode Prefix4;
    public readonly static OpCode Prefix5;
    public readonly static OpCode Prefix6;
    public readonly static OpCode Prefix7;
    public readonly static OpCode Prefixref;
    public readonly static OpCode Readonly;
    public readonly static OpCode Refanytype;
    public readonly static OpCode Refanyval;
    public readonly static OpCode Rem;
    public readonly static OpCode Rem_Un;
    public readonly static OpCode Ret;
    public readonly static OpCode Rethrow;
    public readonly static OpCode Shl;
    public readonly static OpCode Shr;
    public readonly static OpCode Shr_Un;
    public readonly static OpCode Sizeof;
    public readonly static OpCode Starg;
    public readonly static OpCode Starg_S;
    public readonly static OpCode Stelem;
    public readonly static OpCode Stelem_I;
    public readonly static OpCode Stelem_I1;
    public readonly static OpCode Stelem_I2;
    public readonly static OpCode Stelem_I4;
    public readonly static OpCode Stelem_I8;
    public readonly static OpCode Stelem_R4;
    public readonly static OpCode Stelem_R8;
    public readonly static OpCode Stelem_Ref;
    public readonly static OpCode Stfld;
    public readonly static OpCode Stind_I;
    public readonly static OpCode Stind_I1;
    public readonly static OpCode Stind_I2;
    public readonly static OpCode Stind_I4;
    public readonly static OpCode Stind_I8;
    public readonly static OpCode Stind_R4;
    public readonly static OpCode Stind_R8;
    public readonly static OpCode Stind_Ref;
    public readonly static OpCode Stloc;
    public readonly static OpCode Stloc_0;
    public readonly static OpCode Stloc_1;
    public readonly static OpCode Stloc_2;
    public readonly static OpCode Stloc_3;
    public readonly static OpCode Stloc_S;
    public readonly static OpCode Stobj;
    public readonly static OpCode Stsfld;
    public readonly static OpCode Sub;
    public readonly static OpCode Sub_Ovf;
    public readonly static OpCode Sub_Ovf_Un;
    public readonly static OpCode Switch;
    public readonly static OpCode Tailcall;
    public readonly static OpCode Throw;
    public readonly static OpCode Unaligned;
    public readonly static OpCode Unbox;
    public readonly static OpCode Unbox_Any;
    public readonly static OpCode Volatile;
    public readonly static OpCode Xor;
    #endregion
  }
}

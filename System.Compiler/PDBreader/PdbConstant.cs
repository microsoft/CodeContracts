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

namespace Microsoft.Cci.Pdb
{
  internal class PdbConstant
  {
    internal string name;
    internal uint token;
    internal object value;

    internal PdbConstant(BitAccess bits)
    {
      bits.ReadUInt32(out this.token);
      byte tag1;
      bits.ReadUInt8(out tag1);
      byte tag2;
      bits.ReadUInt8(out tag2);
      if (tag2 == 0)
      {
        this.value = tag1;
      }
      else if (tag2 == 0x80)
      {
        switch (tag1)
        {
          case 0x00: //sbyte
            sbyte sb;
            bits.ReadInt8(out sb);
            this.value = sb;
            break;
          case 0x01: //short
            short s;
            bits.ReadInt16(out s);
            this.value = s;
            break;
          case 0x02: //ushort
            ushort us;
            bits.ReadUInt16(out us);
            this.value = us;
            break;
          case 0x03: //int
            int i;
            bits.ReadInt32(out i);
            this.value = i;
            break;
          case 0x04: //uint
            uint ui;
            bits.ReadUInt32(out ui);
            this.value = ui;
            break;
          case 0x05: //float
            this.value = bits.ReadFloat();
            break;
          case 0x06: //double
            this.value = bits.ReadDouble();
            break;
          case 0x09: //long
            long sl;
            bits.ReadInt64(out sl);
            this.value = sl;
            break;
          case 0x0a: //ulong
            ulong ul;
            bits.ReadUInt64(out ul);
            this.value = ul;
            break;
          case 0x10: //string
            string str;
            bits.ReadBString(out str);
            this.value = str;
            break;
          case 0x19: //decimal
            this.value = bits.ReadDecimal();
            break;
          default:
            //TODO: error
            break;
        }
      }
      else
      {
        //TODO: error
      }
      bits.ReadCString(out name);
    }
  }
}

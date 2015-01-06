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

#if true

#define FEATURE_FULL_CONTRACTS

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Contracts;



class Mem
{
  unsafe public static bool valid(void* ptr, int len) { throw new NotImplementedException(); }
  unsafe public static bool overlaps(void* ptr1, int len1, void* ptr2, int len2) { throw new NotImplementedException(); }
  unsafe public static void* malloc(int len) { throw new NotImplementedException(); }
}


static class EmbeddedTypesValidity
{
  [StructLayout(LayoutKind.Explicit,
Size = 8)]
  struct point
  {
    [FieldOffset(0)]
    internal int x;
    [FieldOffset(8)]
    internal int y;
  };

  [StructLayout(LayoutKind.Explicit,
Size = 16)]
  struct rect
  {
    [FieldOffset(0)]
    internal point ll;
    [FieldOffset(8)]
    internal point ur;
  };

  static unsafe void C()
  {
    byte* b = stackalloc byte[1];
    rect* r = (rect*)b;
  }

  static unsafe void M(rect* r)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    r->ll.x = 12;
  }

  static unsafe bool inv_rect(rect* r)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    return r->ll.x <= r->ur.x && r->ll.y <= r->ur.y;
  }

  static unsafe void move_rect_1(rect* r, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    r->ll.x = unchecked(r->ll.x + dx);
    r->ll.y = unchecked(r->ll.y + dy);
    r->ur.x = unchecked(r->ur.x + dx);
    r->ur.y = unchecked(r->ur.y + dy);
  }

  static unsafe void move_point(point* p, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(p) >= (uint)sizeof(point));
    p->x = unchecked(p->x + dx);
    p->y = unchecked(p->y + dy);
  }

  static unsafe void move_rect_2(rect* r, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    Contract.Requires(Contract.WritableBytes(&(r->ll)) >= (uint)sizeof(point));
    Contract.Requires(Contract.WritableBytes(&(r->ur)) >= (uint)sizeof(point));
    move_point(&(r->ll), dx, dy);
    move_point(&(r->ur), dx, dy);

  }
}





class region : Mem
{
  public const region nothing = null;
  unsafe public region() { throw new NotImplementedException(); } //this is the empty region also called nothing
  unsafe public region(void* ptr, int len) { throw new NotImplementedException(); }
  unsafe public static bool isin(void* ptr, region region) { throw new NotImplementedException(); }
  unsafe public static region union(region a, region b) { throw new NotImplementedException(); }
  unsafe public static region intersection(region a, region b) { throw new NotImplementedException(); }
  unsafe public static region difference(region a, region b) { throw new NotImplementedException(); }
  unsafe public static bool subset(region a, region b) { throw new NotImplementedException(); }
  unsafe public static bool disuniont(region a, region b) { throw new NotImplementedException(); }

  unsafe public bool valid(region a) { throw new NotImplementedException(); }
  unsafe public static bool overlaps(region a, region b) { return !disuniont(a, b); }
}


class EmbeddedTypesRegions : region
{
  [StructLayout(LayoutKind.Explicit,
Size = 8)]
  struct point
  {
    [FieldOffset(0)]
    internal int x;
    [FieldOffset(4)]
    internal int y;
  };

  [StructLayout(LayoutKind.Explicit,
Size = 8)]
  unsafe struct rect
  {
    [FieldOffset(0)]
    internal point* ll;
    [FieldOffset(4)]
    internal point* ur;
  };

  static unsafe bool inv_rect(rect* r)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    Contract.Requires(Contract.WritableBytes(r->ll) >= (uint)sizeof(point));
    Contract.Requires(Contract.WritableBytes(r->ur) >= (uint)sizeof(point));
    return
        r->ll->x <= r->ur->x &&
        r->ll->y <= r->ur->y;
  }

  static unsafe region fp_rect(rect* r)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    return union(new region(r, sizeof(rect)), union(new region(r->ll, sizeof(point)), new region(r->ur, sizeof(point))));
  }

  static unsafe void move_rect_1(rect* r, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    Contract.Requires(Contract.WritableBytes(r->ll) >= (uint)sizeof(point));
    Contract.Requires(Contract.WritableBytes(r->ur) >= (uint)sizeof(point));
    r->ll->x = unchecked(r->ll->x + dx);
    r->ll->y = unchecked(r->ll->y + dy);
    r->ur->x = unchecked(r->ur->x + dx);
    r->ur->y = unchecked(r->ur->y + dy);
  }

  static unsafe void move_point(point* p, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(p) >= (uint)sizeof(point));
    p->x = unchecked(p->x + dx);
    p->y = unchecked(p->y + dy);
  }

  static unsafe void move_rect_2(rect* r, int dx, int dy)
  {
    Contract.Requires(Contract.WritableBytes(r) >= (uint)sizeof(rect));
    Contract.Requires(Contract.WritableBytes(r->ll) >= (uint)sizeof(point));
    Contract.Requires(Contract.WritableBytes(r->ur) >= (uint)sizeof(point));
    move_point(r->ll, dx, dy);
    move_point(r->ur, dx, dy);

  }

}





#endif 
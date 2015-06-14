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

// File MS.Win32.Penimc.IPimcTablet.cs
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


namespace MS.Win32.Penimc
{
  internal partial interface IPimcTablet
  {
    #region Methods and constructors
    void GetCursorButtonCount(int iCursor, out int cButtons);

    void GetCursorButtonInfo(int iCursor, int iButton, out string sName, out Guid guid);

    void GetCursorCount(out int cCursors);

    void GetCursorInfo(int iCursor, out string sName, out int id, out bool fInverted);

    void GetDeviceType(out int devType);

    void GetHardwareCaps(out int caps);

    void GetKey(out int key);

    void GetName(out string name);

    void GetPacketButtonInfo(int iButton, out Guid guid);

    void GetPacketDescriptionInfo(out int cProps, out int cButtons);

    void GetPacketPropertyInfo(int iProp, out Guid guid, out int iMin, out int iMax, out int iUnits, out float flResolution);

    void GetPlugAndPlayId(out string plugAndPlayId);

    void GetPropertyInfo(Guid guid, out int min, out int max, out int units, out float resolution);

    void GetTabletAndDisplaySize(out int tabletWidth, out int tabletHeight, out int displayWidth, out int displayHeight);

    void IsPropertySupported(Guid guid, out bool fSupported);

    void RefreshCursorInfo();
    #endregion
  }
}

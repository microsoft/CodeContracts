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
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
  // Summary:
  //     Specifies shortcut keys that can be used by menu items.
  [ComVisible(true)]
  public enum Shortcut
  {
    // Summary:
    //     No shortcut key is associated with the menu item.
    None = 0,
    //
    // Summary:
    //     The shortcut key INSERT.
    Ins = 45,
    //
    // Summary:
    //     The shortcut key DELETE.
    Del = 46,
    //
    // Summary:
    //     The shortcut key F1.
    F1 = 112,
    //
    // Summary:
    //     The shortcut key F2.
    F2 = 113,
    //
    // Summary:
    //     The shortcut key F3.
    F3 = 114,
    //
    // Summary:
    //     The shortcut key F4.
    F4 = 115,
    //
    // Summary:
    //     The shortcut key F5.
    F5 = 116,
    //
    // Summary:
    //     The shortcut key F6.
    F6 = 117,
    //
    // Summary:
    //     The shortcut key F7.
    F7 = 118,
    //
    // Summary:
    //     The shortcut key F8.
    F8 = 119,
    //
    // Summary:
    //     The shortcut key F9.
    F9 = 120,
    //
    // Summary:
    //     The shortcut key F10.
    F10 = 121,
    //
    // Summary:
    //     The shortcut key F11.
    F11 = 122,
    //
    // Summary:
    //     The shortcut key F12.
    F12 = 123,
    //
    // Summary:
    //     The shortcut keys SHIFT+INSERT.
    ShiftIns = 65581,
    //
    // Summary:
    //     The shortcut keys SHIFT+DELETE.
    ShiftDel = 65582,
    //
    // Summary:
    //     The shortcut keys SHIFT+F1.
    ShiftF1 = 65648,
    //
    // Summary:
    //     The shortcut keys SHIFT+F2.
    ShiftF2 = 65649,
    //
    // Summary:
    //     The shortcut keys SHIFT+F3.
    ShiftF3 = 65650,
    //
    // Summary:
    //     The shortcut keys SHIFT+F4.
    ShiftF4 = 65651,
    //
    // Summary:
    //     The shortcut keys SHIFT+F5.
    ShiftF5 = 65652,
    //
    // Summary:
    //     The shortcut keys SHIFT+F6.
    ShiftF6 = 65653,
    //
    // Summary:
    //     The shortcut keys SHIFT+F7.
    ShiftF7 = 65654,
    //
    // Summary:
    //     The shortcut keys SHIFT+F8.
    ShiftF8 = 65655,
    //
    // Summary:
    //     The shortcut keys SHIFT+F9.
    ShiftF9 = 65656,
    //
    // Summary:
    //     The shortcut keys SHIFT+F10.
    ShiftF10 = 65657,
    //
    // Summary:
    //     The shortcut keys SHIFT+F11.
    ShiftF11 = 65658,
    //
    // Summary:
    //     The shortcut keys SHIFT+F12.
    ShiftF12 = 65659,
    //
    // Summary:
    //     The shortcut keys CTRL+INSERT.
    CtrlIns = 131117,
    //
    // Summary:
    //     The shortcut keys CTRL+DELETE.
    CtrlDel = 131118,
    //
    // Summary:
    //     The shortcut keys CTRL+0.
    Ctrl0 = 131120,
    //
    // Summary:
    //     The shortcut keys CTRL+1.
    Ctrl1 = 131121,
    //
    // Summary:
    //     The shortcut keys CTRL+2.
    Ctrl2 = 131122,
    //
    // Summary:
    //     The shortcut keys CTRL+3.
    Ctrl3 = 131123,
    //
    // Summary:
    //     The shortcut keys CTRL+4.
    Ctrl4 = 131124,
    //
    // Summary:
    //     The shortcut keys CTRL+5.
    Ctrl5 = 131125,
    //
    // Summary:
    //     The shortcut keys CTRL+6.
    Ctrl6 = 131126,
    //
    // Summary:
    //     The shortcut keys CTRL+7.
    Ctrl7 = 131127,
    //
    // Summary:
    //     The shortcut keys CTRL+8.
    Ctrl8 = 131128,
    //
    // Summary:
    //     The shortcut keys CTRL+9.
    Ctrl9 = 131129,
    //
    // Summary:
    //     The shortcut keys CTRL+A.
    CtrlA = 131137,
    //
    // Summary:
    //     The shortcut keys CTRL+B.
    CtrlB = 131138,
    //
    // Summary:
    //     The shortcut keys CTRL+C.
    CtrlC = 131139,
    //
    // Summary:
    //     The shortcut keys CTRL+D.
    CtrlD = 131140,
    //
    // Summary:
    //     The shortcut keys CTRL+E.
    CtrlE = 131141,
    //
    // Summary:
    //     The shortcut keys CTRL+F.
    CtrlF = 131142,
    //
    // Summary:
    //     The shortcut keys CTRL+G.
    CtrlG = 131143,
    //
    // Summary:
    //     The shortcut keys CTRL+H.
    CtrlH = 131144,
    //
    // Summary:
    //     The shortcut keys CTRL+I.
    CtrlI = 131145,
    //
    // Summary:
    //     The shortcut keys CTRL+J.
    CtrlJ = 131146,
    //
    // Summary:
    //     The shortcut keys CTRL+K.
    CtrlK = 131147,
    //
    // Summary:
    //     The shortcut keys CTRL+L.
    CtrlL = 131148,
    //
    // Summary:
    //     The shortcut keys CTRL+M.
    CtrlM = 131149,
    //
    // Summary:
    //     The shortcut keys CTRL+N.
    CtrlN = 131150,
    //
    // Summary:
    //     The shortcut keys CTRL+O.
    CtrlO = 131151,
    //
    // Summary:
    //     The shortcut keys CTRL+P.
    CtrlP = 131152,
    //
    // Summary:
    //     The shortcut keys CTRL+Q.
    CtrlQ = 131153,
    //
    // Summary:
    //     The shortcut keys CTRL+R.
    CtrlR = 131154,
    //
    // Summary:
    //     The shortcut keys CTRL+S.
    CtrlS = 131155,
    //
    // Summary:
    //     The shortcut keys CTRL+T.
    CtrlT = 131156,
    //
    // Summary:
    //     The shortcut keys CTRL+U.
    CtrlU = 131157,
    //
    // Summary:
    //     The shortcut keys CTRL+V.
    CtrlV = 131158,
    //
    // Summary:
    //     The shortcut keys CTRL+W.
    CtrlW = 131159,
    //
    // Summary:
    //     The shortcut keys CTRL+X.
    CtrlX = 131160,
    //
    // Summary:
    //     The shortcut keys CTRL+Y.
    CtrlY = 131161,
    //
    // Summary:
    //     The shortcut keys CTRL+Z.
    CtrlZ = 131162,
    //
    // Summary:
    //     The shortcut keys CTRL+F1.
    CtrlF1 = 131184,
    //
    // Summary:
    //     The shortcut keys CTRL+F2.
    CtrlF2 = 131185,
    //
    // Summary:
    //     The shortcut keys CTRL+F3.
    CtrlF3 = 131186,
    //
    // Summary:
    //     The shortcut keys CTRL+F4.
    CtrlF4 = 131187,
    //
    // Summary:
    //     The shortcut keys CTRL+F5.
    CtrlF5 = 131188,
    //
    // Summary:
    //     The shortcut keys CTRL+F6.
    CtrlF6 = 131189,
    //
    // Summary:
    //     The shortcut keys CTRL+F7.
    CtrlF7 = 131190,
    //
    // Summary:
    //     The shortcut keys CTRL+F8.
    CtrlF8 = 131191,
    //
    // Summary:
    //     The shortcut keys CTRL+F9.
    CtrlF9 = 131192,
    //
    // Summary:
    //     The shortcut keys CTRL+F10.
    CtrlF10 = 131193,
    //
    // Summary:
    //     The shortcut keys CTRL+F11.
    CtrlF11 = 131194,
    //
    // Summary:
    //     The shortcut keys CTRL+F12.
    CtrlF12 = 131195,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+0.
    CtrlShift0 = 196656,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+1.
    CtrlShift1 = 196657,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+2.
    CtrlShift2 = 196658,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+3.
    CtrlShift3 = 196659,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+4.
    CtrlShift4 = 196660,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+5.
    CtrlShift5 = 196661,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+6.
    CtrlShift6 = 196662,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+7.
    CtrlShift7 = 196663,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+8.
    CtrlShift8 = 196664,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+9.
    CtrlShift9 = 196665,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+A.
    CtrlShiftA = 196673,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+B.
    CtrlShiftB = 196674,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+C.
    CtrlShiftC = 196675,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+D.
    CtrlShiftD = 196676,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+E.
    CtrlShiftE = 196677,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F.
    CtrlShiftF = 196678,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+G.
    CtrlShiftG = 196679,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+H.
    CtrlShiftH = 196680,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+I.
    CtrlShiftI = 196681,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+J.
    CtrlShiftJ = 196682,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+K.
    CtrlShiftK = 196683,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+L.
    CtrlShiftL = 196684,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+M.
    CtrlShiftM = 196685,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+N.
    CtrlShiftN = 196686,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+O.
    CtrlShiftO = 196687,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+P.
    CtrlShiftP = 196688,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+Q.
    CtrlShiftQ = 196689,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+R.
    CtrlShiftR = 196690,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+S.
    CtrlShiftS = 196691,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+T.
    CtrlShiftT = 196692,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+U.
    CtrlShiftU = 196693,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+V.
    CtrlShiftV = 196694,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+W.
    CtrlShiftW = 196695,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+X.
    CtrlShiftX = 196696,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+Y.
    CtrlShiftY = 196697,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+Z.
    CtrlShiftZ = 196698,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F1.
    CtrlShiftF1 = 196720,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F2.
    CtrlShiftF2 = 196721,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F3.
    CtrlShiftF3 = 196722,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F4.
    CtrlShiftF4 = 196723,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F5.
    CtrlShiftF5 = 196724,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F6.
    CtrlShiftF6 = 196725,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F7.
    CtrlShiftF7 = 196726,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F8.
    CtrlShiftF8 = 196727,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F9.
    CtrlShiftF9 = 196728,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F10.
    CtrlShiftF10 = 196729,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F11.
    CtrlShiftF11 = 196730,
    //
    // Summary:
    //     The shortcut keys CTRL+SHIFT+F12.
    CtrlShiftF12 = 196731,
    //
    // Summary:
    //     The shortcut keys ALT+BACKSPACE.
    AltBksp = 262152,
    //
    // Summary:
    //     The shortcut keys ALT+LEFTARROW.
    AltLeftArrow = 262181,
    //
    // Summary:
    //     The shortcut keys ALT+UPARROW.
    AltUpArrow = 262182,
    //
    // Summary:
    //     The shortcut keys ALT+RIGHTARROW.
    AltRightArrow = 262183,
    //
    // Summary:
    //     The shortcut keys ALT+DOWNARROW.
    AltDownArrow = 262184,
    //
    // Summary:
    //     The shortcut keys ALT+0.
    Alt0 = 262192,
    //
    // Summary:
    //     The shortcut keys ALT+1.
    Alt1 = 262193,
    //
    // Summary:
    //     The shortcut keys ALT+2.
    Alt2 = 262194,
    //
    // Summary:
    //     The shortcut keys ALT+3.
    Alt3 = 262195,
    //
    // Summary:
    //     The shortcut keys ALT+4.
    Alt4 = 262196,
    //
    // Summary:
    //     The shortcut keys ALT+5.
    Alt5 = 262197,
    //
    // Summary:
    //     The shortcut keys ALT+6.
    Alt6 = 262198,
    //
    // Summary:
    //     The shortcut keys ALT+7.
    Alt7 = 262199,
    //
    // Summary:
    //     The shortcut keys ALT+8.
    Alt8 = 262200,
    //
    // Summary:
    //     The shortcut keys ALT+9.
    Alt9 = 262201,
    //
    // Summary:
    //     The shortcut keys ALT+F1.
    AltF1 = 262256,
    //
    // Summary:
    //     The shortcut keys ALT+F2.
    AltF2 = 262257,
    //
    // Summary:
    //     The shortcut keys ALT+F3.
    AltF3 = 262258,
    //
    // Summary:
    //     The shortcut keys ALT+F4.
    AltF4 = 262259,
    //
    // Summary:
    //     The shortcut keys ALT+F5.
    AltF5 = 262260,
    //
    // Summary:
    //     The shortcut keys ALT+F6.
    AltF6 = 262261,
    //
    // Summary:
    //     The shortcut keys ALT+F7.
    AltF7 = 262262,
    //
    // Summary:
    //     The shortcut keys ALT+F8.
    AltF8 = 262263,
    //
    // Summary:
    //     The shortcut keys ALT+F9.
    AltF9 = 262264,
    //
    // Summary:
    //     The shortcut keys ALT+F10.
    AltF10 = 262265,
    //
    // Summary:
    //     The shortcut keys ALT+F11.
    AltF11 = 262266,
    //
    // Summary:
    //     The shortcut keys ALT+F12.
    AltF12 = 262267,
  }
}

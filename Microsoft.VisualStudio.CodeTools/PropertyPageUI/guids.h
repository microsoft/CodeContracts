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

#ifndef guids_h
#define guids_h

#define GuidNull                { 0, 0, 0, { 0, 0, 0, 0, 0, 0, 0, 0 } }
#define GuidTaskManagerPackage  { 0xDA85543E, 0x97EC, 0x4478, { 0x90, 0xEC, 0x45, 0xCB, 0xCB, 0x4F, 0xA5, 0xC1 } }
#define GuidTaskManagerCmdSet   { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x98, 0x2d } }
#define GuidTaskManagerCmdGroup { 0xfb37bbc8, 0x51cd, 0x4c6a, { 0xa1, 0x95, 0x60, 0x7b, 0x69, 0x8a, 0xd0, 0x2c } }

#define GuidTaskManagerGroupRoot   { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x98, 0x00 } }

#define GuidTaskManagerGroupSub0  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x00 } }
#define GuidTaskManagerGroupSub1  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x01 } }
#define GuidTaskManagerGroupSub2  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x02 } }
#define GuidTaskManagerGroupSub3  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x03 } }
#define GuidTaskManagerGroupSub4  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x04 } }
#define GuidTaskManagerGroupSub5  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0xaa, 0x00, 0x05 } }

#define GuidTaskManagerGroupSub00  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0x00, 0x00, 0x00 } }
#define GuidTaskManagerGroupSub01  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0x00, 0x00, 0x01 } }

#define GuidTaskManagerGroupSub10  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0x00, 0x01, 0x00 } }
#define GuidTaskManagerGroupSub11  { 0x2cda027e, 0x722c, 0x4148, { 0xb9, 0x53, 0x6c, 0xce, 0x16, 0x00, 0x01, 0x01 } }

#if !defined( _CTC_GUIDS_ ) && defined( __cplusplus )

// {DA85543E-97EC-4478-90EC-45CBCB4FA5C1}
static const GUID guidTaskManagerPackage  = GuidTaskManagerPackage;

// {2CDA027E-722C-4148-B953-6CCE16AA982D}
static const GUID guidTaskManagerCmdSet = GuidTaskManagerCmdSet;

// {FB37BBC8-51CD-4c6a-A195-607B698AD02C}
static const GUID guidTaskManagerCmdGroup = GuidTaskManagerCmdGroup;

// {2CDA027E-722C-4148-B953-6CCE16AA0000}
static const GUID guidTaskManagerGroupRoot = GuidTaskManagerGroupRoot;

// {2CDA027E-722C-4148-B953-6CCE16AA0100}
static const GUID guidTaskManagerGroupSub0 = GuidTaskManagerGroupSub0;
// {2CDA027E-722C-4148-B953-6CCE16AA0101}
static const GUID guidTaskManagerGroupSub1 = GuidTaskManagerGroupSub1;
// {2CDA027E-722C-4148-B953-6CCE16AA0102}
static const GUID guidTaskManagerGroupSub2 = GuidTaskManagerGroupSub2;
// {2CDA027E-722C-4148-B953-6CCE16AA0103}
static const GUID guidTaskManagerGroupSub3 = GuidTaskManagerGroupSub3;
// {2CDA027E-722C-4148-B953-6CCE16AA0104}
static const GUID guidTaskManagerGroupSub4 = GuidTaskManagerGroupSub4;
// {2CDA027E-722C-4148-B953-6CCE16AA0105}
static const GUID guidTaskManagerGroupSub5 = GuidTaskManagerGroupSub5;

// {2CDA027E-722C-4148-B953-6CCE16AA0200}
static const GUID guidTaskManagerGroupSub00 = GuidTaskManagerGroupSub00;
// {2CDA027E-722C-4148-B953-6CCE16AA0201}
static const GUID guidTaskManagerGroupSub01 = GuidTaskManagerGroupSub01;
// {2CDA027E-722C-4148-B953-6CCE16AA0202}
static const GUID guidTaskManagerGroupSub10 = GuidTaskManagerGroupSub10;
// {2CDA027E-722C-4148-B953-6CCE16AA0203}
static const GUID guidTaskManagerGroupSub11 = GuidTaskManagerGroupSub11;


#endif  
#endif  

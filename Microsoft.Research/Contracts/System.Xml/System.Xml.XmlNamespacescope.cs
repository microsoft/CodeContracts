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

namespace System.Xml
{
  // Summary:
  //     Defines the namespace scope.
  public enum XmlNamespaceScope
  {
    // Summary:
    //     All namespaces defined in the scope of the current node. This includes the
    //     xmlns:xml namespace which is always declared implicitly. The order of the
    //     namespaces returned is not defined.
    All = 0,
    //
    // Summary:
    //     All namespaces defined in the scope of the current node, excluding the xmlns:xml
    //     namespace, which is always declared implicitly. The order of the namespaces
    //     returned is not defined.
    ExcludeXml = 1,
    //
    // Summary:
    //     All namespaces that are defined locally at the current node.
    Local = 2,
  }
}


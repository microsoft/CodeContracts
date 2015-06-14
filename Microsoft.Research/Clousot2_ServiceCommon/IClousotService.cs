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

using System.ServiceModel;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.Cloudot.Common
{
  // all [ServiceContract] classes must be either [Serializable] or [DataContract]
  // but it seems it is not enough, e.g., Encoding makes the service hang

  // We should pass to the service an object to act as a callback (where the service will respond)
  // In our case, the callback object is very simple, just a line output
  [ServiceContract(CallbackContract = typeof(IVerySimpleLineWriter))] 
  public interface IClousotService
  {
    #region Cloudot invocation using analysis packages
    [OperationContract]
    // May return null if it cannot get a directory
    string GetOutputDirectory();

    [OperationContract]
    // Name is just used to create a name for the string
    int AnalyzeUsingAnalysisPackage(string name, string packageLocation, string encodingWebName);

    #endregion

    #region Cloudot invocation using shared directories

    // Main roughly corresponds to ClousotMain
    // If we want to make it more fine grained, we should do it here
    [OperationContract]
    int Main(string[] args, string encodingWebName);
    #endregion
  }
}

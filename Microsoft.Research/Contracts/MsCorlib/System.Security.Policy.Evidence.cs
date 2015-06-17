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

#if !SILVERLIGHT_4_0_WP

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy {
  // Summary:
  //     Defines the set of information that constitutes input to security policy
  //     decisions. This class cannot be inherited.
  // [Serializable]
  // [ComVisible(true)]
  public sealed class Evidence 
  {
    // Summary:
    //     Initializes a new empty instance of the System.Security.Policy.Evidence class.
    //public Evidence() { }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Policy.Evidence class from
    //     a shallow copy of an existing one.
    //
    // Parameters:
    //   evidence:
    //     The System.Security.Policy.Evidence instance from which to create the new
    //     instance. This instance is not deep copied.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The evidence parameter is not a valid instance of System.Security.Policy.Evidence.
    //public Evidence(Evidence evidence) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Policy.Evidence class from
    //     multiple sets of host and assembly evidence.
    //
    // Parameters:
    //   hostEvidence:
    //     The host evidence from which to create the new instance.
    //
    //   assemblyEvidence:
    //     The assembly evidence from which to create the new instance.
    //public Evidence(object[] hostEvidence, object[] assemblyEvidence) { }

    // Summary:
    //     Gets the number of evidence objects in the evidence set.
    //
    // Returns:
    //     The number of evidence objects in the evidence set.


    // Summary:
    //     Adds the specified assembly evidence to the evidence set.
    //
    // Parameters:
    //   id:
    //     Any evidence object.
    //public void AddAssembly(object id);
    //
    // Summary:
    //     Adds the specified evidence supplied by the host to the evidence set.
    //
    // Parameters:
    //   id:
    //     Any evidence object.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     System.Security.Policy.Evidence.Locked is true and the code that calls this
    //     method does not have System.Security.Permissions.SecurityPermissionFlag.ControlEvidence.
    //public void AddHost(object id);


    //
    // Summary:
    //     Determines whether the specified System.Security.Policy.Evidence object is
    //     equal to the current System.Security.Policy.Evidence.
    //
    // Parameters:
    //   obj:
    //     The System.Security.Policy.Evidence object to compare with the current System.Security.Policy.Evidence.
    //
    // Returns:
    //     true if the specified System.Security.Policy.Evidence object is equal to
    //     the current System.Security.Policy.Evidence; otherwise, false.

    //public IEnumerator GetAssemblyEnumerator();

    //
    // Summary:
    //     Enumerates evidence supplied by the host.
    //
    // Returns:
    //     An enumerator for evidence added by the System.Security.Policy.Evidence.AddHost(System.Object)
    //     method.
    //public IEnumerator GetHostEnumerator();
    //
    // Summary:
    //     Merges the specified evidence set into the current evidence set.
    //
    // Parameters:
    //   evidence:
    //     The evidence set to be merged into the current evidence set.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The evidence parameter is not a valid instance of System.Security.Policy.Evidence.
    //
    //   System.Security.SecurityException:
    //     System.Security.Policy.Evidence.Locked is true, the code that calls this
    //     method does not have System.Security.Permissions.SecurityPermissionFlag.ControlEvidence,
    //     and the evidence parameter has a host list that is not empty.
    //public void Merge(Evidence evidence);
    //
    // Summary:
    //     Removes the evidence for a given type from the host and assembly enumerations.
    //
    // Parameters:
    //   t:
    //     The System.Type of the evidence to be removed.
    // [ComVisible(false)]
    //public void RemoveType(Type t);
  }
}

#endif
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

#if !SILVERLIGHT

using System.Diagnostics.Contracts;
using System;
using System.Collections;
using System.Reflection;

namespace System.Diagnostics
{
  // Summary:
  //     Provides a strongly typed collection of System.Diagnostics.ProcessThread
  //     objects.
  public class ProcessThreadCollection
  {
    protected ProcessThreadCollection() { }

    // Summary:
    //     Gets an index for iterating over the set of process threads.
    //
    // Parameters:
    //   index:
    //     The zero-based index value of the thread in the collection.
    //
    // Returns:
    //     A System.Diagnostics.ProcessThread that indexes the threads in the collection.
    public ProcessThread this[int index]
    {
      get
      {
        Contract.Ensures(Contract.Result<ProcessThread>() != null);
        return default(ProcessThread);

      }
    }

    // Summary:
    //     Appends a process thread to the collection.
    //
    // Parameters:
    //   thread:
    //     The thread to add to the collection.
    //
    // Returns:
    //     The zero-based index of the thread in the collection.
    public int Add(ProcessThread thread) {
      return default(int);
    }
    //
    // Summary:
    //     Determines whether the specified process thread exists in the collection.
    //
    // Parameters:
    //   thread:
    //     A System.Diagnostics.ProcessThread instance that indicates the thread to
    //     find in this collection.
    //
    // Returns:
    //     true if the thread exists in the collection; otherwise, false.
    public bool Contains(ProcessThread thread) {
      return default(bool);
    }
    //
    // Summary:
    //     Copies an array of System.Diagnostics.ProcessThread instances to the collection,
    //     at the specified index.
    //
    // Parameters:
    //   array:
    //     An array of System.Diagnostics.ProcessThread instances to add to the collection.
    //
    //   index:
    //     The location at which to add the new instances.
    public void CopyTo(ProcessThread[] array, int index) {
    }
    //
    // Summary:
    //     Provides the location of a specified thread within the collection.
    //
    // Parameters:
    //   thread:
    //     The System.Diagnostics.ProcessThread whose index is retrieved.
    //
    // Returns:
    //     The zero-based index that defines the location of the thread within the System.Diagnostics.ProcessThreadCollection.
    public int IndexOf(ProcessThread thread) {
      return default(int);
    }
    //
    // Summary:
    //     Inserts a process thread at the specified location in the collection.
    //
    // Parameters:
    //   index:
    //     The zero-based index indicating the location at which to insert the thread.
    //
    //   thread:
    //     The thread to insert into the collection.
    public void Insert(int index, ProcessThread thread) {
    //
    // Summary:
    //     Deletes a process thread from the collection.
    //
    // Parameters:
    //   thread:
    //     The thread to remove from the collection.
    }
    public void Remove(ProcessThread thread) {
    }
  }
}

#endif
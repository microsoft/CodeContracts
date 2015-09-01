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


// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.BindingsCollection
// Assembly: System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: E99EB19F-1780-41E7-95D6-F9A9962C7B8D
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\System.Windows.Forms\2.0.0.0__b77a5c561934e089\System.Windows.Forms.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
    // <summary>
    // Represents a collection of <see cref="T:System.Windows.Forms.Binding"/> objects for a control.
    // </summary>
    // <filterpriority>2</filterpriority>
    [DefaultEvent("CollectionChanged")]
    public class BindingsCollection : BaseCollection
    {
        
        // <summary>
        // Gets the total number of bindings in the collection.
        // </summary>
        // 
        // <returns>
        // The total number of bindings in the collection.
        // </returns>
        // public override int Count {get;}
       

        // <summary>
        // Gets the bindings in the collection as an object.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Collections.ArrayList"/> containing all of the collection members.
        // </returns>
        protected ArrayList List 
        {
            get
            {
                Contract.Ensures(Contract.Result<ArrayList>() != null);
                return default(ArrayList);
            }
        }

        // <summary>
        // Gets the <see cref="T:System.Windows.Forms.Binding"/> at the specified index.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.Binding"/> at the specified index.
        // </returns>
        // <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding"/> to find. </param><exception cref="T:System.IndexOutOfRangeException">The collection doesn't contain an item at the specified index. </exception><filterpriority>1</filterpriority>
        public Binding this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index < this.Count);
                return default(Binding);
            }
        }

        // <summary>
        // Occurs when the collection is about to change.
        // </summary>
        // [SRDescription("collectionChangingEventDescr")]
        // public event CollectionChangeEventHandler CollectionChanging
        
        // <summary>
        // Occurs when the collection has changed.
        // </summary>
        // [SRDescription("collectionChangedEventDescr")]
        // public event CollectionChangeEventHandler CollectionChanged
        
        // <summary>
        // Adds the specified binding to the collection.
        // </summary>
        // <param name="binding">The <see cref="T:System.Windows.Forms.Binding"/> to add to the collection. </param>
        // protected internal void Add(Binding binding)
        
        /// <summary>
        /// Adds a <see cref="T:System.Windows.Forms.Binding"/> to the collection.
        /// </summary>
        /// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding"/> to add to the collection.</param><exception cref="T:System.ArgumentNullException">The <paramref name="dataBinding"/> argument was null. </exception>
        protected virtual void AddCore(Binding dataBinding)
        {
            Contract.Requires(dataBinding != null);
        }

        // <summary>
        // Clears the collection of binding objects.
        // </summary>
        // protected internal void Clear()
        
        // <summary>
        // Clears the collection of any members.
        // </summary>
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanging"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"/> that contains event data.</param>
        // protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanged"/> event.
        // </summary>
        // <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"/> that contains the event data. </param>
        // protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
        
        // <summary>
        // Deletes the specified binding from the collection.
        // </summary>
        // <param name="binding">The Binding to remove from the collection. </param>
        // protected internal void Remove(Binding binding)
        
        // <summary>
        // Deletes the binding from the collection at the specified index.
        // </summary>
        // <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding"/> to remove. </param>
        // protected internal void RemoveAt(int index)
        
        // <summary>
        // Removes the specified <see cref="T:System.Windows.Forms.Binding"/> from the collection.
        // </summary>
        // <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding"/> to remove. </param>
        // protected virtual void RemoveCore(Binding dataBinding)
        
        // <summary>
        // Gets a value that indicates whether the collection should be serialized.
        // </summary>
        // 
        // <returns>
        // true if the collection count is greater than zero; otherwise, false.
        // </returns>
        // protected internal bool ShouldSerializeMyAll()
    }
}

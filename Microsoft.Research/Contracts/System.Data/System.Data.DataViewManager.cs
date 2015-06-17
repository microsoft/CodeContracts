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
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
  // Summary:
  //     Contains a default System.Data.DataViewSettingCollection for each System.Data.DataTable
  //     in a System.Data.DataSet.
  // [Designer("Microsoft.VSDesigner.Data.VS.DataViewManagerDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public class DataViewManager // : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
  {
    // Summary:
    //     Initializes a new instance of the System.Data.DataViewManager class.
    //public DataViewManager();
    //
    // Summary:
    //     Initializes a new instance of the System.Data.DataViewManager class for the
    //     specified System.Data.DataSet.
    //
    // Parameters:
    //   dataSet:
    //     The name of the System.Data.DataSet to use.
    //public DataViewManager(DataSet dataSet);

    // Summary:
    //     Gets or sets the System.Data.DataSet to use with the System.Data.DataViewManager.
    //
    // Returns:
    //     The System.Data.DataSet to use.
    // [DefaultValue("")]
    // [ResDescription("DataViewManagerDataSetDescr")]
    //public DataSet DataSet { get; set; }
    //
    // Summary:
    //     Gets or sets a value that is used for code persistence.
    //
    // Returns:
    //     A value that is used for code persistence.
    //public string DataViewSettingCollectionString { get; set; }
    //
    // Summary:
    //     Gets the System.Data.DataViewSettingCollection for each System.Data.DataTable
    //     in the System.Data.DataSet.
    //
    // Returns:
    //     A System.Data.DataViewSettingCollection for each DataTable.
    // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // [ResDescription("DataViewManagerTableSettingsDescr")]
    //public DataViewSettingCollection DataViewSettings { get; }

    // Summary:
    //     Occurs after a row is added to or deleted from a System.Data.DataView.
    //public event ListChangedEventHandler ListChanged;

    // Summary:
    //     Creates a System.Data.DataView for the specified System.Data.DataTable.
    //
    // Parameters:
    //   table:
    //     The name of the System.Data.DataTable to use in the System.Data.DataView.
    //
    // Returns:
    //     A System.Data.DataView object.
    //public DataView CreateDataView(DataTable table);
    //
    // Summary:
    //     Raises the System.Data.DataViewManager.ListChanged event.
    //
    // Parameters:
    //   e:
    //     A System.ComponentModel.ListChangedEventArgs that contains the event data.
    // protected virtual void OnListChanged(ListChangedEventArgs e);
    //
    // Summary:
    //     Raises a System.Data.DataRelationCollection.CollectionChanged event when
    //     a System.Data.DataRelation is added to or removed from the System.Data.DataRelationCollection.
    //
    // Parameters:
    //   sender:
    //     The source of the event.
    //
    //   e:
    //     A System.ComponentModel.CollectionChangeEventArgs that contains the event
    //     data.
    // protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e);
    //
    // Summary:
    //     Raises a System.Data.DataTableCollection.CollectionChanged event when a System.Data.DataTable
    //     is added to or removed from the System.Data.DataTableCollection.
    //
    // Parameters:
    //   sender:
    //     The source of the event.
    //
    //   e:
    //     A System.ComponentModel.CollectionChangeEventArgs that contains the event
    //     data.
    // protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e);
  }
}

' CodeContracts
' 
' Copyright (c) Microsoft Corporation
' 
' All rights reserved. 
' 
' MIT License
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.Collections
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic

  '' Summary:
  ''     A Visual Basic Collection is an ordered set of items that can be referred
  ''     to as a unit.
  ''[Serializable]
  ''[DebuggerDisplay("Count = CountEnd")]
  Public NotInheritable Class Collection
    Implements IList

    '' Summary:
    ''     Creates and returns a new Visual Basic Collection Object.
    Public Sub New()
      Contract.Ensures(Me.Count = 0)
    End Sub

    Public Sub Clear()
      Contract.Ensures(Me.Count = 0)
    End Sub

    <Pure()> _
    Public Function Contains(ByVal Key As String) As Boolean
      Contract.Requires(Key IsNot Nothing)

      Return Nothing
    End Function

    <Pure()> _
    Public Function GetEnumerator() As IEnumerator
      Contract.Ensures(Contract.Result(Of IEnumerator)() IsNot Nothing)

      Return Nothing
    End Function

    Public Sub Remove(ByVal Index As Integer)
      Contract.Requires(Index >= 1)
      Contract.Requires(Index <= Me.Count)

    End Sub

    Public Sub Remove(ByVal Key As String)
      Contract.Requires(Key IsNot Nothing)
    End Sub



    '' Summary:
    ''     Returns an Integer containing the number of elements in a collection. Read-only.
    ''
    '' Returns:
    ''     Returns an Integer containing the number of elements in a collection. Read-only.
    Public ReadOnly Property Count() As Integer
      Get
        Contract.Ensures(Contract.Result(Of Integer)() = DirectCast(Me, ICollection).Count)
        Return Nothing
      End Get
    End Property

    '' Summary:
    ''     Returns a specific element of a Collection object either by position or by
    ''     key. Read-only.
    ''
    '' Parameters:
    ''   Index:
    ''     (A) A numeric expression that specifies the position of an element of the
    ''     collection. Index must be a number from 1 through the value of the collection's
    ''     Count Property (Collection Object). Or (B) An Object expression that specifies
    ''     the position or key string of an element of the collection.
    ''
    '' Returns:
    ''     Returns a specific element of a Collection object either by position or by
    ''     key. Read-only.
    Default Public ReadOnly Property Item(ByVal Index As Integer) As Object
      Get
        Contract.Requires(Index >= 1)
        Contract.Requires(Index <= Count)

        Return Nothing
      End Get
    End Property

    Default Public ReadOnly Property Item(ByVal Key As String) As Object
      Get
        Contract.Requires(Key IsNot Nothing)

        Return Nothing
      End Get
    End Property






    Private Sub ICollectionCopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

    End Sub

    Private ReadOnly Property ICollectionCount As Integer Implements System.Collections.ICollection.Count
      Get
        Return Nothing
      End Get
    End Property

    Private ReadOnly Property ICollectionIsSynchronized As Boolean Implements System.Collections.ICollection.IsSynchronized
      Get
        Return Nothing

      End Get
    End Property

    Private ReadOnly Property ICollectionSyncRoot As Object Implements System.Collections.ICollection.SyncRoot
      Get
        Return Nothing

      End Get
    End Property

    Private Function ICollectionGetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
      Return Nothing

    End Function

    Private Function IListAdd(ByVal value As Object) As Integer Implements System.Collections.IList.Add
      Return Nothing
    End Function

    Private Sub IListClear() Implements System.Collections.IList.Clear

    End Sub

    Private Function IListContains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
      Return Nothing

    End Function

    Private Function IListIndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
      Return Nothing

    End Function

    Private Sub IListInsert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert

    End Sub

    Private ReadOnly Property IListIsFixedSize As Boolean Implements System.Collections.IList.IsFixedSize
      Get
        Return Nothing

      End Get
    End Property

    Private ReadOnly Property IListIsReadOnly As Boolean Implements System.Collections.IList.IsReadOnly
      Get
        Return Nothing

      End Get
    End Property

    Private Property IListItem(ByVal index As Integer) As Object Implements System.Collections.IList.Item
      Get
        Return Nothing
      End Get
      Set(ByVal value As Object)

      End Set
    End Property

    Private Sub IListRemove(ByVal value As Object) Implements System.Collections.IList.Remove

    End Sub

    Private Sub IListRemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt

    End Sub
  End Class
End Namespace

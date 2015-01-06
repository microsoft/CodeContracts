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

// File System.Windows.Controls.InkCanvas.cs
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


namespace System.Windows.Controls
{
  public partial class InkCanvas : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    public bool CanPaste()
    {
      return default(bool);
    }

    public void CopySelection()
    {
    }

    public void CutSelection()
    {
    }

    public static double GetBottom(System.Windows.UIElement element)
    {
      return default(double);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Ink.ApplicationGesture> GetEnabledGestures()
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Ink.ApplicationGesture>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Ink.ApplicationGesture>);
    }

    public static double GetLeft(System.Windows.UIElement element)
    {
      return default(double);
    }

    public static double GetRight(System.Windows.UIElement element)
    {
      return default(double);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.UIElement> GetSelectedElements()
    {
      return default(System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.UIElement>);
    }

    public System.Windows.Ink.StrokeCollection GetSelectedStrokes()
    {
      Contract.Ensures(Contract.Result<System.Windows.Ink.StrokeCollection>() != null);

      return default(System.Windows.Ink.StrokeCollection);
    }

    public System.Windows.Rect GetSelectionBounds()
    {
      return default(System.Windows.Rect);
    }

    public static double GetTop(System.Windows.UIElement element)
    {
      return default(double);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected override System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParams)
    {
      return default(System.Windows.Media.HitTestResult);
    }

    public InkCanvasSelectionHitResult HitTestSelection(System.Windows.Point point)
    {
      return default(InkCanvasSelectionHitResult);
    }

    public InkCanvas()
    {
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
    {
      return default(System.Windows.Size);
    }

    protected virtual new void OnActiveEditingModeChanged(System.Windows.RoutedEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDefaultDrawingAttributesReplaced(System.Windows.Ink.DrawingAttributesReplacedEventArgs e)
    {
    }

    protected virtual new void OnEditingModeChanged(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnEditingModeInvertedChanged(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnGesture(InkCanvasGestureEventArgs e)
    {
    }

    protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnSelectionChanged(EventArgs e)
    {
    }

    protected virtual new void OnSelectionChanging(InkCanvasSelectionChangingEventArgs e)
    {
    }

    protected virtual new void OnSelectionMoved(EventArgs e)
    {
    }

    protected virtual new void OnSelectionMoving(InkCanvasSelectionEditingEventArgs e)
    {
    }

    protected virtual new void OnSelectionResized(EventArgs e)
    {
    }

    protected virtual new void OnSelectionResizing(InkCanvasSelectionEditingEventArgs e)
    {
    }

    protected virtual new void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
    {
    }

    protected virtual new void OnStrokeErased(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnStrokeErasing(InkCanvasStrokeErasingEventArgs e)
    {
    }

    protected virtual new void OnStrokesReplaced(InkCanvasStrokesReplacedEventArgs e)
    {
    }

    public void Paste()
    {
    }

    public void Paste(System.Windows.Point point)
    {
      Contract.Ensures(!double.IsInfinity(point.X));
      Contract.Ensures(!double.IsInfinity(point.Y));
    }

    public void Select(System.Windows.Ink.StrokeCollection selectedStrokes)
    {
    }

    public void Select(IEnumerable<System.Windows.UIElement> selectedElements)
    {
    }

    public void Select(System.Windows.Ink.StrokeCollection selectedStrokes, IEnumerable<System.Windows.UIElement> selectedElements)
    {
    }

    public static void SetBottom(System.Windows.UIElement element, double length)
    {
    }

    public void SetEnabledGestures(IEnumerable<System.Windows.Ink.ApplicationGesture> applicationGestures)
    {
    }

    public static void SetLeft(System.Windows.UIElement element, double length)
    {
    }

    public static void SetRight(System.Windows.UIElement element, double length)
    {
    }

    public static void SetTop(System.Windows.UIElement element, double length)
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string textData)
    {
    }
    #endregion

    #region Properties and indexers
    public InkCanvasEditingMode ActiveEditingMode
    {
      get
      {
        return default(InkCanvasEditingMode);
      }
    }

    public System.Windows.Media.Brush Background
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public UIElementCollection Children
    {
      get
      {
        return default(UIElementCollection);
      }
    }

    public System.Windows.Ink.DrawingAttributes DefaultDrawingAttributes
    {
      get
      {
        return default(System.Windows.Ink.DrawingAttributes);
      }
      set
      {
      }
    }

    public System.Windows.Input.StylusPointDescription DefaultStylusPointDescription
    {
      get
      {
        return default(System.Windows.Input.StylusPointDescription);
      }
      set
      {
      }
    }

    protected System.Windows.Input.StylusPlugIns.DynamicRenderer DynamicRenderer
    {
      get
      {
        return default(System.Windows.Input.StylusPlugIns.DynamicRenderer);
      }
      set
      {
      }
    }

    public InkCanvasEditingMode EditingMode
    {
      get
      {
        return default(InkCanvasEditingMode);
      }
      set
      {
      }
    }

    public InkCanvasEditingMode EditingModeInverted
    {
      get
      {
        return default(InkCanvasEditingMode);
      }
      set
      {
      }
    }

    public System.Windows.Ink.StylusShape EraserShape
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Ink.StylusShape>() != null);

        return default(System.Windows.Ink.StylusShape);
      }
      set
      {
      }
    }

    protected InkPresenter InkPresenter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.InkPresenter>() != null);

        return default(InkPresenter);
      }
    }

    public bool IsGestureRecognizerAvailable
    {
      get
      {
        return default(bool);
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public bool MoveEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IEnumerable<InkCanvasClipboardFormat> PreferredPasteFormats
    {
      get
      {
        return default(IEnumerable<InkCanvasClipboardFormat>);
      }
      set
      {
      }
    }

    public bool ResizeEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Ink.StrokeCollection Strokes
    {
      get
      {
        return default(System.Windows.Ink.StrokeCollection);
      }
      set
      {
      }
    }

    public bool UseCustomCursor
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler ActiveEditingModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Ink.DrawingAttributesReplacedEventHandler DefaultDrawingAttributesReplaced
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler EditingModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler EditingModeInvertedChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasGestureEventHandler Gesture
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SelectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasSelectionChangingEventHandler SelectionChanging
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SelectionMoved
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasSelectionEditingEventHandler SelectionMoving
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SelectionResized
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasSelectionEditingEventHandler SelectionResizing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasStrokeCollectedEventHandler StrokeCollected
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler StrokeErased
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasStrokeErasingEventHandler StrokeErasing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event InkCanvasStrokesReplacedEventHandler StrokesReplaced
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.RoutedEvent ActiveEditingModeChangedEvent;
    public readonly static System.Windows.DependencyProperty ActiveEditingModeProperty;
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty BottomProperty;
    public readonly static System.Windows.DependencyProperty DefaultDrawingAttributesProperty;
    public readonly static System.Windows.RoutedEvent EditingModeChangedEvent;
    public readonly static System.Windows.RoutedEvent EditingModeInvertedChangedEvent;
    public readonly static System.Windows.DependencyProperty EditingModeInvertedProperty;
    public readonly static System.Windows.DependencyProperty EditingModeProperty;
    public readonly static System.Windows.RoutedEvent GestureEvent;
    public readonly static System.Windows.DependencyProperty LeftProperty;
    public readonly static System.Windows.DependencyProperty RightProperty;
    public readonly static System.Windows.RoutedEvent StrokeCollectedEvent;
    public readonly static System.Windows.RoutedEvent StrokeErasedEvent;
    public readonly static System.Windows.DependencyProperty StrokesProperty;
    public readonly static System.Windows.DependencyProperty TopProperty;
    #endregion
  }
}

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

// File System.Windows.Controls.StickyNoteControl.cs
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
  sealed public partial class StickyNoteControl : Control, MS.Internal.Annotations.Component.IAnnotationComponent
  {
    #region Methods and constructors
    void MS.Internal.Annotations.Component.IAnnotationComponent.AddAttachedAnnotation(MS.Internal.Annotations.IAttachedAnnotation attachedAnnotation)
    {
    }

    System.Windows.Media.GeneralTransform MS.Internal.Annotations.Component.IAnnotationComponent.GetDesiredTransform(System.Windows.Media.GeneralTransform transform)
    {
      return default(System.Windows.Media.GeneralTransform);
    }

    void MS.Internal.Annotations.Component.IAnnotationComponent.RemoveAttachedAnnotation(MS.Internal.Annotations.IAttachedAnnotation attachedAnnotation)
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs args)
    {
    }

    protected override void OnIsKeyboardFocusWithinChanged(System.Windows.DependencyPropertyChangedEventArgs args)
    {
    }

    protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
    {
    }

    private StickyNoteControl()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Annotations.IAnchorInfo AnchorInfo
    {
      get
      {
        return default(System.Windows.Annotations.IAnchorInfo);
      }
    }

    public string Author
    {
      get
      {
        return default(string);
      }
    }

    public System.Windows.Media.FontFamily CaptionFontFamily
    {
      get
      {
        return default(System.Windows.Media.FontFamily);
      }
      set
      {
      }
    }

    public double CaptionFontSize
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.FontStretch CaptionFontStretch
    {
      get
      {
        return default(System.Windows.FontStretch);
      }
      set
      {
      }
    }

    public System.Windows.FontStyle CaptionFontStyle
    {
      get
      {
        return default(System.Windows.FontStyle);
      }
      set
      {
      }
    }

    public System.Windows.FontWeight CaptionFontWeight
    {
      get
      {
        return default(System.Windows.FontWeight);
      }
      set
      {
      }
    }

    public bool IsActive
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsExpanded
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsMouseOverAnchor
    {
      get
      {
        return default(bool);
      }
    }

    System.Windows.UIElement MS.Internal.Annotations.Component.IAnnotationComponent.AnnotatedElement
    {
      get
      {
        return default(System.Windows.UIElement);
      }
    }

    System.Collections.IList MS.Internal.Annotations.Component.IAnnotationComponent.AttachedAnnotations
    {
      get
      {
        return default(System.Collections.IList);
      }
    }

    bool MS.Internal.Annotations.Component.IAnnotationComponent.IsDirty
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    int MS.Internal.Annotations.Component.IAnnotationComponent.ZOrder
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public double PenWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public StickyNoteType StickyNoteType
    {
      get
      {
        return default(StickyNoteType);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty AuthorProperty;
    public readonly static System.Windows.DependencyProperty CaptionFontFamilyProperty;
    public readonly static System.Windows.DependencyProperty CaptionFontSizeProperty;
    public readonly static System.Windows.DependencyProperty CaptionFontStretchProperty;
    public readonly static System.Windows.DependencyProperty CaptionFontStyleProperty;
    public readonly static System.Windows.DependencyProperty CaptionFontWeightProperty;
    public readonly static System.Windows.Input.RoutedCommand DeleteNoteCommand;
    public readonly static System.Windows.Input.RoutedCommand InkCommand;
    public readonly static System.Xml.XmlQualifiedName InkSchemaName;
    public readonly static System.Windows.DependencyProperty IsActiveProperty;
    public readonly static System.Windows.DependencyProperty IsExpandedProperty;
    public readonly static System.Windows.DependencyProperty IsMouseOverAnchorProperty;
    public readonly static System.Windows.DependencyProperty PenWidthProperty;
    public readonly static System.Windows.DependencyProperty StickyNoteTypeProperty;
    public readonly static System.Xml.XmlQualifiedName TextSchemaName;
    #endregion
  }
}

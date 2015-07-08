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

using System.Windows;
using Microsoft.VisualStudio.Text;
using System.Diagnostics.Contracts;

namespace Adornments {

  #region IAdornment contract binding
  [ContractClassFor(typeof(IAdornment))]
  abstract class IAdornmentContract : IAdornment
  {
    #region IAdornment Members

    public ITrackingSpan Span
    {
      get
      {
        Contract.Ensures(Contract.Result<ITrackingSpan>() != null);
        throw new System.NotImplementedException();
      }
      set
      {
        Contract.Requires(value != null);

        throw new System.NotImplementedException();
      }
    }

    public FrameworkElement Visual
    {
      get {
        Contract.Ensures(Contract.Result<FrameworkElement>() != null);

        throw new System.NotImplementedException(); }
    }

    public bool IsInCollapsedRegion
    {
      get
      {
        throw new System.NotImplementedException();
      }
      set
      {
        throw new System.NotImplementedException();
      }
    }

    public int CollapsedRegionDepth
    {
      get
      {
        throw new System.NotImplementedException();
      }
      set
      {
        throw new System.NotImplementedException();
      }
    }

    public bool IsClosedByUser
    {
      get
      {
        throw new System.NotImplementedException();
      }
      set
      {
        throw new System.NotImplementedException();
      }
    }

    public bool IsCaretFocus
    {
      get
      {
        throw new System.NotImplementedException();
      }
      set
      {
        throw new System.NotImplementedException();
      }
    }

    public LineTransformBehavior LineTransformBehavior
    {
      get { throw new System.NotImplementedException(); }
    }

    #endregion
  }
  #endregion

  /// <summary>
  /// State necassary to manage an adornment.
  /// </summary>
  [ContractClass(typeof(IAdornmentContract))]
  public interface IAdornment
  {
    /// <summary>
    /// The tracking span that this adornment belongs to.
    /// </summary>
    ITrackingSpan Span { get; set; }
    /// <summary>
    /// The visual that this adornment renders.
    /// </summary>
    FrameworkElement Visual { get; }
    /// <summary>
    /// True if this adornment is in a collapsed region.
    /// </summary>
    bool IsInCollapsedRegion { get; set; }
    int CollapsedRegionDepth { get; set; }
    /// <summary>
    /// True if this adornment was closed by the user.
    /// </summary>
    bool IsClosedByUser { get; set; }
    ///// <summary>
    ///// True if this adornment should notify the text view this adornment belongs to that it should recalculate its line transforms.
    ///// </summary>
    //bool ShouldRefreshLineTransformer { get; set; }
    bool IsCaretFocus { get; set; }
    LineTransformBehavior LineTransformBehavior { get; }
  }
  public enum LineTransformBehavior {
    Above,
    Below,
    BelowWithOneLineAbove,
    None
  }
}
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

// File System.Web.UI.WebControls.WebParts.WebPartPersonalization.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web.UI.WebControls.WebParts
{
  public partial class WebPartPersonalization
  {
    #region Methods and constructors
    protected internal virtual new void ApplyPersonalizationState (WebPart webPart)
    {
    }

    protected internal virtual new void ApplyPersonalizationState ()
    {
    }

    protected virtual new void ChangeScope (PersonalizationScope scope)
    {
    }

    protected internal virtual new void CopyPersonalizationState (WebPart webPartA, WebPart webPartB)
    {
    }

    public void EnsureEnabled (bool ensureModifiable)
    {
    }

    protected internal virtual new void ExtractPersonalizationState ()
    {
    }

    protected internal virtual new void ExtractPersonalizationState (WebPart webPart)
    {
    }

    protected internal virtual new string GetAuthorizationFilter (string webPartID)
    {
      return default(string);
    }

    protected virtual new PersonalizationScope Load ()
    {
      return default(PersonalizationScope);
    }

    public virtual new void ResetPersonalizationState ()
    {
    }

    protected virtual new void Save ()
    {
    }

    protected internal virtual new void SetDirty ()
    {
    }

    protected internal virtual new void SetDirty (WebPart webPart)
    {
    }

    public virtual new void ToggleScope ()
    {
    }

    public WebPartPersonalization (WebPartManager owner)
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanEnterSharedScope
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool HasPersonalizationState
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new PersonalizationScope InitialScope
    {
      get
      {
        return default(PersonalizationScope);
      }
      set
      {
      }
    }

    public bool IsEnabled
    {
      get
      {
        Contract.Ensures (Contract.Result<bool>() == this.IsInitialized);

        return default(bool);
      }
    }

    protected bool IsInitialized
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsModifiable
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string ProviderName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public PersonalizationScope Scope
    {
      get
      {
        return default(PersonalizationScope);
      }
    }

    protected bool ShouldResetPersonalizationState
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected virtual new System.Collections.IDictionary UserCapabilities
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    protected WebPartManager WebPartManager
    {
      get
      {
        return default(WebPartManager);
      }
    }
    #endregion

    #region Fields
    public readonly static WebPartUserCapability EnterSharedScopeUserCapability;
    public readonly static WebPartUserCapability ModifyStateUserCapability;
    #endregion
  }
}

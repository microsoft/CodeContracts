// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.CodeTools
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell.Interop;
    using System.Collections.Generic;

    // The main package class
    #region Attributes
    [Microsoft.VisualStudio.Shell.DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0")
    , Microsoft.VisualStudio.Shell.ProvideLoadKey("Standard", "1.0", "Microsoft.VisualStudio.CodeTools.PropertyPage", "Microsoft", 1)
    , Guid("072DD0C6-AE1E-4ed6-A0BF-B99D5B68D29E")]
    #endregion
    public sealed class PropertyPagePackage : Microsoft.VisualStudio.Shell.Package
    {
        #region Construction
        private PropertyPanes panes;

        protected override void Initialize()
        {
            base.Initialize();
            // Initialize common functionality
            Common.Initialize(this);
            // Store and COM register any user property panes
            panes = new PropertyPanes();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                panes.Release(); // COM unregister any user property panes
                Common.Release();
                GC.SuppressFinalize(this);
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}

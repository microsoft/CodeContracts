using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Specifies the user interface (UI) state of a element within a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    [Flags]
    public enum DataGridViewElementStates
    {
        None = 0,
        Displayed = 1,
        Frozen = 2,
        ReadOnly = 4,
        Resizable = 8,
        ResizableSet = 16,
        Selected = 32,
        Visible = 64,
    }
}

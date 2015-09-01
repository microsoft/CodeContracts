// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.IBindableComponent
// Assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: C6B6066F-BA0A-416A-9BDC-46963B2C53AA
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Windows.Forms.dll

using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
    /// <summary>
    /// Enables a non-control component to emulate the data-binding behavior of a Windows Forms control.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    public interface IBindableComponent : IComponent, IDisposable
    {
        /// <summary>
        /// Gets the collection of data-binding objects for this <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.ControlBindingsCollection"/> for this <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        ControlBindingsCollection DataBindings { get; }

        // <summary>
        // Gets or sets the collection of currency managers for the <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        // </summary>
        // 
        // <returns>
        // The collection of <see cref="T:System.Windows.Forms.BindingManagerBase"/> objects for this <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        // </returns>
        // <filterpriority>1</filterpriority>
        // BindingContext BindingContext { get; set; }
    }
}

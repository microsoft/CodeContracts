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
using System.Runtime.InteropServices;
using System.Drawing;

// for documentation:
// using System.Windows.Forms;

namespace Microsoft.VisualStudio.CodeTools  
{
  /// <summary>
  /// Interface for property panes. 
  /// </summary>
  /// <remarks>
  /// Implement this interface to display property pages for
  /// Visual Studio projects. This interface is usually implemented as part of a
  /// <see cref="System.Windows.Forms.UserControl">UserControl</see> in order to use
  /// the convenient GUI designer to draw the property pane:
  /// <code>
  /// [Guid("00000000-8795-4fe7-8E51-2904E8B5F27B")]
  /// public partial class MyPropertyPane : UserControl
  ///                                     , Microsoft.VisualStudio.CodeTools.IPropertyPane</code>
  /// It is important to give the implementation class a guid (we call it {myguid})and to make it visible
  /// from COM, i.e. write in your <c>AssemblyInfo.cs</c>:
  /// <code>[assembly: ComVisible(false)]</code>
  /// The implementation class should also be registered as a COM class:
  /// <code>
  /// HKCR\
  ///  CLSID\
  ///    {myguid}\
  ///      (default)  =Microsoft.VisualStudio.Contracts.PropertyPane 
  ///      InprocServer32\
  ///        (default)     =mscoree.dll
  ///        Assembly      =MyPropertyPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=da8be8918709caaf
  ///        Class         = [full namespacepath].MyPropertyPane
  ///        CodeBase      =file:///c:/MyPropertyPage/bin/Debug/MyPropertyPage.dll
  ///        RuntimeVersion=v2.0.50215
  ///        ThreadingModel=Both
  ///      ProgId\
  ///        (default)      =Microsoft.VisualStudio.Contracts.PropertyPane
  ///      Implemented Categories\
  ///        {62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}\</code>
  /// Next, we can register the propertypane as a CodeTools property page. First, we generate
  /// an extra guid to identify the property pane, the property page id '{pageid}'. We register it now under
  /// the CodeTools property pages:
  /// <code>
  /// {vsroot}\
  ///   CodeTools\
  ///    {mytool}\
  ///      PropertyPages\
  ///        {pageid}\
  ///          clsid = {myguid}
	///          category= {category}
	///          Projects\
	///            CSharp=""
	///            VisualBasic=""
	///            FSharp=""</code>
  /// Here, {vsroot} is the Visual Studio registry root, for example <c>HKLM\Microsoft\VisualStudio\8.0</c>.
  /// The {mytool} is your own tool name, for example <c>FxCop</c>. Under the <c>PropertyPages</c>
  /// key, we associate the property page id's with the classes that implement the <see cref="IPropertyPane">IPropertyPane</see> interface.
	/// The {category} is the kind fo property page you need: it is either "ConfigPropertyPages" or "CommonPropertyPages" depending whether your
	/// property page is configuration dependent or not. The values under the "Projects" key specify for
	/// which projects you want to be registered; if wanted you can specify a project guid as a value
	/// instead of the language name. The special "CodeToolsUpdate" program looks at these
	/// entries and registers your property page with those projects (automatically taking care for special cases
	/// for FxCop for example.
  /// </remarks>
  [Guid("9F78A659-14A9-46d1-8715-4FDB037D5F86")]
  public interface IPropertyPane
  {
    #region Logical members
    /// <summary>
    /// The title of the property page.
    /// </summary>
    string Title       { get; }
    
    /// <summary>
    /// The helpfile associated with the property page.
    /// </summary>
    string HelpFile    { get; }
    
    /// <summary>
    /// The help context.
    /// </summary>
    int    HelpContext { get; }
    
    /// <summary>
    /// Set the host of the property pane. 
    /// </summary>
    /// <remarks>
    /// This method is called at initialization by the host itself
    /// to allow the property pane to notify the host on property changes.
    /// </remarks>
    /// <param name="host"></param>
    void   SetHost( IPropertyPaneHost host );
    
    /// <summary>
    /// Load the page properties from a <see cref="IPropertyStorage">storage</see>
    /// for a specific set of configurations. Called when the property page is loaded. 
    /// </summary>
    /// <remarks>
    /// For configuration independent pages, the configuration names are a single empty
    /// string (<c>""</c>).
    /// </remarks>
    /// <param name="configNames">The selected configuration names.</param>
    /// <param name="storage">The <see cref="IPropertyStorage">property storage</see>.</param>
    void LoadProperties(string[] configNames, IPropertyStorage storage);
    
    /// <summary>
    /// Save the page properties to a <see cref="IPropertyStorage">property storage</see>
    /// for a specific set of configurations. Called when the property page is closed
    /// and <see cref="IsDirty">dirty</see>.
    /// </summary>
    /// <remarks>
    /// For configuration independent pages, the configuration names are a single empty
    /// string (<c>""</c>).
    /// </remarks>
    /// <param name="configNames">The selected configuration names.</param>
    /// <param name="storage">The <see cref="IPropertyStorage">property storage</see>.</param>
    void SaveProperties(string[] configNames, IPropertyStorage storage);
    #endregion

    #region UI members
    // these are usually implemented already by a UserControl

    /// <summary>
    /// The windows handle of the property pane.
    /// </summary>
    /// <remarks>
    /// Usually implemented by an inherited <see cref="System.Windows.Forms.UserControl">UserControl</see>.
    /// </remarks>
    IntPtr Handle   { get; }

    /// <summary>
    /// The pixel size of the property pane.
    /// </summary>
    /// <remarks>
    /// Usually implemented by an inherited <see cref="System.Windows.Forms.UserControl">UserControl</see>.
    /// </remarks>
    Size Size { get; set; }

    /// <summary>
    /// The location of the property pane.
    /// </summary>
    /// <remarks>
    /// Usually implemented by an inherited <see cref="System.Windows.Forms.UserControl">UserControl</see>.
    /// </remarks>
    Point  Location { get; set; }

    /// <summary>
    /// Show the property pane.
    /// </summary>
    /// <remarks>
    /// Usually implemented by an inherited <see cref="System.Windows.Forms.UserControl">UserControl</see>.
    /// </remarks>
    void   Show();

    /// <summary>
    /// Hide the property pane.
    /// </summary>
    /// <remarks>
    /// Usually implemented by an inherited <see cref="System.Windows.Forms.UserControl">UserControl</see>.
    /// </remarks>
    void   Hide();
    #endregion
  }

  /// <summary>
  /// Abstract interface to property page storage. 
  /// </summary>
  /// <remarks>
  /// The properties can be stored per user, or per project (group). Each of
  /// properties are identified by a name and can be accessed by the MSBuild
  /// system (and for example passed to MSBuild tasks).
  /// 
  /// The property storage will automatically use 'primitive' properties directly
  /// stored on the project object itself if possible. Otherwise, it will store the
  /// properties in the associated project build storage.
  /// </remarks>
  [Guid("38A3802C-F18A-4eac-A7D9-191AD5F38B42")]
  public interface IPropertyStorage
  {
    /// <summary>
    /// Get the (common) value of a property for a set of configurations.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configNames">The set of configuration names.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="defaultValue">The default value of the property, this can not be <c>null</c>.</param>
    /// <returns>If the value of the property is the same (under <see cref="Object.Equals">Equals</see>) 
    /// for configuration, this value is returned. Otherwise, the method returns <c>null</c> (i.e. an
    /// indeterminate value).</returns>
    object GetProperties(bool perUser, string[] configNames, string propertyName, object defaultValue);
    
    /// <summary>
    /// Set the value of a property for multiple configurations.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configNames">The set of configuration names.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>0 on success, otherwise an <c>HRESULT</c> error code.</returns>
    int    SetProperties(bool perUser, string[] configNames, string propertyName, object value);
 
    /// <summary>
    /// Get a value of a property for a given configuration.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configName">The configuration name, use <c>""</c> for a configuration independent property.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="defaultValue">The default value of the property, this can not be <c>null</c>.</param>
    /// <returns>The value of the property, or the <c>defaultValue</c> if it
    /// could not be found (or read, or cast to the <c>defaultValue</c> type). 
    /// This method never returns <c>null</c>.</returns>
    object GetProperty(bool perUser, string configName, string propertyName, object defaultValue);
    
    /// <summary>
    /// Set the value of a property for a given configuration.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configName">The configuration name, use <c>""</c> for a configuration independent property.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>0 on success, otherwise an <c>HRESULT</c> error code.</returns>
    int SetProperty(bool perUser, string configName, string propertyName, object value);

    /// <summary>
    /// Get a value of a property for a given configuration.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configName">The configuration name, use <c>""</c> for a configuration independent property.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="defaultValue">The default value of the property.</param>
    /// <param name="found">Set to true if the property was found</param>
    /// <returns>The value of the property, or the <c>defaultValue</c> if it
    /// could not be found (or read, or cast to the <c>defaultValue</c> type). 
    T GetProperty<T>(bool perUser, string configName, string propertyName, T defaultValue, out bool found);

    /// <summary>
    /// Get the (common) value of a property for a set of configurations.
    /// </summary>
    /// <param name="perUser">Is this property stored per user?</param>
    /// <param name="configNames">The set of configuration names.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="defaultValue">The default value of the property that is assumed if the property was not explicitly set</param>
    /// <param name="anyFound">Set to true if at least one the properties was explicitly set</param>
    /// <param name="indeterminate">Set to true if the configurations differed in their values</param>
    /// <returns>If the value of the property is the same for each configuration, this value is returned. 
    /// Otherwise, the method returns the default value. Properties that were not found get a default value, so if none of the
    /// properties was set, the default value is also returned. This case can be distinguished through the anyfound parameter.</returns>
    T GetProperties<T>(bool perUser, string[] configNames, string propertyName, T defaultValue, out bool anyFound, out bool indeterminate) where T : IComparable<T>;

    /// <summary>
    /// Remove a property for a given configuration
    /// </summary>
    /// <param name="perUser">Was this property stored per user?</param>
    /// <param name="configName">The configuration name, use <c>""</c> for a configuration independent property</param>
    /// <param name="propertyName">The name of the property</param>
    /// <returns>0 on success, an <c>HRESULT</c> error code otherwise</returns>
    int RemoveProperty(bool perUser, string configName, string propertyName);
  }

  /// <summary>
  /// Abstract interface to the host of a <see href="IPropertyPane">property pane</see>.
  /// </summary>
  /// <remarks>
  /// This interface is passed to an <see href="IPropertyPane">property pane</see> who
  /// calls this interface to notify the host when the properties change due to user
  /// UI actions.
  /// </remarks>
  [Guid("D382C8FB-487E-4b00-ACBE-35CBB86B5337")]
  public interface IPropertyPaneHost
  {
    /// <summary>
    /// Notify the property pane host that the properties have changed (due to a user action).
    /// </summary>
    /// <remarks>
    /// Calling this method ensures that properties are properly saved through a call to
    /// <see cref="IPropertyPane.SaveProperties">IPropertyPane.SaveProperties</see>.
    /// </remarks>
    void PropertiesChanged( );
  }
}

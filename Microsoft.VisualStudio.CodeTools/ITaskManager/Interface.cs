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

namespace Microsoft.VisualStudio.CodeTools 
{
  /// <summary>The priority of a task</summary>
  /// <remarks>The priority can be used by the user to order the task list.</remarks>
  [Guid("6EF71246-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public enum TaskPriority
  {
    /// <summary>Lowest priority</summary>
    Low,
    /// <summary>Normal priority</summary>
    Normal,
    /// <summary>Highest priority</summary>
    High
  }

  /// <summary>The category of the task list where the task is shown.</summary>
  /// <remarks>The <c>Error</c>, <c>Warning</c>, and <c>Message</c> category go to the error window.
  /// The other categories go to their specific task list.</remarks>
  [Guid("6EF71245-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public enum TaskCategory
  {
    /// <summary>An error in the error task list.</summary>
    Error,
    /// <summary>A warning in the error task list.</summary>
    Warning,
    /// <summary>A message in the error task list.</summary>
    Message,
    /// <summary>The comment (e.g. hints) task list.</summary>
    Comment,
    /// <summary>The user task list.</summary>
    User,
    /// <summary>The shortcut task list.</summary>
    Shortcut,
    /// <summary>The miscellaneous task list.</summary>
    Other,
    /// <summary>The task does not show up in the task list</summary>
    Invisible
  }

  /// <summary>Exposes pre-defined output panes to send text messsages.</summary>
  /// <remarks>Output panes are identified
  /// by a <see cref="Guid">Guid</see>. The <see cref="TaskOutputPane">TaskOutputPane</see> class
  /// defines three predefined static output pane <see cref="Guid">Guid</see>'s, where <see cref="None">None</see> is used
  /// to suppress output (See also <see cref="ITaskManager.AddTask">ITaskManager.AddTask</see>.)
  /// </remarks>
  /// <seealso cref="ITaskManager.AddTask"/>
  /// <seealso cref="ITaskManager.OutputString"/>
  [Guid("6EF71244-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public static class TaskOutputPane
  {
    /// <summary>The General output pane <see cref="Guid">Guid</see>.</summary>
    public static readonly Guid General = new Guid("{3c24d581-5591-4884-a571-9fe89915cd64}");
    /// <summary>The Build output pane <see cref="Guid">Guid</see>.</summary>
    public static readonly Guid Build = new Guid("{1bd8a850-02d1-11d1-bee7-00a0c913d1f8}");
    /// <summary>The Debug output pane <see cref="Guid">Guid</see>.</summary>
    public static readonly Guid Debug = new Guid("{fc076020-078a-11d1-a7df-00a0c9110051}");
    /// <summary>Suppress output (invisible output pane)</summary>
    public static readonly Guid None = Guid.Empty;
  }

  /// <summary>The marker type used to mark the task in the source code.</summary> 
  /// <remarks>It is only possible to mark tasks with squigglies.</remarks>
  [Guid("6EF71243-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public enum TaskMarker
  {
    /// <summary>No squiggle.</summary>
    Invisible,
    /// <summary>Red intellisense squiggle.</summary>    
    CodeSense,
    /// <summary>Blue build error squiggle.</summary>
    Error,
    /// <summary>Green warning squiggle.</summary>
    Warning,
    /// <summary>Purple 3rd-party squiggle.</summary>
    Other         
  }

  /// <summary>
  /// An ITaskManager is the main interface to send errors, comments, output etc. 
  /// (called <c>tasks</c>) to the Visual Studio environment. 
  /// </summary>
  /// <remarks>
  /// Any added tasks show up automatically in the appropiate task window. Furthermore,
  /// an appropiate message is sent to an output window, squiggles are shown at corresponding
  /// source locations, help is shown, commands can be attached to the tasks, renames and
  /// source code changes are tracked, and task lists are automatically cleared on rebuilds,
  /// or when a project is closed.
  /// 
  /// <para>There are two ways to get access to an <see cref="ITaskManager">ITaskManager</see> interface:</para>
  /// <list type="bullet">
  /// <item>Standard Visual Studio packages (like language services) can use 
  /// <see cref="IServiceProvider.QueryService">QueryService</see> for an <see cref="ITaskManagerFactory">ITaskManagerFactory</see>
  /// (Guid: <c>{6ef71237-5832-4cd5-a75c-067911af5d14}</c>).
  /// Using this interface, one can create a task manager to manage (error) tasks automatically. 
  /// </item>
  /// <item>MSBuild "tasks" can register themselves to recieve an <see cref="ITaskManager">ITaskManager</see> interface
  /// as their <c>HostObject</c> property. The <c>HostObject</c> must be a public property defined on the
  /// MSBuild <c>Task</c> object. The registry must be set as:
  /// <code>
  ///  {vsroot}
  ///    CodeTools
  ///      {toolname}  string value: displayName={displayName} 
  ///        Tasks
  ///          {projectguid}
  ///             {msbuildtarget}  string value: {msbuildtask}=""
  ///             ...
  ///          ...
  /// </code>
  /// Here, <c>{vsroot}</c> is the Visual Studio root key, for example 
  /// <c>HKEY_LOCAL_MACHINE\Software\Microsoft\VisualStudio\8.0</c>. The <c>{toolname}</c> is the name 
  /// of your tool, for example <c>FxCop</c>. Use the <c>displayName</c> value to set a nice
  /// human readable name if necessary. Underneath the <c>{toolname}</c> key you can set 
  /// <c>{projectguid}</c> keys -- only tasks part of such project type receive the task manager. Use a 
  /// guid consisting of zero's to register for all projects. The <c>{msbuildtarget}</c> registers 
  /// a specific MSBuild target name together with a list of <c>{msbuildtask}</c> values. Each
  /// <c>{msbuildtask}</c> part of <c>{msbuildtarget}</c> related to a build of <c>{projectguid}</c>
  /// receives a task manager as a <c>HostObject</c>. For example, one could write an MSBuild <c>targets</c> file
  /// containing:
  /// <code>
  ///   &lt;UsingTask TaskName="MyNameSpace.MyTool" AssemblyFile="MyTool.dll" /&gt; 
  ///   &lt;Target  Name="RunMyTool"
  ///             Condition="'$(UseMyTool)'=='true'"
  ///             DependsOnTargets="Compile"
  ///             ... &gt;
  ///     &lt;MyTool   ProjectName="$(ProjectName)"
  ///               Sources="@(Compile)" 
  ///               ... /&gt;
  ///   &lt;Target/&gt; 
  /// </code>
  /// This would cause the <c>MyTool</c> task to be run after each build in Visual Studio.
  /// See the standard C# targets file for more attributes that can be passed to your tool.
  /// </item>
  /// </list>
  /// <seealso cref="IVsTaskManager"/>
  /// </remarks>
  [Guid("6EF71242-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface ITaskManager : IServiceProvider
  {
    /// <summary>
    /// Add a task: an error message, a comment, a warning, etc.
    /// </summary>
    /// <remarks>This is the main interface to send error messages to Visual Studio.
    /// From MSBuild tasks, this is usually the only method called. If the <paramref name="outputPane">outputPane</paramref>
    /// is set, an error message in the standard format is send to that output pane. If the
    /// <paramref name="marker">marker</paramref> is set, source code locations are automaticcally
    /// squiggelized.</remarks>
    /// <param name="description">Description of the task</param>
    /// <param name="tipText">Possible text to show when the user hovers over the source location. 
    ///                      The <paramref name="description">description</paramref> is used when <c>null</c>.</param>
    /// <param name="code">The code of the error (shown in the standard error output message), usually something like <c>"CS1201"</c>.</param>
    /// <param name="helpKeyword">The help keyword used to find a help topic for this task. Usually the same as <paramref name="code">code</paramref></param>
    /// <param name="priority">The priority of the task. Usually <see cref="TaskPriority">TaskPriority.Normal</see>.</param>
    /// <param name="category">The category of the task. Usually <see cref="TaskCategory">TaskCategory.Error</see>.</param>
    /// <param name="marker">The text marker used to mark the source location associated with the task. Usually <see cref="TaskMarker">TaskMarker.Error</see>.</param>
    /// <param name="outputPane">The output pane used to display the standard error message. Usually <see cref="TaskOutputPane.Build">TaskOutputPane.Build</see>, but one can use <see cref="TaskOutputPane.None">TaskOutputPane.None</see> to suppress the output of the standard message</param>
    /// <param name="projectName">The project associated with the task. Usually passed as the <c>ProjectName</c> property by the MSBuild system</param>
    /// <param name="fileName">The file associated with the task</param>
    /// <param name="startLine">Start line number (1-based)</param>
    /// <param name="startColumn">Start column (1-based)</param>
    /// <param name="endLine">End line number (1-based)</param>
    /// <param name="endColumn">End column number (1-based)</param>
    /// <param name="commands">Pass in a possible <see cref="ITaskCommands">ITaskCommands object</see>, usually <c>null</c>. 
    ///                        This is used to attach context menu commands to the text marker associated with the task.</param>
    void AddTask(string description      
                ,string tipText          
                ,string code             
                ,string helpKeyword      
                ,TaskPriority priority   
                ,TaskCategory category   
                ,TaskMarker marker       
                ,Guid outputPane         
                ,string projectName      
                ,string fileName        
                ,int startLine
                ,int startColumn
                ,int endLine
                ,int endColumn
                ,ITaskCommands commands  
                );
    /// <summary>
    /// Show output in a specific output window pane.
    /// </summary>
    /// <param name="message">The output message.</param>
    /// <param name="outputPane">The <see cref="TaskOutputPane">output pane Guid</see> 
    /// where the output should go to (usually <see cref="TaskOutputPane.Build">Build</see>).</param>
    void OutputString(string message, Guid outputPane);
    /// <summary>Refresh the task list.</summary>
    /// <remarks>Task lists are automatically refreshed at the end of a build.</remarks>
    void Refresh();
    /// <summary>Clear the tasks associated with this task manager.</summary>
    /// <remarks>Tasks lists are automatically cleared at the start of a build or when an associated project is closed.</remarks>
    void ClearTasks();
    /// <summary>Clear tasks specific to a certain sourcefile.</summary>
    /// <remarks>Tasks lists are automatically cleared at the start of a build or when an associated project is closed.</remarks>
    /// <param name="projectName">The project associated with the tasks. Can be <c>null</c>.</param>    
    /// <param name="fileName">The source file associated with the tasks.</param>    
    void ClearTasksOnSource(string projectName, string fileName);
    /// <summary>
    /// Clear tasks that overlap with a certain span in a source file.
    /// </summary>
    /// <remarks>Tasks lists are automatically cleared at the start of a build or when an associated project is closed.</remarks>
    /// <param name="projectName">The project associated with the tasks. Can be <c>null</c>.</param>        
    /// <param name="fileName">The source file associated with the task.</param>
    /// <param name="startLine">The starting line of the span.</param>
    /// <param name="startColumn">The start column of the span.</param>
    /// <param name="endLine">The end line of the span.</param>
    /// <param name="endColumn">The end column of the span.</param>
    void ClearTasksOnSourceSpan( string projectName, string fileName
                                ,int startLine
                                ,int startColumn
                                ,int endLine
                                ,int endColumn
                                );
    /// <summary>
    /// Clear task associated with a certain project.
    /// </summary>
    /// <remarks>Tasks lists are automatically cleared at the start of a build or when an associated project is closed.</remarks>
    /// <param name="projectName">The name of the project.</param>                            
    void ClearTasksOnProject(string projectName);
  }

  /// <summary><see cref="ITaskManager">Task managers</see> associated with Visual Studio</summary>
  /// <remarks>
  /// An extension of <see cref="ITaskManager">TaskManager</see> that gives access to
  /// a Visual Studio project <see cref="Microsoft.VisualStudio.Shell.Interop.IVsHierarchy">IVsHierarchy</see> 
  /// interface.
  /// <seealso cref="ITaskManager"/>
  /// </remarks>
  [Guid("6EF71241-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface IVsTaskManager : ITaskManager
  {
    /// <summary>Return the Visual Studio hierarchy given a project name.</summary>
    /// <returns>The Visual Studio project. Supports the <see cref="Microsoft.VisualStudio.Shell.Interop.IVsHierarchy">IVsHierarchy</see> interface.</returns>
    object GetHierarchy(string projectName);

    /// <summary>Register for the ClearTasksEvent that is called whenever Visual Studio clears the tasks. This happens for example
    /// when a solution is closed or rebuild. This can be used for example to kill background build tasks that would
    /// otherwise still emit error messages. Do not clear your tasks on this event, this is already done automatically
    /// by the task manager.
    /// </summary>
    void AddClearTasksEvent(IClearTasksEvent handler);
    void RemoveClearTasksEvent(IClearTasksEvent handler);
  }

  /// <summary>
  /// Handler for the OnClearTasks event.
  /// </summary>
  /// <param name="taskManager">The current task manager</param>
  /// <param name="projectName">The project whose tasks are cleared (empty for a solution wide clear)</param>
  [Guid("6EF71240-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface IClearTasksEvent
  {
    void Invoke(IVsTaskManager taskManager, string projectName);
  }

  /// <summary>The Visual Studio service to create task managers.</summary>
  /// <remarks>Visual Studio extension packages can <see cref="IServiceProvider.QueryService">QueryService</see>
  /// for the <see cref="ITaskManagerFactory">ITaskManagerFactory</see> and create their own
  /// <see cref="ITaskManager">task managers</see> to handle their tasks (Guid: <c>{6ef71237-5832-4cd5-a75c-067911af5d14}</c>). MSBuild tasks automatically
  /// get a <see cref="ITaskManager">TaskManager</see> passed as an MSBuild <c>HostObject</c> when
  /// the register themselves. See the <see cref="ITaskManager">ITaskManager</see> interface for more information.
  /// </remarks>
  [Guid("6EF71237-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface ITaskManagerFactory
  {
    /// <summary>
    /// Create a new <see cref="ITaskManager">TaskManager</see>.
    /// </summary>
    /// <param name="providerName">The name of the tool or service that provides the tasks.</param>
    /// <returns>A new <see cref="ITaskManager">TaskManager</see>.</returns>
    ITaskManager CreateTaskManager(string providerName);
    
    /// <summary>
    /// Share a single <see cref="ITaskManager">TaskManager</see> per provider name.
    /// </summary>
    /// <param name="providerName">The name of the tool or service that provides the tasks.</param>
    /// <param name="createIfAbsent">Set to <c>true</c> if a new task manager should be created if it is not present yet.</param>
    /// <returns>A new <see cref="ITaskManager">TaskManager</see> or <c>null</c> if not present and <param name="createIfAbsent">createIfAbsent</param> is <c>false</c>.</returns>
    ITaskManager QuerySharedTaskManager(string providerName, bool createIfAbsent);
    
    /// <summary>
    /// Release a previously obtained task manager via a call <see cref="QuerySharedTaskManager">QuerySharedTaskManager</see>.
    /// </summary>
    /// <param name="taskManager">The task manager that is no longer used.</param>
    void ReleaseSharedTaskManager(ITaskManager taskManager);

    /// <summary>
    /// Get a task manager from a registered tool.
    /// </summary>
    /// <param name="toolName">The registration name of the tool</param>
    /// <returns>The taskmanager or null if the tool was not found</returns>
    ITaskManager GetToolTaskManager(string toolName);
  }

  /// <summary>
  /// Interface to tasks.
  /// </summary>
  /// <remarks>This interface is passed to a client when a command is executed.</remarks>
  /// <seealso cref="IVsTask"/>
  /// <seealso cref="ITaskCommands"/>  
  [Guid("6EF71247-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface ITask
  {
    /// <summary>Current start line of the task marker.</summary>
    int StartLine { get; }
    /// <summary>Current start column of the task marker.</summary>    
    int StartColumn { get; }
    /// <summary>Current end line of the task marker.</summary>
    int EndLine { get; }
    /// <summary>Current end column of the task marker.</summary>
    int EndColumn { get; }
    /// <summary>The current source file name (might be changed from the file name when the task was added).</summary>
    string FilePath { get; }
    /// <summary>The current associated project name (might be changed from the project name when the task was added).</summary>
    string ProjectName { get; }
    /// <summary>The <c>code</c> argument passed to <see cref="ITaskManager.AddTask">AddTask</see> when the task was added.</summary>
    string Code { get; }
    /// <summary>The controlling <see cref="ITaskManager">ITaskManager</see>.</summary>    
    ITaskManager TaskManager { get; }

    /// <summary>Description of the task.</summary>
    string Description { get; set; }
    /// <summary>Tiptext of the task.</summary>
    string TipText { get; set; }
    /// <summary>Checked state. (a strike-trough for error tasks)</summary>
    bool Checked { get; set; }
    /// <summary>The task priority</summary>
    TaskPriority Priority { get; set; }
    /// <summary>The task marker kind</summary>
    TaskMarker Marker { get; set; }
    
    /// <summary>
    /// Remove the task item. Usually called from a command handler or language service.
    /// </summary>
    /// <remarks>Also removes the associated marker. A removed task can not be
    /// restored. Use <see cref="ITask.Checked">Checked</see> to strike-through
    /// task items that can be restored.
    /// </remarks>
    /// <seealso cref="ITask.Checked"/>
    void Remove();

    /// <summary>
    /// Explicitly refresh a specific task item. 
    /// </summary>
    void Refresh();
  }

  /// <summary>
  /// Interface to <see cref="ITask">tasks</see> in Visual Studio.
  /// </summary>
  /// <remarks>An extension of <see cref="ITask">ITask</see> that also gives
  /// access to Visual Studio elements.
  /// This interface is passed to a client when a command is executed.
  /// </remarks>
  /// <seealso cref="ITask"/>
  [Guid("6EF71248-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface IVsTask : ITask
  {
    /// <summary>The Visual Studio project hierarchy information.</summary>
    /// <param name="hierarchy">The Visual Studio project. Supports the <see cref="Microsoft.VisualStudio.Shell.Interop.IVsHierarchy">IVsHierarchy</see> interface.</param>
    /// <param name="hierarchyItem">The project hierarchy item id.</param>
    void GetHierarchy(out object hierarchy, out int hierarchyItem);

    /// <summary>Get the Visual Studio text editor buffer.</summary>
    /// <param name="openIfClosed">Pass <c>true</c> if the text buffer should be opened if it was closed.</param>
    /// <returns>The Visual Studio text editor buffer. Supports the <see cref="Microsoft.VisualStudio.TextManager.Interop.IVsTextLines">IVsTextLines</see> interface.</returns>
    object GetTextLines(bool openIfClosed);

    /// <summary>Get the Visual Studio text view.</summary>
    /// <param name="openIfClosed">Pass <c>true</c> if the text view should be opened if it was closed.</param>
    /// <returns>The Visual Studio text view. Supports the <see cref="Microsoft.VisualStudio.TextManager.Interop.IVsTextView">IVsTextView</see> interface.</returns>
    object GetTextView(bool openIfClosed);
    
    /// <summary>Get the Visual Studio window frame.</summary>
    /// <param name="openIfClosed">Pass <c>true</c> if the frame should be opened if it was closed.</param>
    /// <returns>The Visual Studio window frame. Supports the <see cref="Microsoft.VisualStudio.TextManager.Interop.IVsWindowFrame">IVsWindowFrame</see> interface.</returns>
    object GetWindowFrame(bool openIfClosed);

    /// <summary>Navigate to the task location</summary>
    /// <param name="highlight">Pass <c>true</c> if the span should be selected.</param>
    void NavigateTo(bool highlight);
    
    /// <summary>Show help.</summary>
    /// <param name="helpKeyword">The help keyword. Pass <c>null</c> to show the default task help.</param>
    void NavigateToHelp(string helpKeyword);

    /// <summary>Navigate to an arbitrary location</summary>
    /// <param name="projectName">The project name; pass <c>null</c> to use the task's project</param>
    /// <param name="fileName">The file name; pass <c>null</c> to use the task's filename</param>
    /// <param name="startLine">The start line</param>
    /// <param name="startColumn">The start column</param>
    /// <param name="endLine">The end line</param>
    /// <param name="endColumn">The end column</param>
    /// <param name="highlight">Pass <c>true</c> if the span should be selected</param>
    void NavigateToLocation(string projectName, string fileName, int startLine, int startColumn, int endLine, int endColumn, bool highlight);
  }
  
  
  /// <summary>
  /// The kind of menu item in a context menu.
  /// </summary>
  [Guid("6EF71249-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public enum TaskMenuKind
  {
    /// <summary>Normal menu item.</summary>
    Normal,     
    /// <summary>Normal menu item that is only enabled is a single task item is selected.</summary>
    Singular,   
    /// <summary>Checkable menu item. For these items, <see cref="ITaskCommands.IsChecked">IsChecked</see> is called.</summary>    
    Checkable,  
    /// <summary>Submenu.</summary>    
    Submenu
  }

  /// <summary>
  /// A menu item.
  /// </summary>
  /// <remarks>The identity of a menu item is determined by the object 
  /// identity. Menu items should therefore be allocated only once (or otherwise
  /// they are duplicated in a context menu on multiple selected tasks).
  /// </remarks>  
  /// <seealso cref="ITaskCommands"/>
  [Guid("6EF7124A-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface ITaskMenuItem
  {
    /// <summary>The caption text of a menu item.</summary>
    string Caption { get; }
    /// <summary>The menu item kind.</summary>
    TaskMenuKind Kind { get; }
  }

  /// <summary>
  /// Callback interface to handle commands on task markers.
  /// </summary>
  /// <remarks>Pass this to <see cref="ITaskManager.AddTask">AddTask</see> to 
  /// enable commands on task markers.</remarks>
  [Guid("6EF7124B-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public interface ITaskCommands : IDisposable
  {
    /// <summary>
    /// Give a list of context menu items on the task marker.
    /// </summary>
    /// <remarks>Note that menu items are compared by object identity.
    /// Usually, this means that menu items should be allocated statically.
    /// </remarks>
    /// <seealso>ITaskMenuItem</seealso>
    ITaskMenuItem[] GetMenuItems(ITaskMenuItem parent);

    /// <summary>
    /// Is a checkable menu item checked? 
    /// </summary>
    /// <param name="menuItem">The menu item.</param>
    /// <returns><c>true</c> if a checkable menu item is checked. <c>false</c> otherwise.</returns>
    bool IsChecked(ITaskMenuItem menuItem);

    /// <summary>
    /// Is a menu item enabled?
    /// </summary>
    /// <param name="menuItem">The menu item.</param>
    /// <returns><c>true</c> if this menu item is enabled. <c>false</c> otherwise.</returns>
    bool IsEnabled(ITaskMenuItem menuItem);

    /// <summary>
    /// Called when a context menu item is selected by the user.
    /// </summary>
    /// <param name="task">The task associated with the menu.</param>
    /// <param name="menuItem">The selected menu item.</param>
    void OnContextMenu( ITask task
                      , ITaskMenuItem menuItem
                      );
    /// <summary>
    /// Called when a task marker is double clicked.
    /// </summary>
    /// <param name="task">The task associated with the marker.</param>
    void OnDoubleClick( ITask task );

    /// <summary>
    /// Called when the user hovers over the task marker.
    /// </summary>
    /// <param name="task">The task associated with the marker.</param>
    void OnHover( ITask task);                
  }

  /// <summary>
  /// Default implementation of <see cref="ITaskMenuItem">ITaskMenuItem</see>..
  /// </summary>
  [Guid("6EF7124C-5832-4cd5-A75C-067911AF5D14")]
  [ComVisible(true)]
  public class TaskMenuItem : ITaskMenuItem
  {
    private string caption;
    private TaskMenuKind kind;

    /// <summary>The menu caption.</summary>    
    public string Caption
    {
      get { return caption; }
      set { caption = value; }
    }

    /// <summary>The kind of the menu item.</summary>    
    public TaskMenuKind Kind
    {
      get { return kind; }
      set { kind = value; }
    }

    /// <summary>
    /// Create a new menu item.
    /// </summary>
    /// <param name="caption">The caption of the menu item.</param>
    /// <param name="kind">The kind of menu item.</param>
    public TaskMenuItem(string caption, TaskMenuKind kind)
    {
      this.caption = caption;
      this.kind = kind;
    }
  }
}

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Research.CodeAnalysis;

namespace CCVersions
{
  public partial class MainForm : Form
  {
    ISourceManager selectedSourceManager = null;

    string _outputDirectory;
    string outputDirectory
    {
      get { return this._outputDirectory; }
      set
      {
        Contract.Requires(this.listAssemblies != null);
        Contract.Requires(this.splitMain != null);
        Contract.Requires(value != null);
        if (value != this._outputDirectory)
        {
          this._outputDirectory = value;
          this.UpdateAssembliesList();
        }
      }
    }

    List<ISourceManagerVersionSpec> versions = new List<ISourceManagerVersionSpec>();

    public MainForm()
    {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.listChartTypes != null);
      Contract.Requires(this.listProjects != null);
      Contract.Requires(this.listSources != null);
      Contract.Requires(this.splitMain != null);

      this.listChartTypes.SelectedIndex = 0;

      var xRootPath = Program.config.XConfig.Element("RootPath");
      if (xRootPath != null)
      {
        this.rootPath = xRootPath.Value;
      }
      else
      {
        this.rootPath = @"c:\temp";
      }


      var xSources = Program.config.XConfig.Element("Sources");
      if (xSources != null)
      {
        var xxSources = xSources.Elements("Source");
//        if (xxSources != null)
        {
          this.listSources.DataSource = xxSources.Select(SourceEntry.FromXElement).ToList();
        }
      }
      if (this.listSources.Items.Count > 0)
      {
        this.listSources.SelectedIndex = 0;
        listSources_SelectedIndexChanged(sender, e);
      }
    }

    private void listSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.listSources != null);
      Contract.Requires(this.listProjects != null);
      Contract.Requires(this.splitMain != null);

      var selectedSource = this.listSources.SelectedValue as SourceEntry;

      if (selectedSource == null)
      {
        this.selectedSourceManager = null;
      }
      else
      {
        this.selectedSourceManager = selectedSource.SourceManager;
        if (this.selectedSourceManager == null)
        {
          MessageBox.Show(String.Format("Unknown source type: \"{0}\"", selectedSource.Type));
        }
      }

      if (this.selectedSourceManager == null)
      {
        this.listProjects.DataSource = null;
      }
      else
      {
        if (this.selectedSourceManager.WorkingDirectory == null)
        {
          var workingDirectory = Path.Combine(Path.Combine(this.rootPath, "workspaces"), this.selectedSourceManager.Id);
          Directory.CreateDirectory(workingDirectory);
          this.selectedSourceManager.WorkingDirectory = workingDirectory;
        }

        this.outputDirectory = Path.Combine(Path.Combine(this.rootPath, "outputs"), this.selectedSourceManager.Id);
        Directory.CreateDirectory(this.outputDirectory);

        this.listProjects.DataSource = selectedSource.Projects;
        // todo: save selection
        this.listProjects.ClearSelected();
        if (this.listProjects.Items.Count > 0)
          this.listProjects.SelectedIndex = 0;
      }
    }

    private void buttonDoIt_Click(object sender, EventArgs e)
    {
      Contract.Requires(this.backgroundEnumerateVersions != null);
      Contract.Requires(!this.backgroundEnumerateVersions.IsBusy);
      Contract.Requires(this.buttonCancel != null);
      Contract.Requires(this.labelStatus != null);
      Contract.Requires(this.listProjects != null);
      Contract.Requires(this.textFirstVersion != null);
      Contract.Requires(this.textLastVersion != null);

      if (this.selectedSourceManager == null)
      {
        MessageBox.Show("Please choose a source!");
        return;
      }

      this.buttonDoIt.Enabled = false;
      this.buttonCancel.Enabled = true;

      // Parse the versions
      var firstVersion = this.selectedSourceManager.ParseVersionSpec(this.textFirstVersion.Text);
      var lastVersion = this.selectedSourceManager.ParseVersionSpec(this.textLastVersion.Text);

      var selectedProjects = this.listProjects.SelectedItems.Cast<ProjectEntry>().ToArray();

      Contract.Assert(firstVersion != null);
      Contract.Assert(lastVersion != null);
      Contract.Assert(selectedProjects != null);

      // Launch the worker
      this.backgroundEnumerateVersions.RunWorkerAsync(new EnumerateVersionsArguments {
        versions = new List<ISourceManagerVersionSpec>(this.versions),
        selectedProjects = selectedProjects
      });
    }

    private void backgroundEnumerateVersions_DoWork(object sender, DoWorkEventArgs e)
    {
      Contract.Requires(this.backgroundEnumerateVersions != null);
      Contract.Requires(this.outputDirectory != null);
      Contract.Requires(this.outputDirectory.Length >= 1);
      Contract.Requires(this.selectedSourceManager != null);
      Contract.Requires(this.selectedSourceManager.WorkingDirectory != null);
      Contract.Requires(e.Argument != null);
      Contract.Requires(e.Argument is EnumerateVersionsArguments);
      Contract.Requires((e.Argument as EnumerateVersionsArguments).versions != null);
      Contract.Requires((e.Argument as EnumerateVersionsArguments).selectedProjects != null);

      var arguments = e.Argument as EnumerateVersionsArguments;

      // to update the UI from the background worker
      this.backgroundEnumerateVersions.ReportProgress(0, new EnumerateVersionsStatus { version = DummyVersionSpec.Instance, status = "Enumerating versions..." });

      // path for the projects
      var projectsPath = arguments.selectedProjects.Select(p => p.Path);

      // the versions to analyze
      var versions = arguments.versions;
      Contract.Assume(Contract.ForAll<ISourceManagerVersionSpec>(versions, v => v != null));
      const int numSubSteps = 2;
      var numSteps = versions.Count() * numSubSteps;
      var currentStep = 0;

      if (this.backgroundEnumerateVersions.CancellationPending)
      {
        e.Cancel = true;
        return;
      }
      this.backgroundEnumerateVersions.ReportProgress(0, new EnumerateVersionsStatus { numSteps = numSteps });

      // Get the default, because at the moment we have only one
      var builder = BuilderFactories.Default(this.selectedSourceManager.WorkingDirectory);
      Contract.Assume(builder != null);
      // Main Loop
      foreach (var version in versions)
      {
        Contract.Assume(versions.Count() > 0);
        Contract.Assert(version != null);

        if (this.backgroundEnumerateVersions.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        this.backgroundEnumerateVersions.ReportProgress(currentStep * 100 / numSteps,
          new EnumerateVersionsStatus { step = currentStep, version = version, status = "Getting sources..." });
        currentStep++;

        // Get the sources
        if (!this.selectedSourceManager.GetSources(version))
        {
          e.Result = new EnumerateVersionsResult { GetFailed = true };
          return;
        }

        if (this.backgroundEnumerateVersions.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        this.backgroundEnumerateVersions.ReportProgress(currentStep * 100 / numSteps,
          new EnumerateVersionsStatus { step = currentStep, version = version, status = "Building projects..." });
        currentStep++;

        // Do the build
        var buildResult = builder.Build(projectsPath, version.Id, this.outputDirectory);

        this.backgroundEnumerateVersions.ReportProgress(currentStep * 100 / numSteps,
          new EnumerateVersionsStatus { buildFinished = true, status = String.Format("Build {0}", buildResult ? "succeeded" : "failed") });
      }
    }

    private void backgroundEnumerateVersions_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      Contract.Requires(this.chart != null);
      Contract.Requires(this.labelStatus != null);
      Contract.Requires(this.labelVersion != null);
      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.outputDirectory != null);
      Contract.Requires(this.progressEnumerateVersions != null);
      Contract.Requires(this.splitMain != null);
      //Contract.Requires(e != null);
      Contract.Requires(e.UserState != null);
      Contract.Requires(e.UserState is EnumerateVersionsStatus);

      var userState = e.UserState as EnumerateVersionsStatus;

      if (userState.numSteps.HasValue)
      {
        this.progressEnumerateVersions.Maximum = userState.numSteps.Value;
      }
      this.progressEnumerateVersions.Value = userState.step ?? e.ProgressPercentage * this.progressEnumerateVersions.Maximum / 100;

      if (userState.version != null)
      {
        this.labelVersion.Text = userState.version.ToString();
      }
      if (userState.status != null)
      {
        this.labelStatus.Text = userState.status;
      }
      if (userState.buildFinished)
      {
        // Important: After each build, we refresh the graphs. this is not done in background, and it may slow down the interface
        this.UpdateAssembliesList();
        this.UpdateChart();
      }
      //Application.DoEvents(); // stack overflow if uncommented!
    }

    private void backgroundEnumerateVersions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      Contract.Requires(this.buttonCancel != null);
      Contract.Requires(this.buttonDoIt != null);
      Contract.Requires(this.labelStatus != null);
      Contract.Requires(this.labelVersion != null);
      Contract.Requires(this.progressEnumerateVersions != null);

      this.progressEnumerateVersions.Value = this.progressEnumerateVersions.Maximum;

      if (e.Cancelled)
      {
        this.labelStatus.Text = "Cancelled";
      }
      else if (e.Error != null)
      {
        this.labelStatus.Text = "Error: " + e.Error.Message;
      }
      else
      {
        var result = e.Result as EnumerateVersionsResult;

        if (result != null && result.GetFailed)
        {
          this.labelStatus.Text = "Get failed";
        }
        else
        {
          this.labelVersion.Text = "";
          this.labelStatus.Text = "Finished!";
        }
      }

      this.buttonDoIt.Enabled = true;
      this.buttonCancel.Enabled = false;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      Contract.Requires(this.buttonCancel != null);
      Contract.Requires(this.buttonDoIt != null);
      Contract.Requires(this.backgroundEnumerateVersions != null);

      if (this.backgroundEnumerateVersions.IsBusy)
      {
        this.backgroundEnumerateVersions.CancelAsync();
        this.labelStatus.Text = "Waiting for the current operation to terminate...";
      }
      else
      {
        MessageBox.Show("Nothing to cancel");
        this.buttonCancel.Enabled = false;
        this.buttonDoIt.Enabled = true;
      }
    }

    private void buttonCharts_Click(object sender, EventArgs e)
    {
      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.outputDirectory != null);
      Contract.Requires(this.splitMain != null);

      if (!this.splitMain.Panel2Collapsed)
      {
        this.splitMain.Panel2Collapsed = true;
      }
      else
      {
        UpdateAssembliesList();

        this.splitMain.Panel2Collapsed = false;
      }
    }

#if false
    /// <summary>
    /// Aggregate values for the results
    /// </summary>
    private class CachedChartPoints
    {
      public DateTime CacheTime;          // Time stamp of the .sdf cache 
      public VersionResult[] Points;      // Points in the graph
    }

    private IDictionary<string, CachedChartPoints> ChartDataCache;

    private VersionResult[] ReadChartPoints(string FileName)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(FileName));
      Contract.Ensures(Contract.Result<VersionResult[]>() != null);

      VersionResult[] res;
      var ConnectionString = "data source='" + FileName + "'";//; mode='Read Only';"; doesn't work

      // read the sql DB
      var entityConnectionString = new EntityConnectionStringBuilder
      {
        Metadata = "res://*/ClousotCacheModel.csdl|res://*/ClousotCacheModel.ssdl|res://*/ClousotCacheModel.msl",
        Provider = "System.Data.SqlServerCe.3.5",
        ProviderConnectionString = ConnectionString
      }.ToString();

      using (var connection = new EntityConnection(entityConnectionString))
      {
        connection.Open();

        using (var objectContext = new ClousotCacheEntities(connection))
        {
          objectContext.ContextOptions.LazyLoadingEnabled = true; // avoid having to manually load every method

          var resList = new List<VersionResult>();
#if false
          var versionMethods = objectContext.VersionBindings.GroupBy(b => b.Version);
          var versionResults = objectContext.VersionResults.ToDictionary(r => r.Version);

          foreach (var versionMethod in versionMethods)
          {
            VersionResult versionResult;
            // Sanity check
            if (versionResults.TryGetValue(versionMethod.Key, out versionResult) && versionResult.Methods == versionMethod.LongCount())
              versionResult.Complete(); // previously computed values can be reused
            else
            {
              if (versionResult != null) // the entry needs an update
                objectContext.DeleteObject(versionResult);
              versionResult = new VersionResult(versionMethod.Key, versionMethod.Select(b => b.Method)); // recompute
              objectContext.VersionResults.AddObject(versionResult);
            }
            resList.Add(versionResult);
          }

          objectContext.SaveChanges();

          foreach (var versionResult in resList)
            objectContext.Detach(versionResult);

#endif
          res = resList.ToArray();
#if false
          var points = objectContext.VersionBindings.Include("Method").GroupBy(b => b.Version, b => b.Method, ChartPoint.Create);
          res = points.OrderBy(m => m.Version).ToArray();
#endif
        }

        connection.Close();
      }

      return res;
    }

    private VersionResult[] GetChartPoints(string FileName)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(FileName));

      CachedChartPoints cachedChartPoints = null;

      if (ChartDataCache == null)
      {
        ChartDataCache = new Dictionary<string, CachedChartPoints>();
      }
      else
      {
        ChartDataCache.TryGetValue(FileName, out cachedChartPoints);
      }
      
      DateTime lastModified;
      try
      {
        lastModified = File.GetLastWriteTime(FileName);
      }
      catch
      {
        return null;
      }

      if (cachedChartPoints == null || cachedChartPoints.CacheTime < lastModified)
      {
        cachedChartPoints = new CachedChartPoints();
        cachedChartPoints.Points = ReadChartPoints(FileName);
        cachedChartPoints.CacheTime = DateTime.Now;

        ChartDataCache[FileName] = cachedChartPoints;
      }

      return cachedChartPoints.Points;
    }
#endif

    private AssemblyEntry SelectedAssembly;
    private string rootPath;

    private void listAssemblies_SelectedIndexChanged(object sender, EventArgs e)
    {
      Contract.Requires(this.chart != null);
      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.splitMain != null);

      var oldSelectedAssembly = this.SelectedAssembly;

      var newSelectedAssembly = this.listAssemblies.SelectedItem as AssemblyEntry;

      if (newSelectedAssembly != null && newSelectedAssembly != oldSelectedAssembly)
      {
        this.SelectedAssembly = newSelectedAssembly;
        UpdateChart();
      }
    }

    private void UpdateChart()
    {
      Contract.Requires(this.chart != null);
      Contract.Requires(this.splitMain != null);

      if (this.splitMain.Panel2Collapsed)
        return;
      if (this.SelectedAssembly == null)
        return;

      //this.chart.DataSource = GetChartPoints(this.SelectedAssembly.FileName);
      //this.chart.DataBind();
    }

    private void UpdateAssembliesList()
    {
      // todo: auto-check of the output directory for new files

      Contract.Requires(this.listAssemblies != null);
      Contract.Requires(this.splitMain != null);
      Contract.Requires(this.outputDirectory != null);

      if (this.splitMain.Panel2Collapsed)
        return;

      const string fileNamePrefix = "cccheck";
      const string fileNameSuffix = ".sdf";
      const string searchString = fileNamePrefix + "*" + fileNameSuffix;

      var selectedAssembly = this.listAssemblies.SelectedItem == null ? null : this.listAssemblies.SelectedItem.ToString();
      var selectedIndex = this.listAssemblies.SelectedIndex;

      var assemblies = new List<AssemblyEntry>();
      var dir = new DirectoryInfo(this.outputDirectory);

      if (dir.Exists)
      {
        foreach (var file in dir.EnumerateFiles(searchString))
        {
          var assemblyName = file.Name.Substring(fileNamePrefix.Length, file.Name.Length - fileNamePrefix.Length - fileNameSuffix.Length);
          assemblies.Add(new AssemblyEntry(file.FullName, assemblyName));
        }
      }

      this.listAssemblies.SuspendLayout();
      this.listAssemblies.DataSource = assemblies;

      int index = ListBox.NoMatches;
      if (selectedAssembly != null)
      {
        index = this.listAssemblies.FindStringExact(selectedAssembly);
      }
      if (index == ListBox.NoMatches && this.SelectedAssembly != null)
      {
        index = this.listAssemblies.FindStringExact(this.SelectedAssembly.AssemblyName);
      }
      if (index == ListBox.NoMatches)
      {
        index = Math.Min(selectedIndex, this.listAssemblies.Items.Count - 1);
      }
      this.listAssemblies.SelectedIndex = index;

      this.listAssemblies.ResumeLayout(false);
    }

    private void listChartTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      Contract.Requires(this.chart != null);
      Contract.Requires(this.chart.ChartAreas != null);
      Contract.Requires(Contract.ForAll<System.Windows.Forms.DataVisualization.Charting.ChartArea>(this.chart.ChartAreas, c => c != null));
      Contract.Requires(this.chart.Legends != null);
      Contract.Requires(Contract.ForAll<System.Windows.Forms.DataVisualization.Charting.Legend>(this.chart.Legends, l => l != null));
      Contract.Requires(this.listChartTypes != null);

      for (int i = 0; i < this.chart.ChartAreas.Count(); i++)
      {
        this.chart.ChartAreas[i].Visible = i == this.listChartTypes.SelectedIndex;
      }
      for (int i = 0; i < this.chart.Legends.Count(); i++)
      {
        this.chart.Legends[i].Enabled = i == this.listChartTypes.SelectedIndex;
      }
    }

    private void listProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.selectedSourceManager != null)
      {
        // Parse the versions
        var firstVersion = this.selectedSourceManager.ParseVersionSpec(this.textFirstVersion.Text);
        var lastVersion = this.selectedSourceManager.ParseVersionSpec(this.textLastVersion.Text);

        this.versions = new List<ISourceManagerVersionSpec>(this.selectedSourceManager.VersionRange(10, lastVersion));
        listVersions.DataSource = this.versions;
      }
    }

    private void listVersions_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void GetVersionsButton_Click(object sender, EventArgs e)
    {
      if (this.selectedSourceManager != null)
      {
        var filter = versionFilterBox.Text;
        // Parse the versions
        var firstVersion = this.selectedSourceManager.ParseVersionSpec(this.textFirstVersion.Text);
        var lastVersion = this.selectedSourceManager.ParseVersionSpec(this.textLastVersion.Text);

        this.versions = new List<ISourceManagerVersionSpec>(this.selectedSourceManager.VersionRange(firstVersion, lastVersion, filter));
        listVersions.DataSource = this.versions;

      }
    }
  }

  class EnumerateVersionsArguments
  {
    public List<ISourceManagerVersionSpec> versions;
    public ProjectEntry[] selectedProjects;
  }

  class EnumerateVersionsStatus
  {
    public ISourceManagerVersionSpec version;
    public string status;
    public int? step;
    public int? numSteps;
    public bool buildFinished;
  }

  class EnumerateVersionsResult
  {
    public bool GetFailed;
  }

  static class XElementExtensions
  {
    static public string PropertyValue(this XElement xe, XName xname)
    {
      Contract.Requires(xe != null);

      var xAttr = xe.Attribute(xname);
      if (xAttr != null)
        return xAttr.Value;
      var xChild = xe.Element(xname);
      if (xChild != null)
        return xChild.Value;
      return null;
    }
  }

  class SourceEntry
  {
    public readonly string Name;
    public readonly string Type;
    public readonly XElement XConfig;
    public readonly IList<ProjectEntry> Projects;

    private SourceEntry(string name, string type, XElement xConfig, IList<ProjectEntry> projects)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(name));
      Contract.Requires(!String.IsNullOrWhiteSpace(type));
      Contract.Requires(xConfig != null);
      Contract.Requires(projects != null);
      Contract.Requires(Contract.ForAll<ProjectEntry>(projects, p => p != null));

      this.Name = name;
      this.Type = type;
      this.XConfig = xConfig;
      this.Projects = projects;
    }

    public static SourceEntry FromXElement(XElement xe)
    {
      Contract.Requires(xe != null);
      Contract.Ensures(Contract.Result<SourceEntry>() != null);

      var name = xe.PropertyValue("Name") ?? "<missing name>";
      var type = xe.PropertyValue("Type") ?? "<missing type>";
      var config = xe.Element("Config") ?? new XElement("Config");
      IList<ProjectEntry> projects = null;
      var xProjects = xe.Element("Projects");
      if (xProjects != null)
      {
        var xxProjects = xProjects.Elements("Project");
//        if (xxProjects != null) // Can't be null because of postcondition
        {
          projects = xxProjects.Select(ProjectEntry.FromXElement).ToList();
        }
      }
      if (projects == null)
      {
        projects = new List<ProjectEntry>();
      }

      return new SourceEntry(name, type, config, projects);
    }

    private ISourceManager sourceManager;
    public ISourceManager SourceManager
    {
      get
      {
        if (this.sourceManager == null)
        {
          var smf = SourceManagerFactories.Find(this.Type);
          if (smf != null)
          {
            this.sourceManager = smf(this.XConfig);
          }
        }
        return this.sourceManager;
      }
    }

    public override string ToString()
    {
      return this.Name;
    }
  }

  class ProjectEntry
  {
    public readonly string Name;
    public readonly string Path;

    private ProjectEntry(string name, string path)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(name));
      Contract.Requires(path != null);

      this.Name = name;
      this.Path = path;
    }

    public static ProjectEntry FromXElement(XElement xe)
    {
      Contract.Requires(xe != null);
      Contract.Ensures(Contract.Result<ProjectEntry>() != null);

      var name = xe.PropertyValue("Name");
      if (String.IsNullOrWhiteSpace(name))
      {
        name = "<missing name>";
      }
      var path = xe.PropertyValue("Path") ?? "";

      return new ProjectEntry(name, path);
    }

    public override string ToString()
    {
      return this.Name;
    }
  }

  class AssemblyEntry
  {
    public readonly string FileName;
    public readonly string AssemblyName;

    public AssemblyEntry(string fileName, string assemblyName)
    {
      Contract.Requires(fileName != null);
      Contract.Requires(assemblyName != null);

      this.FileName = fileName;
      this.AssemblyName = assemblyName;
    }
    public override string ToString()
    {
      return this.AssemblyName;
    }
  }
}

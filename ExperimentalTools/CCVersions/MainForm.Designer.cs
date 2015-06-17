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

namespace CCVersions
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      System.Windows.Forms.GroupBox groupProjects;
      System.Windows.Forms.GroupBox groupSources;
      System.Windows.Forms.Label label3;
      System.Windows.Forms.Label label1;
      System.Windows.Forms.Label label2;
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.listProjects = new System.Windows.Forms.ListBox();
      this.listSources = new System.Windows.Forms.ListBox();
      this.backgroundEnumerateVersions = new System.ComponentModel.BackgroundWorker();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonCharts = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.progressEnumerateVersions = new System.Windows.Forms.ProgressBar();
      this.labelStatus = new System.Windows.Forms.Label();
      this.textFirstVersion = new System.Windows.Forms.TextBox();
      this.textLastVersion = new System.Windows.Forms.TextBox();
      this.buttonDoIt = new System.Windows.Forms.Button();
      this.labelVersion = new System.Windows.Forms.Label();
      this.versionsGroup = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.versionFilterBox = new System.Windows.Forms.TextBox();
      this.listVersions = new System.Windows.Forms.ListBox();
      this.splitRight = new System.Windows.Forms.SplitContainer();
      this.splitTopRight = new System.Windows.Forms.SplitContainer();
      this.groupAssemblies = new System.Windows.Forms.GroupBox();
      this.listAssemblies = new System.Windows.Forms.ListBox();
      this.groupChartTypes = new System.Windows.Forms.GroupBox();
      this.listChartTypes = new System.Windows.Forms.ListBox();
      this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.GetVersionsButton = new System.Windows.Forms.Button();
      groupProjects = new System.Windows.Forms.GroupBox();
      groupSources = new System.Windows.Forms.GroupBox();
      label3 = new System.Windows.Forms.Label();
      label1 = new System.Windows.Forms.Label();
      label2 = new System.Windows.Forms.Label();
      groupProjects.SuspendLayout();
      groupSources.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.tableLayoutPanelLeft.SuspendLayout();
      this.panel1.SuspendLayout();
      this.versionsGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
      this.splitRight.Panel1.SuspendLayout();
      this.splitRight.Panel2.SuspendLayout();
      this.splitRight.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitTopRight)).BeginInit();
      this.splitTopRight.Panel1.SuspendLayout();
      this.splitTopRight.Panel2.SuspendLayout();
      this.splitTopRight.SuspendLayout();
      this.groupAssemblies.SuspendLayout();
      this.groupChartTypes.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
      this.SuspendLayout();
      // 
      // groupProjects
      // 
      groupProjects.Controls.Add(this.listProjects);
      groupProjects.Dock = System.Windows.Forms.DockStyle.Fill;
      groupProjects.Location = new System.Drawing.Point(3, 148);
      groupProjects.Name = "groupProjects";
      groupProjects.Size = new System.Drawing.Size(544, 140);
      groupProjects.TabIndex = 2;
      groupProjects.TabStop = false;
      groupProjects.Text = "Projects/Solutions";
      // 
      // listProjects
      // 
      this.listProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listProjects.DisplayMember = "Id";
      this.listProjects.FormattingEnabled = true;
      this.listProjects.IntegralHeight = false;
      this.listProjects.Location = new System.Drawing.Point(9, 19);
      this.listProjects.Name = "listProjects";
      this.listProjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listProjects.Size = new System.Drawing.Size(526, 115);
      this.listProjects.TabIndex = 2;
      this.listProjects.ValueMember = "Id";
      this.listProjects.SelectedIndexChanged += new System.EventHandler(this.listProjects_SelectedIndexChanged);
      // 
      // groupSources
      // 
      groupSources.Controls.Add(this.listSources);
      groupSources.Dock = System.Windows.Forms.DockStyle.Fill;
      groupSources.Location = new System.Drawing.Point(3, 3);
      groupSources.Name = "groupSources";
      groupSources.Size = new System.Drawing.Size(544, 139);
      groupSources.TabIndex = 1;
      groupSources.TabStop = false;
      groupSources.Text = "Sources";
      // 
      // listSources
      // 
      this.listSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listSources.DisplayMember = "Id";
      this.listSources.FormattingEnabled = true;
      this.listSources.IntegralHeight = false;
      this.listSources.Location = new System.Drawing.Point(9, 19);
      this.listSources.Name = "listSources";
      this.listSources.Size = new System.Drawing.Size(526, 114);
      this.listSources.TabIndex = 1;
      this.listSources.ValueMember = "Id";
      this.listSources.SelectedIndexChanged += new System.EventHandler(this.listSources_SelectedIndexChanged);
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new System.Drawing.Point(9, 44);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(45, 13);
      label3.TabIndex = 26;
      label3.Text = "Version:";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(6, 15);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(67, 13);
      label1.TabIndex = 23;
      label1.Text = "From version";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new System.Drawing.Point(240, 15);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(16, 13);
      label2.TabIndex = 21;
      label2.Text = "to";
      // 
      // backgroundEnumerateVersions
      // 
      this.backgroundEnumerateVersions.WorkerReportsProgress = true;
      this.backgroundEnumerateVersions.WorkerSupportsCancellation = true;
      this.backgroundEnumerateVersions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundEnumerateVersions_DoWork);
      this.backgroundEnumerateVersions.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundEnumerateVersions_ProgressChanged);
      this.backgroundEnumerateVersions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundEnumerateVersions_RunWorkerCompleted);
      // 
      // splitMain
      // 
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(0, 0);
      this.splitMain.Name = "splitMain";
      // 
      // splitMain.Panel1
      // 
      this.splitMain.Panel1.Controls.Add(this.tableLayoutPanelLeft);
      this.splitMain.Panel1MinSize = 550;
      // 
      // splitMain.Panel2
      // 
      this.splitMain.Panel2.Controls.Add(this.splitRight);
      this.splitMain.Panel2MinSize = 100;
      this.splitMain.Size = new System.Drawing.Size(1396, 809);
      this.splitMain.SplitterDistance = 550;
      this.splitMain.TabIndex = 0;
      // 
      // tableLayoutPanelLeft
      // 
      this.tableLayoutPanelLeft.ColumnCount = 1;
      this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanelLeft.Controls.Add(groupProjects, 0, 1);
      this.tableLayoutPanelLeft.Controls.Add(groupSources, 0, 0);
      this.tableLayoutPanelLeft.Controls.Add(this.panel1, 0, 3);
      this.tableLayoutPanelLeft.Controls.Add(this.versionsGroup, 0, 2);
      this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
      this.tableLayoutPanelLeft.RowCount = 4;
      this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.01151F));
      this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.28242F));
      this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.70606F));
      this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
      this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanelLeft.Size = new System.Drawing.Size(550, 809);
      this.tableLayoutPanelLeft.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonCharts);
      this.panel1.Controls.Add(this.buttonCancel);
      this.panel1.Controls.Add(label3);
      this.panel1.Controls.Add(this.progressEnumerateVersions);
      this.panel1.Controls.Add(this.labelStatus);
      this.panel1.Controls.Add(label1);
      this.panel1.Controls.Add(this.textFirstVersion);
      this.panel1.Controls.Add(label2);
      this.panel1.Controls.Add(this.textLastVersion);
      this.panel1.Controls.Add(this.buttonDoIt);
      this.panel1.Controls.Add(this.labelVersion);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 661);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(544, 145);
      this.panel1.TabIndex = 3;
      // 
      // buttonCharts
      // 
      this.buttonCharts.Location = new System.Drawing.Point(444, 68);
      this.buttonCharts.Name = "buttonCharts";
      this.buttonCharts.Size = new System.Drawing.Size(75, 23);
      this.buttonCharts.TabIndex = 7;
      this.buttonCharts.Text = "Charts";
      this.buttonCharts.UseVisualStyleBackColor = true;
      this.buttonCharts.Click += new System.EventHandler(this.buttonCharts_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Enabled = false;
      this.buttonCancel.Location = new System.Drawing.Point(444, 39);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 6;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // progressEnumerateVersions
      // 
      this.progressEnumerateVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressEnumerateVersions.Location = new System.Drawing.Point(9, 108);
      this.progressEnumerateVersions.Name = "progressEnumerateVersions";
      this.progressEnumerateVersions.Size = new System.Drawing.Size(526, 23);
      this.progressEnumerateVersions.TabIndex = 8;
      // 
      // labelStatus
      // 
      this.labelStatus.AutoSize = true;
      this.labelStatus.Location = new System.Drawing.Point(73, 68);
      this.labelStatus.Name = "labelStatus";
      this.labelStatus.Size = new System.Drawing.Size(0, 13);
      this.labelStatus.TabIndex = 24;
      // 
      // textFirstVersion
      // 
      this.textFirstVersion.Location = new System.Drawing.Point(76, 12);
      this.textFirstVersion.Name = "textFirstVersion";
      this.textFirstVersion.Size = new System.Drawing.Size(143, 20);
      this.textFirstVersion.TabIndex = 3;
      // 
      // textLastVersion
      // 
      this.textLastVersion.Location = new System.Drawing.Point(272, 12);
      this.textLastVersion.Name = "textLastVersion";
      this.textLastVersion.Size = new System.Drawing.Size(143, 20);
      this.textLastVersion.TabIndex = 4;
      // 
      // buttonDoIt
      // 
      this.buttonDoIt.Location = new System.Drawing.Point(444, 10);
      this.buttonDoIt.Name = "buttonDoIt";
      this.buttonDoIt.Size = new System.Drawing.Size(75, 23);
      this.buttonDoIt.TabIndex = 5;
      this.buttonDoIt.Text = "Do it!";
      this.buttonDoIt.UseVisualStyleBackColor = true;
      this.buttonDoIt.Click += new System.EventHandler(this.buttonDoIt_Click);
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Location = new System.Drawing.Point(73, 44);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(0, 13);
      this.labelVersion.TabIndex = 18;
      // 
      // versionsGroup
      // 
      this.versionsGroup.Controls.Add(this.GetVersionsButton);
      this.versionsGroup.Controls.Add(this.label4);
      this.versionsGroup.Controls.Add(this.versionFilterBox);
      this.versionsGroup.Controls.Add(this.listVersions);
      this.versionsGroup.Location = new System.Drawing.Point(3, 294);
      this.versionsGroup.Name = "versionsGroup";
      this.versionsGroup.Size = new System.Drawing.Size(544, 361);
      this.versionsGroup.TabIndex = 4;
      this.versionsGroup.TabStop = false;
      this.versionsGroup.Text = "Versions";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(14, 23);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "Filter";
      // 
      // versionFilterBox
      // 
      this.versionFilterBox.Location = new System.Drawing.Point(49, 20);
      this.versionFilterBox.Name = "versionFilterBox";
      this.versionFilterBox.Size = new System.Drawing.Size(276, 20);
      this.versionFilterBox.TabIndex = 1;
      // 
      // listVersions
      // 
      this.listVersions.FormattingEnabled = true;
      this.listVersions.Location = new System.Drawing.Point(12, 45);
      this.listVersions.Name = "listVersions";
      this.listVersions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
      this.listVersions.Size = new System.Drawing.Size(523, 303);
      this.listVersions.TabIndex = 0;
      this.listVersions.SelectedIndexChanged += new System.EventHandler(this.listVersions_SelectedIndexChanged);
      // 
      // splitRight
      // 
      this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitRight.Location = new System.Drawing.Point(0, 0);
      this.splitRight.Name = "splitRight";
      this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitRight.Panel1
      // 
      this.splitRight.Panel1.Controls.Add(this.splitTopRight);
      this.splitRight.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
      // 
      // splitRight.Panel2
      // 
      this.splitRight.Panel2.Controls.Add(this.chart);
      this.splitRight.Size = new System.Drawing.Size(842, 809);
      this.splitRight.SplitterDistance = 164;
      this.splitRight.TabIndex = 9;
      // 
      // splitTopRight
      // 
      this.splitTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitTopRight.Location = new System.Drawing.Point(0, 0);
      this.splitTopRight.Name = "splitTopRight";
      // 
      // splitTopRight.Panel1
      // 
      this.splitTopRight.Panel1.Controls.Add(this.groupAssemblies);
      this.splitTopRight.Panel1MinSize = 50;
      // 
      // splitTopRight.Panel2
      // 
      this.splitTopRight.Panel2.Controls.Add(this.groupChartTypes);
      this.splitTopRight.Panel2MinSize = 50;
      this.splitTopRight.Size = new System.Drawing.Size(838, 164);
      this.splitTopRight.SplitterDistance = 426;
      this.splitTopRight.TabIndex = 10;
      // 
      // groupAssemblies
      // 
      this.groupAssemblies.Controls.Add(this.listAssemblies);
      this.groupAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupAssemblies.Location = new System.Drawing.Point(0, 0);
      this.groupAssemblies.Name = "groupAssemblies";
      this.groupAssemblies.Size = new System.Drawing.Size(426, 164);
      this.groupAssemblies.TabIndex = 11;
      this.groupAssemblies.TabStop = false;
      this.groupAssemblies.Text = "Assemblies";
      // 
      // listAssemblies
      // 
      this.listAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listAssemblies.DisplayMember = "AssemblyName";
      this.listAssemblies.FormattingEnabled = true;
      this.listAssemblies.IntegralHeight = false;
      this.listAssemblies.Location = new System.Drawing.Point(6, 22);
      this.listAssemblies.Name = "listAssemblies";
      this.listAssemblies.Size = new System.Drawing.Size(414, 139);
      this.listAssemblies.TabIndex = 11;
      this.listAssemblies.ValueMember = "FileName";
      this.listAssemblies.SelectedIndexChanged += new System.EventHandler(this.listAssemblies_SelectedIndexChanged);
      // 
      // groupChartTypes
      // 
      this.groupChartTypes.Controls.Add(this.listChartTypes);
      this.groupChartTypes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupChartTypes.Location = new System.Drawing.Point(0, 0);
      this.groupChartTypes.Name = "groupChartTypes";
      this.groupChartTypes.Size = new System.Drawing.Size(408, 164);
      this.groupChartTypes.TabIndex = 12;
      this.groupChartTypes.TabStop = false;
      this.groupChartTypes.Text = "Chart Types";
      // 
      // listChartTypes
      // 
      this.listChartTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listChartTypes.FormattingEnabled = true;
      this.listChartTypes.IntegralHeight = false;
      this.listChartTypes.Items.AddRange(new object[] {
            "Ok, filtered, warnings",
            "Contracts",
            "Contracts per method",
            "Contract density",
            "Ok, filtered, warnings (normalized)"});
      this.listChartTypes.Location = new System.Drawing.Point(6, 22);
      this.listChartTypes.Name = "listChartTypes";
      this.listChartTypes.Size = new System.Drawing.Size(396, 139);
      this.listChartTypes.TabIndex = 12;
      this.listChartTypes.SelectedIndexChanged += new System.EventHandler(this.listChartTypes_SelectedIndexChanged);
      // 
      // chart
      // 
      chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea1.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea1.AxisY2.MajorGrid.Enabled = false;
      chartArea1.Name = "Stacked3";
      chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea2.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea2.AxisY2.MajorGrid.Enabled = false;
      chartArea2.Name = "Contracts";
      chartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea3.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea3.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea3.AxisY2.MajorGrid.Enabled = false;
      chartArea3.Name = "ContractsPerMethod";
      chartArea4.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea4.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea4.AxisX2.MajorGrid.Enabled = false;
      chartArea4.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea4.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea4.AxisY2.MajorGrid.Enabled = false;
      chartArea4.Name = "ContractDensity";
      chartArea5.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea5.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea5.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
      chartArea5.AxisY2.MajorGrid.Enabled = false;
      chartArea5.Name = "Stacked3norm";
      this.chart.ChartAreas.Add(chartArea1);
      this.chart.ChartAreas.Add(chartArea2);
      this.chart.ChartAreas.Add(chartArea3);
      this.chart.ChartAreas.Add(chartArea4);
      this.chart.ChartAreas.Add(chartArea5);
      this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
      legend1.Alignment = System.Drawing.StringAlignment.Center;
      legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
      legend1.Name = "Stacked3";
      legend2.Alignment = System.Drawing.StringAlignment.Center;
      legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend2.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
      legend2.Name = "Contracts";
      legend3.Alignment = System.Drawing.StringAlignment.Center;
      legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend3.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
      legend3.Name = "ContractsPerMethod";
      legend4.Alignment = System.Drawing.StringAlignment.Center;
      legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend4.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
      legend4.Name = "ContractDensity";
      legend5.Alignment = System.Drawing.StringAlignment.Center;
      legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend5.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
      legend5.Name = "Stacked3norm";
      this.chart.Legends.Add(legend1);
      this.chart.Legends.Add(legend2);
      this.chart.Legends.Add(legend3);
      this.chart.Legends.Add(legend4);
      this.chart.Legends.Add(legend5);
      this.chart.Location = new System.Drawing.Point(0, 0);
      this.chart.Name = "chart";
      series1.ChartArea = "Stacked3";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
      series1.CustomProperties = "PointWidth=1";
      series1.IsXValueIndexed = true;
      series1.Legend = "Stacked3";
      series1.LegendText = "OK";
      series1.Name = "Stacked3_StatsTrue";
      series1.XValueMember = "Version";
      series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series1.YValueMembers = "StatsTrue";
      series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      series2.ChartArea = "Stacked3";
      series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
      series2.CustomProperties = "PointWidth=1";
      series2.IsXValueIndexed = true;
      series2.Legend = "Stacked3";
      series2.LegendText = "Filtered";
      series2.Name = "Stacked3_SwallowedWarnings";
      series2.XValueMember = "Version";
      series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series2.YValueMembers = "SwallowedWarnings";
      series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      series3.ChartArea = "Stacked3";
      series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
      series3.CustomProperties = "PointWidth=1";
      series3.IsXValueIndexed = true;
      series3.Legend = "Stacked3";
      series3.LegendText = "Warnings";
      series3.Name = "Stacked3_DisplayedWarnings";
      series3.XValueMember = "Version";
      series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series3.YValueMembers = "DisplayedWarnings";
      series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      series4.BorderWidth = 3;
      series4.ChartArea = "Stacked3";
      series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series4.Color = System.Drawing.Color.SteelBlue;
      series4.IsXValueIndexed = true;
      series4.Legend = "Stacked3";
      series4.LegendText = "Methods";
      series4.Name = "Stacked3_Methods";
      series4.XValueMember = "Version";
      series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
      series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
      series4.YValueMembers = "Methods";
      series5.BorderWidth = 3;
      series5.ChartArea = "ContractsPerMethod";
      series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series5.Color = System.Drawing.Color.SteelBlue;
      series5.IsXValueIndexed = true;
      series5.Legend = "ContractsPerMethod";
      series5.LegendText = "Methods";
      series5.Name = "ContractsPerMethod_Methods";
      series5.XValueMember = "Version";
      series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
      series5.YValueMembers = "Methods";
      series6.BorderWidth = 3;
      series6.ChartArea = "ContractsPerMethod";
      series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      series6.IsXValueIndexed = true;
      series6.Legend = "ContractsPerMethod";
      series6.LegendText = "Contracts per method";
      series6.Name = "ContractsPerMethod";
      series6.XValueMember = "Version";
      series6.YValueMembers = "ContractsPerMethod";
      series7.BorderWidth = 3;
      series7.ChartArea = "Contracts";
      series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series7.Color = System.Drawing.Color.SteelBlue;
      series7.IsXValueIndexed = true;
      series7.Legend = "Contracts";
      series7.LegendText = "Methods";
      series7.Name = "Contracts_Methods";
      series7.XValueMember = "Version";
      series7.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
      series7.YValueMembers = "Methods";
      series8.BorderWidth = 3;
      series8.ChartArea = "Contracts";
      series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series8.Color = System.Drawing.Color.SeaGreen;
      series8.IsXValueIndexed = true;
      series8.Legend = "Contracts";
      series8.LegendText = "Contracts";
      series8.Name = "Contracts_Contracts";
      series8.XValueMember = "Version";
      series8.YValueMembers = "Contracts";
      series9.BorderWidth = 3;
      series9.ChartArea = "ContractDensity";
      series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series9.Color = System.Drawing.Color.SaddleBrown;
      series9.IsXValueIndexed = true;
      series9.Legend = "ContractDensity";
      series9.LegendText = "Contract density";
      series9.Name = "ContractDensity";
      series9.XValueMember = "Version";
      series9.YValueMembers = "ContractDensity";
      series10.ChartArea = "Stacked3norm";
      series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
      series10.Color = System.Drawing.Color.DodgerBlue;
      series10.CustomProperties = "PointWidth=1";
      series10.IsXValueIndexed = true;
      series10.Legend = "Stacked3norm";
      series10.LegendText = "OK";
      series10.Name = "Stacked3norm_StatsTrue";
      series10.XValueMember = "Version";
      series10.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series10.YValueMembers = "StatsTrue";
      series10.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      series11.ChartArea = "Stacked3norm";
      series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
      series11.Color = System.Drawing.Color.Orange;
      series11.CustomProperties = "PointWidth=1";
      series11.IsXValueIndexed = true;
      series11.Legend = "Stacked3norm";
      series11.LegendText = "Filtered";
      series11.Name = "Stacked3norm_SwallowedWarnings";
      series11.XValueMember = "Version";
      series11.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series11.YValueMembers = "SwallowedWarnings";
      series11.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      series12.ChartArea = "Stacked3norm";
      series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
      series12.Color = System.Drawing.Color.OrangeRed;
      series12.CustomProperties = "PointWidth=1";
      series12.IsXValueIndexed = true;
      series12.Legend = "Stacked3norm";
      series12.LegendText = "Warnings";
      series12.Name = "Stacked3norm_DisplayedWarnings";
      series12.XValueMember = "Version";
      series12.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
      series12.YValueMembers = "DisplayedWarnings";
      series12.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
      this.chart.Series.Add(series1);
      this.chart.Series.Add(series2);
      this.chart.Series.Add(series3);
      this.chart.Series.Add(series4);
      this.chart.Series.Add(series5);
      this.chart.Series.Add(series6);
      this.chart.Series.Add(series7);
      this.chart.Series.Add(series8);
      this.chart.Series.Add(series9);
      this.chart.Series.Add(series10);
      this.chart.Series.Add(series11);
      this.chart.Series.Add(series12);
      this.chart.Size = new System.Drawing.Size(842, 641);
      this.chart.TabIndex = 13;
      // 
      // GetVersionsButton
      // 
      this.GetVersionsButton.Location = new System.Drawing.Point(331, 17);
      this.GetVersionsButton.Name = "GetVersionsButton";
      this.GetVersionsButton.Size = new System.Drawing.Size(75, 23);
      this.GetVersionsButton.TabIndex = 3;
      this.GetVersionsButton.Text = "Get Versions";
      this.GetVersionsButton.UseVisualStyleBackColor = true;
      this.GetVersionsButton.Click += new System.EventHandler(this.GetVersionsButton_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1396, 809);
      this.Controls.Add(this.splitMain);
      this.MinimumSize = new System.Drawing.Size(600, 300);
      this.Name = "MainForm";
      this.Text = "CCVersions";
      this.Load += new System.EventHandler(this.MainForm_Load);
      groupProjects.ResumeLayout(false);
      groupSources.ResumeLayout(false);
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.tableLayoutPanelLeft.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.versionsGroup.ResumeLayout(false);
      this.versionsGroup.PerformLayout();
      this.splitRight.Panel1.ResumeLayout(false);
      this.splitRight.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
      this.splitRight.ResumeLayout(false);
      this.splitTopRight.Panel1.ResumeLayout(false);
      this.splitTopRight.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitTopRight)).EndInit();
      this.splitTopRight.ResumeLayout(false);
      this.groupAssemblies.ResumeLayout(false);
      this.groupChartTypes.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundEnumerateVersions;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.ListBox listProjects;
        private System.Windows.Forms.ListBox listSources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressEnumerateVersions;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textFirstVersion;
        private System.Windows.Forms.TextBox textLastVersion;
        private System.Windows.Forms.Button buttonDoIt;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.Button buttonCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.SplitContainer splitTopRight;
        private System.Windows.Forms.GroupBox groupAssemblies;
        private System.Windows.Forms.ListBox listAssemblies;
        private System.Windows.Forms.GroupBox groupChartTypes;
        private System.Windows.Forms.ListBox listChartTypes;
        private System.Windows.Forms.GroupBox versionsGroup;
        private System.Windows.Forms.ListBox listVersions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox versionFilterBox;
        private System.Windows.Forms.Button GetVersionsButton;
    }
}


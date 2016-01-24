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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CloudotControlCenter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    delegate void initUI();

    public MainWindow()
    {
      InitializeComponent();

      InitializeDataStructures();
    }

    private void InitializeDataStructures()
    {
      this.TextBoxForCloudotServiceAddress.Text = "net.tcp://cloudotserver:9922/ClousotService/";
      //  "<FlowDocument><Paragraph>This is a richTextBox. And this is a <Hyperlink NavigateUri=\"http://www.microsoft.com\">Hyperlink</Hyperlink>.</Paragraph></FlowDocument>"; 

      /*
      var para = new Paragraph();
      para.Inlines.Add(new Run("Sito web di MSR "));
      para.Inlines.Add(new Hyperlink(new Run("http://research.microsoft.com")));
      this.TextBoxForLogging.Document = new FlowDocument(para);

      para = new Paragraph();
      para.Inlines.Add(new Run("Sito web di MSR 2"));
      para.Inlines.Add(new Hyperlink(new Run("http://research.microsoft.com/~logozzo")));
      
      this.TextBoxForLogging.Document.Blocks.Add(para);
      */

      // F: I do not understand why C# does not allow me a lambda here
      this.Dispatcher.BeginInvoke((initUI)delegate()
        {
          var data = CloudotLogFileView.GetFromXML();
          this.AnalysesDataGrid.ItemsSource = data;
          this.LabelForAnalyses.Content = string.Format("Analyses {0}({1} entries)", Environment.NewLine, this.AnalysesDataGrid.Items.Count); 
        }
        );
    }

    private void AnalyzeSelectedEntry(object sender, RoutedEventArgs e)
    {
      var entry = this.AnalysesDataGrid.SelectedItem as CloudotLogFileView;

      if (entry != null) // Sanity check...
      {
        AnalyzeSelectedItem(entry);
      }
    }
    private void AnalysesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var element = e.MouseDevice.DirectlyOver as FrameworkElement;
      if (element != null)
      {
        var gridCell = element.Parent as DataGridCell;
        if(gridCell != null)
        {
          //AnalyzeSelectedItem(gridCell as CloudotLogFileView);

        }
      }
    }
    private void AnalyzeSelectedItem(CloudotLogFileView entry)
    {
      if (entry == null)
      {
        return;
      }
      this.AddLineToLog("Request the analysis of {0} to Cloudot", entry.Assembly);

      CloudotInteraction.FireAnalysisAndForget(TextBoxForCloudotServiceAddress.Text,
        entry.Assembly, CloudotLogFileView.String2CmdLine(entry.CommandLine), AddLineToLog);
    }

    #region Write to the Log space
    [ContractVerification(false)]
    public void AddLineToLog(string str)
    {
      this.Dispatcher.BeginInvoke((Action)
          (() => this.TextBoxForLogging.AppendText(string.Format("[{0}] {1}{2}", DateTime.Now, str, Environment.NewLine)))
        );
    }

/*    public void AddLineToLog(Paragraph par)
    {
      this.Dispatcher.BeginInvoke((Action)(
        () => 
          {
            var newPar = new Paragraph();
            newPar.Inlines.Add(new Run(string.Format("[{0}]", DateTime.Now.ToString())));
            newPar.Inlines.AddRange(par.Inlines);
            this.TextBoxForLogging.Document.Blocks.Add(newPar);
          }
        ));
    }
    */
    [ContractVerification(false)]
    public object RunInUIThread(Func<object> action)
    {
      object result = default(object);
      this.Dispatcher.BeginInvoke((Action) (() => result = action()));
      return result;
    }

    public void AddLineToLog(string format, params string[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      this.AddLineToLog(string.Format(format, args));
    }
    #endregion


  }
}

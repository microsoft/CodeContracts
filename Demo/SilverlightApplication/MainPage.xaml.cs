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
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics.Contracts;

namespace SilverlightApplication
{
  public partial class MainPage : UserControl
  {
    public MainPage()
    {
      InitializeComponent();
    }

    int count;
    private void button1_Click(object sender, RoutedEventArgs e)
    {

#if !BAD
      DoSomething(sender, e);
#else
      if (count < 4)
      {
        DoSomething(sender, e);
      }
      else
      {
        var button = (System.Windows.Controls.Button)sender;
        button.Content = "Clicked many";
      }
#endif
     // DoSomething(null, e);
    }

    private void DoSomething(object sender, RoutedEventArgs e)
    {
      Contract.Requires(sender != null);
      Contract.Requires(e != null);
      Contract.Requires(count < 4);
      Contract.Ensures(count < 5);

      count++;
      var button = (System.Windows.Controls.Button)sender;
      button.Content = "Clicked " + count;

    }
  }
}

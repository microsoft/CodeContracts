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
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using System.Net;
using System.Media;
using System.IO;
using System.Diagnostics.Contracts;

namespace Ding
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    struct Counts
    {
      public int numUsers;
      public int numRuns;
      public int numFail;

      public static Counts operator -(Counts a, Counts b) {
        return new Counts { numUsers = a.numUsers - b.numUsers, numRuns = a.numRuns - b.numRuns, numFail = a.numFail - b.numFail };
      }
    }

    Counts counts;
    Counts latestDiffs;

    int artificialCount;
    Timer timer;
    SoundPlayer ding;

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
    }

    void InitCounter()
    {
    }

    private Counts GetCurrentCodeContractCounters()
    {
      var leaderboardEntities = new LeaderboardDataService.LeaderboardEntitySet(new Uri("http://leaderboard/odata/LeaderboardDataService.svc"));
      leaderboardEntities.Credentials = CredentialCache.DefaultCredentials;
      int users = artificialCount;
      var runs = artificialCount;
      var fail = artificialCount;
      try
      {
        IEnumerable<int> numUsers = leaderboardEntities.Execute<int>(new Uri("http://leaderboard/odata/LeaderboardDataService.svc/GetNumUsersByID?appId=555555"));
        users += numUsers.FirstOrDefault();

        var allUses = (from agg in leaderboardEntities.UserAggregations
                      where agg.appid == 555555
                      select agg).ToList();
        runs += allUses.Sum(agg => agg.uses);

        fail += allUses.Sum(agg => (agg.featureid < 0 || agg.featureid > 4000000) ? agg.uses : 0); 
      }
      catch { }
      return new Counts { numUsers = users, numFail = fail, numRuns = runs };
    }

    private void StartTimer()
    {
      timer = new Timer(20000);
      timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
      timer.AutoReset = true;
      timer.Enabled = true;
    }

    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      UpdateCounters();

    }

    private void UpdateCounters()
    {
      UIPost(ShowUpdating);
      try
      {
        var newCounts = GetCurrentCodeContractCounters();
        if (this.counts.numRuns == 0)
        {
          this.counts.numRuns = newCounts.numRuns;
          PostUpdateCount(); // likely startup had timeout, so this is startup
        }
        if (this.counts.numUsers == 0)
        {
          this.counts.numUsers = newCounts.numUsers;
          PostUpdateCount(); // likely startup had timeout, so this is startup
        }
        if (this.counts.numFail == 0)
        {
          this.counts.numFail = newCounts.numFail;
          PostUpdateCount(); // likely startup had timeout, so this is startup
        }
        var diffs = this.latestDiffs = newCounts - this.counts;
        if (diffs.numRuns > 0)
        {
          this.counts.numRuns = newCounts.numRuns;
          PostUpdateCount();
          PlayWavResource("notify.wav", true);
        }
        while (diffs.numUsers > 0)
        {
          this.counts.numUsers++;
          diffs.numUsers--;
          PostUpdateCount();
          PlayWavResource("tada.wav", true);
        }
        if (diffs.numFail > 0)
        {
          this.counts.numFail = newCounts.numFail;
          PostUpdateCount();
          PlayWavResource("fail.wav", true);
        }
      }
      finally
      {
        UIPost(HideUpdating);
      }
    }

    private void ShowUpdating()
    {
      this.updatingLabel.Visibility = System.Windows.Visibility.Visible;
    }

    private void HideUpdating()
    {
      this.updatingLabel.Visibility = System.Windows.Visibility.Hidden;
    }

    private void UIPost(Action action) {
      this.UserCount.Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
    }

    private void PostUpdateCount()
    {
      this.UserCount.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(UpdateCounts));
    }

    void UpdateCounts() {
      this.UserCount.Content = this.counts.numUsers.ToString();
      this.RunCount.Content = this.counts.numRuns.ToString();
      this.FailureCount.Content = this.counts.numFail.ToString();
      UpdateDiff(this.UserCountDiff, this.latestDiffs.numUsers);
      UpdateDiff(this.RunCountDiff, this.latestDiffs.numRuns);
      UpdateDiff(this.FailCountDiff, this.latestDiffs.numFail);
    }

    private void UpdateDiff(Label label, int diff)
    {
      Contract.Requires(label != null);

      if (diff > 0)
      {
        label.Content = "+" + diff.ToString();
      }
      else
      {
        label.Content = "";
      }
    }

    private void ForceUpdate(object sender, RoutedEventArgs e)
    {
      this.artificialCount++;
      timer_Elapsed(null, null); //cheat
    }

    public void PlayWavResource(string wav, bool sync)
    {
      // get the namespace 
      string strNameSpace =
      System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();

      // get the resource into a stream
      Stream str =
      System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(strNameSpace + "." + wav);
      if (str == null)
        return;

      this.ding.Stop();
      this.ding.Stream = str;
      if (sync)
      {
        ding.PlaySync();
      }
      else
      {
        ding.Play();
      }
    }

    private void OnInitialized(object sender, EventArgs e)
    {
      ding = new SoundPlayer();
      InitCounter();
      UpdateCounters();
      StartTimer();
    }


  }
}

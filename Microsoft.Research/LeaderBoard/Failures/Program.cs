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
using System.Net;

namespace LeaderBoardAccess
{
  class Options : Microsoft.Research.DataStructures.OptionParsing
  {
    public enum Feature
    {
      fail,
      cccheck,
      ccdoc,
      ccrewrite,
    }

    public Feature feature = Feature.fail;

  }

  class Program
  {
    static void Main(string[] args)
    {
      var options = new Options();
      options.Parse(args);
      if (options.HasErrors) {
        options.PrintErrorsAndExit(Console.Out);
      }
      if (options.HelpRequested || options.GeneralArguments.Count > 0)
      {
        options.PrintOptions("");
        Environment.Exit(0);
      }



      LeaderboardDataService.LeaderboardEntitySet leaderboardEntities = new LeaderboardDataService.LeaderboardEntitySet(new Uri("http://leaderboard/odata/LeaderboardDataService.svc"));
      leaderboardEntities.Credentials = CredentialCache.DefaultCredentials;

      var allUses = (from agg in leaderboardEntities.UserAggregations
                       where agg.appid == 555555
                       select agg).ToList();

      var failures = from fail in allUses
                     where FeatureMatch(fail.featureid, options.feature)
                     select fail;

      foreach (var failure in failures)
      {
        Console.WriteLine("User {0} used feature {3} {1} times (code {2})", failure.userid, failure.uses, failure.featureid, options.feature);
        if (failure.featureid < 0) { DecodeFailure(failure.featureid); }
      }
    }

    private static bool FeatureMatch(int featureid, Options.Feature requested)
    {
      switch (requested)
      {
        case Options.Feature.fail:
          return (featureid < 0 || featureid > 40000000);

        case Options.Feature.ccrewrite:
          return 
            (((featureid >> 12) & 7) == 1);

        case Options.Feature.ccdoc:
          return
            (((featureid >> 12) & 7) == 2);

        case Options.Feature.cccheck:
          return
            (((featureid >> 12) & 7) == 3);

        default:
          return false;
      }
    }

    private static void DecodeFailure(int failure)
    {
      failure   = -failure;
      var tool  = failure / 100000000;
      failure   = failure % 100000000;
      var major = failure / 10000000;
      failure   = failure % 10000000;
      var minor = failure / 1000000;
      failure   = failure % 1000000;
      var build = failure / 10;
      var revision = failure % 10;

      var toolString = tool == 1 ? "ccrewrite" : (tool == 2) ? "ccdoc" : (tool == 3) ? "cccheck" : "unknown tool";

      Console.WriteLine("  {0} version {1}.{2}.{3}.{4}", toolString, major, minor, build, revision);

    }

  }
}

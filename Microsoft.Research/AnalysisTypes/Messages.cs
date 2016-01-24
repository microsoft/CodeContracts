// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Research.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Microsoft.Research.ClousotPulse.Messages
{
    public class CommonNames
    {
        private const string PIPENAME = "CCCHECKPulsePipe";
        private const string PIPENAME_CALLBACK = "CCCHECKPulsePipeForCallback";

        [Pure]
        public static string GetPipeNameForCCCheckPulse()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return GetPipeNameForCCCheckPulse(Process.GetCurrentProcess().Id);
        }

        [Pure]
        public static string GetPipeNameForCCCheckPulse(int procID)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return PIPENAME + procID;
        }

        [Pure]
        public static string GetPipeNameForCCCheckPulseCallBack(int procID)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return PIPENAME_CALLBACK + procID;
        }
    }

    public class PipeStreamSimple
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(formatter != null);
        }

        private readonly PipeStream stream;
        private readonly IFormatter formatter;

        public PipeStreamSimple(PipeStream stream)
        {
            Contract.Requires(stream != null);

            this.stream = stream;
            formatter = new BinaryFormatter();
        }

        public bool TryRead<T>(out T result)
        {
            try
            {
                if (stream.IsConnected)
                {
                    var tmp = formatter.Deserialize(stream);
                    if (tmp is T)
                    {
                        result = (T)tmp;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("error in reading from the stream: {0}", e.Message);
                // do nothing?
            }

            result = default(T);
            return false;
        }

        public bool WriteIfConnected(string graph)
        {
            try
            {
                if (stream.IsConnected)
                {
                    formatter.Serialize(stream, graph);
                    return true;
                }
            }
            catch
            {
                // do nothing?
            }

            return false; // we failed
        }

        public bool WriteIfConnected(List<Tuple<string, object>> graph)
        {
            try
            {
                if (stream.IsConnected)
                {
                    formatter.Serialize(stream, graph);
                    return true;
                }
            }
            catch
            {
                // do nothing?
            }

            return false; // we failed
        }
    }

    public abstract class ClousotAnalysisInfoBaseClass
    {
        #region Poor man serialization, to avoid the issues from the type being defined in cccheck (via ILMERGE) and cccpulse

        public List<Tuple<string, object>> ToList()
        {
            Contract.Ensures(Contract.Result<List<Tuple<string, object>>>() != null);

            var result = new List<Tuple<string, object>>();
            foreach (var f in this.GetType().GetFields())
            {
                result.Add(new Tuple<string, object>(f.Name, f.GetValue(this)));
            }

            return result;
        }

        protected void FromList(List<Tuple<string, object>> list)
        {
            Contract.Requires(list != null);

            foreach (var tuple in list)
            {
                Contract.Assume(tuple != null);
                var f = this.GetType().GetField(tuple.Item1);
                Contract.Assume(f != null);
                f.SetValue(this, tuple.Item2);
            }
        }

        #endregion
    }

    [Serializable]
    public class ClousotAnalysisState : ClousotAnalysisInfoBaseClass
    {
        public int CurrMethodNumber;
        public string CurrMethodName;
        public double CurrPercentage;
        public string CurrPhase;
        public string CurrAssembly;
        public int CurrMemory;

        public ClousotAnalysisState() { }

        public ClousotAnalysisState(List<Tuple<string, object>> list)
        {
            Contract.Requires(list != null);

            this.FromList(list);
        }

        public override string ToString()
        {
            return string.Format("{0,6:P1}, Method #{1} {2} {3}", CurrPercentage, CurrMethodNumber, CurrMethodName, CurrPhase ?? " ");
        }
    }

    [Serializable]
    public class ClousotAnalysisResults : ClousotAnalysisInfoBaseClass
    {
        public int Pid;

        public string AnalyzedAssembly;

        // Assertions
        public long Total, True, Top, False, Bottom, Masked;


        // Methods
        public int MethodsTotal, MethodsFromCache, MethodsMissedInCache, MethodsTimedOut, MethodsWithCheaperAD;

        public DateTime StartTime, EndTime;

        public TimeSpan Elapsed;

        // Crashed?
        public bool ClousotCrashed = false;

        public ClousotAnalysisResults()
        {
            this.Pid = Process.GetCurrentProcess().Id;
            this.StartTime = DateTime.Now;
        }

        public ClousotAnalysisResults(List<Tuple<string, object>> list)
        {
            Contract.Requires(list != null);
            this.FromList(list);
        }

        public void NotifyDone()
        {
            this.EndTime = DateTime.Now;
            this.Elapsed = this.EndTime - this.StartTime;
        }

        public void UpdateAssertionStatisticsFrom(ClousotAnalysisResults results, int outputWarns)
        {
            Contract.Requires(results != null);

            this.Total = results.Total;
            this.True = results.True;
            this.False = results.False;
            this.Bottom = results.Bottom;
            this.Top = results.Top;
            this.Masked = results.Masked;
        }
    }
}

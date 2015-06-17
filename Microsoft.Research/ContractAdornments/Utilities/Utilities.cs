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
using System.Collections;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;

namespace UtilitiesNamespace {
  public class Logger {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.WriteActions != null);
    }
    
    public Action<Exception, string> PublicEntryException { get; private set; }
    public List<Action<string>> WriteActions { get; private set; }
    public Action<Action> QueueWorkItem { get; private set; }
    public event Action Failed;
    public bool EnableLogging { get; set; }

    public Logger(Action<Exception, string> publicEntryException, Action<Action> queueWorkItem, params Action<string>[] writeActions) {
      Contract.Requires(publicEntryException != null);
      Contract.Requires(writeActions != null);

      PublicEntryException = publicEntryException;
      QueueWorkItem = queueWorkItem;
      WriteActions = writeActions.ToList();
      EnableLogging = false; // make people opt-in.
    }

    public void OnFailed() {
      if (Failed != null)
        Failed();
    }

    public void WriteToLog(string message) {
      if (EnableLogging)
        foreach (var writeAction in WriteActions) {
          try {
            writeAction(message);
          } catch (Exception) { }
        }
    }
    public void WriteToLog(string format, params string[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);
      if (EnableLogging)
        foreach (var writeAction in WriteActions)
        {
          try
          {
            writeAction(String.Format(format, args));
          }
          catch (Exception) { }
        }
    }

    public void WriteAlways(string format, params string[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

        foreach (var writeAction in WriteActions)
        {
            try
            {
                writeAction(String.Format(format, args));
            }
            catch (Exception) { }
        }
    }

    public void PublicEntry(Action<object> action, object state = null, string entryName = "") {
      Contract.Requires(!String.IsNullOrWhiteSpace(entryName));
      Contract.Requires(action != null);

      try {
        action(state);
      } catch (Exception e) {
        PublicEntryException(e, entryName);
      }
    }
    public void PublicEntry(Action action, string entryName = "") {
      Contract.Requires(!String.IsNullOrWhiteSpace(entryName));
      Contract.Requires(action != null);

      try {
        var startTime = DateTime.Now;
        this.WriteToLog("Starting {0} at {1}", entryName, startTime.ToShortTimeString());
        action();
        this.WriteToLog("Finished {0} in {1}ms", entryName, (DateTime.Now-startTime).Milliseconds.ToString());
      }
      catch (Exception e)
      {
        PublicEntryException(e, entryName);
      }
    }
    public T PublicEntry<T>(Func<T> action, string entryName = "") {
      Contract.Requires(!String.IsNullOrWhiteSpace(entryName));
      Contract.Requires(action != null);

      try {
        return action();
      } catch (Exception e) {
        PublicEntryException(e, entryName);
      }
      return default(T);
    }
    public T PublicEntry<T>(Func<object, T> action, object state = null, string entryName = "") {
      Contract.Requires(!String.IsNullOrWhiteSpace(entryName));
      Contract.Requires(action != null);

      try {
        return action(state);
      } catch (Exception e) {
        PublicEntryException(e, entryName);
      }
      return default(T);
    }
  }
  public static class Utilities {
    public static void ForAll<T>(this IEnumerable<T> @this, Action<T> action) {
      Contract.Requires(@this != null);
      Contract.Requires(action != null);

      foreach (T t in @this) {
        action(t);
      }
    }
    public static void AddAll<T>(this ICollection<T> @this, IEnumerable<T> target) {
      Contract.Requires(target != null);
      Contract.Requires(@this != null);

      foreach (var t in target)
        @this.Add(t);
    }


    // Don't really delay: when that happens, the action is executed
    // on the timer thread instead of the idle thread and then there
    // is a race condition on the work queue, as well as all of the
    // other shared state. This led to null items being pulled off
    // of the queue, even though there is a check to make sure null
    // actions are never put onto the queue. Leaving the code here
    // in case we need it again. In that case, it might work to just
    // put a lock around the work queue and make this always just 
    // put the action onto the work queue after the delay. That way
    // the action would always be dequeued and executed by the idle
    // thread.
    public static void Delay(Action action, double delayInMilliseconds) {
      Contract.Requires(action != null);
      Contract.Requires(delayInMilliseconds >= 0);

      
      action();
      //var timer = new Timer(delayInMilliseconds);
      //timer.AutoReset = false;
      //timer.Start();
      //timer.Elapsed += (_1, _2) => { action(); timer.Close(); };
    }

    //myString = myString.SmartReplace("Contract.Result<{0}>()", "result");
    //myString = myString.SmartReplace("Contract.OldValue<{0}>({1})", "old {1}");
    public static string SmartReplace(this string @this, string oldString, string newString) {
      Contract.Requires(!String.IsNullOrEmpty(oldString));
      Contract.Requires(!String.IsNullOrEmpty(newString));
      Contract.Requires(!String.IsNullOrEmpty(@this));

      Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

      //Is our input null?
      if (String.IsNullOrEmpty(@this))
        return @this;

      //Copy into result so we don't mess up the input
      var result = @this;

      //Initialize a dictionary from indices to variables
      var oldStringIndicesToVars = new Dictionary<int, int>();

      //Initialize a dictionary from variables to their values
      var varsToString = new Dictionary<int, string>();

      //We use this to ignore successive variables (like "{0}{1}{2}")
      bool wasPreviousAVariable = false;

      //Iterate over the oldString
      for (int i = 0; i < oldString.Length; i++) {

        //Grab the current character
        var c = oldString[i];

        //Are we at a variable?
        if (c == '{') {
          if (i + 2 < oldString.Length) {
            if (oldString[i + 2] == '}') {

              if (!wasPreviousAVariable) {
                //Try to get our variable
                int varInt;
                if (Int32.TryParse(oldString[i + 1].ToString(), out varInt)) {

                  //Store our variable into our dictionaries
                  oldStringIndicesToVars.Add(i, varInt);
                  varsToString.Add(varInt, "");
                }
              }

              //Jump our counter forward
              i = i + 2;

              //Note that this was a variable
              wasPreviousAVariable = true;
            }
          }
        } else {

          //Note that this was not a variable
          wasPreviousAVariable = false;
        }
      }

      //Initialize a new list of 'search strings', these will be searched for in our input string
      var oldStringSearchStrings = new List<string>();

      //Do we variables?
      if (oldStringIndicesToVars.Count > 0) {

        //The right after the end of our last variable
        var lastIndex = 0;

        //Iterate over our variable indices
        foreach (var index in oldStringIndicesToVars.Keys) {

          //Is this index right next to the end of our last variable?
          if (index - lastIndex <= 0)
            continue;

          //Grab everything between this and the previous variable
          var s = oldString.Substring(lastIndex, index - lastIndex);
          oldStringSearchStrings.Add(s);

          //Set our last index to right after the end of this index
          lastIndex = index + 3;

          //Is our last index overstepping our string?
          if (lastIndex >= oldString.Length)
            break;
        }

        //Was there anything after our last variable?
        if (lastIndex < oldString.Length) {

          //Grab the last bit of our string
          var s = oldString.Substring(lastIndex, oldString.Length - lastIndex);
          oldStringSearchStrings.Add(s);
        }

        //The entire oldString is one large search string
      } else
        oldStringSearchStrings.Add(oldString);

      //Intialize the variable that will let us break out of our iteration
      bool @break = oldStringSearchStrings.Count <= 0;

      //Iterate over the whole input string, searching and replacing
      while (!@break) {

        int startIndex = -1;//The start index of the string to replace
        int endIndex = -1;//The end index of the string to replace
        int endOfLast = -1;//The end of the last search string found in our input string
        int valsHit = 0;//Number of variables we have hit so far

        //Foreach search string
        for (int i = 0; i < oldStringSearchStrings.Count; i++) {
          var searchString = oldStringSearchStrings[i];

          //What is the index of our search string in our input string?
          Contract.Assume(searchString != null);
          Contract.Assume(endOfLast + 1 <= result.Length);
          var j = result.IndexOf(searchString, endOfLast + 1);

          //Does our search string exist in the input string?
          if (j == -1) {
            @break = true;
            break;
          }

          //Is this a non-first search string?
          if (endOfLast != -1) {

            //What is the length of the text between our last and current search string?
            var l = j - endOfLast - 1;

            //Is there any text between our last and current search string?
            if (l > 0) {

              Contract.Assume(endOfLast + 1 <= result.Length - (j - endOfLast -1));
              //Grab the text between our last and current search string
              var val = result.Substring(endOfLast + 1, j - endOfLast - 1);

              //What was our 'valsHit'th variable?
              var @var = oldStringIndicesToVars.ElementAt(valsHit).Value;
              valsHit++;

              //Save the text between our last and current search string as the value for the aforementioned variable
              varsToString[@var] = val;
            }
          }

          //Is this the first search string? If so, this is the start index of the text we will eventually replace
          if (i == 0)
            startIndex = j;

          //Set endOfLast to the end of our current search string
          endOfLast = j + searchString.Length - 1;

          //Is this the last search string? If so, this is the end index of the text we will eventually replace
          if (i == oldStringSearchStrings.Count - 1)
            endIndex = endOfLast;
        }

        //Are we done?
        if (@break || startIndex == -1 || endIndex == -1 || endIndex < startIndex)
          break;

        //This will be the text we will replace the old text with
        var swapString = newString;

        //Swap the variables we found into our newString
        foreach (var varValPair in varsToString) {
          var oldValue = "{" + varValPair.Key + "}";
          Contract.Assume(oldValue.Length > 0, "weakness in CC");
          swapString = swapString.Replace(oldValue, varValPair.Value);
        }

        //Swap the old text with the new text
        result = result.Remove(startIndex, endIndex - startIndex + 1);

        Contract.Assume(startIndex <= result.Length);
        result = result.Insert(startIndex, swapString);

        Contract.Assert(result != null);
      }

      //Return our result.
      return result;
    }

    //public static Task<T> SafeStartNew<T>(this TaskFactory @this, Func<object, T> action, object state = null, string description = "") {
    //  Contract.Requires(description != null);
    //  return @this.StartNew<T>((s) => {
    //    return PublicEntry(action, state, "Asynchronous action: " + description);
    //  }, state);
    //}
    //public static Task SafeStartNew(this TaskFactory @this, Action<object> action, object state = null, string description = "") {
    //  return @this.StartNew((s) => {
    //    PublicEntry(action, state, "Asynchronous action: " + description);
    //  }, state);
    //}
  }
}

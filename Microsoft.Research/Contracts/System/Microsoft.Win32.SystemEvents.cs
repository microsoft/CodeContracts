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

using System.Diagnostics.Contracts;
using System;

namespace Microsoft.Win32
{

    public class SystemEvents
    {

        public static void KillTimer (int timerId) {

        }
        public static void InvokeOnEventsThread (Delegate method) {

        }
        public static int CreateTimer (int interval) {
            Contract.Requires(interval > 0);

          return default(int);
        }
        public static void remove_UserPreferenceChanging (UserPreferenceChangingEventHandler value) {

        }
        public static void add_UserPreferenceChanging (UserPreferenceChangingEventHandler value) {

        }
        public static void remove_UserPreferenceChanged (UserPreferenceChangedEventHandler value) {

        }
        public static void add_UserPreferenceChanged (UserPreferenceChangedEventHandler value) {

        }
        public static void remove_TimerElapsed (TimerElapsedEventHandler value) {

        }
        public static void add_TimerElapsed (TimerElapsedEventHandler value) {

        }
        public static void remove_TimeChanged (EventHandler value) {

        }
        public static void add_TimeChanged (EventHandler value) {

        }
        public static void remove_SessionEnding (SessionEndingEventHandler value) {

        }
        public static void add_SessionEnding (SessionEndingEventHandler value) {

        }
        public static void remove_SessionEnded (SessionEndedEventHandler value) {

        }
        public static void add_SessionEnded (SessionEndedEventHandler value) {

        }
        public static void remove_PowerModeChanged (PowerModeChangedEventHandler value) {

        }
        public static void add_PowerModeChanged (PowerModeChangedEventHandler value) {

        }
        public static void remove_PaletteChanged (EventHandler value) {

        }
        public static void add_PaletteChanged (EventHandler value) {

        }
        public static void remove_LowMemory (EventHandler value) {

        }
        public static void add_LowMemory (EventHandler value) {

        }
        public static void remove_InstalledFontsChanged (EventHandler value) {

        }
        public static void add_InstalledFontsChanged (EventHandler value) {

        }
        public static void remove_EventsThreadShutdown (EventHandler value) {

        }
        public static void add_EventsThreadShutdown (EventHandler value) {

        }
        public static void remove_DisplaySettingsChanged (EventHandler value) {

        }
        public static void add_DisplaySettingsChanged (EventHandler value) {
        }
    }
}


To build a new version of the CodeTools:
----------------------------------------
1. If you changed ITaskManager.dll, make sure to unregister ITaskManager.dll and run the following steps as ADMIN.

2. Goto CodeToolsSetup

3. Update the version number in BuildMSM.bat. If you don't, then upgrading the installer will not do anything, i.e., it will leave the old version in place.

4. Run BUILDMSM.bat export if ITaskManager changed, or if you want to copy the files into the CodeContracts import directory (necessary prior to building a Contract installer).

You may have to make some files writable in order for this to succeed.


To debug code tools
-------------------

- Easiest way is to add a Debugger.Launch() command in the TaskManager initialization code and attach a debugger that way.
- Build the debug version, become admin, copy the new bits and pdb into the contract installed directory (make sure all VS are quit first). Restart VS, attach the debugger.



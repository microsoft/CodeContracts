
Patching a Sandcastle Installation to produce Contract Sections
===============================================================

We provide 2 set of patch files depending on your Sandcastle
installation (http://www.codeplex.com/Sandcastle). If you installed
Sandcastle using the installer (msi), please go to Section 1. If you
built/installed Sandcastle from source files, please go to Section 2.
The reason that there are two sets of files is that the Sandcastle
installer version and the Sandcastle source versions differ slightly
(the source being newer).


1. Patching an MSI installed Sandcastle
---------------------------------------

Copy the entire sub-directory tree msi\vs2005\... in this zip file to
the corresponding directory in your Sandcastle installation
directory. This typically will be at 

  c:\Program Files\Sandcastle\Presentation\vs2005\...

You may need Administrator privilege to perform this copy. Choose to
overwrite the existing files.



2. Patching a Source Based Sandcastle Installation
--------------------------------------------------

Copy the entire sub-directory tree source\vs2005\... in this zip file to
the corresponding directory in your Sandcastle installation
directory. This typically will be at 

  c:\Program Files\Sandcastle\Presentation\vs2005\...

You may need Administrator privilege to perform this copy. Choose to
overwrite the existing files.


3. Running Sandcastle
---------------------

No additional steps are needed after the patching (Section 1 or 2) has
been performed. When you run Sandcastle on your files that have been
produced with contract elements, the output should show contract
sections for methods and types with contracts.

4. Undoing the Patch
--------------------
NOTE: The above method does not produce a backup of the original Sandcastle
files being overwritten. If you run into problems, you will have to
reinstall Sandcastle or copy the original files from a saved location.


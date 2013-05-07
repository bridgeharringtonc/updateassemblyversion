updateassemblyversion
=====================

Provides custom tasks for MSBuild relating to version numbering.

AssemblyVersionTask
-------------------

Replaces thes AssemblyVersion and AssemblyFileVersion values in AssemblyInfo.cs files.

Parameters:

* FileList				(Required)	list of files to update
* AssemblyVersion 		(Required)	new version value
* AssemblyFileVersion	(Required)	new file version value

IncrementAssemblyVersionTask
----------------------------

Increments the specified part of the AssemblyVersion and AssemblyFileVersion values in AssemblyInfo.cs files by the specified amount.

Parameters:

* FileList				(Required)	list of files to update
* IncrementValue					amount to increment by; default to 1
* Position							position of the value to increment (e.g. 1 = major, 2 = minor etc. in 1.1.1 format); default to last
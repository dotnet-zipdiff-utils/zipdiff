# ZipDiff

Use the ZipDiff tool when you need to compare the contents of two zip files.

Run it as a standalone executable. The tool supports four output formats: plain text, XML, HTML and ZIP (containing differences).

This version of ZipDiff has been ported from Java to C# (.NET)

### Command line arguments

	zipdiff.exe --file1 foo.zip --file2 bar.zip [--options]

Valid options are:

	--comparecrcvalues     Compares the crc values instead of the file content
	--comparetimestamps    Compares timestamps instead of file content
	--outputfile           Name of the output file
	--exitwitherrorondifference   Use an error code other than 0, if differences have been detected
	--regex                Regular expression to match files to exclude
	--verbose              Print detail messages

### References
This version can be found at https://github.com/leekelleher/ZipDiff.NET

The original zipdiff project was developed by Sean C. Sullivan and James Stewart at http://zipdiff.sourceforge.net/
A recent fork can also be found on GitHub: https://github.com/nhnb/zipdiff

### License
This project is licensed under [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0).
Please see LICENSE.md and NOTICE.txt for further details.

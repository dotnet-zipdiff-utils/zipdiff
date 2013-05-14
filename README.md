# ZipDiff

Use the ZipDiff tool when you need to compare the contents of two zip files.

Run it as a standalone executable. The tool supports four output formats: plain text, XML, HTML and ZIP (containing differences).

This version of ZipDiff has been ported from Java to C# (.NET)

## Download



## Command line arguments

	zipdiff.exe --file1 foo.zip --file2 bar.zip [--options]

Valid options are:

	--comparecrcvalues            Compares the crc values instead of the file content
	--comparetimestamps           Compares timestamps instead of file content
	--outputfile                  Name of the output file
	--exitwitherrorondifference   Use an error code other than 0, if differences have been detected
	--regex                       Regular expression to match files to exclude
	--verbose                     Print detail messages

## References
This version can be found at https://github.com/leekelleher/ZipDiff.NET

The original [zipdiff](http://zipdiff.sourceforge.net/) project was developed by Sean C. Sullivan and James Stewart.

A fork (with enhancements) was recently made by [Hendrik Brummermann](https://github.com/nhnb). This can also be found on GitHub: https://github.com/nhnb/zipdiff

## License
Copyright &copy; 2004 Sean C. Sullivan and James Stewart<br/>
Copyright &copy; 2009 Hendrik Brummermann<br/>
Copyright &copy; 2013 Lee Kelleher<br/>

This project is licensed under [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0).

Please see [LICENSE](LICENSE.txt) and [NOTICE](NOTICE.txt) for further details.

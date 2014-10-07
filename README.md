# ZipDiff

Use the ZipDiff tool when you need to compare the contents of two zip files.

Run it as a standalone executable. The tool supports four output formats: plain text, XML, HTML and ZIP (containing differences).

This version of ZipDiff has been ported from Java to C# (.NET)


## Download

To get a copy of ZipDiff you have the following options:

* Download the source and run the `build.cmd` (which runs an MSBuild against `build.proj`)
* Install using NuGet. Your options are:
  * [ZipDiff](https://nuget.org/packages/ZipDiff/) - the command-line utility
  * [ZipDiff.Core](https://nuget.org/packages/ZipDiff.Core/) - the class-library (to use in your own applications)
* or finally, you can download the executable directly from my DropBox share: [zipdiff.exe](https://dl.dropboxusercontent.com/u/3504568/Projects/OSS/ZipDiff/zipdiff.exe)


### Prerequisites

To use ZipDiff you will need to have the [.NET Framework 4.0](http://www.microsoft.com/en-GB/download/details.aspx?id=17851) installed.


## Command line arguments

	zipdiff.exe --file1 foo.zip --file2 bar.zip [--options]

Valid options are:

	--comparecrcvalues            Compares the CRC values instead of the file content
	--comparetimestamps           Compares timestamps instead of file content
	--ignorecase                  Performs case-insensitive string comparison on the file name
	--outputfile                  Name of the output file
	--exitwitherrorondifference   Use an error code other than 0, if differences have been detected
	--regex                       Regular expression to match files to exclude
	--verbose                     Print detail messages


### Output formats

When using the `--outputfile` option, the following formats are available:

* Plain-Text
* HTML
* XML
* Zip (containing the differences between `--file1` and `--file2`)

For example, to output a zip file:

	zipdiff.exe --file1 foo.zip --file2 bar.zip --outputfile diff.zip

If the output file extension can not be determined, then the format will default to a plain-text file.


## References

This version can be found at https://github.com/leekelleher/ZipDiff

The original [zipdiff](http://zipdiff.sourceforge.net/) project was developed by Sean C. Sullivan and James Stewart.

A fork (with enhancements) was recently made by [Hendrik Brummermann](https://github.com/nhnb). This can also be found on GitHub: https://github.com/nhnb/zipdiff


## License

Copyright &copy; 2004 Sean C. Sullivan and James Stewart<br/>
Copyright &copy; 2009 Hendrik Brummermann<br/>
Copyright &copy; 2013 Lee Kelleher<br/>

This project is licensed under [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0).

Please see [LICENSE](LICENSE.txt) and [NOTICE](NOTICE.txt) for further details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipDiff.Core
{
	public class DifferenceCalculator
	{
		private ZipFile zip1;
		private ZipFile zip2;

		public bool CompareCrcValues { get; set; }
		public bool CompareTimestamps { get; set; }
		public string RegExPattern { get; set; }

		public DifferenceCalculator(string file1, string file2)
			: this(new FileInfo(file1), new FileInfo(file2))
		{
		}

		public DifferenceCalculator(FileInfo file1, FileInfo file2)
			: this(new ZipFile(file1.OpenRead()), new ZipFile(file2.OpenRead()))
		{
		}

		public DifferenceCalculator(ZipFile file1, ZipFile file2)
		{
			zip1 = file1;
			zip2 = file2;
		}

		protected Dictionary<string, ZipEntry> BuildZipEntryMap(ZipFile zip)
		{
			var map = new Dictionary<string, ZipEntry>();

			for (int i = 0; i < zip.Count; i++)
			{
				var entry = zip[i];

				if (!map.ContainsKey(entry.Name))
					map.Add(entry.Name, entry);
			}

			zip1.Close();

			return map;
		}

		protected Differences CalculateDifferences(ZipFile file1, ZipFile file2)
		{
			var map1 = BuildZipEntryMap(file1);
			var map2 = BuildZipEntryMap(file2);

			return CalculateDifferences(map1, map2);
		}

		protected Differences CalculateDifferences(Dictionary<string, ZipEntry> map1, Dictionary<string, ZipEntry> map2)
		{
			var diff = new Differences(zip1.Name, zip2.Name);

			var allNames = new List<string>();
			allNames.AddRange(map1.Keys);
			allNames.AddRange(map2.Keys);

			foreach (var name in allNames.Distinct())
			{
				if (IgnoreFile(name))
				{
					continue;
				}
				else if (map1.ContainsKey(name) && (!map2.ContainsKey(name)))
				{
					diff.Removed.Add(name, map1[name]);
				}
				else if (map2.ContainsKey(name) && (!map1.ContainsKey(name)))
				{
					diff.Added.Add(name, map2[name]);
				}
				else if (map1.ContainsKey(name) && (map2.ContainsKey(name)))
				{
					var entry1 = map1[name];
					var entry2 = map2[name];

					var match = entry1.IsDirectory == entry2.IsDirectory;
					match = match && entry1.Size == entry2.Size;
					match = match && entry1.CompressedSize == entry2.CompressedSize;
					match = match && entry1.Name == entry2.Name;

					if (CompareTimestamps)
						match = match && entry1.DateTime == entry2.DateTime;

					if (CompareCrcValues && (entry1.HasCrc && entry2.HasCrc))
						match = match && entry1.Crc == entry2.Crc;

					if (!match)
						diff.Changed.Add(name, new[] { entry1, entry2 });
				}
			}

			return diff;
		}

		public Differences GetDifferences()
		{
			return CalculateDifferences(zip1, zip2);
		}

		protected bool IgnoreFile(string entryName)
		{
			if (string.IsNullOrWhiteSpace(entryName))
				return false;

			if (string.IsNullOrWhiteSpace(RegExPattern))
				return false;

			var match = Regex.Match(entryName, RegExPattern, RegexOptions.IgnoreCase);
			if (match.Success)
				Console.WriteLine("Found a match against : {0} so excluding.", entryName);

			return match.Success;
		}
	}
}
namespace ZipDiff.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using ICSharpCode.SharpZipLib.Zip;

	public class Differences
	{
		private bool Verbose { get; set; }

		public string File1 { get; set; }

		public string File2 { get; set; }

		public Dictionary<string, ZipEntry> Added { get; set; }

		public Dictionary<string, ZipEntry[]> Changed { get; set; }

		public Dictionary<string, ZipEntry> Ignored { get; set; }

		public Dictionary<string, ZipEntry> Removed { get; set; }

		internal Dictionary<string, ZipEntry> Unchanged { get; set; }

		public Differences(string file1, string file2, bool ignoreCase = false, bool verbose = false)
		{
			File1 = file1;
			File2 = file2;

			var comparer = ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

			Added = new Dictionary<string, ZipEntry>(comparer);
			Changed = new Dictionary<string, ZipEntry[]>(comparer);
			Ignored = new Dictionary<string, ZipEntry>(comparer);
			Removed = new Dictionary<string, ZipEntry>(comparer);
			Unchanged = new Dictionary<string, ZipEntry>(comparer);

			Verbose = verbose;
		}

		public bool HasDifferences()
		{
			return Added.Count > 0 || Changed.Count > 0 || Removed.Count > 0;
		}

		public override string ToString()
		{
			return ToString(Verbose);
		}

		public string ToString(bool verbose)
		{
			if (verbose == false)
				return string.Format("[Added: {0}; Removed: {1}; Changed: {2}]", Added.Count, Removed.Count, Changed.Count);

			var log = new StringBuilder();

			log.Append("File 1:\t")
				.Append(Path.GetFileName(File1))
				.Append("File 2:\t")
				.Append(Path.GetFileName(File2))
				.AppendLine();

			log.Append(Added.Count)
				.Append(Added.Count == 1 ? " file was" : " files were")
				.Append(" added to ")
				.Append(Path.GetFileName(File2))
				.AppendLine();

			if (Added.Keys.Count > 0)
			{
				log.Append("\t[added] ")
					.Append(string.Join("\r\n\t[added] ", Added.Keys))
					.AppendLine();
			}

			log.Append(Removed.Count)
				.Append(Removed.Count == 1 ? " file was" : " files were")
				.Append(" removed from ")
				.Append(Path.GetFileName(File2))
				.AppendLine();

			if (Removed.Keys.Count > 0)
			{
				log.Append("\t[removed] ")
					.Append(string.Join("\r\n\t[removed] ", Removed.Keys))
					.AppendLine();
			}

			log.Append(Changed.Count)
				.Append(Changed.Count == 1 ? " file changed" : " files changed")
				.AppendLine();

			foreach (var changed in Changed.Where(x => x.Value.Length > 1))
			{
				log.AppendFormat("\t[changed] {0} (size {1} : {2})", changed.Key, changed.Value[0].Size, changed.Value[1].Size)
					.AppendLine();
			}

			var differenceCount = Added.Count + Changed.Count + Removed.Count;
			log.AppendFormat("Total differences: {0}", differenceCount);

			return log.ToString();
		}
	}
}
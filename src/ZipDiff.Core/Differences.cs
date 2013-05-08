using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipDiff.Core
{
	public class Differences
	{
		public string File1 { get; set; }

		public string File2 { get; set; }

		public Dictionary<string, ZipEntry> Added { get; set; }

		public Dictionary<string, ZipEntry[]> Changed { get; set; }

		public Dictionary<string, ZipEntry> Ignored { get; set; }

		public Dictionary<string, ZipEntry> Removed { get; set; }

		internal Dictionary<string, ZipEntry> Unchanged { get; set; }

		public Differences(string file1, string file2)
		{
			File1 = file1;
			File2 = file2;

			Added = new Dictionary<string, ZipEntry>();
			Changed = new Dictionary<string, ZipEntry[]>();
			Ignored = new Dictionary<string, ZipEntry>();
			Removed = new Dictionary<string, ZipEntry>();
			Unchanged = new Dictionary<string, ZipEntry>();
		}

		public bool HasDifferences()
		{
			return Added.Count > 0 || Changed.Count > 0 || Removed.Count > 0;
		}

		public override string ToString()
		{
			var logg = new StringBuilder();

			logg.Append(Added.Count)
				.Append(Added.Count == 1 ? " file was" : " files were")
				.Append(" added to ")
				.Append(File2)
				.AppendLine()
				.Append("\t[added] ")
				.Append(string.Join("\r\n\t[added] ", Added.Keys))
				.AppendLine();

			logg.Append(Removed.Count)
				.Append(Removed.Count == 1 ? " file was" : " files were")
				.Append(" removed from ")
				.Append(File2)
				.AppendLine()
				.Append("\t[removed] ")
				.Append(string.Join("\r\n\t[removed] ", Removed.Keys))
				.AppendLine();

			logg.Append(Changed.Count)
				.Append(Changed.Count == 1 ? " file changed" : " files changed")
				.AppendLine();

			foreach (var changed in Changed.Where(x => x.Value.Length > 1))
			{
				logg.AppendFormat("\t[changed] {0} (size {1} : {2})", changed.Key, changed.Value[0].Size, changed.Value[1].Size)
					.AppendLine();
			}

			var differenceCount = Added.Count + Changed.Count + Removed.Count;
			logg.AppendFormat("Total differences: {0}", differenceCount);

			return logg.ToString();
		}
	}
}
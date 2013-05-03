using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipDiff
{
	class Differences
	{
		public string File1 { get; set; }
		public string File2 { get; set; }

		public Dictionary<string, ZipEntry> Added { get; set; }
		public Dictionary<string, ZipEntry[]> Changed { get; set; }
		public Dictionary<string, ZipEntry> Ignored { get; set; }
		public Dictionary<string, ZipEntry> Removed { get; set; }

		public Differences(string file1, string file2)
		{
			File1 = file1;
			File2 = file2;

			Added = new Dictionary<string, ZipEntry>();
			Changed = new Dictionary<string, ZipEntry[]>();
			Ignored = new Dictionary<string, ZipEntry>();
			Removed = new Dictionary<string, ZipEntry>();
		}

		public bool HasDifferences()
		{
			return Added.Count > 0 || Changed.Count > 0 || Removed.Count > 0;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			if (Added.Count == 1)
			{
				sb.Append("1 file was");
			}
			else
			{
				sb.Append(Added.Count).Append(" files were");
			}

			sb.Append(" added to ").Append(File2).AppendLine();
			sb.Append("\t[added] ").Append(string.Join("\r\n\t[added] ", Added.Keys)).AppendLine();

			if (Removed.Count == 1)
			{
				sb.Append("1 file was");
			}
			else
			{
				sb.Append(Removed.Count).Append(" files were");
			}

			sb.Append(" removed from ").Append(File2).AppendLine();
			sb.Append("\t[removed] ").Append(string.Join("\r\n\t[removed] ", Removed.Keys)).AppendLine();

			if (Changed.Count == 1)
			{
				sb.Append("1 file changed\n");
			}
			else
			{
				sb.Append(Changed.Count).Append(" files changed").AppendLine();
			}

			foreach (var changed in Changed.Where(x => x.Value.Length > 1))
			{
				sb.AppendFormat("\t[changed] {0} (size {1} : {2})", changed.Key, changed.Value[0].Size, changed.Value[1].Size).AppendLine();
			}

			var differenceCount = Added.Count + Changed.Count + Removed.Count;

			sb.Append("Total differences: ").Append(differenceCount);

			return sb.ToString();
		}
	}
}
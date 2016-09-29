namespace ZipDiff.Core.Output
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using ICSharpCode.SharpZipLib.Zip;

	public class ZipBuilder : AbstractBuilder
	{
		private bool _includeRemovedFile;

		public ZipBuilder() : this(false)
		{
		}

		public ZipBuilder(bool includeRemovedFile)
		{
			_includeRemovedFile = includeRemovedFile;
		}

		public override void Build(StreamWriter writer, Differences diff)
		{
			var entries = new List<ZipEntry>();

			entries.AddRange(diff.Added.Values);
			entries.AddRange(diff.Changed.Values.Select(x => x[1]));

			var zipOut = new ZipOutputStream(writer.BaseStream);
			var zipFile = new ZipFile(diff.File2);

			foreach (var entry in entries)
			{
				var zipEntry = zipFile.GetEntry(entry.Name);
				using (var stream = zipFile.GetInputStream(zipEntry))
				{
					var count = 0x800;
					var buffer = new byte[0x800];

					zipOut.PutNextEntry(new ZipEntry(entry.Name));

					while (true)
					{
						count = stream.Read(buffer, 0, buffer.Length);
						if (count <= 0)
							break;

						zipOut.Write(buffer, 0, count);
					}

					zipOut.CloseEntry();
					buffer = null;
				}
			}

			zipFile.Close();

			if (_includeRemovedFile && diff.Removed.Count > 0)
			{
				var removedItems = Encoding.UTF8.GetBytes(string.Join("\r\n", diff.Removed.Keys));

				zipOut.PutNextEntry(new ZipEntry("___removed.txt"));
				zipOut.Write(removedItems, 0, removedItems.Length);
				zipOut.CloseEntry();
			}

			zipOut.Finish();
			zipOut.Flush();
		}
	}
}
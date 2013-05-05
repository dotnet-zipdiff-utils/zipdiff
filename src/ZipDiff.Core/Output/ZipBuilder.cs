using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipDiff.Core.Output
{
	class ZipBuilder : AbstractBuilder
	{
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

			zipOut.Finish();
			zipOut.Flush();
		}
	}
}
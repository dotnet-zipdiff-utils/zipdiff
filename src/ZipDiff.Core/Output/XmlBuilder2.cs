namespace ZipDiff.Core.Output
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class XmlBuilder2 : AbstractBuilder
	{
		public override void Build(StreamWriter writer, Differences diff)
		{
			Action<string> writeEntry = x =>
			{
				writer.WriteLine("<file>{0}</file>", x);
			};

			Action<ICollection<string>, string> writeEntries = (keys, element) =>
			{
				if (keys.Count > 0)
				{
					writer.WriteLine("<{0}>", element);
					keys.ToList().ForEach(writeEntry);
					writer.WriteLine("</{0}>", element);
				}
			};

			writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			writer.WriteLine("<differences filename1=\"{0}\" filename2=\"{1}\">", Path.GetFileName(diff.File1), Path.GetFileName(diff.File2));

			writeEntries(diff.Added.Keys, "added");
			writeEntries(diff.Removed.Keys, "removed");
			writeEntries(diff.Changed.Keys, "changed");

			writer.WriteLine("</differences>");

			writer.Flush();
		}
	}
}
namespace ZipDiff.Core.Output
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class JsonBuilder : AbstractBuilder
	{
		public override void Build(StreamWriter writer, Differences diff)
		{
			Action<ICollection<string>, string> writeEntries = (keys, element) =>
			{
				var items = keys.Count > 0
					? string.Concat("\r\n\t\t\"", string.Join("\",\r\n\t\t\"", keys), "\"\r\n\t")
					: " ";

				writer.Write("\t\"{0}\" : [{1}]", element, items);
			};

			writer.WriteLine("{");
			writer.WriteLine("\t\"filename1\" : \"{0}\",", Path.GetFileName(diff.File1));
			writer.WriteLine("\t\"filename2\" : \"{0}\",", Path.GetFileName(diff.File2));

			writeEntries(diff.Added.Keys, "added");
			writer.WriteLine(",");
			writeEntries(diff.Removed.Keys, "removed");
			writer.WriteLine(",");
			writeEntries(diff.Changed.Keys, "changed");

			writer.WriteLine("\r\n}");

			writer.Flush();
		}
	}
}
namespace ZipDiff.Core.Output
{
	using System.IO;

	public class XmlBuilder : AbstractBuilder
	{
		public override void Build(StreamWriter writer, Differences diff)
		{
			writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			writer.WriteLine("<zipdiff filename1=\"{0}\" filename2=\"{1}\">", diff.File1, diff.File2);

			writer.WriteLine("<differences>");

			foreach (var item in diff.Added)
				writer.WriteLine("<added>{0}</added>", item.Key);

			foreach (var item in diff.Removed)
				writer.WriteLine("<removed>{0}</removed>", item.Key);

			foreach (var item in diff.Changed)
				writer.WriteLine("<changed>{0}</changed>", item.Key);

			writer.WriteLine("</differences>");
			writer.WriteLine("</zipdiff>");

			writer.Flush();
		}
	}
}
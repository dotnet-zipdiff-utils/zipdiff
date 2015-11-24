namespace ZipDiff.Core.Output
{
	using System;

	public class BuilderFactory
	{
		public static IBuilder Create(string filename)
		{
			if (filename.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
				return new HtmlBuilder();

			if (filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
				return new JsonBuilder();

			if (filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
				return new TextBuilder();

			if (filename.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
				return new XmlBuilder2();

			if (filename.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
				return new ZipBuilder();

			return new TextBuilder();
		}
	}
}
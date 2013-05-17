using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDiff.Core.Output
{
	public class BuilderFactory
	{
		public static IBuilder Create(String filename)
		{
			if (filename.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
				return new HtmlBuilder();

			if (filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
				return new TextBuilder();

			if (filename.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
				return new XmlBuilder();

			if (filename.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
				return new ZipBuilder();

			return new TextBuilder();
		}
	}
}

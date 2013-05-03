using System;
using System.IO;
using System.Linq;

namespace ZipDiff.Output
{
	class HtmlBuilder : AbstractBuilder
	{
		public override void Build(StreamWriter writer, Differences diff)
		{
			writer.WriteLine("<html>");
			writer.WriteLine("<META http-equiv=\"Content-Type\" content=\"text/html\">");
			writer.WriteLine("<head>");
			writer.WriteLine("<title>ZipDiff - File differences</title>");
			writer.WriteLine("<style type=\"text/css\">");
			writer.WriteLine(@"
body, p { font-family: verdana,arial,helvetica; font-size: 80%; color:#000000; }
.diffs { font-family: verdana,arial,helvetica; font-size: 80%; font-weight: bold; text-align:left; background:#a6caf0; }
tr, td { font-family: verdana,arial,helvetica; font-size: 80%; background:#eeeee0; }");
			writer.WriteLine("</style>");
			writer.WriteLine("</head>");

			writer.WriteLine("<body>");
			writer.WriteLine("<p>First file: {0}<br/>Second file: {1}</p>", diff.File1, diff.File2);

			WriteDiffSet(writer, "Added", diff.Added.Keys.ToArray());
			WriteDiffSet(writer, "Removed", diff.Removed.Keys.ToArray());
			WriteDiffSet(writer, "Changed", diff.Changed.Keys.ToArray());

			writer.WriteLine("<hr>");
			writer.WriteLine("<p>Generated at {0}</p>", DateTime.Now);
			writer.WriteLine("</body>");

			writer.WriteLine("</html>");

			writer.Flush();
		}

		protected void WriteDiffSet(StreamWriter writer, string label, string[] entries)
		{
			writer.WriteLine("<table cellspacing=\"1\" cellpadding=\"3\" width=\"100%\" border=\"0\">");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class=\"diffs\" colspan=\"2\">{0} ({1} entries)</td>", label, entries.Length);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td width=\"20\">");
			writer.WriteLine("</td>");
			writer.WriteLine("<td>");

			if (entries.Length > 0)
			{
				writer.WriteLine("<ul>");

				foreach (var entry in entries)
				{
					writer.WriteLine("<li>{0}</li>", entry);
				}

				writer.WriteLine("</ul>");
			}

			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
	}
}
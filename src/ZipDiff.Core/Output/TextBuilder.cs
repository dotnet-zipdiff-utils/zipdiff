namespace ZipDiff.Core.Output
{
	using System.IO;

	public class TextBuilder : AbstractBuilder
	{
		public override void Build(StreamWriter writer, Differences diff)
		{
			writer.Write(diff.ToString());
			writer.Flush();
		}
	}
}
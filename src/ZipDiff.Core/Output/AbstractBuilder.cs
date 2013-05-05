using System.IO;

namespace ZipDiff.Core.Output
{
	abstract class AbstractBuilder : IBuilder
	{
		public void Build(string path, Differences diff)
		{
			using (var writer = new StreamWriter(path))
			{
				Build(writer, diff);
			}
		}

		public abstract void Build(StreamWriter writer, Differences diff);
	}
}
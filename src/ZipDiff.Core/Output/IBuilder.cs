using System.IO;

namespace ZipDiff.Core.Output
{
	public interface IBuilder
	{
		void Build(string path, Differences diff);
		void Build(StreamWriter writer, Differences diff);
	}
}

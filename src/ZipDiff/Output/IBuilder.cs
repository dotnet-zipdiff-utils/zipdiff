using System.IO;

namespace ZipDiff.Output
{
	interface IBuilder
	{
		void Build(string path, Differences diff);
		void Build(StreamWriter writer, Differences diff);
	}
}

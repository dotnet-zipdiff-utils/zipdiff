namespace ZipDiff.Core.Output
{
	using System.IO;

	public interface IBuilder
	{
		void Build(string path, Differences diff);
		void Build(StreamWriter writer, Differences diff);
	}
}
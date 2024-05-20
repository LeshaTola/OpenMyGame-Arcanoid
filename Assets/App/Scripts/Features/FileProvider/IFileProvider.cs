using UnityEngine;

namespace Features.FileProvider
{
	public interface IFileProvider
	{
		TextAsset GetTextAsset(string path);
	}
}

using UnityEngine;

namespace Features.FileProvider
{
	public class ResourcesFileProvider : IFileProvider
	{
		public TextAsset GetTextAsset(string path)
		{
			return Resources.Load<TextAsset>(path);
		}
	}
}

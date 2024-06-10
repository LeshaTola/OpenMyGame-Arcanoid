using Features.FileProvider;
using Newtonsoft.Json;

namespace Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders
{
	public class LevelInfoProvider : ILevelInfoProvider
	{
		private IFileProvider fileProvider;

		public LevelInfoProvider(IFileProvider fileProvider)
		{
			this.fileProvider = fileProvider;
		}

		public LevelInfo GetLevelInfoByPath(string path)
		{
			/*if (string.IsNullOrEmpty(path))
			{
				Debug.LogError($"filePath is empty");
			}

			if (!File.Exists(path))
			{
				Debug.LogError($"file doesn't exist: {path}");
			}

			using (StreamReader reader = new StreamReader(path))
			{
				return GetLevelInfo(reader.ReadToEnd());
			}*/

			return GetLevelInfo(fileProvider.GetTextAsset(path).text);
		}

		public LevelInfo GetLevelInfo(string json)
		{
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}
	}
}

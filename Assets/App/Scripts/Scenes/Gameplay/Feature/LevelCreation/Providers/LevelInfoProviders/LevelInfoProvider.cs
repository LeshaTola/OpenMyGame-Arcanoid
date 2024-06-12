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
			return GetLevelInfo(fileProvider.GetTextAsset(path).text);
		}

		public LevelInfo GetLevelInfo(string json)
		{
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}
	}
}

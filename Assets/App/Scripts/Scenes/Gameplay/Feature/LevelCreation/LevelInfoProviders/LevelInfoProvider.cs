using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders
{
	public class LevelInfoProvider : ILevelInfoProvider
	{
		public LevelInfo GetLevelInfoByPath(string path)
		{
			if (string.IsNullOrEmpty(path))
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
			}
		}

		public LevelInfo GetLevelInfo(string json)
		{
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}
	}
}

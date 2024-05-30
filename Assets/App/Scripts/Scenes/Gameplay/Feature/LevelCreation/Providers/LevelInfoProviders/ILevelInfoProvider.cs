namespace Scenes.Gameplay.Feature.LevelCreation.LevelInfoProviders
{
	public interface ILevelInfoProvider
	{
		LevelInfo GetLevelInfoByPath(string path);
		LevelInfo GetLevelInfo(string json);
	}
}

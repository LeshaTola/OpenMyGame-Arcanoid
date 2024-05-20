namespace Scenes.Gameplay.Feature.LevelCreation
{
	public interface ILevelGenerator
	{
		void DestroyLevel();
		void GenerateLevel(LevelInfo levelInfo);
	}
}
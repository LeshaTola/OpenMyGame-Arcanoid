namespace Scenes.Gameplay.Feature.LevelCreation.Saves
{
	public interface ILevelSavingService
	{
		LevelInfo LoadedLevelInfo { get; }

		void LoadData();
		void SaveData();
	}
}
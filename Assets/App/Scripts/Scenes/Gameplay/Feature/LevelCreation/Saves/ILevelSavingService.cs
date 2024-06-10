namespace Scenes.Gameplay.Feature.LevelCreation.Saves
{
	public interface ILevelSavingService
	{
		void Cleanup();
		void LoadData();
		void SaveData();
	}
}
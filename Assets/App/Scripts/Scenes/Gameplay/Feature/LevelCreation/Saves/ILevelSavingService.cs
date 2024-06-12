using Cysharp.Threading.Tasks;

namespace Scenes.Gameplay.Feature.LevelCreation.Saves
{
	public interface ILevelSavingService
	{
		void Cleanup();
		UniTask LoadDataAsync();
		void SaveData();
	}
}
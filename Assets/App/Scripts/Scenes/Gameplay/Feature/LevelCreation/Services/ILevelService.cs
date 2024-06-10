using Cysharp.Threading.Tasks;
using Features.Saves;
using Features.Saves.Gameplay.DTOs.Level;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Threading.Tasks;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public interface ILevelService
	{
		LevelState GetLevelState();
		void SetLevelState(LevelState levelState);

		UniTask SetupDefaultLevelAsync();
		Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData);

		void TurnOffColliders();
		void TurnOnColliders();
	}
}
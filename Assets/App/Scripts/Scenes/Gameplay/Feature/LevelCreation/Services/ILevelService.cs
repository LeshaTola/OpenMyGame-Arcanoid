using Cysharp.Threading.Tasks;
using Features.Saves;
using Features.Saves.Gameplay.DTOs.Level;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public interface ILevelService
	{
		List<Block> Blocks { get; }

		LevelState GetLevelState();
		UniTask SetLevelStateAsync(LevelState levelState);

		UniTask SetupDefaultLevelAsync();
		Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData);
	}
}
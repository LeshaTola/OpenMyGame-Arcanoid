using Cysharp.Threading.Tasks;
using Features.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Threading.Tasks;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public interface ILevelService
	{
		UniTask SetupDefaultLevelAsync();
		Task SetupLevelFromPackAsync(Pack currentPack, SavedPackData savedPackData);
	}
}
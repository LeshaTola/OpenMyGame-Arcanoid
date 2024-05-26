using Scenes.PackSelection.Feature.Packs;

namespace Assets.App.Scripts.Features.Saves.PlayerProgress.Controllers
{
	public interface IPlayerProgressSavesController
	{
		void SavePlayerProgress(IPackProvider packProvider);
		void LoadPlayerProgress(IPackProvider packProvider);
	}
}

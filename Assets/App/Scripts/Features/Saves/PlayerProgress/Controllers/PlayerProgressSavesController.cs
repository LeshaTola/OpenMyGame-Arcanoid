using Features.Saves;
using Module.Saves;
using Scenes.PackSelection.Feature.Packs;
using System.Linq;

namespace Assets.App.Scripts.Features.Saves.PlayerProgress.Controllers
{
	public class PlayerProgressSavesController : IPlayerProgressSavesController
	{

		private IDataProvider<PlayerProgressData> playerProgressDataProvider;

		public PlayerProgressSavesController(IDataProvider<PlayerProgressData> playerProgressDataProvider)
		{
			this.playerProgressDataProvider = playerProgressDataProvider;
		}

		public void SavePlayerProgress(IPackProvider packProvider)
		{
			//PlayerProgressData playerProgress = new();
		}

		public void LoadPlayerProgress(IPackProvider packProvider)
		{
			var playerProgressData = playerProgressDataProvider.GetData();
			if (playerProgressData == null)
			{
				playerProgressData = FormFirstPlayerProgressData(packProvider);
				playerProgressDataProvider.SaveData(playerProgressData);
			}
		}

		private PlayerProgressData FormFirstPlayerProgressData(IPackProvider packProvider)
		{
			PlayerProgressData playerProgress = new();

			foreach (var pack in packProvider.Packs)
			{

				SavedPackData savedPackData = new()
				{
					Id = pack.Id,
					CurrentLevel = 0,
					IsOpened = false,
					IsCompeted = false,
				};

				playerProgress.Packs.Add(pack.Id, savedPackData);
			}
			playerProgress.Packs[packProvider.Packs.Last().Id].IsOpened = true;
			playerProgress.IsFirstSession = true;
			return playerProgress;
		}
	}
}

using Features.Saves;
using Features.StateMachine.States;
using Module.Saves;
using Scenes.Gameplay.StateMachine.States.Win.Routers;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinState : State
	{
		private IRouterShowWin routerShowWin;
		private IPackProvider packProvider;
		private IDataProvider<PlayerProgressData> dataProvider;

		public WinState(IRouterShowWin routerShowWin, IPackProvider packProvider, IDataProvider<PlayerProgressData> dataProvider)
		{
			this.routerShowWin = routerShowWin;
			this.packProvider = packProvider;
			this.dataProvider = dataProvider;
		}

		public override void Enter()
		{
			base.Enter();
			PlayerProgressData playerData = dataProvider.GetData();

			ProcessPacks(playerData);
			routerShowWin.ShowWin();

			SaveData(playerData);
		}

		private void SaveData(PlayerProgressData playerData)
		{
			var savedData = packProvider.SavedPackData;
			playerData.Packs[savedData.Id] = savedData;
			dataProvider.SaveData(playerData);
		}

		private void ProcessPacks(PlayerProgressData playerData)
		{
			SavedPackData savedPackData = packProvider.SavedPackData;
			Pack originalPack = packProvider.Packs[packProvider.PackIndex];
			if (savedPackData == null || originalPack == null)
			{
				return;
			}

			if (savedPackData.CurrentLevel <= originalPack.MaxLevel)
			{
				CompleteLevel(savedPackData);
			}
			else
			{
				OpenNextPack(savedPackData, playerData);
			}
		}

		private void CompleteLevel(SavedPackData savedPackData)
		{
			savedPackData.CurrentLevel++;
		}

		private void OpenNextPack(SavedPackData savedPackData, PlayerProgressData playerData)
		{
			savedPackData.IsCompeted = true;
			int nextPackIndex = packProvider.PackIndex - 1;
			if (packProvider.Packs.Count > nextPackIndex)
			{
				string nextPackID = packProvider.Packs[nextPackIndex].Id;
				playerData.Packs[nextPackID].IsOpened = true;
			}
		}
	}
}

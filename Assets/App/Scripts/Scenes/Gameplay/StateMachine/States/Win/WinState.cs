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

			routerShowWin.ShowWin(packProvider.CurrentPack, packProvider.SavedPackData);

			ProcessPacks(playerData);
		}

		private void ProcessPacks(PlayerProgressData playerData)
		{
			SavedPackData savedPackData = packProvider.SavedPackData;
			Pack currentPack = packProvider.CurrentPack;
			if (savedPackData == null || currentPack == null)
			{
				return;
			}

			CompleteLevel(savedPackData, playerData);
			if (savedPackData.CurrentLevel > currentPack.MaxLevel)
			{
				OpenNextPack(savedPackData, playerData);
			}
			SaveData(playerData);
		}

		private void CompleteLevel(SavedPackData savedPackData, PlayerProgressData playerData)
		{
			savedPackData.CurrentLevel++;
			playerData.Packs[savedPackData.Id] = savedPackData;
		}

		private void OpenNextPack(SavedPackData savedPackData, PlayerProgressData playerData)
		{
			savedPackData.IsCompeted = true;

			int nextPackIndex = packProvider.PackIndex - 1;
			if (packProvider.Packs.Count <= nextPackIndex)
			{
				return;
			}
			string nextPackID = packProvider.Packs[nextPackIndex].Id;
			playerData.Packs[nextPackID].IsOpened = true;

			SetNextPack(playerData, nextPackIndex, nextPackID);
		}

		private void SetNextPack(PlayerProgressData playerData, int nextPackIndex, string nextPackID)
		{
			packProvider.PackIndex = nextPackIndex;
			packProvider.SavedPackData = playerData.Packs[nextPackID];
		}

		private void SaveData(PlayerProgressData playerData)
		{
			if (packProvider.SavedPackData == null)
			{
				return;
			}
			dataProvider.SaveData(playerData);
		}
	}
}

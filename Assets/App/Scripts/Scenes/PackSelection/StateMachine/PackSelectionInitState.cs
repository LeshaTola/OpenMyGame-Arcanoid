using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Module.Saves;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;
using Scenes.PackSelection.Feature.UI;
using System.Linq;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionInitState : State
	{
		private PackMenu packMenu;
		private HeaderUI headerUI;
		private IPackProvider packProvider;
		private ISceneTransition sceneTransition;
		private IDataProvider<PlayerProgressData> dataProvider;

		private PlayerProgressData playerProgressData;

		public PackSelectionInitState(PackMenu packMenu,
								HeaderUI headerUI,
								IPackProvider packProvider,
								ISceneTransition sceneTransition,
								IDataProvider<PlayerProgressData> dataProvider)
		{
			this.packMenu = packMenu;
			this.headerUI = headerUI;
			this.packProvider = packProvider;
			this.sceneTransition = sceneTransition;
			this.dataProvider = dataProvider;
		}

		public override void Enter()
		{
			headerUI.OnExitButtonClicked += OnExitButtonClicked;
			packMenu.OnPackSelected += OnPackSelected;

			LoadPlayerProgress();
			packMenu.GeneratePackList(packProvider.Packs, playerProgressData);
			sceneTransition.PlayOff();
		}

		private void LoadPlayerProgress()
		{
			var loadedData = dataProvider.GetData();

			if (loadedData == null)
			{
				loadedData = FormFirstSaveData();
				dataProvider.SaveData(loadedData);
			}
			playerProgressData = loadedData;

		}

		public override void Exit()
		{
			headerUI.OnExitButtonClicked -= OnExitButtonClicked;
			packMenu.OnPackSelected -= OnPackSelected;
		}

		private void OnPackSelected(Pack pack)
		{
			SavedPackData savedPackData = playerProgressData.Packs[pack.Id];

			packProvider.PackIndex = packProvider.Packs.IndexOf(pack);
			packProvider.SavedPackData = savedPackData;
			if (savedPackData.CurrentLevel > pack.MaxLevel)
			{
				savedPackData.CurrentLevel = 0;
			}

			StateMachine.ChangeState<LoadSceneState>();
		}

		public void OnExitButtonClicked()
		{
			StateMachine.ChangeState<LoadMainMenuState>();
		}


		private PlayerProgressData FormFirstSaveData()//TODO Move
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
			return playerProgress;

		}
	}
}
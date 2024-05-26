using Features.Energy;
using Features.Energy.Providers;
using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Module.Saves;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;
using Scenes.PackSelection.Feature.UI;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionInitState : State
	{
		private PackMenu packMenu;
		private HeaderUI headerUI;
		private IPackProvider packProvider;
		private ISceneTransition sceneTransition;
		private IDataProvider<PlayerProgressData> playerProgressDataProvider;
		private IEnergyController energyController;
		private IEnergyProvider energyProvider;

		private PlayerProgressData playerProgressData;

		public PackSelectionInitState(PackMenu packMenu,
								HeaderUI headerUI,
								IPackProvider packProvider,
								ISceneTransition sceneTransition,
								IDataProvider<PlayerProgressData> playerProgressDataProvider,
								IEnergyController energyController,
								IEnergyProvider energyProvider)
		{
			this.packMenu = packMenu;
			this.headerUI = headerUI;
			this.packProvider = packProvider;
			this.sceneTransition = sceneTransition;
			this.playerProgressDataProvider = playerProgressDataProvider;
			this.energyController = energyController;
			this.energyProvider = energyProvider;
		}

		public override void Enter()
		{
			headerUI.OnExitButtonClicked += OnExitButtonClicked;
			packMenu.OnPackSelected += OnPackSelected;

			energyController.UpdateUI();

			var loadedData = playerProgressDataProvider.GetData();
			playerProgressData = loadedData;
			packMenu.GeneratePackList(packProvider.Packs, loadedData);
			sceneTransition.PlayOff();
		}

		public override void Exit()
		{
			headerUI.OnExitButtonClicked -= OnExitButtonClicked;
			packMenu.OnPackSelected -= OnPackSelected;
		}

		private void OnPackSelected(Pack pack)
		{
			if (energyProvider.CurrentEnergy < energyProvider.Config.PlayCost)
			{
				return;
			}

			SetSelectedPack(pack);
			energyProvider.ReduceEnergy(energyProvider.Config.PlayCost);
			StateMachine.ChangeState<LoadSceneState>();
		}

		private void SetSelectedPack(Pack pack)
		{
			SavedPackData savedPackData = playerProgressData.Packs[pack.Id];

			packProvider.PackIndex = packProvider.Packs.IndexOf(pack);
			packProvider.SavedPackData = savedPackData;
			if (savedPackData.CurrentLevel > pack.MaxLevel)
			{
				savedPackData.CurrentLevel = 0;
			}
		}

		private void OnExitButtonClicked()
		{
			StateMachine.ChangeState<LoadMainMenuState>();
		}
	}
}
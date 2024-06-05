using Features.Energy.Controllers;
using Features.Energy.Providers;
using Features.Routers;
using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
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
		private IEnergyController energyController;
		private IEnergyProvider energyProvider;
		private IRouterShowInfoPopup routerShowInfo;


		public PackSelectionInitState(PackMenu packMenu,
								HeaderUI headerUI,
								IPackProvider packProvider,
								ISceneTransition sceneTransition,
								IEnergyController energyController,
								IEnergyProvider energyProvider,
								IRouterShowInfoPopup routerShowInfo)
		{
			this.packMenu = packMenu;
			this.headerUI = headerUI;
			this.packProvider = packProvider;
			this.sceneTransition = sceneTransition;
			this.energyController = energyController;
			this.energyProvider = energyProvider;
			this.routerShowInfo = routerShowInfo;
		}

		public override void Enter()
		{
			headerUI.OnExitButtonClicked += OnExitButtonClicked;
			packMenu.OnPackSelected += OnPackSelected;

			energyController.UpdateUI();

			packMenu.GeneratePackList(packProvider.Packs, packProvider.PacksData);
			sceneTransition.PlayOff();
		}

		public override void Exit()
		{
			energyController.CleanUp();
			headerUI.OnExitButtonClicked -= OnExitButtonClicked;
			packMenu.OnPackSelected -= OnPackSelected;
		}

		private void OnPackSelected(Pack pack)
		{
			if (energyProvider.CurrentEnergy < energyProvider.Config.PlayCost)
			{
				routerShowInfo.ShowInfo("not enough energy");
				return;
			}

			SetSelectedPack(pack);
			energyProvider.ReduceEnergy(energyProvider.Config.PlayCost);
			StateMachine.ChangeState<LoadSceneState>();
		}

		private void SetSelectedPack(Pack pack)
		{
			SavedPackData savedPackData = packProvider.PacksData[pack.Id];

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
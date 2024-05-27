using Features.Energy.Providers;
using Features.Routers;
using Features.StateMachine.States;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class LoadNextLevelCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;
		private IEnergyProvider energyProvider;
		private IRouterShowInfoPopup routerInfoPopup;

		public string Label { get; }
		public bool IsNextLevel { get; set; } = true;

		public LoadNextLevelCommand(Features.StateMachine.StateMachine stateMachine,
							  IPopupController popupController,
							  IEnergyProvider energyProvider,
							  IRouterShowInfoPopup routerInfoPopup,
							  string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			this.energyProvider = energyProvider;
			this.routerInfoPopup = routerInfoPopup;
			Label = label;
		}

		public void Execute()
		{
			if (IsNextLevel)
			{
				LoadNextLevel();
				return;
			}
			LoadMainMenu();
		}

		private void LoadMainMenu()
		{
			stateMachine.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}

		private void LoadNextLevel()
		{
			if (energyProvider.CurrentEnergy < energyProvider.Config.MaxEnergy)
			{
				routerInfoPopup.ShowInfo("not enough energy");
				return;
			}
			energyProvider.ReduceEnergy(energyProvider.Config.PlayCost);

			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
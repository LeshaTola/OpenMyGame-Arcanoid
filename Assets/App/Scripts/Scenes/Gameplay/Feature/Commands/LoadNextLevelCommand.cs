using Features.Energy.Providers;
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
		IEnergyProvider energyProvider;

		public string Label { get; }
		public bool IsNextLevel { get; set; } = true;

		public LoadNextLevelCommand(Features.StateMachine.StateMachine stateMachine,
							  IPopupController popupController,
							  IEnergyProvider energyProvider,
							  string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			this.energyProvider = energyProvider;
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
				//TODO:Show popup
				return;
			}
			energyProvider.ReduceEnergy(energyProvider.Config.PlayCost);

			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
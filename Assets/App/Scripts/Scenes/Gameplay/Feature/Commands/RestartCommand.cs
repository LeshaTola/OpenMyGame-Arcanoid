using Features.Energy.Providers;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class RestartCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;
		private IEnergyProvider energyProvider;

		public string Label { get; }

		public RestartCommand(Features.StateMachine.StateMachine stateMachine,
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
			if (energyProvider.CurrentEnergy < energyProvider.Config.PlayCost)
			{
				return;
			}

			energyProvider.ReduceEnergy(energyProvider.Config.PlayCost);
			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
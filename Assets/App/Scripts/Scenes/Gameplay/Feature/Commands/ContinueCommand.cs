using Features.Energy.Providers;
using Features.Routers;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class ContinueCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;
		private IRouterShowInfoPopup routerInfoPopup;
		private IEnergyProvider energyProvider;

		public string Label { get; }

		public ContinueCommand(Features.StateMachine.StateMachine stateMachine,
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
			int continueCost = (int)(energyProvider.Config.PlayCost * energyProvider.Config.ContinueCostMultiplier);
			if (continueCost < energyProvider.Config.PlayCost)
			{
				routerInfoPopup.ShowInfo("not enough energy");
				return;
			}
			energyProvider.ReduceEnergy(continueCost);

			popupController.HidePopup();
			stateMachine.ChangeState<ResetState>();
		}
	}
}
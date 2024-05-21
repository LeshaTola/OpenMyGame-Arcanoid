using Features.Commands;
using Features.Popups.LossPopup;
using Features.Popups.LossPopup.ViewModels;
using Features.StateMachine;
using Module.PopupLogic.General;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class RouterShowLoss : IRouterShowLoss
	{
		private IPopupController popupController;
		private StateMachineHandler stateMachineHandler;
		private Transform container;

		public RouterShowLoss(IPopupController popupController, StateMachineHandler stateMachineHandler, Transform container)
		{
			this.popupController = popupController;
			this.stateMachineHandler = stateMachineHandler;
			this.container = container;
		}

		public void ShowLoss()
		{
			LossPopup popup = popupController.ShowPopup<LossPopup>();
			RestartCommand restartCommand = new RestartCommand(stateMachineHandler, popupController, "Restart");
			LossPopupViewModel lossPopupViewModel = new LossPopupViewModel(restartCommand);
			popup.transform.SetParent(container);
			popup.Setup(lossPopupViewModel);
		}
	}
}
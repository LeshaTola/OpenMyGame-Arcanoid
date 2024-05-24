using Features.Popups.Loss;
using Features.Popups.Loss.ViewModels;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;

namespace Scenes.Gameplay.StateMachine.States.Loss.Routers
{
	public class RouterShowLoss : IRouterShowLoss
	{
		private IPopupController popupController;
		private RestartCommand restartCommand;

		public RouterShowLoss(IPopupController popupController,
						RestartCommand restartCommand)
		{
			this.popupController = popupController;
			this.restartCommand = restartCommand;
		}

		public void ShowLoss()
		{
			LossPopup popup = popupController.GetPopup<LossPopup>();
			LossPopupViewModel lossPopupViewModel = new(restartCommand);
			popup.Setup(lossPopupViewModel);
			popup.Show();
		}
	}
}
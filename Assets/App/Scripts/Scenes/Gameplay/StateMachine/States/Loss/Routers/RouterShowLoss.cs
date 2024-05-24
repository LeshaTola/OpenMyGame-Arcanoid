using Features.Popups.Loss;
using Features.Popups.Loss.ViewModels;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;

namespace Scenes.Gameplay.StateMachine.States.Loss.Routers
{
	public class RouterShowLoss : IRouterShowLoss
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private RestartCommand restartCommand;

		public RouterShowLoss(IPopupController popupController,
						RestartCommand restartCommand,
						ILocalizationSystem localizationSystem)
		{
			this.popupController = popupController;
			this.restartCommand = restartCommand;
			this.localizationSystem = localizationSystem;
		}

		public void ShowLoss()
		{
			LossPopup popup = popupController.GetPopup<LossPopup>();
			LossPopupViewModel lossPopupViewModel = new(restartCommand, localizationSystem);
			popup.Setup(lossPopupViewModel);
			popup.Show();
		}
	}
}
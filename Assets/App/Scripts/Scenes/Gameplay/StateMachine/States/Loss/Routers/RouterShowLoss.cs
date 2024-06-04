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
		private ContinueCommand continueCommand;
		private BackCommand backCommand;

		public RouterShowLoss(IPopupController popupController,
						RestartCommand restartCommand,
						ContinueCommand continueCommand,
						BackCommand backCommand,
						ILocalizationSystem localizationSystem)
		{
			this.popupController = popupController;
			this.restartCommand = restartCommand;
			this.continueCommand = continueCommand;
			this.backCommand = backCommand;
			this.localizationSystem = localizationSystem;
		}

		public async void ShowLoss()
		{
			LossPopup popup = popupController.GetPopup<LossPopup>();
			LossPopupViewModel lossPopupViewModel = new(restartCommand, continueCommand, backCommand, localizationSystem);
			popup.Setup(lossPopupViewModel);
			await popup.Show();
		}
	}
}
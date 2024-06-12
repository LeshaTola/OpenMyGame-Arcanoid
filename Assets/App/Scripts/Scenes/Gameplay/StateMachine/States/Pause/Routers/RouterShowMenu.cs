using Features.Popups.Animations.Animator;
using Features.Popups.Menu;
using Features.Popups.Menu.ViewModels;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public class RouterShowMenu : IRouterShowMenu
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private IPopupAnimator popupAnimator;

		private RestartCommand restartCommand;
		private BackCommand backCommand;
		private ResumeCommand resumeCommand;

		public RouterShowMenu(IPopupController popupController,
						RestartCommand restartCommand,
						BackCommand backCommand,
						ResumeCommand resumeCommand,
						ILocalizationSystem localizationSystem,
						IPopupAnimator popupAnimator)
		{
			this.popupController = popupController;
			this.localizationSystem = localizationSystem;
			this.popupAnimator = popupAnimator;

			this.restartCommand = restartCommand;
			this.backCommand = backCommand;
			this.resumeCommand = resumeCommand;
		}

		public async void ShowMenu()
		{
			MenuPopup popup = popupController.GetPopup<MenuPopup>();
			MenuPopupViewModel popupViewModel = new(restartCommand,
										   backCommand,
										   resumeCommand,
										   localizationSystem,
										   popupAnimator);
			popup.Setup(popupViewModel);
			await popup.Show();
		}
	}
}
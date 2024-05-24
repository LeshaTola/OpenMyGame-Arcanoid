using Features.Popups.Menu;
using Features.Popups.Menu.ViewModels;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public class RouterShowMenu : IRouterShowMenu
	{
		private IPopupController popupController;

		private RestartCommand restartCommand;
		private BackCommand backCommand;
		private ResumeCommand resumeCommand;


		public RouterShowMenu(IPopupController popupController,
						RestartCommand restartCommand,
						BackCommand backCommand,
						ResumeCommand resumeCommand)
		{
			this.popupController = popupController;
			this.restartCommand = restartCommand;
			this.backCommand = backCommand;
			this.resumeCommand = resumeCommand;
		}

		public void ShowMenu()
		{
			MenuPopup popup = popupController.GetPopup<MenuPopup>();
			MenuPopupViewModel popupViewModel = new(restartCommand, backCommand, resumeCommand);
			popup.Setup(popupViewModel);
			popup.Show();
		}
	}
}
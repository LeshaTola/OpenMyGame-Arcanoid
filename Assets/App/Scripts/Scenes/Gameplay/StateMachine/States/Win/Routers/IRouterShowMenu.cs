using Features.Commands;
using Features.Popups.Menu;
using Features.Popups.Menu.ViewModels;
using Module.PopupLogic.General.Controller;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public interface IRouterShowMenu
	{
		void ShowMenu();
	}

	public class RouterShowMenu : IRouterShowMenu
	{
		private IPopupController popupController;

		private RestartCommand restartCommand;
		private BackCommand backCommand;
		private ResumeCommand resumeCommand;

		private Transform container;

		public RouterShowMenu(IPopupController popupController,
						RestartCommand restartCommand,
						BackCommand backCommand,
						ResumeCommand resumeCommand,
						Transform container)
		{
			this.popupController = popupController;

			this.restartCommand = restartCommand;
			this.backCommand = backCommand;
			this.resumeCommand = resumeCommand;

			this.container = container;
		}

		public void ShowMenu()
		{
			MenuPopup popup = popupController.ShowPopup<MenuPopup>();
			MenuPopupViewModel popupViewModel = new(restartCommand, backCommand, resumeCommand);
			popup.transform.SetParent(container);
			popup.Setup(popupViewModel);
		}
	}
}
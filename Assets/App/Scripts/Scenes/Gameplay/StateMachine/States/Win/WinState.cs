using Features.Popups;
using Features.StateMachine.States;
using Module.PopupLogic.General;
using Zenject;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinState : State
	{
		private IPopupController popupController;

		public WinState(IPopupController popupController)
		{
			this.popupController = popupController;
		}

		public override void Enter()
		{
			base.Enter();

			popupController.ShowPopup<WinPopup>();
		}

		public override void Exit()
		{
			base.Exit();
			popupController.HidePopup();
		}
	}
}

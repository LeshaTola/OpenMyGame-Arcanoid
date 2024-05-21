using Features.Popups;
using Features.StateMachine.States;
using Module.PopupLogic.General;
using Module.TimeProvider;

namespace Scenes.Gameplay.StateMachine.States
{
	public class PauseState : State
	{
		private IPopupController popupController;
		private ITimeProvider timeProvider;

		public PauseState(IPopupController popupController, ITimeProvider timeProvider)
		{
			this.popupController = popupController;
			this.timeProvider = timeProvider;
		}

		public override void Enter()
		{
			base.Enter();
			timeProvider.TimeMultiplier = 0;
			popupController.ShowPopup<MenuPopup>();
		}

		public override void Exit()
		{
			base.Exit();
			timeProvider.TimeMultiplier = 1;
			popupController.HidePopup();
		}
	}
}

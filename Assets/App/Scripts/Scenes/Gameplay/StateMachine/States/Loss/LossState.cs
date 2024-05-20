using Features.Popups;
using Features.StateMachine.States;
using Module.PopupLogic.General;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class LossState : State
	{
		IPopupController popupController;

		public LossState(IPopupController popupController)
		{
			this.popupController = popupController;
		}

		public override void Enter()
		{
			base.Enter();

			popupController.ShowPopup<LossPopup>();
		}

		public override void Exit()
		{
			base.Exit();
			popupController.HidePopup();
		}
	}
}

using Features.Popups;
using Features.StateMachine.States.General;
using Module.PopupLogic.General;
using Zenject;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class LossStateStep : StateStep
	{
		IPopupController popupController;

		[Inject]
		public LossStateStep(IPopupController popupController)
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

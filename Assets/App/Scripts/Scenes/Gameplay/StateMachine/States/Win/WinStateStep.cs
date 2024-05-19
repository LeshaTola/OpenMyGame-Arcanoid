using Features.Popups;
using Features.StateMachine.States.General;
using Module.PopupLogic.General;
using Zenject;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinStateStep : StateStep
	{
		IPopupController popupController;

		[Inject]
		public WinStateStep(IPopupController popupController)
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

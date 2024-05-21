using Features.StateMachine;
using Features.StateMachine.States;
using Module.Commands;
using Module.PopupLogic.General;

namespace Features.Commands
{
	public class BackCommand : ILabeledCommand
	{
		private StateMachineHandler stateMachineHandler;
		private IPopupController popupController;

		public string Label { get; }

		public BackCommand(StateMachineHandler stateMachineHandler, IPopupController popupController, string label)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.popupController = popupController;
			Label = label;
		}

		public void Execute()
		{
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}
	}
}
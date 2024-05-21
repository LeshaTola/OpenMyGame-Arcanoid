using Features.StateMachine;
using Module.Commands;
using Module.PopupLogic.General;
using Scenes.Gameplay.StateMachine.States;

namespace Features.Commands
{
	public class RestartCommand : ILabeledCommand
	{
		private StateMachineHandler stateMachineHandler;
		private IPopupController popupController;

		public string Label { get; }

		public RestartCommand(StateMachineHandler stateMachineHandler, IPopupController popupController, string label)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.popupController = popupController;
			Label = label;
		}


		public void Execute()
		{
			stateMachineHandler.Core.ChangeState<InitialState>();
			popupController.HidePopup();
		}
	}
}
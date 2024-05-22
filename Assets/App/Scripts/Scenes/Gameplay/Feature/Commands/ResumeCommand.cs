using Features.StateMachine;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class ResumeCommand : ILabeledCommand
	{
		private StateMachineHandler stateMachineHandler;
		private IPopupController popupController;

		public string Label { get; }

		public ResumeCommand(StateMachineHandler stateMachineHandler, IPopupController popupController, string label)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.popupController = popupController;
			Label = label;
		}

		public void Execute()
		{
			stateMachineHandler.Core.ChangeState<GameplayState>();
			popupController.HidePopup();
		}
	}
}
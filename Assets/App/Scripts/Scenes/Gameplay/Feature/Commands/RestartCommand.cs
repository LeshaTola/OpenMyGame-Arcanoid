using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class RestartCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;

		public string Label { get; }

		public RestartCommand(Features.StateMachine.StateMachine stateMachine, IPopupController popupController, string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			Label = label;
		}

		public void Execute()
		{
			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
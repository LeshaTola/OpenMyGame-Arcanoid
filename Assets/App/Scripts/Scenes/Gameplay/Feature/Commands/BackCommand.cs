using Features.StateMachine.States;
using Module.Commands;
using Module.PopupLogic.General.Controller;

namespace Scenes.Gameplay.Feature.Commands
{
	public class BackCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;

		public string Label { get; }

		public BackCommand(Features.StateMachine.StateMachine stateMachine, IPopupController popupController, string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			Label = label;
		}

		public void Execute()
		{
			ExecuteAsync();
		}

		public async void ExecuteAsync()
		{
			await popupController.HidePopup();
			stateMachine.ChangeState<LoadSceneState>();
		}
	}
}
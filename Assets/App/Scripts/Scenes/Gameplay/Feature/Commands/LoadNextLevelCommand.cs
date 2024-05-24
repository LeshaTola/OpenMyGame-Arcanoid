using Features.StateMachine.States;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;

namespace Scenes.Gameplay.Feature.Commands
{
	public class LoadNextLevelCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;

		public string Label { get; }
		public bool IsNextLevel { get; set; } = true;

		public LoadNextLevelCommand(Features.StateMachine.StateMachine stateMachine,
							  IPopupController popupController,
							  string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			Label = label;
		}

		public void Execute()
		{
			if (IsNextLevel)
			{
				LoadNextLevel();
				return;
			}
			LoadMainMenu();
		}

		private void LoadMainMenu()
		{
			stateMachine.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}

		private void LoadNextLevel()
		{
			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
using Features.StateMachine.States;
using Module.Commands;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.StateMachine.States;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.Gameplay.Feature.Commands
{
	public class LoadNextLevelCommand : ILabeledCommand
	{
		private Features.StateMachine.StateMachine stateMachine;
		private IPopupController popupController;
		private IPackProvider packProvider;

		public string Label { get; }

		public LoadNextLevelCommand(Features.StateMachine.StateMachine stateMachine,
							  IPopupController popupController,
							  IPackProvider packProvider,
							  string label)
		{
			this.stateMachine = stateMachine;
			this.popupController = popupController;
			this.packProvider = packProvider;
			Label = label;
		}

		public void Execute()
		{
			Pack currentPack = packProvider.CurrentPack;
			if (currentPack == null || currentPack.CurrentLevel == currentPack.MaxLevel)
			{
				LoadMainMenu();
				return;
			}
			LoadNextLevel(currentPack);
		}

		private void LoadMainMenu()
		{
			stateMachine.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}

		private void LoadNextLevel(Pack currentPack)
		{
			currentPack.CurrentLevel++;
			popupController.HidePopup();
			stateMachine.ChangeState<InitialState>();
		}
	}
}
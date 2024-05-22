using Features.StateMachine;
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
		private StateMachineHandler stateMachineHandler;
		private IPopupController popupController;
		private IPackProvider packProvider;

		public string Label { get; }

		public LoadNextLevelCommand(StateMachineHandler stateMachineHandler,
							  IPopupController popupController,
							  IPackProvider packProvider,
							  string label)
		{
			this.stateMachineHandler = stateMachineHandler;
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
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}

		private void LoadNextLevel(Pack currentPack)
		{
			currentPack.CurrentLevel++;
			popupController.HidePopup();
			stateMachineHandler.Core.ChangeState<InitialState>();
		}
	}
}
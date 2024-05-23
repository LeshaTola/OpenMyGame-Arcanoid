using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;
using Scenes.PackSelection.Feature.UI;
using UnityEngine;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionInitState : State
	{
		private PackMenu packMenu;
		private HeaderUI headerUI;
		private IPackProvider packProvider;
		private ISceneTransition sceneTransition;

		public PackSelectionInitState(PackMenu packMenu,
								HeaderUI headerUI,
								IPackProvider packProvider,
								ISceneTransition sceneTransition)
		{
			this.packMenu = packMenu;
			this.headerUI = headerUI;
			this.packProvider = packProvider;
			this.sceneTransition = sceneTransition;
		}

		public override void Enter()
		{
			headerUI.OnExitButtonClicked += OnExitButtonClicked;
			packMenu.OnPackSelected += OnPackSelected;

			packMenu.GeneratePackList(packProvider.Packs);
			sceneTransition.PlayOff();
		}

		public override void Exit()
		{
			headerUI.OnExitButtonClicked -= OnExitButtonClicked;
			packMenu.OnPackSelected -= OnPackSelected;
		}

		private void OnPackSelected(Pack pack)
		{
			packProvider.IndexOfOriginal = packProvider.Packs.IndexOf(pack);
			packProvider.CurrentPack = GameObject.Instantiate(pack);
			if (pack.CurrentLevel == pack.MaxLevel)
			{
				packProvider.CurrentPack.CurrentLevel = 0;
			}
			StateMachine.ChangeState<LoadSceneState>();
		}

		public void OnExitButtonClicked()
		{
			StateMachine.ChangeState<LoadMainMenuState>();
		}
	}
}
using Features.StateMachine;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;
using UnityEngine;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionInitState : State
	{
		private StateMachineHandler stateMachineHandler;
		private PackMenu packMenu;
		private IPackProvider packProvider;
		private ISceneTransition sceneTransition;

		public PackSelectionInitState(StateMachineHandler stateMachineHandler,
								PackMenu packMenu,
								IPackProvider packProvider,
								ISceneTransition sceneTransition)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.packMenu = packMenu;
			this.packProvider = packProvider;
			this.sceneTransition = sceneTransition;
		}

		public override void Enter()
		{
			packMenu.OnPackSelected += OnPackSelected;
			packMenu.GeneratePackList(packProvider.Packs);
			sceneTransition.PlayOff();
		}

		private void OnPackSelected(Pack pack)
		{
			packProvider.IndexOfOriginal = packProvider.Packs.IndexOf(pack);
			packProvider.CurrentPack = GameObject.Instantiate(pack);
			if (pack.CurrentLevel == pack.MaxLevel)
			{
				packProvider.CurrentPack.CurrentLevel = 0;
			}
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
		}
	}
}
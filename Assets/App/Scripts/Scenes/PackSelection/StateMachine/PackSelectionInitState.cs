using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.PackSelection.Feature.Packs.UI;
using UnityEngine;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionInitState : State
	{
		[SerializeField] private PackMenu packMenu;
		[SerializeField] private ISceneTransition sceneTransition;

		public override void Enter()
		{
			packMenu.GeneratePackList();
			sceneTransition.PlayOff();
		}
	}
}
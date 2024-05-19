using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using UnityEngine;

namespace Scenes.Main.StateMachine.States
{
	public class MainMenuInitialState : State
	{
		[SerializeField] private ISceneTransition sceneTransition;

		public override void Enter()
		{
			base.Enter();
			sceneTransition.PlayOff();
		}
	}
}

using Features.StateMachine;
using Features.StateMachine.States;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class GameplayState : State
	{
		[SerializeField] private List<IUpdatable> updatables;

		public override void Update()
		{
			base.Update();
			foreach (var updatable in updatables)
			{
				updatable.Update();
			}
		}
	}
}

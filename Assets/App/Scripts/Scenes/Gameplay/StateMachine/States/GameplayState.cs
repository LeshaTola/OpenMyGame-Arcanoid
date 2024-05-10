using System.Collections.Generic;
using App.Scripts.Features.StateMachine;
using App.Scripts.Features.StateMachine.States;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.StateMachine.States
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

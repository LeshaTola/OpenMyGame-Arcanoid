using Features.StateMachine;
using Features.StateMachine.States;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : State
{
	//[SerializeField] private List<SerializableInterface<IUpdatable>> updatables;
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

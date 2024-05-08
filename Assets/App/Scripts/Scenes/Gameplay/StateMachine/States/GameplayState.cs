using Features.StateMachine;
using Features.StateMachine.States;
using System.Collections.Generic;
using TNRD;

public class GameplayState : State
{
	private List<SerializableInterface<IUpdatable>> updatables;

	public GameplayState(StateMachine stateMachine, List<SerializableInterface<IUpdatable>> updatables) : base(stateMachine)
	{
		this.updatables = updatables;
	}

	public override void Update()
	{
		base.Update();
		foreach (var updatable in updatables)
		{
			updatable.Value.Update();
		}
	}
}

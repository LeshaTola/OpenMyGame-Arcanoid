using Features.StateMachine.States.General;
using System.Collections.Generic;

namespace Features.StateMachine.States
{
	public abstract class State
	{
		protected StateMachine StateMachine;
		protected List<IStateStep> StateSteps;

		public virtual void Init(StateMachine stateMachine)
		{
			StateMachine = stateMachine;
			StateSteps = new();
		}

		public virtual void Enter()
		{
			foreach (var step in StateSteps)
			{
				step.Enter();
			}
		}

		public virtual void Exit()
		{
			foreach (var step in StateSteps)
			{
				step.Exit();
			}

		}

		public virtual void Update()
		{

			foreach (var step in StateSteps)
			{
				step.Update();
			}
		}


		public void AddStep(IStateStep step)
		{
			StateSteps.Add(step);
			step.Init(this, StateMachine);
		}

		public void RemoveStep(IStateStep step)
		{
			StateSteps.Remove(step);
		}
	}
}
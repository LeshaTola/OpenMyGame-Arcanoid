namespace Features.StateMachine.States.General
{
	public abstract class StateStep : IStateStep
	{
		public bool IsComplete { get; private set; }

		public virtual void Enter() { IsComplete = false; }
		public virtual void Exit() { IsComplete = true; }
		public virtual void Update() { }
	}


}

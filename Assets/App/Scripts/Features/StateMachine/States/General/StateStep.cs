namespace Features.StateMachine.States.General
{
	public abstract class StateStep : IStateStep
	{
		protected State ParentState;
		protected StateMachine StateMachine;

		public void Init(State parentState, StateMachine stateMachine)
		{
			ParentState = parentState;
			StateMachine = stateMachine;
		}

		public bool IsComplete { get; private set; }

		public virtual void Enter() { IsComplete = false; }
		public virtual void Exit() { IsComplete = true; }
		public virtual void Update() { }
	}
}

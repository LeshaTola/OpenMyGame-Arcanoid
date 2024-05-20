namespace Features.StateMachine.States.General
{
	public interface IStateStep
	{
		public bool IsComplete { get; }

		public void Init(State parentState, StateMachine stateMachine);
		public void Enter();
		public void Exit();
		public void Update();
	}
}

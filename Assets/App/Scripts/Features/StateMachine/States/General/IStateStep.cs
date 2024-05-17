namespace Features.StateMachine.States.General
{
	public interface IStateStep
	{
		public bool IsComplete { get; }
		public void Enter();
		public void Exit();
		public void Update();
	}
}

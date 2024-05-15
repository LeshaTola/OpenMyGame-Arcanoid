namespace Features.StateMachine.States
{
	public abstract class State
	{
		protected StateMachine StateMachine;

		public void Init(StateMachine stateMachine)
		{
			StateMachine = stateMachine;
		}

		public virtual void Enter() { }
		public virtual void Exit() { }
		public virtual void Update() { }
	}
}
namespace App.Scripts.Features.StateMachine.States
{
	public abstract class State
	{
		protected StateMachine stateMachine;

		public void Init(StateMachine stateMachine)
		{
			this.stateMachine = stateMachine;
		}

		public virtual void Enter() { }
		public virtual void Exit() { }
		public virtual void Update() { }
	}
}
using Features.StateMachine.States;
using System.Collections.Generic;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		private IEnumerable<IResetable> resetables;

		public ResetState(IEnumerable<IResetable> resetables)
		{
			this.resetables = resetables;
		}

		public override void Enter()
		{
			base.Enter();
			foreach (IResetable resetable in resetables)
			{
				resetable.Reset();
			}

			StateMachine.ChangeState<GameplayState>();
		}
	}
}

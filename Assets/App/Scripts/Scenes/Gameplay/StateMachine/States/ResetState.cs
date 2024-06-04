using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Reset.Services;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		private IResetService resetService;

		public ResetState(IResetService resetService)
		{
			this.resetService = resetService;
		}

		public override void Enter()
		{
			base.Enter();

			resetService.Reset();
			StateMachine.ChangeState<GameplayState>();
		}
	}
}

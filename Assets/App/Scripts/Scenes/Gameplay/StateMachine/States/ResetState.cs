using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Reset.Services;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		private IResetService resetService;
		private IBonusService bonusService;

		public ResetState(IResetService resetService, IBonusService bonusService)
		{
			this.resetService = resetService;
			this.bonusService = bonusService;
		}

		public override void Enter()
		{
			base.Enter();
			resetService.Reset();
			bonusService.Cleanup();
			StateMachine.ChangeState<GameplayState>();
		}
	}
}

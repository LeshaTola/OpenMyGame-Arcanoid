using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.Reset.Services;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		private IResetService resetService;
		private IBonusServicesProvider bonusServicesProvider;

		public ResetState(IResetService resetService,
					   IBonusServicesProvider bonusServicesProvider)
		{
			this.resetService = resetService;
			this.bonusServicesProvider = bonusServicesProvider;
		}

		public override void Enter()
		{
			base.Enter();

			resetService.Reset();
			bonusServicesProvider.Cleanup();
			StateMachine.ChangeState<GameplayState>();
		}
	}
}

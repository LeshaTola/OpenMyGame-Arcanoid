using Features.StateMachine.States;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.LevelCreation.Saves;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.StateMachine.States.Win.Routers;

namespace Scenes.Gameplay.StateMachine.States
{
	public class PauseState : State
	{
		private IRouterShowMenu routerShowMenu;
		private ITimeProvider timeProvider;
		private IBallService ballService;
		private ILevelSavingService levelSavingService;

		public PauseState(IRouterShowMenu routerShowMenu,
					ITimeProvider timeProvider,
					IBallService ballService,
					ILevelSavingService levelSavingService)
		{
			this.routerShowMenu = routerShowMenu;
			this.timeProvider = timeProvider;
			this.ballService = ballService;
			this.levelSavingService = levelSavingService;
		}

		public override void Enter()
		{
			base.Enter();
			levelSavingService.SaveData();
			timeProvider.TimeMultiplier = 0;
			routerShowMenu.ShowMenu();
			ballService.StopBalls();
		}

		public override void Exit()
		{
			base.Exit();
			timeProvider.TimeMultiplier = 1;
			ballService.PushBalls();
		}
	}
}

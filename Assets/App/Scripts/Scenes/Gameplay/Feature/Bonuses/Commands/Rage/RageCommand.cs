using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Player.Ball.Services;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Rage
{
	public class RageCommand : BonusCommand
	{
		private IBallService ballService;
		private ILevelService levelService;

		public RageCommand(IBallService ballService,
					 ILevelService levelService)
		{
			this.ballService = ballService;
			this.levelService = levelService;
		}

		public override void StartBonus()
		{
			base.StartBonus();

			ballService.ActivateRageMode();
			levelService.TurnOffColliders();
		}

		public override void StopBonus()
		{
			base.StopBonus();

			ballService.DeactivateRageMode();
			levelService.TurnOnColliders();
		}
	}
}

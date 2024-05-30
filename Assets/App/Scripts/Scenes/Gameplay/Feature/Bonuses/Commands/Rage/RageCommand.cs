using Scenes.Gameplay.Feature.LevelCreation.Providers.Level;
using Scenes.Gameplay.Feature.Player.Ball.Services;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Rage
{
	public class RageCommand : BonusCommand
	{
		private IBallService ballService;
		private ILevelProvider levelProvider;

		public RageCommand(IBallService ballService,
					 ILevelProvider levelProvider)
		{
			this.ballService = ballService;
			this.levelProvider = levelProvider;
		}

		public override void StartBonus()
		{
			base.StartBonus();

			ballService.ActivateRageMode();
			levelProvider.TurnOffColliders();
		}

		public override void StopBonus()
		{
			base.StopBonus();

			ballService.DeactivateRageMode();
			levelProvider.TurnOnColliders();
		}
	}
}

using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Rage
{
	public class RageCommand : BonusCommand
	{
		[SerializeField] private BonusConfig config;

		private IBallService ballService;
		private ILevelService levelService;

		public RageCommand(IBallService ballService,
					 ILevelService levelService,
					  BonusConfig config)
		{
			this.ballService = ballService;
			this.levelService = levelService;
			this.config = config;
		}

		public override BonusConfig Config => config;

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

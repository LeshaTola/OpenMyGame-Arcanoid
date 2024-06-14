using Scenes.Gameplay.Feature.RageMode.Services;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Rage
{
	public class RageCommand : BonusCommand
	{
		[SerializeField] private BonusConfig config;

		private IRageModeService rageModeService;

		public RageCommand(IRageModeService rageModeService,
					  BonusConfig config)
		{
			this.rageModeService = rageModeService;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			rageModeService.ActivateRageMode();
		}

		public override void StopBonus()
		{
			base.StopBonus();
			rageModeService.DeactivateRageMode();
		}
	}
}

using Scenes.Gameplay.Feature.Autopilot.Services;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.AutoPilot
{
	public class Autopilot : BonusCommand
	{
		[SerializeField] private BonusConfig config;

		private IAutopilotService autopilotServiceService;

		public Autopilot(IAutopilotService autopilotServiceService,
					  BonusConfig config)
		{
			this.autopilotServiceService = autopilotServiceService;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			autopilotServiceService.ActivateAutopilot();
		}

		public override void StopBonus()
		{
			base.StopBonus();
			autopilotServiceService.DeactivateAutopilot();
		}
	}
}

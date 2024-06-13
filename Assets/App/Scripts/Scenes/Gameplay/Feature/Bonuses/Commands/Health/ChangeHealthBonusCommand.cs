using Scenes.Gameplay.Feature.Health;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Health
{
	[Serializable]
	public class ChangeHealthBonusConfig : BonusConfig
	{
		[SerializeField] private int health;

		public int Health { get => health; }
	}

	public class ChangeHealthBonusCommand : BonusCommand
	{
		[SerializeField] private ChangeHealthBonusConfig config;

		private IHealthController controller;

		public ChangeHealthBonusCommand(IHealthController controller,
								  ChangeHealthBonusConfig config)
		{
			this.controller = controller;
			this.config = config;
		}

		public override BonusConfig Config { get => config; }

		public override void StartBonus()
		{
			if (config.Health <= 0)
			{
				if (controller.CurrentHealth > 1)
				{
					controller.ReduceHealth(config.Health * -1);
				}
				return;
			}

			controller.AddHealth(config.Health);
		}
	}
}

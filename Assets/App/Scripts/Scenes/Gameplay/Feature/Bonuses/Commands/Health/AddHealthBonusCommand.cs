using Scenes.Gameplay.Feature.Health;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Health
{
	public class AddHealthBonusCommand : BonusCommand
	{
		[SerializeField, Min(0)] private int health;

		private IHealthController controller;

		public AddHealthBonusCommand(IHealthController controller)
		{
			this.controller = controller;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);
			AddHealthBonusCommand concreteCommand = ((AddHealthBonusCommand)command);
			health = concreteCommand.health;
		}

		public override void StartBonus()
		{
			controller.AddHealth(health);
		}
	}
}

using Scenes.Gameplay.Feature.Health;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Health
{
	public class ReduceHealthBonusCommand : BonusCommand
	{
		[SerializeField, Min(0)] private int health;

		private IHealthController controller;

		public ReduceHealthBonusCommand(IHealthController controller)
		{
			this.controller = controller;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);
			ReduceHealthBonusCommand concreteCommand = ((ReduceHealthBonusCommand)command);
			health = concreteCommand.health;
		}

		public override void StartBonus()
		{
			controller.ReduceHealth(health);
		}
	}
}

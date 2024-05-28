using Scenes.Gameplay.Feature.Health;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Health
{
	public class ReduceHealthBonusCommand : IBonusCommand
	{
		[SerializeField] private Sprite sprite;
		[SerializeField] private Sprite blockSprite;
		[SerializeField] private int health;

		private IHealthController controller;

		public ReduceHealthBonusCommand(IHealthController controller)
		{
			this.controller = controller;
		}

		public Sprite Sprite { get => sprite; }
		public Sprite BlockSprite { get => blockSprite; }

		public void Clone(IBonusCommand command)
		{
			ReduceHealthBonusCommand concreteCommand = ((ReduceHealthBonusCommand)command);
			health = concreteCommand.health;
			sprite = concreteCommand.sprite;
		}

		public void StartBonus()
		{
			controller.ReduceHealth(health);
		}
	}
}

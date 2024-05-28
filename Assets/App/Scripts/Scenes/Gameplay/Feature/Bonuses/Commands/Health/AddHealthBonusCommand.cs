using Scenes.Gameplay.Feature.Health;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Health
{
	public class AddHealthBonusCommand : IBonusCommand
	{
		[SerializeField] private Sprite sprite;
		[SerializeField] private Sprite blockSprite;
		[SerializeField] private int health;

		private IHealthController controller;

		public AddHealthBonusCommand(IHealthController controller)
		{
			this.controller = controller;
		}

		public Sprite Sprite { get => sprite; }
		public Sprite BlockSprite { get => blockSprite; }

		public void Clone(IBonusCommand command)
		{
			AddHealthBonusCommand concreteCommand = ((AddHealthBonusCommand)command);
			health = concreteCommand.health;
			sprite = concreteCommand.sprite;
		}

		public void StartBonus()
		{
			controller.AddHealth(health);
		}
	}
}

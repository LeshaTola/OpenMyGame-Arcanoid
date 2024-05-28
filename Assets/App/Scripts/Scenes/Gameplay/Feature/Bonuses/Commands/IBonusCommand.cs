using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public interface IBonusCommand
	{
		Sprite Sprite { get; }
		Sprite BlockSprite { get; }

		void Clone(IBonusCommand command);
		void StartBonus();
	}
}

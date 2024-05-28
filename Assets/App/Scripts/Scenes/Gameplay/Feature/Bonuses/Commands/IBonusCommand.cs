using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public interface IBonusCommand
	{
		int Id { get; }
		Sprite Sprite { get; }
		Sprite BlockSprite { get; }
		float Duration { get; }

		float Timer { get; set; }

		void Clone(IBonusCommand command);
		void StartBonus();
		void StopBonus();
	}
}

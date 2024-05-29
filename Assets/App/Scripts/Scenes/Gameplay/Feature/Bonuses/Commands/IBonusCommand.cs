using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public interface IBonusCommand
	{
		int Id { get; }
		Sprite Sprite { get; }
		Sprite BlockSprite { get; }
		float Duration { get; }

		List<int> Conflicts { get; }

		float Timer { get; set; }

		void Clone(IBonusCommand command);
		void StartBonus();
		void StopBonus();
	}
}

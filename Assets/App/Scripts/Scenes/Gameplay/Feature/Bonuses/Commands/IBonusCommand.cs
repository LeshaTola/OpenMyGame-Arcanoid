using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public interface IBonusCommand
	{
		string Id { get; }
		Sprite Sprite { get; }
		Sprite BlockSprite { get; }
		float Duration { get; }

		List<string> Conflicts { get; }

		float Timer { get; set; }

		public void Init(string Id);
		void Clone(IBonusCommand command);
		void StartBonus();
		void StopBonus();
	}
}

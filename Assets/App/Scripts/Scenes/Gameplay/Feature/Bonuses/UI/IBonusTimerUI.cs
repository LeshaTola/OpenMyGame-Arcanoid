using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.UI
{
	public interface IBonusTimerUI
	{
		void UpdateSprite(Sprite sprite);
		void UpdateTimer(float timerValue);
	}
}
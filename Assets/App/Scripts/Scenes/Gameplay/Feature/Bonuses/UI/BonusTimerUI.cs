using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Feature.Bonuses.UI
{
	public class BonusTimerUI : MonoBehaviour, IBonusTimerUI
	{
		[SerializeField] private Image bonusImage;
		[SerializeField] private Slider timerSlider;

		public void UpdateSprite(Sprite sprite)
		{
			bonusImage.sprite = sprite;
		}

		public void UpdateTimer(float timerValue)
		{
			timerSlider.value = timerValue;
		}
	}
}

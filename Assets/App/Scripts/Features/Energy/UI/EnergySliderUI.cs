using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Energy.UI
{
	public class EnergySliderUI : MonoBehaviour, IEnergySliderUI
	{
		[SerializeField] private Slider slider;
		[SerializeField] private TextMeshProUGUI energyText;
		[SerializeField] private TextMeshProUGUI recoveringTimerText;

		public void UpdateUI(int currentValue, int maxValue)
		{
			energyText.text = $"{currentValue}/{maxValue}";

			if (currentValue >= maxValue)
			{
				recoveringTimerText.gameObject.SetActive(false);
			}
			else
			{
				recoveringTimerText.gameObject.SetActive(true);
			}

			float value = CalculateSliderValue(currentValue, maxValue);
			slider.value = value;
		}

		public void UpdateTimer(int totalSeconds)
		{
			int minutes = totalSeconds / 60;
			int seconds = totalSeconds % 60;
			string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
			recoveringTimerText.text = timeString;
		}

		private static float CalculateSliderValue(int currentValue, int maxValue)
		{
			float value = (float)currentValue / maxValue;
			value = Mathf.Clamp01(value);
			return value;
		}
	}
}

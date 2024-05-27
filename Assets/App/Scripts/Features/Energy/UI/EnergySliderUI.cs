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

			float value = CalculateSliderValue(currentValue, maxValue);
			slider.value = value;
		}

		public void UpdateTimer(int totalSeconds)
		{
			int minutes = totalSeconds / 60;
			int seconds = totalSeconds % 60;
			recoveringTimerText.text = $"{minutes}:{seconds}";
		}

		private static float CalculateSliderValue(int currentValue, int maxValue)
		{
			float value = (float)currentValue / maxValue;
			value = Mathf.Clamp01(value);
			return value;
		}
	}
}

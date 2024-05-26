using Features.Energy.Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Energy.UI
{
	public class EnergySliderUI : MonoBehaviour, IEnergySliderUI
	{
		[SerializeField] private Slider slider;
		[SerializeField] private TextMeshProUGUI energyText;

		private IEnergyProvider energyProvider;

		[Inject]
		public void Construct(IEnergyProvider energyProvider)
		{
			this.energyProvider = energyProvider;

			energyProvider.OnEnergyChanged += OnEnergyChanged;
		}

		public void UpdateUI(int currentValue, int maxValue)
		{
			energyText.text = $"{currentValue}/{maxValue}";

			float value = CalculateSliderValue(currentValue, maxValue);
			slider.value = value;
		}

		private static float CalculateSliderValue(int currentValue, int maxValue)
		{
			float value = (float)currentValue / maxValue;
			value = Mathf.Clamp01(value);
			return value;
		}

		private void OnEnergyChanged()
		{
			CalculateSliderValue(energyProvider.CurrentEnergy, energyProvider.Config.MaxEnergy);
		}
	}
}

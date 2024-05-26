﻿using Features.Energy.Providers;
using Features.Energy.UI;

namespace Features.Energy
{
	public class EnergyController : IEnergyController
	{
		IEnergySliderUI energySliderUI;
		IEnergyProvider energyProvider;

		public EnergyController(IEnergySliderUI energySliderUI, IEnergyProvider energyProvider)
		{
			this.energySliderUI = energySliderUI;
			this.energyProvider = energyProvider;

			energyProvider.OnEnergyChanged += OnEnergyChanged;
			energyProvider.OnEnergyTimerChanged += OnEnergyTimerChanged;
		}

		public void UpdateUI()
		{
			energySliderUI.UpdateUI(energyProvider.CurrentEnergy, energyProvider.Config.MaxEnergy);
		}

		private void OnEnergyChanged()
		{
			UpdateUI();
		}

		private void OnEnergyTimerChanged()
		{
			energySliderUI.UpdateTimer((int)energyProvider.RemainingRecoveryTime);
		}
	}
}

using Features.Energy.Providers;
using Module.Saves;
using System;

namespace Features.Saves.Energy.Controllers
{
	public class EnergySavesController : IEnergySavesController
	{
		private IDataProvider<EnergyData> energyDataProvider;

		public EnergySavesController(IDataProvider<EnergyData> energyDataProvider)
		{
			this.energyDataProvider = energyDataProvider;
		}

		public void SaveEnergyData(IEnergyProvider energyProvider)
		{
			var energyData = new EnergyData()
			{
				Energy = energyProvider.CurrentEnergy,
				RemainingRecoveryTime = energyProvider.RemainingRecoveryTime,
				ExitTime = DateTime.Now
			};
			energyDataProvider.SaveData(energyData);
		}

		public void LoadEnergyData(IEnergyProvider energyProvider)
		{
			var energyData = energyDataProvider.GetData();
			if (energyData == null)
			{
				energyData = FormFirstEnergyData(energyProvider);
				energyDataProvider.SaveData(energyData);
			}
			//Calculate energy between sessions

			energyProvider.AddEnergy(energyData.Energy);
		}

		private EnergyData FormFirstEnergyData(IEnergyProvider energyProvider)
		{
			EnergyData energyData = new()
			{
				Energy = energyProvider.Config.MaxEnergy,
				RemainingRecoveryTime = 0,
				ExitTime = DateTime.Now
			};
			return energyData;
		}
	}
}

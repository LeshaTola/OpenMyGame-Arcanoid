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

			int totalEnergy = energyData.Energy;
			int additionalEnergy = GetAdditionalEnergyBetweenSessions(energyProvider, energyData, totalEnergy);
			totalEnergy += additionalEnergy;

			energyProvider.AddEnergy(totalEnergy);
		}

		private int GetAdditionalEnergyBetweenSessions(IEnergyProvider energyProvider, EnergyData energyData, int totalEnergy)
		{
			if (totalEnergy >= energyProvider.Config.MaxEnergy)
			{
				return 0;
			}

			TimeSpan timeSpan = DateTime.Now - energyData.ExitTime;
			float recoverAmount = (float)(timeSpan.TotalSeconds + energyData.RemainingRecoveryTime) / energyProvider.Config.RecoveryTime;
			int recoverAmountInt = (int)recoverAmount;

			float remainingRecoveryTime = (recoverAmount - recoverAmountInt) * energyProvider.Config.RecoveryTime;
			energyProvider.RemainingRecoveryTime = remainingRecoveryTime;

			int offlineEnergy = recoverAmountInt * energyProvider.Config.RecoveryEnergy;
			int remainingEnergy = energyProvider.Config.MaxEnergy - totalEnergy;

			return GetAdditionalEnergy(offlineEnergy, remainingEnergy);
		}

		private static int GetAdditionalEnergy(int offlineEnergy, int remainingEnergy)
		{
			int additionalEnergy = remainingEnergy;
			if (remainingEnergy > offlineEnergy)
			{
				additionalEnergy = offlineEnergy;
			}

			return additionalEnergy;
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

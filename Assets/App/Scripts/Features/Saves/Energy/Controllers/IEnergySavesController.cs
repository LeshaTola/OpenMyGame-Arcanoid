using Features.Energy.Providers;

namespace Features.Saves.Energy.Controllers
{
	public interface IEnergySavesController
	{
		void LoadEnergyData(IEnergyProvider energyProvider);
		void SaveEnergyData(IEnergyProvider energyProvider);
	}
}
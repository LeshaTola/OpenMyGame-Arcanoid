using Assets.App.Scripts.Features.Saves.PlayerProgress.Controllers;
using Features.Energy.Providers;
using Features.ProjectCondition.Providers;
using Features.Saves.Energy.Controllers;
using Scenes.PackSelection.Feature.Packs;

namespace Features.Saves
{
	public class ProjectSavesController : IProjectSavesController
	{
		private IEnergySavesController energySavesController;
		private IEnergyProvider energyProvider;

		private IPlayerProgressSavesController playerProgressSavesController;
		private IPackProvider packProvider;

		public ProjectSavesController(IProjectConditionProvider projectConditionProvider,
								IPlayerProgressSavesController playerProgressSavesController,
								IEnergySavesController energySavesController,
								IEnergyProvider energyProvider,
								IPackProvider packProvider)
		{
			this.playerProgressSavesController = playerProgressSavesController;
			this.energySavesController = energySavesController;
			this.energyProvider = energyProvider;
			this.packProvider = packProvider;

			projectConditionProvider.OnApplicationStart += OnApplicationStart; ;
			projectConditionProvider.OnApplicationQuitted += OnApplicationQuitted;
			projectConditionProvider.OnApplicationPaused += OnApplicationPaused;
		}

		public void SaveAllData()
		{
			energySavesController.SaveEnergyData(energyProvider);
		}

		private void LoadAllData()
		{
			energySavesController.LoadEnergyData(energyProvider);
			playerProgressSavesController.LoadPlayerProgress(packProvider);
		}

		private void OnApplicationStart()
		{
			LoadAllData();
			energyProvider.StartEnergyRecoveringAsync(energyProvider.RemainingRecoveryTime);
		}

		private void OnApplicationPaused(bool pause)
		{
			if (pause)
			{
				SaveAllData();
			}
		}

		private void OnApplicationQuitted()
		{
			SaveAllData();
		}
	}
}

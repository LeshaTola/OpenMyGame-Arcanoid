using Features.Energy.Providers;
using Features.ProjectCondition.Providers;
using Features.Saves.Gameplay.Providers;
using Scenes.PackSelection.Feature.Packs;

namespace Features.Saves
{
	public class ProjectSavesController : IProjectSavesController
	{
		private IEnergyProvider energyProvider;
		private IPackProvider packProvider;
		private IGameplaySavesProvider gameplaySavesProvider;

		public ProjectSavesController(IProjectConditionProvider projectConditionProvider,
								IEnergyProvider energyProvider,
								IPackProvider packProvider,
								IGameplaySavesProvider gameplaySavesProvider)
		{
			this.energyProvider = energyProvider;
			this.packProvider = packProvider;
			this.gameplaySavesProvider = gameplaySavesProvider;

			projectConditionProvider.OnApplicationStart += OnApplicationStart;
			projectConditionProvider.OnApplicationQuitted += OnApplicationQuitted;
			projectConditionProvider.OnApplicationPaused += OnApplicationPaused;
		}

		public void SaveAllData()
		{
			energyProvider.SaveData();
			packProvider.SaveData();
			gameplaySavesProvider.SaveData();
		}

		private void LoadAllData()
		{
			energyProvider.LoadData();
			packProvider.LoadData();
		}

		private void OnApplicationStart()
		{
			LoadAllData();
			energyProvider.StartEnergyRecoveringAsync();
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

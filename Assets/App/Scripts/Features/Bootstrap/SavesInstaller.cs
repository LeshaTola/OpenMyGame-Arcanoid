using Assets.App.Scripts.Features.Saves.PlayerProgress.Controllers;
using Features.ProjectCondition.Providers;
using Features.Saves;
using Features.Saves.Energy;
using Features.Saves.Energy.Controllers;
using Features.Saves.Energy.Keys;
using Features.Saves.Keys;
using Features.Saves.Localization;
using Features.Saves.Localization.Keys;
using Module.Saves;
using TNRD;
using UnityEngine;
using Zenject;

namespace Features.Bootstrap
{
	public class SavesInstaller : MonoInstaller
	{
		[SerializeField] private SerializableInterface<IProjectConditionProvider> projectConditionProvider;

		public override void InstallBindings()
		{
			BindStorage();
			BindPlayerProgressDataProvider();
			BindLocalizationDataProvider();
			BindEnergyDataProvider();

			Container.Bind<IEnergySavesController>().To<EnergySavesController>().AsSingle();
			Container.Bind<IPlayerProgressSavesController>().To<PlayerProgressSavesController>().AsSingle();

			Container.Bind<IProjectSavesController>().To<ProjectSavesController>().AsSingle().NonLazy();
			Container.Bind<IProjectConditionProvider>().FromInstance(projectConditionProvider.Value).AsSingle();
		}

		private void BindStorage()
		{
			Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
		}

		private void BindPlayerProgressDataProvider()
		{
			Container.Bind<IDataProvider<PlayerProgressData>>()
				.To<DataProvider<PlayerProgressData>>()
				.AsSingle()
				.WithArguments(PlayerProgressDataKey.KEY);
		}

		private void BindLocalizationDataProvider()
		{
			Container.Bind<IDataProvider<LocalizationData>>()
				.To<DataProvider<LocalizationData>>()
				.AsSingle()
				.WithArguments(LocalizationDataKey.KEY);
		}

		private void BindEnergyDataProvider()
		{
			Container.Bind<IDataProvider<EnergyData>>()
				.To<DataProvider<EnergyData>>()
				.AsSingle()
				.WithArguments(EnergyDataKey.KEY);
		}

	}
}

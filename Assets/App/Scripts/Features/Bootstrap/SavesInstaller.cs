using Features.Energy.Saves;
using Features.Energy.Saves.Keys;
using Features.ProjectCondition.Providers;
using Features.Saves;
using Features.Saves.Gameplay;
using Features.Saves.Gameplay.Keys;
using Features.Saves.Gameplay.Providers;
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
			BindGameplayDataProvider();
			Container.Bind<IGameplaySavesProvider>().To<GameplaySavesProvider>().AsSingle();

			Container.Bind<IProjectSavesController>().To<ProjectSavesController>().AsSingle().NonLazy();
			Container.Bind<IProjectConditionProvider>().FromInstance(projectConditionProvider.Value).AsSingle();
		}

		private void BindGameplayDataProvider()
		{
			Container.Bind<IDataProvider<GameplayData>>()
				.To<DataProvider<GameplayData>>()
				.AsSingle()
				.WithArguments(GameplayDataKey.KEY);
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

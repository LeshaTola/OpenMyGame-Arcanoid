using Features.Energy.Configs;
using Features.Energy.Providers;
using Features.Popups.Languages;
using Features.ProjectInitServices;
using Module.Localization;
using Module.Localization.Configs;
using Module.Localization.Parsers;
using Module.Scenes;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Bootstrap
{
	public class GlobalInstaller : MonoInstaller
	{
		[SerializeField] private int targetFrameRate = 60;
		[SerializeField] private List<Pack> packs;
		[SerializeField] private PopupButton popupButtonTemplate;

		[Header("Energy")]
		[SerializeField] private EnergyConfig energyConfig;

		[Header("Localization")]
		[SerializeField] private string startLanguage;
		[SerializeField] private LocalizationDictionary localizationDictionary;

		public override void InstallBindings()
		{
			BindEnergyProvider();

			BindParser();
			BindLocalizationSystem();

			BindInitProjectService();
			BindSceneLoadService();

			BindPackProvider();
			BindButtonsFactory();
		}

		private void BindEnergyProvider()
		{
			Container.Bind<IEnergyProvider>()
				.To<EnergyProvider>()
				.AsSingle()
				.WithArguments(energyConfig);
		}

		private void BindButtonsFactory()
		{
			Container.Bind<IButtonsFactory>()
				.To<ButtonsFactory>()
				.AsSingle()
				.WithArguments(popupButtonTemplate);
		}

		private void BindLocalizationSystem()
		{
			Container.Bind<ILocalizationSystem>()
				.To<LocalizationSystem>()
				.AsSingle()
				.WithArguments(startLanguage, localizationDictionary);
		}

		private void BindParser()
		{
			Container.Bind<IParser>()
				.To<CSVParser>()
				.AsSingle();
		}

		private void BindPackProvider()
		{
			Container.Bind<IPackProvider>()
				.To<PackProvider>()
				.AsSingle()
				.WithArguments(packs);
		}

		private void BindInitProjectService()
		{
			Container.Bind<IProjectInitService>()
							.To<ProjectInitService>()
							.AsSingle()
							.WithArguments(targetFrameRate);
		}

		private void BindSceneLoadService()
		{
			Container
				.Bind<ISceneLoadService>()
				.To<SceneLoadService>()
				.AsSingle();
		}
	}
}

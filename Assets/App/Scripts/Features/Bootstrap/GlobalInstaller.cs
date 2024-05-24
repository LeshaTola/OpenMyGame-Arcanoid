using Features.Popups.Languages;
using Features.ProjectInitServices;
using Features.Saves;
using Features.Saves.Keys;
using Features.Saves.Localization;
using Features.Saves.Localization.Keys;
using Module.Localization;
using Module.Localization.Configs;
using Module.Localization.Parsers;
using Module.Saves;
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

		[Header("Localization")]
		[SerializeField] private string startLanguage;
		[SerializeField] private LocalizationDictionary localizationDictionary;

		public override void InstallBindings()
		{
			BindParser();
			BindLocalizationSystem();

			BindStorage();
			BindPlayerProgressDataProvider();
			BindLocalizationDataProvider();

			BindInitProjectService();
			BindSceneLoadService();

			BindPackProvider();
			BindButtonsFactory();
		}

		private void BindLocalizationDataProvider()
		{
			Container.Bind<IDataProvider<LocalizationData>>()
				.To<DataProvider<LocalizationData>>()
				.AsSingle()
				.WithArguments(LocalizationDataKey.KEY);
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

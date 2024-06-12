using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.Bonuses.Controllers;
using Scenes.Gameplay.Feature.Bonuses.Factories;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Bonuses.Services.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.UI;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class BonusInstaller : MonoInstaller
	{
		[Header("Bonus")]
		[SerializeField] private BonusesDatabase bonusesDatabase;
		[SerializeField] private Bonus bonusTemplate;
		[SerializeField] private Transform container;
		[SerializeField] private int preloadCount;


		[Header("UI")]
		[SerializeField] private BonusTimerUI bonusesTimerTemplate;
		[SerializeField] private RectTransform BonusTimersContainer;

		public override void InstallBindings()
		{
			BindBonusTimersPool();
			BindBonusesController();

			BindBonusesPool();
			BindBonusCommandsFactory();
			BindBonusService();
			BindBonusServicesProvider();

		}

		private void BindBonusServicesProvider()
		{
			Container.Bind<IBonusServicesProvider>().To<BonusServicesProvider>().AsSingle().NonLazy();
		}

		private void BindBonusesController()
		{
			Container.Bind<IBonusesController>().To<BonusesController>().AsSingle().NonLazy();
		}

		private void BindBonusTimersPool()
		{
			Container.Bind<IPool<BonusTimerUI>>()
						.To<MonoBehObjectPool<BonusTimerUI>>()
						.AsSingle()
						.WithArguments(bonusesTimerTemplate, preloadCount, BonusTimersContainer);
		}

		private void BindBonusCommandsFactory()
		{
			Container.Bind<IBonusCommandsFactory>().To<BonusCommandsFactory>().AsSingle().WithArguments(bonusesDatabase);
		}

		private void BindBonusService()
		{
			Container.BindInterfacesTo<BonusCommandService>().AsSingle().NonLazy();
			Container.BindInterfacesTo<BonusService>().AsSingle().NonLazy();
		}

		private void BindBonusesPool()
		{
			Container.Bind<IPool<Bonus>>()
				.To<MonoBehObjectPool<Bonus>>()
				.AsSingle()
				.WithArguments(bonusTemplate, preloadCount, container);
		}
	}
}

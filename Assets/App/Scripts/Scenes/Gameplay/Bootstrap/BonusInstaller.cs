using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.Bonuses.Factories;
using Scenes.Gameplay.Feature.Bonuses.Services;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class BonusInstaller : MonoInstaller
	{
		[SerializeField] private Bonus bonusTemplate;
		[SerializeField] private Transform container;
		[SerializeField] private int preloadCount;

		[SerializeField] private BonusesDatabase bonusesDatabase;

		public override void InstallBindings()
		{
			BindBonusesPool();
			BindBonusCommandsFactory();
			BindBonusService();
		}

		private void BindBonusCommandsFactory()
		{
			Container.Bind<IBonusCommandsFactory>().To<BonusCommandsFactory>().AsSingle().WithArguments(bonusesDatabase);
		}

		private void BindBonusService()
		{
			Container.Bind<IBonusService>().To<BonusService>().AsSingle().NonLazy();
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

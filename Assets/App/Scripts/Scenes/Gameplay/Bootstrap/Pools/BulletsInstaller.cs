using Module.ObjectPool;
using Scenes.Gameplay.Feature.Player.Machineguns.Bullets;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class BulletsInstaller : MonoInstaller
	{
		[SerializeField] private int bulletCount;
		[SerializeField] private Bullet bulletTemplate;
		[SerializeField] private Transform container;

		public override void InstallBindings()
		{
			BindBulletsPool();
		}

		private void BindBulletsPool()
		{
			Container.Bind<IPool<Bullet>>()
				.To<MonoBehObjectPool<Bullet>>()
				.AsSingle()
				.WithArguments(bulletTemplate, bulletCount, container);
		}
	}
}

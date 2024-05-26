using Features.Bootstrap;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.UI;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PacksInstaller : MonoInstaller
	{
		[SerializeField] private PackUI packTemplate;
		[SerializeField] private PackMenu menu;

		public override void InstallBindings()
		{
			EnergyInstaller.Install(Container);

			BindPackFactory();
			BindMenu();
		}

		private void BindPackFactory()
		{
			Container.Bind<IPackFactory>()
					.To<PackFactory>()
					.AsSingle()
					.WithArguments(packTemplate);
		}

		private void BindMenu()
		{
			Container.BindInstance(menu);
		}
	}
}
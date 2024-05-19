using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.UI;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PacksInstaller : MonoInstaller
	{
		[SerializeField] private PackUI completePackTemplate;
		[SerializeField] private PackUI inProgressPackTemplate;
		[SerializeField] private PackUI closedPack;
		[SerializeField] private Transform container;
		[SerializeField] private PackMenu menu;

		public override void InstallBindings()
		{
			BindPackFactory();
		}

		private void BindPackFactory()
		{
			Container.Bind<IPackFactory>()
					.To<PackFactory>()
					.AsSingle()
					.WithArguments(completePackTemplate, inProgressPackTemplate, closedPack, container);
		}
	}
}
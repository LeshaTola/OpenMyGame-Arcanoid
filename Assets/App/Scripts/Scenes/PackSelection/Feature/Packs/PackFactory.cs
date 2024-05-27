using Scenes.PackSelection.Feature.Packs.UI;
using Zenject;

namespace Scenes.PackSelection.Feature.Packs
{
	public class PackFactory : IPackFactory
	{
		private PackUI packTemplate;

		private DiContainer diContainer;

		public PackFactory(PackUI packTemplate, DiContainer diContainer)
		{
			this.packTemplate = packTemplate;
			this.diContainer = diContainer;
		}

		public PackUI GetPackUI()
		{
			return diContainer.InstantiatePrefabForComponent<PackUI>(packTemplate);
		}
	}
}

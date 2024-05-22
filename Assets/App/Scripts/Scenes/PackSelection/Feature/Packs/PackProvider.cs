using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;

namespace Scenes.PackSelection.Feature.Packs
{
	public class PackProvider : IPackProvider
	{
		public List<Pack> Packs { get; }
		public Pack CurrentPack { get; set; }
		public int IndexOfOriginal { get; set; }

		public PackProvider(List<Pack> packs)
		{
			Packs = packs;
		}
	}
}

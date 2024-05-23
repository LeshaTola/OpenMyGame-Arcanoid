using Features.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;

namespace Scenes.PackSelection.Feature.Packs
{
	public class PackProvider : IPackProvider
	{
		public List<Pack> Packs { get; }
		public int PackIndex { get; set; }
		public SavedPackData SavedPackData { get; set; }

		public Pack CurrentPack => Packs[PackIndex];

		public PackProvider(List<Pack> packs)
		{
			Packs = packs;
		}
	}
}

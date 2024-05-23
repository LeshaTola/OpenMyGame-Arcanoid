using Features.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;

namespace Scenes.PackSelection.Feature.Packs
{
	public interface IPackProvider
	{
		List<Pack> Packs { get; }

		int PackIndex { get; set; }
		Pack CurrentPack { get; }
		SavedPackData SavedPackData { get; set; }
	}
}

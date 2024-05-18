using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.PackSelection.Feature.Packs
{
	public interface IPackFactory
	{
		PackUI GetPackUI(Pack pack);
	}
}
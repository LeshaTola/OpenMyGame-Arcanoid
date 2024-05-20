using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.PackSelection.Feature.Packs
{
	public interface IPackProvider
	{
		Pack CurrentPack { get; set; }
	}
}

using Cysharp.Threading.Tasks;

namespace Scenes.Gameplay.Feature.LevelCreation.Services
{
	public interface ILevelService
	{
		UniTask SetupLevelAsync();
	}
}
using Features.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public interface IRouterShowWin
	{
		public void ShowWin(Pack currentPack, SavedPackData savedPackData);
	}
}

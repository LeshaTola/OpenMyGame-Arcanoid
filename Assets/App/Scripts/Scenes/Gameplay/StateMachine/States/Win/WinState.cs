using Features.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Win.Routers;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinState : State
	{
		private IRouterShowWin routerShowWin;
		private IPackProvider packProvider;

		public WinState(IRouterShowWin routerShowWin, IPackProvider packProvider)
		{
			this.routerShowWin = routerShowWin;
			this.packProvider = packProvider;
		}

		public override void Enter()
		{
			base.Enter();
			ProcessPacks();
			routerShowWin.ShowWin();

		}

		private void ProcessPacks()
		{
			if (packProvider.CurrentPack == null)
			{
				return;
			}

			if (packProvider.CurrentPack.CurrentLevel == packProvider.CurrentPack.MaxLevel)
			{
				OpenNextPack();
			}
			else
			{
				CompleteLevel();
			}
		}

		private void CompleteLevel()
		{
			var originalPack = packProvider.Packs[packProvider.IndexOfOriginal];
			if (originalPack.CurrentLevel < packProvider.CurrentPack.CurrentLevel + 1)
			{
				originalPack.CurrentLevel++;
			}
		}

		private void OpenNextPack()
		{
			int nextPackIndex = packProvider.IndexOfOriginal - 1;
			if (packProvider.Packs.Count > nextPackIndex)
			{
				packProvider.Packs[nextPackIndex].IsOpened = true;
			}
		}
	}
}

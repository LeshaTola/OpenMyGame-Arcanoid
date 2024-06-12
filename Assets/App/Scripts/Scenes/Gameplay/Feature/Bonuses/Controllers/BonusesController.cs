using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Bonuses.UI;

namespace Scenes.Gameplay.Feature.Bonuses.Controllers
{
	public class BonusesController : IBonusesController
	{
		private IBonusesUI bonusesUI;
		private IPool<BonusTimerUI> pool;

		public BonusesController(IBonusesUI bonusesUI,
						   IBonusCommandService bonusService,
						   IPool<BonusTimerUI> pool)
		{
			this.bonusesUI = bonusesUI;
			this.pool = pool;

			bonusService.OnBonusStart += OnBonusStart;
			bonusService.OnBonusUpdate += OnBonusUpdate;
			bonusService.OnBonusStop += OnBonusStop;
		}

		private void OnBonusStart(IBonusCommand command)
		{
			BonusTimerUI bonusTimer = pool.Get();
			bonusesUI.AddTimer(command, bonusTimer);
		}

		private void OnBonusUpdate(IBonusCommand command)
		{
			bonusesUI.UpdateTimer(command);
		}

		private void OnBonusStop(IBonusCommand command)
		{
			BonusTimerUI removedTimer = bonusesUI.RemoveTimer(command);
			pool.Release(removedTimer);
		}
	}
}

using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses
{
	public class Bonus : MonoBehaviour
	{
		[SerializeField] private BonusVisual bonusVisual;

		private IBonusCommand bonusCommand;
		private IPool<Bonus> pool;
		IBonusService bonusService;

		public void Setup(IBonusCommand bonusCommand, IPool<Bonus> pool, IBonusService bonusService)
		{
			this.pool = pool;
			this.bonusCommand = bonusCommand;
			this.bonusService = bonusService;
			bonusVisual.UpdateVisual(bonusCommand.Sprite);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.TryGetComponent(out Plate plate))
			{
				bonusService.StartBonus(bonusCommand);
				Release();
			}
		}

		public void Release()
		{
			pool.Release(this);
		}
	}
}

using Module.ObjectPool;
using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Bonuses.General;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses
{
	public class Bonus : MonoBehaviour
	{
		[SerializeField] private BonusVisual visual;
		[SerializeField] private BonusMovement movement;

		private IBonusCommand bonusCommand;
		private IPool<Bonus> pool;
		IBonusService bonusService;

		public BonusMovement Movement { get => movement; }
		public IBonusCommand BonusCommand { get => bonusCommand; }

		public void Setup(IBonusCommand bonusCommand, IPool<Bonus> pool, IBonusService bonusService)
		{
			this.pool = pool;
			this.bonusCommand = bonusCommand;
			this.bonusService = bonusService;
			visual.UpdateVisual(bonusCommand.Sprite);
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

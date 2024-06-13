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
		[SerializeField] private Configs.BonusConfig config;

		private IBonusCommand bonusCommand;
		private IPool<Bonus> pool;
		private IBonusCommandService bonusService;

		public BonusMovement Movement { get => movement; }
		public IBonusCommand BonusCommand { get => bonusCommand; }
		public float SizeMultiplier { get; private set; }

		public void Setup(IBonusCommand bonusCommand, IPool<Bonus> pool, IBonusCommandService bonusService)
		{
			this.pool = pool;
			this.bonusCommand = bonusCommand;
			this.bonusService = bonusService;
			visual.UpdateVisual(bonusCommand.Config.Sprite);
		}

		public void Resize(float multiplier)
		{
			SizeMultiplier = config.SizeMultiplier.Clamp(multiplier);
			Resize();
		}

		private void Resize()
		{
			float xSize = transform.localScale.x * SizeMultiplier;
			float ySize = transform.localScale.y * SizeMultiplier;
			transform.localScale = new Vector2(xSize, ySize);
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
			SizeMultiplier = 1 / SizeMultiplier;
			Resize();
		}
	}
}

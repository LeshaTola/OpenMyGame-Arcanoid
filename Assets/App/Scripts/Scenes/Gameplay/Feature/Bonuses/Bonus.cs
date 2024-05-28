using Scenes.Gameplay.Feature.Bonuses.Commands;
using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses
{
	public class Bonus : MonoBehaviour
	{
		[SerializeField] private BonusVisual bonusVisual;

		private IBonusCommand bonusCommand;

		public void Setup(IBonusCommand bonusCommand)
		{
			this.bonusCommand = bonusCommand;
			bonusVisual.UpdateVisual(bonusCommand.Sprite);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.TryGetComponent(out Plate plate))
			{
				bonusCommand.StartBonus();
				Destroy(gameObject);
			}
		}
	}
}

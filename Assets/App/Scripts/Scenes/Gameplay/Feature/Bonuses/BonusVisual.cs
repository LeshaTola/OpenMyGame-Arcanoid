using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses
{
	public class BonusVisual : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void UpdateVisual(Sprite sprite)
		{
			spriteRenderer.sprite = sprite;
		}
	}
}

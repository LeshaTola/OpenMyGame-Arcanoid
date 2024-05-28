using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
	public class PlateVisual : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		private float defaultWidth;

		public void Init()
		{
			defaultWidth = spriteRenderer.size.x;
		}

		public void ChangeWidth(float multiplier)
		{
			spriteRenderer.size = new Vector2(defaultWidth * multiplier, spriteRenderer.size.y);
		}

		public void ResetWidth()
		{
			spriteRenderer.size = new Vector2(defaultWidth, spriteRenderer.size.y);
		}
	}
}

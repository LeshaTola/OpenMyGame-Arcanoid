using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks
{
	public class BlockVisual : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private SpriteRenderer crackRenderer;

		public void Init(Sprite sprite)
		{
			spriteRenderer.sprite = sprite;
			SetCrack(null);
		}

		public void Resize(float multiplier)
		{
			Vector3 localScale = transform.localScale;
			transform.localScale = new Vector2(localScale.x * multiplier, localScale.y * multiplier);
		}

		public void SetCrack(Sprite sprite)
		{
			crackRenderer.sprite = sprite;
		}
	}
}
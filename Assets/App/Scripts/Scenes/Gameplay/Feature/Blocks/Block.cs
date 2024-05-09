using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Blocks
{
	public class Block : MonoBehaviour
	{
		[SerializeField] private BlockConfig config;
		[SerializeField] private BoxCollider2D boxCollider;
		[SerializeField] private BlockVisual blockVisual;

		public BlockVisual BlockVisual { get => blockVisual; }
		public BlockConfig Config { get => config; }

		public float Width
		{
			get => boxCollider.size.x;
			private set => boxCollider.size = new Vector2(value, boxCollider.size.y);
		}

		public float Height
		{
			get => boxCollider.size.y;
			private set => boxCollider.size = new Vector2(boxCollider.size.x, value);
		}


		public void Init(BlockConfig config)
		{
			this.config = config;
			blockVisual.Init(config.Sprite);
		}

		public void ResizeBlock(float width)
		{
			float multiplier = width / Width;

			ResizeCollider(multiplier);
			blockVisual.Resize(multiplier);
		}

		private void ResizeCollider(float multiplier)
		{
			Width *= multiplier;
			Height *= multiplier;
		}
	}
}
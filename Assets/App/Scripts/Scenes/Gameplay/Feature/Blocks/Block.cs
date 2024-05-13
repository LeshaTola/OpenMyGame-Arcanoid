using Scenes.Gameplay.Feature.Blocks.Config;
using Scenes.Gameplay.Feature.Blocks.Config.Components;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks
{
	public class Block : MonoBehaviour
	{
		[SerializeField] private BlockConfig config;
		[SerializeField] private BlockVisual visual;
		[SerializeField] private BoxCollider2D boxCollider;

		public BlockVisual Visual { get => visual; }
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
			visual.Init(config.Sprite);
		}

		public void ResizeBlock(float width)
		{
			float multiplier = width / Width;

			ResizeCollider(multiplier);
			visual.Resize(multiplier);
		}

		private void ResizeCollider(float multiplier)
		{
			Width *= multiplier;
			Height *= multiplier;
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			var collisionComponent = config.GetComponent<CollisionComponent>();
			collisionComponent?.Execute();
		}
	}
}
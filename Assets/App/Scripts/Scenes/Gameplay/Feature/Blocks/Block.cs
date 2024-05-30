using Scenes.Gameplay.Feature.Blocks.Config;
using Scenes.Gameplay.Feature.Blocks.Config.Components;
using Scenes.Gameplay.Feature.Player.Ball;
using System.Collections.Generic;
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
		public Vector2Int MatrixPosition { get; private set; }
		public Dictionary<Vector2Int, Block> Neighbors { get; private set; }
		public BoxCollider2D BoxCollider { get => boxCollider; }

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

		public void Setup(Dictionary<Vector2Int, Block> neighbors, Vector2Int matrixPosition)
		{
			this.Neighbors = neighbors;
			MatrixPosition = matrixPosition;
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

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.gameObject.TryGetComponent(out Ball ball))
			{
				return;
			}

			var triggerComponent = config.GetComponent<TriggerComponent>();
			triggerComponent?.Execute();
		}
	}
}
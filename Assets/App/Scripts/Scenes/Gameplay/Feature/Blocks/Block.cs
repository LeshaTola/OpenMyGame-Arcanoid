using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using Scenes.Gameplay.Feature.Blocks.Config;
using Scenes.Gameplay.Feature.Blocks.Config.Components;
using Scenes.Gameplay.Feature.Damage;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Blocks
{
	public class Block : MonoBehaviour, IDamageable
	{
		[SerializeField] private BlockConfig config;
		[SerializeField] private BlockVisual visual;
		[SerializeField] private BoxCollider2D boxCollider;

		public BlockVisual Visual { get => visual; }
		public BlockConfig Config { get => config; }
		public Vector2Int MatrixPosition { get; private set; }
		public Dictionary<Vector2Int, Block> Neighbors { get; private set; }
		public BoxCollider2D BoxCollider { get => boxCollider; }
		public IBallService BallService { get; private set; }
		public KeyPool<PooledParticle> KeyPool { get; private set; }

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

		public float SizeMultiplier { get; private set; }

		[Inject]
		public void Construct(IBallService ballService, KeyPool<PooledParticle> keyPool)
		{
			BallService = ballService;
			KeyPool = keyPool;
		}

		public void Init(BlockConfig config)
		{
			this.config = config;

			visual.Init(config.Sprite);
		}

		public void Setup(Dictionary<Vector2Int, Block> neighbors, Vector2Int matrixPosition)
		{
			Neighbors = neighbors;
			MatrixPosition = matrixPosition;
		}

		public void Resize(float width)
		{
			SizeMultiplier = width / Width;

			ResizeCollider(SizeMultiplier);
			visual.Resize(SizeMultiplier);
		}

		private void ResizeCollider(float multiplier)
		{
			Width *= multiplier;
			Height *= multiplier;
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (config.TryGetComponent(out CollisionComponent collisionComponent))
			{
				collisionComponent.Collision2D = col;
				collisionComponent.Execute();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.gameObject.TryGetComponent(out Ball ball))
			{
				return;
			}

			if (config.TryGetComponent(out TriggerComponent triggerComponent))
			{
				triggerComponent.TriggerGameObject = collision.gameObject;
				triggerComponent.Execute();
			}
		}
	}
}
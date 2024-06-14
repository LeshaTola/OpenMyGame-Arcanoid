using Cysharp.Threading.Tasks;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.RageMode.Services;
using System.Threading;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird
{
	public class BirdLevelMechanics : ILevelMechanics
	{
		[SerializeField] private BirdConfig config;

		private IPool<Bird> birdsPool;
		private IFieldSizeProvider fieldSizeProvider;
		private ITimeProvider timeProvider;
		private IRageModeService rageModeService;

		private CancellationTokenSource cancellationTokenSource;
		private Bird bird;
		private bool isBirdAlive;
		private float timer;
		private Vector2 targetPosition;
		private Vector2 direction;

		public BirdLevelMechanics(IPool<Bird> birdsPool,
							IFieldSizeProvider fieldSizeProvider,
							ITimeProvider timeProvider,
							IRageModeService rageModeService,
							BirdConfig config)
		{
			this.birdsPool = birdsPool;
			this.fieldSizeProvider = fieldSizeProvider;
			this.rageModeService = rageModeService;
			this.timeProvider = timeProvider;
			this.config = config;

			timer = config.TimeToRespawn;
		}

		public void StartMechanics()
		{
			if (cancellationTokenSource == null)
			{
				cancellationTokenSource = new CancellationTokenSource();
				ControlBirdAsync(cancellationTokenSource.Token).Forget();
			}
		}

		public void StopMechanics()
		{
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
				cancellationTokenSource.Dispose();
				cancellationTokenSource = null;
			}
		}

		public void Cleanup()
		{
			if (bird != null)
			{
				rageModeService.RemoveEnraged(bird);
				birdsPool.Release(bird);
				bird = null;
				isBirdAlive = false;
			}

			StopMechanics();
		}

		private async UniTaskVoid ControlBirdAsync(CancellationToken cancellationToken)
		{
			while (true)
			{
				await UniTask.Yield();

				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}

				if (!isBirdAlive)
				{
					ProcessTimer();
					continue;
				}

				if (bird == null)
				{
					isBirdAlive = false;
					return;
				}

				if (!IsValidPosition())
				{
					OnBirdDeath();
				}

				MoveBird();
			}
		}

		private void ProcessTimer()
		{
			timer -= timeProvider.DeltaTime;
			if (timer <= 0)
			{
				timer = config.TimeToRespawn;
				isBirdAlive = true;
				SetupBird();
			}
		}

		private void MoveBird()
		{
			float xOffset = config.Speed * timeProvider.DeltaTime * targetPosition.x;
			if (xOffset == 0)
			{
				return;
			}

			float x = bird.transform.position.x + xOffset;
			float y = bird.transform.position.y + Mathf.Sin(x * config.Frequency) * config.Amplitude;
			bird.Move(new Vector2(x, y));
		}

		private void SetupBird()
		{
			if (bird == null)
			{
				bird = birdsPool.Get();
				rageModeService.AddEnraged(bird);
				bird.OnDeath += OnBirdDeath;
			}

			bird.gameObject.SetActive(true);
			bird.Init(config.Health);
			SetBirdPosition();
		}

		private void SetBirdPosition()
		{
			int sign = (Random.Range(0, 2) * 2) - 1;
			float xPosition = (fieldSizeProvider.GameField.Width / 2 + config.XOffset) * sign;
			float yPosition = fieldSizeProvider.GameField.Height * config.YPosition;
			bird.transform.position = new Vector2(xPosition, yPosition);

			targetPosition = new Vector2(-xPosition, yPosition);

			direction = targetPosition - (Vector2)bird.transform.position;
			direction.Normalize();
		}

		private bool IsValidPosition()
		{
			var gameField = fieldSizeProvider.GameField;
			Vector3 position = bird.transform.position;
			return position.x >= gameField.MinX - config.XOffset && position.x <= gameField.MaxX + config.XOffset;
		}

		private void OnBirdDeath()
		{
			isBirdAlive = false;
			bird.gameObject.SetActive(false);
		}
	}
}


using DG.Tweening;
using Features.StateMachine;
using Module.ObjectPool;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.Machineguns.Bullets;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player.Machineguns
{
	public class Machinegun : MonoBehaviour, IUpdatable
	{
		[SerializeField] private List<Transform> bulletSpawnPoints = new();

		private List<Vector3> defaultBulletSpawnPoints = new();
		private IPool<Bullet> bulletsPool;
		private ITimeProvider timeProvider;

		private float timeBetweenShots;
		private float timer;
		private BulletParams bulletParams;

		public bool IsActive { get; set; }

		[Inject]
		public void Construct(IPool<Bullet> bulletsPool, ITimeProvider timeProvider)
		{
			this.timeProvider = timeProvider;
			this.bulletsPool = bulletsPool;

			Init();
		}

		public void Init()
		{
			foreach (var spawnPoint in bulletSpawnPoints)
			{
				defaultBulletSpawnPoints.Add(spawnPoint.localPosition);
			}
		}

		public void Setup(float timeBetweenShots, BulletParams bulletParams)
		{
			this.timeBetweenShots = timeBetweenShots;
			this.bulletParams = bulletParams;
		}

		void IUpdatable.Update()
		{
			if (!IsActive)
			{
				return;
			}

			if (timer > 0)
			{
				timer -= timeProvider.DeltaTime;
				return;
			}

			timer = timeBetweenShots;
			foreach (var spawnPoint in bulletSpawnPoints)
			{
				Bullet bullet = bulletsPool.Get();
				bullet.Setup(bulletParams);
				bullet.transform.position = spawnPoint.position;
				bullet.Shoot(spawnPoint.up);
			}
		}

		public void ChangeWidth(float multiplier, float duration = 0)
		{

			for (int i = 0; i < bulletSpawnPoints.Count; i++)
			{
				Transform spawnPoint = bulletSpawnPoints[i];
				var distanceVector = defaultBulletSpawnPoints[i] - Vector3.zero;
				AnimateWidth(spawnPoint, spawnPoint.localPosition.x, distanceVector.x * multiplier, duration);
			}
		}

		public void ResetWidth(float duration = 0)
		{
			for (int i = 0; i < bulletSpawnPoints.Count; i++)
			{
				AnimateWidth(bulletSpawnPoints[i], bulletSpawnPoints[i].localPosition.x, defaultBulletSpawnPoints[i].x, duration);
			}
		}

		private void AnimateWidth(Transform spawnPoint, float from, float to, float duration = 0)
		{
			DOVirtual.Float(from, to, duration, value =>
			{
				spawnPoint.localPosition = new Vector2(value, spawnPoint.localPosition.y);
			});
		}
	}
}

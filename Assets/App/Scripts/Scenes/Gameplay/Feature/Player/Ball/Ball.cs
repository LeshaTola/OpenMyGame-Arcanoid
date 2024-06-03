using Scenes.Gameplay.Feature.Damage;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class Ball : MonoBehaviour, IDamager
	{
		public event Action<Ball, Collision2D> OnCollisionEnter;

		[SerializeField] private BallMovement movement;
		[SerializeField] private BallVisual visual;

		private IBallService service;

		public BallMovement Movement => movement;
		public BallVisual Visual { get => visual; }
		public IBallService Service { get => service; }

		public void Init(IBallService service)
		{
			this.service = service;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			OnCollisionEnter?.Invoke(this, collision);
		}

		public void Release()
		{
			if (service != null)
			{
				service.ReleaseBall(this);
				return;
			}
			Destroy(gameObject);
		}
	}
}

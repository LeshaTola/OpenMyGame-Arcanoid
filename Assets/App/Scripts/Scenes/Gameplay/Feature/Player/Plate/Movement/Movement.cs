using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.Configs;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
	public class Movement : IMovement
	{
		private MovementConfig config;
		private Transform transform;
		private ITimeProvider timeProvider;

		private Vector2 targetPosition;
		private float speed;

		public Movement(MovementConfig config,
				  Transform transform,
				  ITimeProvider timeProvider)
		{
			this.config = config;
			this.transform = transform;
			this.timeProvider = timeProvider;
		}

		public void Move(Vector2 targetPosition, float speedMultiplier = 1f)
		{
			speed = config.Speed * speedMultiplier;
			this.targetPosition = targetPosition;
			MoveImmediate();
		}

		public void ApplyDrag()
		{
			ApplyDrag(config.Drag);
		}

		private void ApplyDrag(float drag)
		{
			if (speed <= 0)
			{
				speed = 0;
				return;
			}

			speed -= drag * timeProvider.DeltaTime;
			MoveImmediate();
		}

		private void MoveImmediate()
		{
			transform.position = Vector2.MoveTowards(transform.position,
				 targetPosition,
				 timeProvider.DeltaTime * speed);
		}

		public void Stop()
		{
			targetPosition = transform.position;
			speed = 0f;
		}
	}
}

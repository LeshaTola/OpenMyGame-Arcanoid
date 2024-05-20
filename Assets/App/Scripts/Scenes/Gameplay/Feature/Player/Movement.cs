﻿using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.Configs;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
	public class Movement : IMovement
	{
		private MovementConfig config;
		private Rigidbody2D rb;
		private ITimeProvider timeProvider;

		public Movement(MovementConfig config,
				  Rigidbody2D rb,
				  ITimeProvider timeProvider)
		{
			this.config = config;
			this.rb = rb;
			this.timeProvider = timeProvider;
		}

		public void Move(Vector2 moveDirection)
		{
			if (moveDirection.magnitude < config.DeadZone)
			{
				rb.velocity = Vector2.zero;
				return;
			}

			rb.velocity = moveDirection.normalized * config.Speed;
		}

		public void ApplyDrag()
		{
			ApplyDrag(config.Drag);
		}

		private void ApplyDrag(float drag)
		{
			var velocity = rb.velocity;
			velocity -= velocity * timeProvider.DeltaTime * drag;
			rb.velocity = velocity;
		}
	}
}

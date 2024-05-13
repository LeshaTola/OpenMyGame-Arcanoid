using System;
using Features.StateMachine;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player.Configs;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Gameplay.Feature.Player
{
	public interface IMovement
	{
		public void Move(Vector2 moveDirection);
	}

	public class Movement : MonoBehaviour,  IMovement, IUpdatable
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Rigidbody2D rb;

		private ITimeProvider timeProvider;
		
		public void Init( ITimeProvider timeProvider)
		{
			this.timeProvider = timeProvider;
		}

		public void Move(Vector2 moveDirection)
		{
			rb.velocity = moveDirection.normalized * config.Speed;
			if (moveDirection.magnitude < config.DeadZone)
			{
				rb.velocity = Vector2.zero;
			}
		}
		
		private void ApplyDrag()
		{
			var velocity = rb.velocity;
			velocity -= velocity * timeProvider.DeltaTime * config.Drag;
			rb.velocity = velocity;
		}

		void IUpdatable.Update()
		{
			ApplyDrag();
		}
	}
}

using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.Configs;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player
{
	public class Movement : MonoBehaviour, IMovement
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Rigidbody2D rb;

		private ITimeProvider timeProvider;

		[Inject]
		public void Construct(ITimeProvider timeProvider)
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

		public void ApplyDrag()
		{
			var velocity = rb.velocity;
			velocity -= velocity * timeProvider.DeltaTime * config.Drag;
			rb.velocity = velocity;
		}
	}
}

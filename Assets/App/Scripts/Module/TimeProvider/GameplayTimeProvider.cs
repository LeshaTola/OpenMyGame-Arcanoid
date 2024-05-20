using UnityEngine;

namespace Module.TimeProvider
{
	public class GameplayTimeProvider : ITimeProvider
	{
		public float TimeMultiplier { get; set; } = 1f;

		public float DeltaTime => Time.deltaTime * TimeMultiplier;

		public float FixedDeltaTime => Time.fixedDeltaTime * TimeMultiplier;
	}
}

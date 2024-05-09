using UnityEngine;

namespace App.Scripts.Module.TimeProvider
{
	public class GameplayTimeProvider : ITimeProvider
	{
		public float TimeMultiplier { get; set; } = 1f;

		public float DeltaTime => Time.deltaTime * TimeMultiplier;
	}
}

using Features.Bootstrap;
using Module.ObjectPool;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class BallsController : MonoBehaviour, IInitializable
	{
		[SerializeField] private int ballCount;
		[SerializeField] private Ball ballTemplate;
		private IPool<Ball> ballPool;

		public IPool<Ball> BallPool { get => ballPool; }

		public void Init()
		{
			ballPool = new MonoBehObjectPool<Ball>(ballTemplate, ballCount, transform);
		}
	}
}

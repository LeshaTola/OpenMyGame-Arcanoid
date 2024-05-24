using Cysharp.Threading.Tasks;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public interface IBallService
	{
		Ball GetBall();
		void PauseBalls();
		void ReleaseBall(Ball ball);
		void Reset();
		void ResumeBalls();
		UniTask StopAllBallsAsync(float duration);
	}
}
using Cysharp.Threading.Tasks;

namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public interface IBallService
	{
		Ball GetBall();
		void ReleaseBall(Ball ball);
		UniTask StopAllBallsAsync(float duration);
	}
}
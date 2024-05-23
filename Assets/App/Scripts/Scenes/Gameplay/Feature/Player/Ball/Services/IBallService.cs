namespace Scenes.Gameplay.Feature.Player.Ball.Services
{
	public interface IBallService
	{
		Ball GetBall();
		void ReleaseBall(Ball ball);
	}
}
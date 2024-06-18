namespace Scenes.Gameplay.Feature.Autopilot.Services
{
	public interface IAutopilotService
	{
		bool IsActive { get; }

		void ActivateAutopilot();
		void DeactivateAutopilot();
	}


}
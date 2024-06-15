using Scenes.Gameplay.Feature.Autopilot.Services.Entities;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Autopilot.Services
{
	public interface IAutopilotService
	{
		List<AutopilotTarget> Targets { get; }
		bool IsActive { get; }

		void ActivateAutopilot();
		void DeactivateAutopilot();
	}


}
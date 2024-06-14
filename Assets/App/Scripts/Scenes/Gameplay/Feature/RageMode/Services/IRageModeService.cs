using Scenes.Gameplay.Feature.RageMode.Entities;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.RageMode.Services
{
	public interface IRageModeService
	{
		public bool IsActive { get; }
		IReadOnlyCollection<IEnraged> EnragedList { get; }

		public void AddEnraged(IEnraged enraged);
		public void RemoveEnraged(IEnraged enraged);

		public void ActivateRageMode();
		public void DeactivateRageMode();
	}
}

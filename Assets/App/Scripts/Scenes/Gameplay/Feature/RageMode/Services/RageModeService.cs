using Scenes.Gameplay.Feature.RageMode.Entities;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.RageMode.Services
{
	public class RageModeService : IRageModeService
	{
		private List<IEnraged> enragedList = new();

		public bool IsActive { get; private set; } = false;
		public IReadOnlyCollection<IEnraged> EnragedList => enragedList;

		public void AddEnraged(IEnraged enraged)
		{
			if (IsActive)
			{
				enraged.ActivateRageMode();
			}
			enragedList.Add(enraged);
		}

		public void RemoveEnraged(IEnraged enraged)
		{
			enraged.DeactivateRageMode();
			enragedList.Remove(enraged);
		}

		public void ActivateRageMode()
		{
			IsActive = true;
			foreach (var enraged in enragedList)
			{
				enraged.ActivateRageMode();
			}
		}

		public void DeactivateRageMode()
		{
			IsActive = false;
			foreach (var enraged in enragedList)
			{
				enraged.DeactivateRageMode();
			}
		}
	}
}

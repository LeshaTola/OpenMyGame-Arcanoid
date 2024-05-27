using Module.Localization;
using Module.Localization.Localizers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Bootstrap
{
	public class SceneLocalizationService : SerializedMonoBehaviour
	{
		[SerializeField] List<TMProLocalizer> TMProLocalizers;

		private ILocalizationSystem localizationSystem;

		[Inject]
		public void Construct(ILocalizationSystem localizationSystem)
		{
			this.localizationSystem = localizationSystem;
			foreach (var localizer in TMProLocalizers)
			{
				localizer.Init(localizationSystem);
				localizer.Translate();
			}
		}

		[Button, PropertyOrder(-1)]
		private void FindAllLocalizers()
		{
			TMProLocalizer[] sceneLocalizers = FindObjectsOfType<TMProLocalizer>();
			TMProLocalizers.Clear();
			TMProLocalizers.AddRange(sceneLocalizers);
		}

	}
}

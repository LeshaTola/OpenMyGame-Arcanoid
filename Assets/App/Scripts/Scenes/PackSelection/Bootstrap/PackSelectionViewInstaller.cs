using Features.Energy.UI;
using Features.UI.SceneTransitions;
using Scenes.PackSelection.Feature.UI;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PackSelectionViewInstaller : MonoInstaller
	{
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
		[SerializeField] private SerializableInterface<IEnergySliderUI> energySliderUI;
		[SerializeField] private HeaderUI headerUI;

		public override void InstallBindings()
		{
			Container.BindInstance(sceneTransition.Value);
			Container.BindInstance(energySliderUI.Value);
			Container.BindInstance(headerUI);
		}
	}
}

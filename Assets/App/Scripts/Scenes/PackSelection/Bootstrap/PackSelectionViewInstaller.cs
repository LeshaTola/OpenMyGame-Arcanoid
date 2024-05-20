using Features.UI.SceneTransitions;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PackSelectionViewInstaller : MonoInstaller
	{
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;

		public override void InstallBindings()
		{
			Container.BindInstance(sceneTransition.Value);
		}
	}
}

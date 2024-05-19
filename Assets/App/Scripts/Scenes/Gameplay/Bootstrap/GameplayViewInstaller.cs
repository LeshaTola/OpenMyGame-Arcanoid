using Features.UI.SceneTransitions;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayViewInstaller : MonoInstaller
	{
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;

		public override void InstallBindings()
		{
			Container.BindInstance(sceneTransition);
		}
	}
}

using Features.UI.SceneTransitions;
using Scenes.Main.Feature.UI;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuViewInstaller : MonoInstaller
	{
		[SerializeField] private MainMenuUI mainMenuUI;
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;

		public override void InstallBindings()
		{
			Container.BindInstance(mainMenuUI);
			Container.BindInstance(sceneTransition.Value);
		}
	}
}
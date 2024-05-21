using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.UI;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayViewInstaller : MonoInstaller
	{
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
		[SerializeField] private SerializableInterface<IProgressUI> progressUI;
		[SerializeField] private SerializableInterface<IHealthBarUI> healthBarUI;
		[SerializeField] private SerializableInterface<IPackInfoUI> packInfoUI;
		[SerializeField] private GameplayHeaderUI headerUI;

		public override void InstallBindings()
		{
			Container.BindInstance(sceneTransition.Value);
			Container.BindInstance(progressUI.Value);
			Container.BindInstance(healthBarUI.Value);
			Container.BindInstance(packInfoUI.Value);
			Container.BindInstance(headerUI);
		}
	}
}

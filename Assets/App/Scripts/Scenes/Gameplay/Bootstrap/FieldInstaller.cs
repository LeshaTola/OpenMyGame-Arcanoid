
using Scenes.Gameplay.Feature.Field;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class FieldInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Paddings paddings;
		[SerializeField] private FieldDrawer fieldDrawer;


		public override void InstallBindings()
		{
			Container
				.Bind<IFieldSizeProvider>()
				.To<FieldSizeProvider>().AsSingle()
				.WithArguments(mainCamera, paddings);
		}

	}
}
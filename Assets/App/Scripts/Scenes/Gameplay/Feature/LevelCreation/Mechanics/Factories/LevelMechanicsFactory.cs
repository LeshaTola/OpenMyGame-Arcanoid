using System;
using Zenject;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Factories
{
	public class LevelMechanicsFactory : ILevelMechanicsFactory
	{
		private DiContainer diContainer;

		public LevelMechanicsFactory(DiContainer diContainer)
		{
			this.diContainer = diContainer;
		}

		public ILevelMechanics GetLevelMechanics(ILevelMechanics originalMechanics)
		{
			Type type = originalMechanics.GetType();
			return (ILevelMechanics)diContainer.Resolve(type);
		}
	}
}

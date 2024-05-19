using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.PackSelection.Feature.Packs.Configs;
using Scenes.PackSelection.Feature.Packs.UI;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Feature.Packs
{
	public class PackFactory : IPackFactory
	{
		private PackUI completePackTemplate;
		private PackUI inProgressPackTemplate;
		private PackUI closedPack;
		private Transform container;

		private DiContainer diContainer;
		private IPackProvider packProvider;
		private StateMachineHandler stateMachineHandler;

		public PackFactory(PackUI completePackTemplate,
					 PackUI inProgressPackTemplate,
					 PackUI closedPack,
					 Transform container,
					 DiContainer diContainer,
					 StateMachineHandler stateMachineHandler,
					 IPackProvider packProvider)
		{
			this.completePackTemplate = completePackTemplate;
			this.inProgressPackTemplate = inProgressPackTemplate;
			this.closedPack = closedPack;
			this.container = container;
			this.diContainer = diContainer;
			this.stateMachineHandler = stateMachineHandler;
			this.packProvider = packProvider;
		}

		public PackUI GetPackUI(Pack pack)
		{
			if (!pack.IsOpened)
			{
				return InstantiatePrefab(closedPack, pack);
			}

			PackUI newPackUI;
			if (pack.CurrentLevel == pack.MaxLevel)
			{
				newPackUI = InstantiatePrefab(completePackTemplate, pack);
				newPackUI.Init(pack.Sprite, pack.Name, pack.CurrentLevel, pack.MaxLevel, () => OnPackUIClicked(pack));
			}
			else
			{
				newPackUI = InstantiatePrefab(inProgressPackTemplate, pack);
				newPackUI.Init(pack.Sprite, pack.Name, pack.CurrentLevel, pack.MaxLevel, () => OnPackUIClicked(pack));
			}

			return newPackUI;
		}

		private void OnPackUIClicked(Pack pack)
		{
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
			packProvider.CurrentPack = pack;
		}

		private PackUI InstantiatePrefab(PackUI template, Pack pack)
		{
			return diContainer.InstantiatePrefabForComponent<PackUI>(template, container);
		}
	}
}

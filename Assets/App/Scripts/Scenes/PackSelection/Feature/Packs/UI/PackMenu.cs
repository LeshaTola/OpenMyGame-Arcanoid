using Features.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Feature.Packs.UI
{
	public class PackMenu : MonoBehaviour
	{
		public event Action<Pack> OnPackSelected;

		[SerializeField] private RectTransform container;

		private IPackFactory packFactory;

		[Inject]
		public void Construct(IPackFactory packFactory)
		{
			this.packFactory = packFactory;
		}

		public void GeneratePackList(IEnumerable packs, PlayerProgressData playerProgressData)
		{
			foreach (Pack pack in packs)
			{
				PackUI packUI = packFactory.GetPackUI();
				packUI.UpdateUI(pack, playerProgressData.Packs[pack.Id]);
				packUI.transform.SetParent(container);
				packUI.transform.localScale = Vector3.one;
				packUI.onPackClicked += () => OnPackSelected?.Invoke(pack);
			}
		}
	}
}

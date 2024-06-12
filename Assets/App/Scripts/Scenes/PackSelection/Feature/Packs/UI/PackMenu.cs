using Features.Saves;
using Module.Localization;
using Scenes.PackSelection.Feature.Packs.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Feature.Packs.UI
{
	public class PackMenu : MonoBehaviour
	{
		public event Action<Pack> OnPackSelected;

		[SerializeField] private RectTransform container;

		private IPackFactory packFactory;
		private ILocalizationSystem localizationSystem;

		[Inject]
		public void Construct(IPackFactory packFactory, ILocalizationSystem localizationSystem)
		{
			this.packFactory = packFactory;
			this.localizationSystem = localizationSystem;
		}

		public void GeneratePackList(List<Pack> packs, Dictionary<string, SavedPackData> packsData)
		{
			foreach (Pack pack in packs)
			{

				PackUI packUI = packFactory.GetPackUI();
				SavedPackData packData = GetPackData(packsData, packs, pack);
				packUI.UpdateUI(pack, packData, localizationSystem);
				packUI.transform.SetParent(container);
				packUI.transform.localScale = Vector3.one;
				packUI.onPackClicked += () => OnPackSelected?.Invoke(pack);
			}
		}

		private static SavedPackData GetPackData(Dictionary<string, SavedPackData> packsData, List<Pack> packs, Pack pack)
		{
			SavedPackData packData;
			if (!packsData.ContainsKey(pack.Id))
			{
				packData = new SavedPackData()
				{
					Id = pack.Id,
					CurrentLevel = 0,
					IsCompeted = false,
					IsOpened = packs.Last() == pack
				};
				packsData.Add(pack.Id, packData);
			}
			else
			{
				packData = packsData[pack.Id];
			}

			return packData;
		}
	}
}

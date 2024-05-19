using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Feature.Packs.UI
{
	public class PackMenu : MonoBehaviour
	{
		[SerializeField] private List<Pack> packs;

		private IPackFactory packFactory;

		[Inject]
		public void Construct(IPackFactory packFactory)
		{
			this.packFactory = packFactory;
		}

		public void GeneratePackList()
		{
			foreach (Pack pack in packs)
			{
				packFactory.GetPackUI(pack);
			}
		}
	}
}

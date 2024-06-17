using Features.Saves;
using Module.Saves;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.PackSelection.Feature.Packs
{
	public class PackProvider : IPackProvider
	{
		private IDataProvider<PlayerProgressData> dataProvider;

		public bool IsFirstSession { get; private set; }
		public Dictionary<string, SavedPackData> PacksData { get; private set; }
		public List<Pack> Packs { get; }
		public int PackIndex { get; set; }
		public SavedPackData SavedPackData { get; set; }

		public Pack CurrentPack => Packs[PackIndex];

		public PackProvider(List<Pack> packs,
			IDataProvider<PlayerProgressData> dataProvider)
		{
			Packs = packs;
			this.dataProvider = dataProvider;
		}

		public void SaveData()
		{
			if (PacksData == null || PacksData.Count <= 0)
			{
				Debug.LogError("Attempt to load empty PacksData");
				return;
			}

			dataProvider.SaveData(new PlayerProgressData
			{
				IsFirstSession = this.IsFirstSession,
				Packs = PacksData
			});
		}

		public void LoadData()
		{
			var playerProgressData = dataProvider.GetData();
			if (playerProgressData == null)
			{
				playerProgressData = FormFirstPlayerProgressData();
				dataProvider.SaveData(playerProgressData);
			}
			IsFirstSession = playerProgressData.IsFirstSession;
			PacksData = playerProgressData.Packs;
		}

		private PlayerProgressData FormFirstPlayerProgressData()
		{
			PlayerProgressData playerProgress = new();

			foreach (var pack in Packs)
			{

				SavedPackData savedPackData = new()
				{
					Id = pack.Id,
					CurrentLevel = 0,
					IsOpened = false,
					IsCompeted = false,
				};

				playerProgress.Packs.Add(pack.Id, savedPackData);
			}
			playerProgress.Packs[Packs.Last().Id].IsOpened = true;
			playerProgress.IsFirstSession = true;
			return playerProgress;
		}
	}
}

using Module.Saves;
using System;

namespace Features.Saves.Gameplay.Providers
{
	public class GameplaySavesProvider : IGameplaySavesProvider
	{
		public event Action OnSave;
		public event Action OnLoad;

		private IDataProvider<GameplayData> dataProvider;

		public GameplaySavesProvider(IDataProvider<GameplayData> dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		public bool IsContinue { get; set; }

		public void SaveData()
		{
			OnSave?.Invoke();
		}

		public void LoadData()
		{
			OnLoad?.Invoke();
		}

		public bool CanContinue()
		{
			return dataProvider.HasData();
		}
	}
}

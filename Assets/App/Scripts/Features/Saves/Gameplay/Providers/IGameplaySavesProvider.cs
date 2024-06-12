using System;

namespace Features.Saves.Gameplay.Providers
{
	public interface IGameplaySavesProvider
	{
		public event Action OnSave;
		public event Action OnLoad;

		bool IsContinue { get; set; }

		bool CanContinue();
		void SaveData();
		void LoadData();
	}
}

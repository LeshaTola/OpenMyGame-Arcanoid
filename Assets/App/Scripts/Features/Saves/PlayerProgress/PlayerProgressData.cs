using System.Collections.Generic;

namespace Features.Saves
{
	public class PlayerProgressData
	{
		public Dictionary<string, SavedPackData> Packs = new();
	}

	public class SavedPackData
	{
		public string Id;
		public int CurrentLevel;
		public bool IsOpened;
		public bool IsCompeted;
	}
}

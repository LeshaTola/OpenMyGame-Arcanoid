using System;
using UnityEngine;

namespace Module.Localization.Configs
{
	[Serializable]
	public struct Language
	{
		public string LanguageName;
		public TextAsset SCVFile;
	}
}

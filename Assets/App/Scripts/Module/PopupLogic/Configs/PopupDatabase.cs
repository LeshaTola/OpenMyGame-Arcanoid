using Module.PopupLogic.General.Popups;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Module.PopupLogic.Configs
{
	[CreateAssetMenu(fileName = "PopupDatabase", menuName = "Dictionaries/Popup")]
	public class PopupDatabase : SerializedScriptableObject
	{
		[SerializeField] private List<Popup> popups;

		public List<Popup> Popups { get => popups; }
	}
}
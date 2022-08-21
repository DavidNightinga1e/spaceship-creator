using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly.Inspector
{
	public class AddResponseDropdownElement : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI label;
		[SerializeField] private Button button;

		public event Action<ShipInputAction> Clicked;

		private ShipInputAction _action;

		private void Awake()
		{
			button.onClick.AddListener(() => Clicked?.Invoke(_action));
		}

		public void SetAction(ShipInputAction action)
		{
			label.text = action.ToString();
			_action = action;
		}
	}
}
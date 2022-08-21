using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly.Inspector
{
	public class AddResponseDropdown : MonoBehaviour
	{
		[SerializeField] private RectTransform elementTemplate;
		[SerializeField] private RectTransform elementContainer;
		[SerializeField] private Button blockerButton;

		public event Action<ShipInputAction> ActionSelected;

		private void Awake()
		{
			for (int i = 0; i < (int)ShipInputAction.MaxValue; i++)
			{
				RectTransform instance = Instantiate(elementTemplate, elementContainer);
				var element = instance.GetComponent<AddResponseDropdownElement>();
				element.SetAction((ShipInputAction)i);
				element.Clicked += OnElementClicked;
			}

			elementTemplate.gameObject.SetActive(false);

			blockerButton.onClick.AddListener(() => SetVisible(false));
			
			gameObject.SetActive(false);
		}

		private void OnElementClicked(ShipInputAction action)
		{
			ActionSelected?.Invoke(action);
			SetVisible(false);
		}

		private void SetVisible(bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}

		public void Show()
		{
			SetVisible(true);
		}
	}
}
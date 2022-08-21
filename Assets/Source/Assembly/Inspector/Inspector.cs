using System;
using System.Linq;
using Assembly.ShipParts;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly.Inspector
{
	public class Inspector : MonoBehaviour
	{
		[SerializeField] private RectTransform responsiveContainer;
		[SerializeField] private RectTransform responsiveElementTemplate;
		[SerializeField] private RectTransform responsiveElementsContainer;
		[SerializeField] private Button addResponseButton;
		[SerializeField] private AddResponseDropdown addResponseDropdown;
		[SerializeField] private InspectorDirectionModule inspectorDirectionModule;

		private ShipPart _currentPart;

		private ShipPartResponsive CurrentPartResponsive =>
			_currentPart as ShipPartResponsive
			?? throw new Exception("Current part is not responsive");


		private void Awake()
		{
			addResponseDropdown.ActionSelected += AddResponseDropdownActionSelectedHandler;
			addResponseButton.onClick.AddListener(AddResponseButtonHandler);

			inspectorDirectionModule.DirectionChanged += InspectorDirectionModuleDirectionChangedHandler;
		}

		private void InspectorDirectionModuleDirectionChangedHandler(ShipPartDirection direction)
		{
			_currentPart.Direction = direction;
		}

		private void AddResponseButtonHandler()
		{
			addResponseDropdown.Show();
		}

		public void SetVisible(bool visible)
		{
			gameObject.SetActive(visible);
		}

		public void Inspect(ShipPart shipPart)
		{
			SetVisible(true);
			_currentPart = shipPart;

			if (shipPart is ShipPartResponsive responsive)
			{
				responsiveContainer.gameObject.SetActive(true);
				InspectResponsive(responsive);
			}
			else
			{
				responsiveContainer.gameObject.SetActive(false);
			}

			InspectDefault(shipPart);
		}

		private void InspectDefault(ShipPart shipPart)
		{
			inspectorDirectionModule.InspectPart(shipPart);
		}

		private void InspectResponsive(ShipPartResponsive shipPartResponsive)
		{
			ClearResponsiveElements();

			foreach (var response in shipPartResponsive.responses)
				AddElement(response.Key, response.Value);

			responsiveElementTemplate.gameObject.SetActive(false);
		}

		private void ClearResponsiveElements()
		{
			var elements = responsiveElementsContainer.GetComponentsInChildren<InspectorResponsesListElement>();
			foreach (InspectorResponsesListElement element in elements.Skip(1)) Destroy(element.gameObject);
		}

		private void AddElement(ShipInputAction action, float force)
		{
			GameObject instance = Instantiate(responsiveElementTemplate.gameObject, responsiveElementsContainer);
			var element = instance.GetComponent<InspectorResponsesListElement>();
			element.gameObject.SetActive(true);
			element.Setup(action, force);

			element.ResponseForceChanged += ElementResponseForceChangedHandler;
			element.ResponseDeleted += ElementResponseDeletedHandler;
		}

		private void AddResponseDropdownActionSelectedHandler(ShipInputAction action)
		{
			CurrentPartResponsive.responses.TryAdd(action, 1f);
			AddElement(action, 1f);
		}

		private void ElementResponseDeletedHandler(ShipInputAction action)
		{
			CurrentPartResponsive.responses.Remove(action);
		}

		private void ElementResponseForceChangedHandler(ShipInputAction action, float force)
		{
			CurrentPartResponsive.responses[action] = force;
		}
	}
}
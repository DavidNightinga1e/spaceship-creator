using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly.Inspector
{
	public class InspectorResponsesListElement : MonoBehaviour
	{
		[SerializeField] private Button deleteButton;
		[SerializeField] private TextMeshProUGUI actionLabel;
		[SerializeField] private Slider forceSlider;
		[SerializeField] private TextMeshProUGUI forceValueLabel;

		public ShipInputAction Action { get; private set; }
		public float Force { get; private set; }

		public event Action<ShipInputAction, float> ResponseForceChanged;
		public event Action<ShipInputAction> ResponseDeleted;

		private void Awake()
		{
			deleteButton.onClick.AddListener(OnDeleteButtonClicked);
			forceSlider.onValueChanged.AddListener(OnForceSliderValueChanged);
		}

		private void OnForceSliderValueChanged(float value)
		{
			ResponseForceChanged?.Invoke(Action, Force);
			forceValueLabel.text = $"{value:P0}";
		}

		private void OnDeleteButtonClicked()
		{
			ResponseDeleted?.Invoke(Action);
			Destroy(gameObject);
		}

		public void Setup(ShipInputAction action, float force)
		{
			Action = action;
			Force = force;
			
			actionLabel.text = action.ToString();
			forceSlider.SetValueWithoutNotify(force);
			forceValueLabel.text = $"{force:P0}";
		}
	}
}
using System;
using Assembly.ShipParts;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly.Inspector
{
	public class InspectorDirectionModule : MonoBehaviour
	{
		[SerializeField] private Toggle upToggle;
		[SerializeField] private Toggle downToggle;
		[SerializeField] private Toggle leftToggle;
		[SerializeField] private Toggle rightToggle;
		[SerializeField] private Toggle frontToggle;
		[SerializeField] private Toggle backToggle;

		public event Action<ShipPartDirection> DirectionChanged;

		private void Awake()
		{
			void TryInvokeDirectionChanged(bool isToggleOn, ShipPartDirection direction)
			{
				if (isToggleOn)
					DirectionChanged?.Invoke(direction);
			}

			upToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Up));
			downToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Down));
			leftToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Left));
			rightToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Right));
			frontToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Front));
			backToggle.onValueChanged.AddListener(b => TryInvokeDirectionChanged(b, ShipPartDirection.Back));
		}

		public void InspectPart(ShipPart shipPart)
		{
			Toggle targetToggle = shipPart.Direction switch
			{
				ShipPartDirection.Up => upToggle,
				ShipPartDirection.Down => downToggle,
				ShipPartDirection.Left => leftToggle,
				ShipPartDirection.Right => rightToggle,
				ShipPartDirection.Front => frontToggle,
				ShipPartDirection.Back => backToggle,
				_ => throw new ArgumentOutOfRangeException()
			};

			targetToggle.SetIsOnWithoutNotify(true);
		}
	}
}
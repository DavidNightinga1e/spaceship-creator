using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ShipInputSetup : MonoBehaviour
	{
		private readonly List<ShipInputMapping> _mappings = new()
		{
			new ShipInputMapping { action = ShipInputAction.Forward, key = KeyCode.W },
			new ShipInputMapping { action = ShipInputAction.Backward, key = KeyCode.S },
			new ShipInputMapping { action = ShipInputAction.Left, key = KeyCode.A },
			new ShipInputMapping { action = ShipInputAction.Right, key = KeyCode.D },
			new ShipInputMapping { action = ShipInputAction.YawLeft, key = KeyCode.Q },
			new ShipInputMapping { action = ShipInputAction.YawRight, key = KeyCode.E },
			
			new ShipInputMapping { action = ShipInputAction.PitchUp, key = KeyCode.K },
			new ShipInputMapping { action = ShipInputAction.PitchDown, key = KeyCode.I },
			new ShipInputMapping { action = ShipInputAction.RollLeft, key = KeyCode.J },
			new ShipInputMapping { action = ShipInputAction.RollRight, key = KeyCode.L },
			
			new ShipInputMapping { action = ShipInputAction.Up, key = KeyCode.Space },
			new ShipInputMapping { action = ShipInputAction.Down, key = KeyCode.LeftShift },
		};

		private void Awake()
		{
			ShipInput.Initialize(_mappings);
		}
	}
}
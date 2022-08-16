using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
	public static class ShipInput
	{
		private static Dictionary<ShipInputAction, KeyCode> _actionToKey;

		public static void Initialize(IEnumerable<ShipInputMapping> mappings)
		{
			if (_actionToKey is null)
				_actionToKey = mappings.ToDictionary(
					k => k.action,
					v => v.key);
			else
				throw new Exception("ShipInput was already initialized");
		}

		public static bool GetAction(ShipInputAction action)
		{
			KeyCode keyCode = _actionToKey[action];
			return Input.GetKey(keyCode);
		}
	}
}
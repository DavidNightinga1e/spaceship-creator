using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public struct ShipInputMapping
	{
		public KeyCode key;
		public ShipInputAction action;
	}
}
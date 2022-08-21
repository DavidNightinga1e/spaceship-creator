using System.Collections.Generic;
using Game;

namespace Assembly.ShipParts
{
	public class ShipPartResponsive : ShipPart
	{
		public Dictionary<ShipInputAction, float> responses = new();
	}
}
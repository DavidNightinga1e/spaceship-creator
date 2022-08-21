using Assembly.ShipParts;
using UnityEngine;

namespace Assembly.Pointer
{
	public class ToolPointerState
	{
		public readonly bool IsValid;
		public readonly Vector3 Position;
		public readonly ShipPart Part;

		public ToolPointerState(bool isValid, Vector3 position, ShipPart part)
		{
			IsValid = isValid;
			Position = position;
			Part = part;
		}
	}
}
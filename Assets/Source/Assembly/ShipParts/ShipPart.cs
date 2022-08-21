using UnityEngine;

namespace Assembly.ShipParts
{
	public abstract class ShipPart : MonoBehaviour
	{
		private ShipPartDirection _direction = ShipPartDirection.Front;

		public ShipPartDirection Direction
		{
			get => _direction;
			set
			{
				_direction = value;
				transform.rotation = ShipPartDirectionUtility.DirectionToQuaternion(value);
			}
		}
	}
}
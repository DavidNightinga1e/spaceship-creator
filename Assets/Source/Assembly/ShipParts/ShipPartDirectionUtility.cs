using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assembly.ShipParts
{
	public static class ShipPartDirectionUtility
	{
		private static Dictionary<ShipPartDirection, Vector3> _directionToEuler = new()
		{
			{ ShipPartDirection.Up, Vector3.right * 90f },
			{ ShipPartDirection.Down, Vector3.right * -90f },
			{ ShipPartDirection.Left, Vector3.up * -90f },
			{ ShipPartDirection.Right, Vector3.up * 90f },
			{ ShipPartDirection.Front, Vector3.zero },
			{ ShipPartDirection.Back, Vector3.up * 180f },
		};

		public static Quaternion DirectionToQuaternion(ShipPartDirection direction) =>
			Quaternion.Euler(_directionToEuler[direction]);

		public static Vector3 DirectionToEuler(ShipPartDirection direction) =>
			_directionToEuler[direction];

		// todo: slow function
		public static ShipPartDirection EulerToDirection(Vector3 euler)
		{
			bool VectorEqualityFunc(KeyValuePair<ShipPartDirection, Vector3> t) => euler.Equals(t.Value);
			return _directionToEuler.First(VectorEqualityFunc).Key;
		}

		public static ShipPartDirection QuaternionToDirection(Quaternion q) => EulerToDirection(q.eulerAngles);
	}
}
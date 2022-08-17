using UnityEngine;

namespace Game
{
	public class GyroStabilizer : MonoBehaviour, IShipInjectable
	{
		[SerializeField, Range(0, 1000f)] private float stability;
		
		private Ship _ship;
		
		public void InjectShip(Ship ship)
		{
			_ship = ship;
		}

		private void Update()
		{
			bool stabilityAction = Input.GetKey(KeyCode.R);
			_ship.Rigidbody.angularDrag = stabilityAction ? stability : 0f;
		}
	}
}
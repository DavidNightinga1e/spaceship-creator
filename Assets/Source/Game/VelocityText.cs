using System;
using TMPro;
using UnityEngine;

namespace Game
{
	public class VelocityText : MonoBehaviour, IShipInjectable
	{
		[SerializeField] private TextMeshProUGUI text;
		
		private Ship _ship;
		private string _format;

		private void Awake()
		{
			_format = text.text;
		}

		public void InjectShip(Ship ship)
		{
			_ship = ship;
		}

		private void Update()
		{
			Rigidbody shipRigidbody = _ship.Rigidbody;
			Vector3 angularVelocity = shipRigidbody.angularVelocity;
			float velocity = shipRigidbody.velocity.magnitude;
			text.text = string.Format(_format, velocity, angularVelocity.x * Mathf.Rad2Deg, angularVelocity.y * Mathf.Rad2Deg, angularVelocity.z * Mathf.Rad2Deg);
		}
	}
}
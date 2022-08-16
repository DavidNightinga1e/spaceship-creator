using System;
using UnityEngine;

namespace Game
{
	public class Ship : MonoBehaviour
	{
		[SerializeField] private Rigidbody rb;
		[SerializeField] private float power;

		public Rigidbody Rigidbody => rb;
		public float Power => power;

		private void Awake()
		{
			var shipInjectables = GetComponentsInChildren<IShipInjectable>();
			foreach (IShipInjectable shipInjectable in shipInjectables) 
				shipInjectable.InjectShip(this);
		}
	}
}
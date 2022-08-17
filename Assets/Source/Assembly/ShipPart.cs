using UnityEngine;

namespace Assembly
{
	public class ShipPart : MonoBehaviour
	{
		[SerializeField] private ShipPartType shipPartType;
		
		public ShipPartType Type => shipPartType;
	}
}
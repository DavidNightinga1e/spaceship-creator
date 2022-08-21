using Assembly.Pointer;
using Assembly.ShipParts;
using UnityEngine;

namespace Assembly.Tools
{
	public class ThrusterTool : Tool
	{
		public ThrusterTool(PointerMode pointerMode) : base(pointerMode)
		{
		}
		
		public override void Use(ToolPointerState pointerState)
		{
			GameObject prefab = ShipPartPrefabHandler.Instance.GetPrefab<Thruster>();
			Vector3 position = pointerState.Position + Vector3.one * .5f;
			Object.Instantiate(prefab, position, Quaternion.identity);
		}
	}
}
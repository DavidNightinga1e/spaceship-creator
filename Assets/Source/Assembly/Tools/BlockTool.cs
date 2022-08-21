using Assembly.Pointer;
using Assembly.ShipParts;
using UnityEngine;

namespace Assembly.Tools
{
	public class BlockTool : Tool
	{
		public override void Use(ToolPointerState pointerState)
		{
			GameObject prefab = ShipPartPrefabHandler.Instance.GetPrefab<Block>();
			Vector3 position = pointerState.Position + Vector3.one * .5f;
			Object.Instantiate(prefab, position, Quaternion.identity);
		}

		public BlockTool(PointerMode pointerMode) : base(pointerMode)
		{
		}
	}
}
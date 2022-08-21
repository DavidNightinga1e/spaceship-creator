using Assembly.Pointer;
using UnityEngine;

namespace Assembly.Tools
{
	public class DeleteTool : Tool
	{
		public override void Use(ToolPointerState pointerState)
		{
			Object.Destroy(pointerState.Part.gameObject);
		}

		public DeleteTool(PointerMode pointerMode) : base(pointerMode)
		{
		}
	}
}
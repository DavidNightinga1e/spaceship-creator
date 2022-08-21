using Assembly.Pointer;

namespace Assembly.Tools
{
	public class SelectionTool : Tool
	{
		public override void Use(ToolPointerState pointerState)
		{
			Domain.Inspector.Inspect(pointerState.Part);
		}

		public SelectionTool(PointerMode pointerMode) : base(pointerMode)
		{
		}
	}
}
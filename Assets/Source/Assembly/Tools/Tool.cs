using Assembly.Pointer;

namespace Assembly.Tools
{
	public abstract class Tool
	{
		public readonly PointerMode PointerMode;

		protected Tool(PointerMode pointerMode)
		{
			PointerMode = pointerMode;
		}

		public abstract void Use(ToolPointerState pointerState);
	}
}
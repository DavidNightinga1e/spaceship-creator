using UnityEngine;

namespace Application
{
	public static class Startup
	{
		[RuntimeInitializeOnLoadMethod]
		public static void Start()
		{
			UnityEngine.Application.targetFrameRate = 60;
		}
	}
}
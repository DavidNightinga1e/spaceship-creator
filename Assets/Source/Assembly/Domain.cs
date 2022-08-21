using System;
using Assembly.Pointer;
using UnityEngine;

namespace Assembly
{
	[DefaultExecutionOrder(-1)]
	public class Domain : MonoBehaviour
	{
		public static Inspector.Inspector Inspector;
		public static ToolPointer ToolPointer;
		
		private static T GetSingleInstance<T>() where T : MonoBehaviour
		{
			var instances = FindObjectsOfType<T>();
			if (instances.Length is 1)
				return instances[0];

			if (instances.Length is 0)
				throw new Exception($"Instance of {typeof(T)} not found");
			throw new Exception($"Multiple instances of {typeof(T)} found");
		}
		
		private void Awake()
		{
			Inspector = GetSingleInstance<Inspector.Inspector>();
			ToolPointer = GetSingleInstance<ToolPointer>();
		}

		private void OnDestroy()
		{
			Inspector = null;
			ToolPointer = null;
		}
	}
}
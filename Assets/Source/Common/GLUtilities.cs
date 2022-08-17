using UnityEngine;

namespace Common
{
	public static class GLUtilities
	{
		#region ShaderProperties

		private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
		private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
		private static readonly int Cull = Shader.PropertyToID("_Cull");
		private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

		#endregion

		public static Material CreateMaterial(Color color)
		{
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			var material = new Material(shader)
			{
				color = color,
				hideFlags = HideFlags.HideAndDontSave
			};
			material.SetInt(SrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			material.SetInt(DstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetInt(Cull, (int)UnityEngine.Rendering.CullMode.Off);
			material.SetInt(ZWrite, 0);
			return material;
		}

		public static void DrawCube(Vector3 lowestPoint, float delta)
		{
			float x = lowestPoint.x;
			float y = lowestPoint.y;
			float z = lowestPoint.z;

			float dx = x + delta;
			float dy = y + delta;

			// front
			GL.Vertex3(x, y, z);
			GL.Vertex3(dx, y, z);

			GL.Vertex3(dx, y, z);
			GL.Vertex3(dx, dy, z);

			GL.Vertex3(dx, dy, z);
			GL.Vertex3(x, dy, z);

			GL.Vertex3(x, dy, z);
			GL.Vertex3(x, y, z);

			float dz = z + delta;

			// back
			GL.Vertex3(x, y, dz);
			GL.Vertex3(dx, y, dz);

			GL.Vertex3(dx, y, dz);
			GL.Vertex3(dx, dy, dz);

			GL.Vertex3(dx, dy, dz);
			GL.Vertex3(x, dy, dz);

			GL.Vertex3(x, dy, dz);
			GL.Vertex3(x, y, dz);

			// linkages

			GL.Vertex3(x, y, z);
			GL.Vertex3(x, y, dz);

			GL.Vertex3(dx, y, z);
			GL.Vertex3(dx, y, dz);

			GL.Vertex3(x, dy, z);
			GL.Vertex3(x, dy, dz);

			GL.Vertex3(dx, dy, z);
			GL.Vertex3(dx, dy, dz);
		}
	}
}
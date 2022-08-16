using UnityEngine;

namespace RuntimeDebugging
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Range(1f, 20f)] private float gridSize;

        private Material _materialMain;
        private Material _materialOther;

        void Start()
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _materialMain = new Material(shader)
            {
                color = Color.magenta,
                hideFlags = HideFlags.HideAndDontSave
            };
            // Turn on alpha blending
            _materialMain.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _materialMain.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            _materialMain.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            _materialMain.SetInt("_ZWrite", 0);

            _materialOther = new Material(_materialMain)
            {
                color = Color.white
            };
        }

        private static void GlDrawCube(Vector3 lowestPoint, float delta)
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
        
        void OnRenderObject()
        {
            Vector3 position = target.position;

            float x = Mathf.Floor(position.x / gridSize) * gridSize;
            float y = Mathf.Floor(position.y / gridSize) * gridSize;
            float z = Mathf.Floor(position.z / gridSize) * gridSize;
            
            GL.Begin(GL.LINES);
            
            _materialOther.SetPass(0);
            GlDrawCube(new Vector3(x + gridSize, y, z), gridSize);
            GlDrawCube(new Vector3(x - gridSize, y, z), gridSize);
            GlDrawCube(new Vector3(x, y + gridSize, z), gridSize);
            GlDrawCube(new Vector3(x, y - gridSize, z), gridSize);
            GlDrawCube(new Vector3(x, y, z + gridSize), gridSize);
            GlDrawCube(new Vector3(x, y, z - gridSize), gridSize);
            
            GL.End();
            
            GL.Begin(GL.LINES);
            
            _materialMain.SetPass(0);
            GlDrawCube(new Vector3(x, y, z), gridSize);
            
            GL.End();
        }
    }
}

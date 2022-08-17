using System;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assembly
{
	public class Pointer : MonoBehaviour
	{
		private const float BlockSize = 1f;

		private Camera _camera;
		private Material _material;
		
		private PointerMode _mode;

		public PointerMode Mode
		{
			get => _mode;
			set
			{
				_mode = value;
				
				Color color = value switch
				{
					PointerMode.Add => Color.cyan,
					PointerMode.Edit => Color.yellow,
					PointerMode.Remove => Color.red,
					_ => throw new ArgumentOutOfRangeException()
				};
				_material.color = color;
			}
		}

		public bool IsPointerValid { get; private set; }
		public Vector3 PointerPosition { get; private set; }
		public ShipPart SelectedPart { get; private set; }

		private void Awake()
		{
			_camera = Camera.main;
			_material = GLUtilities.CreateMaterial(Color.cyan);
		}

		private void Update()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				IsPointerValid = false;
				return;
			}

			Vector3 mousePosition = Input.mousePosition;
			Ray ray = _camera.ScreenPointToRay(mousePosition);
			int addMask = LayerMask.GetMask("ShipPart", "Default");
			int editAndRemoveMask = LayerMask.GetMask("ShipPart");
			int targetMask = Mode is PointerMode.Add ? addMask : editAndRemoveMask;
			bool raycastResult = Physics.Raycast(ray, out RaycastHit hitInfo, 100f, targetMask);
			
			IsPointerValid = raycastResult;
			
			if (!raycastResult)
				return;
			
			Vector3 position = Mode switch
			{
				PointerMode.Add => hitInfo.point + hitInfo.normal * 0.1f,
				PointerMode.Edit => hitInfo.point - hitInfo.normal * 0.1f,
				PointerMode.Remove => hitInfo.point - hitInfo.normal * 0.1f,
				_ => throw new ArgumentOutOfRangeException()
			};

			SelectedPart = Mode switch
			{
				PointerMode.Add => null,
				PointerMode.Edit => hitInfo.transform.GetComponent<ShipPart>(),
				PointerMode.Remove => hitInfo.transform.GetComponent<ShipPart>(),
				_ => throw new ArgumentOutOfRangeException()
			};

			float x = Mathf.Floor(position.x / BlockSize) * BlockSize;
			float y = Mathf.Floor(position.y / BlockSize) * BlockSize;
			float z = Mathf.Floor(position.z / BlockSize) * BlockSize;
			
			var lowestPoint = new Vector3(x, y, z);
			PointerPosition = lowestPoint;
		}

		private void OnRenderObject()
		{
			if (!IsPointerValid)
				return; 
			
			GL.Begin(GL.LINES);
			_material.SetPass(0);
			GLUtilities.DrawCube(PointerPosition, BlockSize);
			GL.End();
		}
	}
}
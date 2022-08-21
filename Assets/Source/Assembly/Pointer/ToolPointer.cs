using System;
using Assembly.ShipParts;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assembly.Pointer
{
	public class ToolPointer : MonoBehaviour
	{
		private const float BlockSize = 1f;

		private Camera _camera;
		private Material _material;

		private PointerMode _mode;

		private bool _isPointerValid;
		private Vector3 _pointerPosition;
		private ShipPart _selectedPart;

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

		public ToolPointerState GetState() => new(_isPointerValid, _pointerPosition, _selectedPart);

		private void Awake()
		{
			_camera = Camera.main;
			_material = GLUtilities.CreateMaterial(Color.cyan);
		}

		private void Update()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				_isPointerValid = false;
				return;
			}

			Vector3 mousePosition = Input.mousePosition;
			Ray ray = _camera.ScreenPointToRay(mousePosition);
			int addMask = LayerMask.GetMask("ShipPart", "Default");
			int editAndRemoveMask = LayerMask.GetMask("ShipPart");
			int targetMask = Mode is PointerMode.Add ? addMask : editAndRemoveMask;
			bool raycastResult = Physics.Raycast(ray, out RaycastHit hitInfo, 100f, targetMask);

			_isPointerValid = raycastResult;

			if (!raycastResult)
				return;

			Vector3 position = Mode switch
			{
				PointerMode.Add => hitInfo.point + hitInfo.normal * 0.1f,
				PointerMode.Edit => hitInfo.point - hitInfo.normal * 0.1f,
				PointerMode.Remove => hitInfo.point - hitInfo.normal * 0.1f,
				_ => throw new ArgumentOutOfRangeException()
			};

			_selectedPart = Mode switch
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
			_pointerPosition = lowestPoint;
		}

		private void OnRenderObject()
		{
			if (!_isPointerValid)
				return;

			GL.Begin(GL.LINES);
			_material.SetPass(0);
			GLUtilities.DrawCube(_pointerPosition, BlockSize);
			GL.End();
		}
	}
}
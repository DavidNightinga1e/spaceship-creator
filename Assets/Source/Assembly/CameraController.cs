using UnityEngine;

namespace Assembly
{
	public class CameraController : MonoBehaviour
	{
		private const float MaxPitch = 85f;
		private const float MinPitch = -85f;
		private const float CameraDistanceFromCenter = 20f;
		private const float MaxCameraFov = 90f;
		private const float MinCameraFov = 20f;

		private Camera _camera;

		private float _currentZoom = .4f;
		private float _currentRotation = 45f;
		private float _currentPitch = 40f;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			Vector2 mouseScrollDelta = Input.mouseScrollDelta * (Time.deltaTime * 60f);
			if (Input.GetKey(KeyCode.LeftControl))
			{
				_currentZoom -= mouseScrollDelta.y * .1f;
			}
			else
			{
				_currentRotation -= mouseScrollDelta.x * 3f;
				_currentPitch -= mouseScrollDelta.y;
			}

			_currentZoom = Mathf.Clamp01(_currentZoom);
			_currentPitch = Mathf.Clamp(_currentPitch, MinPitch, MaxPitch);

			Transform cameraTransform = _camera.transform;
			cameraTransform.SetPositionAndRotation(Vector3.forward * CameraDistanceFromCenter, Quaternion.identity);
			cameraTransform.RotateAround(Vector3.zero, Vector3.left, _currentPitch);
			cameraTransform.RotateAround(Vector3.zero, Vector3.up, _currentRotation);
			cameraTransform.LookAt(Vector3.zero);
			float targetZoom = Mathf.Lerp(MinCameraFov, MaxCameraFov, _currentZoom);
			_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetZoom, 8f * Time.deltaTime);
		}
	}
}
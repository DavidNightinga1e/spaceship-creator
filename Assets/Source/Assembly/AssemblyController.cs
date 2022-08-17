using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assembly
{
	public class AssemblyController : MonoBehaviour
	{
		public enum Mode
		{
			Selection,
			Block,
			Thruster,
			Delete
		}

		[SerializeField] private GameObject blockPrefab;
		[SerializeField] private GameObject thrusterPrefab;

		[SerializeField] private Toggle selectionModeToggle;
		[SerializeField] private Toggle blockModeToggle;
		[SerializeField] private Toggle thrusterModeToggle;
		[SerializeField] private Toggle deleteModeToggle;

		private Mode _currentMode;
		private Pointer _pointer;

		public Mode CurrentMode
		{
			get => _currentMode;
			set
			{
				_pointer.Mode = value switch
				{
					Mode.Selection => PointerMode.Edit,
					Mode.Block => PointerMode.Add,
					Mode.Thruster => PointerMode.Add,
					Mode.Delete => PointerMode.Remove,
					_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
				};
				_currentMode = value;
			}
		}

		private void Awake()
		{
			_pointer = FindObjectOfType<Pointer>();

			void TrySetMode(bool isToggleActive, Mode targetMode)
			{
				if (isToggleActive)
					CurrentMode = targetMode;
			}

			selectionModeToggle.onValueChanged.AddListener(b => TrySetMode(b, Mode.Selection));
			blockModeToggle.onValueChanged.AddListener(b => TrySetMode(b, Mode.Block));
			thrusterModeToggle.onValueChanged.AddListener(b => TrySetMode(b, Mode.Thruster));
			deleteModeToggle.onValueChanged.AddListener(b => TrySetMode(b, Mode.Delete));
		}

		private void Update()
		{
			if (!(Input.GetMouseButtonDown(0) && _pointer.IsPointerValid))
				return;

			switch (CurrentMode)
			{
				case Mode.Selection:
					_pointer.SelectedPart.transform.Rotate(0f, 90f, 0f);
					break;
				case Mode.Block:
					Instantiate(blockPrefab, _pointer.PointerPosition + Vector3.one * .5f, Quaternion.identity);
					break;
				case Mode.Thruster:
					Instantiate(thrusterPrefab, _pointer.PointerPosition + Vector3.one * .5f, Quaternion.identity);
					break;
				case Mode.Delete:
					Destroy(_pointer.SelectedPart.gameObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
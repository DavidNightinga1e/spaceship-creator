using Assembly.Pointer;
using Assembly.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assembly
{
	public class Toolbox : MonoBehaviour
	{
		[SerializeField] private Toggle selectionToolToggle;
		[SerializeField] private Toggle blockToolToggle;
		[SerializeField] private Toggle thrusterToolToggle;
		[SerializeField] private Toggle deleteToolToggle;

		private Tool _currentTool;

		private void Awake()
		{
			var blockTool = new BlockTool(PointerMode.Add);
			var selectionTool = new SelectionTool(PointerMode.Edit);
			var thrusterTool = new ThrusterTool(PointerMode.Add);
			var deleteTool = new DeleteTool(PointerMode.Remove);

			void TrySetTool(bool isToggleActive, Tool targetTool)
			{
				if (!isToggleActive)
					return;

				Domain.Inspector.SetVisible(false);
				Domain.ToolPointer.Mode = targetTool.PointerMode;
				_currentTool = targetTool;
			}

			selectionToolToggle.onValueChanged.AddListener(b => TrySetTool(b, selectionTool));
			blockToolToggle.onValueChanged.AddListener(b => TrySetTool(b, blockTool));
			thrusterToolToggle.onValueChanged.AddListener(b => TrySetTool(b, thrusterTool));
			deleteToolToggle.onValueChanged.AddListener(b => TrySetTool(b, deleteTool));
		}

		private void Start()
		{
			selectionToolToggle.isOn = true;
		}

		private void Update()
		{
			bool isToolInput = Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
			ToolPointerState state = Domain.ToolPointer.GetState();
			if (isToolInput && state.IsValid)
				_currentTool.Use(state);
		}
	}
}
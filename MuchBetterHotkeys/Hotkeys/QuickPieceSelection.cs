namespace MuchBetterHotkeys {

	using UnityEngine;

	public partial class PlayerHotkeyPatch {

		private static bool QuickPieceSelection(Player player) {
			KeyCode[] keycodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
			for (int idx = 0; idx < keycodes.Length; idx++) {
				KeyCode curKeyCode = keycodes[idx];
				if (Input.GetKeyDown(curKeyCode)) {
					player.SetSelectedPiece(new Vector2Int(idx, 0));
					Hud.instance.TogglePieceSelection();
					return false;
				}
			}
			return true;
		}

	}
}

namespace MuchBetterHotkeys {

	using UnityEngine;

	public partial class PlayerHotkeyPatch {

		private static bool BuildRotationSnap(Player player) {
			if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
				player.m_placeRotation -= 4;
				return false;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
				player.m_placeRotation += 4;
				return false;
			}
			return true;
		}
	}
}

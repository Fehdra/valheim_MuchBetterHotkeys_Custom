namespace MuchBetterHotkeys {

	using System;
	using System.Collections.Generic;

	public partial class PlayerHotkeyPatch {
		private static void QuickEquipAxe(Player player) {
			if (Hud.IsPieceSelectionVisible()) {
				return;
			}
			Predicate<ItemDrop.ItemData> isTorch = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name == "$item_torch"; };
			List<ItemDrop.ItemData> torches = player.m_inventory.m_inventory.FindAll(isTorch);
			torches.Sort(new DurabilityComparer());
			if (torches.Count > 0) {
				player.QueueEquipItem(torches[0]);
			}
		}
	}
}

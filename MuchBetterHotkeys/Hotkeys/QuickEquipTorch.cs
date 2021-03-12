namespace MuchBetterHotkeys {

	using System;
	using System.Collections.Generic;

	public partial class PlayerHotkeyPatch {
		private static void QuickEquipTorch(Player player) {
			var rightItem = player.m_rightItem;
			if (rightItem != null && rightItem.m_shared.m_name == "$item_torch") {
				if (!player.InAttack()) {
					player.QueueUnequipItem(player.m_rightItem);
					return;
				}
			}

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

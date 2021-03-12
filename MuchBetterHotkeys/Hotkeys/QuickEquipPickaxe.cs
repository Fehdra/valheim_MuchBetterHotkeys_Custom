namespace MuchBetterHotkeys {
	using UnityEngine;
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public partial class PlayerHotkeyPatch {
		public static string[] pickaxeCompareTexts = new string[] { "Rock", "Tin deposit", "Muddy scraps", "Copper deposit", "Obsidian deposit", "Silver vein" };

		private static bool QuickEquipPickaxe(Player player) {
			GameObject hoverObject = player.GetHoverObject();
			Hoverable hoverable = ( hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null );
			if (hoverable == null) {
				return true;
			}
			string hoverText = hoverable.GetHoverText();
			if (Array.Exists(pickaxeCompareTexts, element => element == hoverText)) {
				Predicate<ItemDrop.ItemData> isPickaxe = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name.Contains("$item_pickaxe"); };
				List<ItemDrop.ItemData> pickaxes = player.m_inventory.m_inventory.FindAll(isPickaxe);
				List<ItemDrop.ItemData> durablePickaxes = pickaxes.Where(pick => pick.m_durability != 0).ToList();
				if (durablePickaxes.Count > 0) {
					int maxTier = durablePickaxes.Max(pick => pick.m_shared.m_toolTier);
					List<ItemDrop.ItemData> topTierPickaxes = durablePickaxes.Where(pick => pick.m_shared.m_toolTier == maxTier).ToList();
					topTierPickaxes.Sort(new DurabilityComparer());
					if (topTierPickaxes.Count > 0) {
						player.QueueUnequipItem(player.m_rightItem);
						player.QueueEquipItem(topTierPickaxes[0]);
						return false;
					}
				}
			}
			return true;
		}
	}
}

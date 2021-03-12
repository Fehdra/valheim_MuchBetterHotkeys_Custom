namespace MuchBetterHotkeys
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public partial class PlayerHotkeyPatch
    {
        public static string[] pickaxeCompareTexts = new string[] { "Rock", "Tin deposit", "Muddy scraps", "Copper deposit", "Obsidian", "Silver vein" };

        private static bool QuickEquipPickaxe(Player player) {
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            string hoverText = hoverable.GetHoverText();
            if (Array.Exists(pickaxeCompareTexts, element => element == hoverText)) {
                Predicate<ItemDrop.ItemData> isPickaxe = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name.Contains("$item_pickaxe"); };
                List<ItemDrop.ItemData> pickaxes = player.m_inventory.m_inventory.FindAll(isPickaxe);
                // TODO: Check for the best quality pickaxe
                pickaxes.Sort(new DurabilityComparer());
                if (pickaxes.Count > 0) {
                    player.EquipItem(pickaxes[0]);
                    return false;
                }
            }
            return true;
        }
    }
}

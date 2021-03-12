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
                Predicate<ItemDrop.ItemData> isAxe = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name.Contains("$item_axe"); };
                List<ItemDrop.ItemData> axes = player.m_inventory.m_inventory.FindAll(isAxe);
                // TODO: Check for the best quality axe
                axes.Sort(new DurabilityComparer());
                if (axes.Count > 0) {
                    player.EquipItem(axes[0]);
                    return false;
                }
            }
            return true;
        }
    }
}

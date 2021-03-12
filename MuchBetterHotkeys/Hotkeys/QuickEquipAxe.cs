namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {

        public static string[] axeCompareTexts = new string[] { "Log", "Beech", "Birch", "Oak", "Ancient tree" };
        private static bool QuickEquipAxe(Player player) {
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            string hoverText = hoverable.GetHoverText();
            if (Array.Exists(axeCompareTexts, element => element == hoverText)) {
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

namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {
        private static void QuickEquipHammer(Player player) {
            if (player.m_rightItem != null) {
                if (player.m_rightItem.m_shared.m_name == "$item_hammer") {
                    if (player.m_rightItem != null) {
                        if (!player.InAttack()) {
                            player.UnequipItem(player.m_rightItem);
                            return;
                        }
                    }
                }
            }
            if (Hud.IsPieceSelectionVisible()) {
                return;
            }
            Predicate<ItemDrop.ItemData> isHammer = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name == "$item_hammer"; };
            List<ItemDrop.ItemData> hammers = player.m_inventory.m_inventory.FindAll(isHammer);
            hammers.Sort(new DurabilityComparer());
            if (hammers.Count > 0) {
                player.EquipItem(hammers[0]);
            }
        }
    }
}

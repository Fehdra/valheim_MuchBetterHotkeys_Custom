namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;

    public partial class PlayerHotkeyPatch
    {
        private static void QuickEquipTorch(Player player) {
            if (player.m_rightItem != null) {
                if (player.m_rightItem.m_shared.m_name == "$item_torch") {
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

            Predicate<ItemDrop.ItemData> isTorch = delegate (ItemDrop.ItemData item) { return item.m_shared.m_name == "$item_torch"; };
            List<ItemDrop.ItemData> torches = player.m_inventory.m_inventory.FindAll(isTorch);
            torches.Sort(new DurabilityComparer());
            if (torches.Count > 0) {
                player.EquipItem(torches[0]);
            }
        }
    }
}

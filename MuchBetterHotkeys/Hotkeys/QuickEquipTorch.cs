namespace MuchBetterHotkeys
{

    using System.Collections.Generic;
    using System;

    public partial class PlayerHotkeyPatch
    {
        private static void QuickEquipTorch(Player player) {
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

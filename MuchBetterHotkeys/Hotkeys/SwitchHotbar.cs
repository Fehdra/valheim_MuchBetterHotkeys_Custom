namespace MuchBetterHotkeys
{

    public partial class PlayerHotkeyPatch
    {

        private static void SwitchHotbar(Player player) {
            ref Inventory inv = ref ((Humanoid)player).m_inventory;
            int width = inv.GetWidth();
            ItemDrop.ItemData currentLeftHidden = player.m_hiddenLeftItem;
            ItemDrop.ItemData currentRightHidden = player.m_hiddenRightItem;
            for (int x = 0; x < width; x++) {
                ItemDrop.ItemData row1Item = inv.GetItemAt(x, 0);
                ItemDrop.ItemData row2Item = inv.GetItemAt(x, 1);
                bool row1ItemIsEquipped = player.IsItemEquiped(row1Item);
                bool row2ItemIsEquipped = player.IsItemEquiped(row2Item);
                ((Humanoid)player).RemoveFromEquipQueue(row1Item);
                ((Humanoid)player).RemoveFromEquipQueue(row2Item);
                player.UnequipItem(row1Item);
                player.UnequipItem(row2Item);
                if (row2Item != null) {
                    // Row 2 -> Row 1
                    if (row1Item != null) {
                        // Removing item
                        inv.RemoveItem(row1Item);
                    }
                    inv.MoveItemToThis(inv, row2Item, row2Item.m_stack, x, 0);
                }
                if (row1Item != null) {
                    // Row 1 -> Row 2
                    // TODO: This generates a "Item is not in this container message" and AddItem does not work for some reason
                    inv.MoveItemToThis(inv, row1Item, row1Item.m_stack, x, 1);
                }

                if (row1ItemIsEquipped) {
                    player.EquipItem(inv.GetItemAt(x, 1), false);
                }
                if (row2ItemIsEquipped) {
                    player.EquipItem(inv.GetItemAt(x, 0), false);
                }
            }
            if (currentLeftHidden != null) {
                player.m_hiddenLeftItem = inv.GetItemAt(currentLeftHidden.m_gridPos.x, currentLeftHidden.m_gridPos.y == 0 ? 1 : 0);
                player.SetupVisEquipment(player.m_visEquipment, false);
            }
            if (currentRightHidden != null) {
                player.m_hiddenRightItem = inv.GetItemAt(currentRightHidden.m_gridPos.x, currentRightHidden.m_gridPos.y == 0 ? 1 : 0);
                player.SetupVisEquipment(player.m_visEquipment, false);
            }
            inv.Changed();
            return;
        }

    }
}

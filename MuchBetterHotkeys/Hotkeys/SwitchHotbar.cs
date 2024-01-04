namespace MuchBetterHotkeys
{

	public partial class PlayerHotkeyPatch
	{

		private static void SwitchHotbar(Player player)
		{
            // ref Inventory inv = ref ((Humanoid)player).m_inventory; -will not build-  so removed ref . no clue that that does Long term
            // im not a coder lol
            Inventory inv = ((Humanoid)player).m_inventory;
			Inventory temp = new Inventory(inv.m_name + "_temp", inv.GetBkg(), inv.GetWidth(), inv.GetHeight());
			int width = inv.GetWidth();
			ItemDrop.ItemData currentLeftHidden = player.m_hiddenLeftItem;
			ItemDrop.ItemData currentRightHidden = player.m_hiddenRightItem;
			for (int x = 0; x < width; x++)
			{
				ItemDrop.ItemData row1Item = inv.GetItemAt(x, 0);
				ItemDrop.ItemData row2Item = inv.GetItemAt(x, 1);
				bool row1ItemIsEquipped = player.IsItemEquiped(row1Item);
				bool row2ItemIsEquipped = player.IsItemEquiped(row2Item);
				((Humanoid)player).RemoveEquipAction(row1Item);
				((Humanoid)player).RemoveEquipAction(row2Item);
				player.UnequipItem(row1Item);
				player.UnequipItem(row2Item);
				if (row1Item != null)
				{
					// Inv Row 1 -> Temp Row 1
					temp.MoveItemToThis(inv, row1Item, row1Item.m_stack, x, 0);
				}
				if (row2Item != null)
				{
					// Inv Row 2 -> Inv Row 1
					inv.MoveItemToThis(inv, row2Item, row2Item.m_stack, x, 0);
				}
				ItemDrop.ItemData r1 = temp.GetItemAt(x, 0);
				if (r1 != null)
				{
					// Temp Row 1 -> Inv Row 2
					inv.MoveItemToThis(temp, r1, r1.m_stack, x, 1);
				}

				if (row1ItemIsEquipped)
				{
					player.EquipItem(inv.GetItemAt(x, 1), false);
				}
				if (row2ItemIsEquipped)
				{
					player.EquipItem(inv.GetItemAt(x, 0), false);
				}
			}
			if (currentLeftHidden != null)
			{
				player.m_hiddenLeftItem = inv.GetItemAt(currentLeftHidden.m_gridPos.x, currentLeftHidden.m_gridPos.y == 0 ? 1 : 0);
				player.SetupVisEquipment(player.m_visEquipment, false);
			}
			if (currentRightHidden != null)
			{
				player.m_hiddenRightItem = inv.GetItemAt(currentRightHidden.m_gridPos.x, currentRightHidden.m_gridPos.y == 0 ? 1 : 0);
				player.SetupVisEquipment(player.m_visEquipment, false);
			}
			inv.Changed();
			return;
		}

	}
}

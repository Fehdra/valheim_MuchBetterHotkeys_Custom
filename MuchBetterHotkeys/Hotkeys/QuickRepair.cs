namespace MuchBetterHotkeys
{
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {

        private static void QuickRepair(Player player) {
            Debug.Log(player.GetRightItem().m_shared.m_name);
            // This code should not run if we are not holding the hammer, the placement menu is open or we're not pointing at something
            if (player.InPlaceMode()) {
                if (player.GetRightItem() != null || player.GetLeftItem() != null) {
                    if (!player.InAttack()) {
                        player.HideHandItems();
                    }
                }
            }
            if (Hud.IsPieceSelectionVisible()) {
                return;
            }
            ItemDrop.ItemData rightItem = player.GetRightItem();
            if (rightItem != null && player.HaveStamina(rightItem.m_shared.m_attack.m_attackStamina)) {
                player.Repair(rightItem, player.m_buildPieces.GetSelectedPiece());
            }
        }
    }
}

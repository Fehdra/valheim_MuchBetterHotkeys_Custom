namespace MuchBetterHotkeys
{
    public partial class PlayerHotkeyPatch
    {
        private static void QuickRepair(Player player)
        {
            // This code should not run if we are not holding the hammer, the placement menu is open, or we're not pointing at something
            if (Hud.IsPieceSelectionVisible() || !player.InPlaceMode())
            {
                return;
            }

            ItemDrop.ItemData rightItem = player.GetRightItem();
            Piece selectedPiece = player.m_buildPieces?.GetSelectedPiece();

            if (rightItem != null && selectedPiece != null && player.HaveStamina(rightItem.m_shared.m_attack.m_attackStamina))
            {
                player.Repair(rightItem, selectedPiece);
            }
        }
    }
}

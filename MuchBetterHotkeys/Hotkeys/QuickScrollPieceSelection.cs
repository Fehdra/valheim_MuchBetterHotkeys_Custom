using UnityEngine;

namespace MuchBetterHotkeys
{
    public partial class PlayerHotkeyPatch
    {
        private static void QuickScrollPieceSelection(Player player) {
            Vector2Int currentIndex = player.m_buildPieces.GetSelectedIndex();

            int newX = currentIndex.x + ((Input.mouseScrollDelta.y > 0.0) ? 1 : -1);
            int newY = currentIndex.y;

            if (newX < 0) {
                newX = 9;
                newY = currentIndex.y - 1;
            } else if (newX > 9) {
                newX = 0;
                newY = currentIndex.y + 1;
            }

            if (newY < 0) {
                player.m_buildPieces.PrevCategory();
                newY = player.m_buildPieces.GetAvailablePiecesInSelectedCategory() / 10;
            } else if (newY > (player.m_buildPieces.GetAvailablePiecesInSelectedCategory() / 10)) {
                player.m_buildPieces.NextCategory();
                newY = 0;
            }

            currentIndex = new Vector2Int(newX, newY);
            Debug.Log(currentIndex);

            player.SetSelectedPiece(currentIndex);

            // Update the placement ghost
            player.SetupPlacementGhost();

            // Update the requirements UI
            Hud.instance.m_hoveredPiece = null;
        }
    }

}

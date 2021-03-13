using UnityEngine;

namespace MuchBetterHotkeys
{
    public partial class PlayerHotkeyPatch
    {
        private static void QuickScrollPieceSelection(Player player) {
            Debug.Log("Not Implemented Yet");
            return;
            Vector2Int currentIndex = player.m_buildPieces.GetSelectedIndex();
            Debug.Log(currentIndex);
            var newX = currentIndex.x + ((Input.mouseScrollDelta.y > 0.0) ? 1 : -1);
            int newY = 0;
            if (newX < 0) {
                newY = currentIndex.y - 1;
            }
            if (newX >= 10) {
                newY = currentIndex.y + 1;
            }
            newX = newX % 10;

            if (newY < 0) {
                player.m_buildPieces.PrevCategory();
            }

            Debug.Log(currentIndex);
            player.SetSelectedPiece(currentIndex);

            // Update the placement ghost
            player.SetupPlacementGhost();

            // Update the requirements UI
            Hud.instance.m_hoveredPiece = null;
        }
    }

}

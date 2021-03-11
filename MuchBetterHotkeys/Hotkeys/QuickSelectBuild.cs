namespace MuchBetterHotkeys
{

    using System;
    using System.Reflection;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {

        private static void QuickSelectBuild(Player player) {
            // This code should not run if we are not holding the hammer, the placement menu is open or we're not pointing at something
            if (!player.InPlaceMode() && Hud.IsPieceSelectionVisible()) {
                return;
            }

            Piece currentHoverPiece = player.GetHoveringPiece();
            if (currentHoverPiece == null) {
                return;
            }
            bool objectFound = false;

            PieceTable currentPieceTable = player.m_buildPieces;
            Piece.PieceCategory category = currentHoverPiece.m_category;

            int availablePiecesInCategoryLength = currentPieceTable.GetAvailablePiecesInCategory(category);

            // Loop over all available pieces until we have the same as what we hover over
            for (int y = 0; y < availablePiecesInCategoryLength && !objectFound; ++y) {
                Piece elem = currentPieceTable.GetPiece((int)category, new Vector2Int(y % 10, y / 10));
                if (elem.m_icon.name == currentHoverPiece.m_icon.name) {
                    Debug.Log(elem.m_icon);
                    // Set the selected category and selected piece in the piece table of the player
                    currentPieceTable.SetCategory((int)elem.m_category);

                    // TODO: Check what the length of the grid row is
                    // currentPieceTable.SetSelected(new Vector2Int(y % 10, y / 10));
                    player.SetSelectedPiece(new Vector2Int(y % 10, y / 10));

                    // Set rotation of piece to be equal to that of the hovered piece
                    player.m_placeRotation = (int)(Math.Round(currentHoverPiece.transform.localRotation.eulerAngles.y / 22.5f));

                    // Update the placement ghost
                    MethodInfo setupPlacementGhostRef = typeof(Player).GetMethod("SetupPlacementGhost", BindingFlags.NonPublic | BindingFlags.Instance);
                    setupPlacementGhostRef.Invoke(player, new object[] { });

                    // Update the requirements UI
                    Hud.instance.m_hoveredPiece = null;

                    objectFound = true;
                }
            }

        }
    }
}

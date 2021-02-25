namespace MuchBetterHotkeys
{
    using BepInEx;
    using HarmonyLib;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using UnityEngine.UI;

    [BepInPlugin(MID, "Much Better Hotkeys", pluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class MuchBetterHotkeys : BaseUnityPlugin
    {
        private const string modName = "MuchBetterHotkeys";
        private const string MID = "org.drir.plugins.muchbetterhotkeys";
        private const string pluginVersion = "0.1.0";

        // Awake is called once when both the game and the plug-in are loaded
        void Awake() {
            var harmony = new Harmony(MID);

            // Game's version in format "major.minor.patch"
            // string gameVersion = Version.GetVersionString();
            // Version(s) you target
            string[] supportedGameVersions = { "0.145.6" };

            // Skip patching if it's the wrong version

            harmony.PatchAll();
        }

        void OnDestroy() {
            var harmony = new Harmony(MID);
            harmony.UnpatchAll();
        }
    }

    [HarmonyPatch]
    public class PlayerHotkeyPatch : BaseUnityPlugin
    {
        private static Piece GetPiece(Player player) {
            RaycastHit hit;
            if (Physics.Raycast(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, out hit, 50f, (int)typeof(Player).GetField("m_removeRayMask", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(player), QueryTriggerInteraction.UseGlobal) && (Vector3.Distance(hit.point, ((Character)player).m_eye.position) < player.m_maxPlaceDistance)) {
                Piece componentInParent = hit.collider.GetComponentInParent<Piece>();
                return componentInParent;
            }
            return null;
        }

        private static List<List<Piece>> GetAvailablePieces(PieceTable pieceTable) {
            FieldInfo m_availablePiecesRef = typeof(PieceTable).GetField("m_availablePieces", BindingFlags.NonPublic | BindingFlags.Instance);
            List<List<Piece>> m_availablePieces = (List<List<Piece>>)m_availablePiecesRef.GetValue(pieceTable);

            return m_availablePieces;
        }

        private static PieceTable GetPieceTable(Player player) {
            FieldInfo m_buildPiecesRef = typeof(Player).GetField("m_buildPieces", BindingFlags.NonPublic | BindingFlags.Instance);
            PieceTable ___m_buildPieces = (PieceTable)m_buildPiecesRef.GetValue(player);

            // Not just for the hammer, will also return the hoe menu
            return ___m_buildPieces;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static void Postfix_Update(Player __instance) {

            // We should be the local player and we should be able to take input
            if (Player.m_localPlayer != __instance || !(bool)typeof(Player).GetMethod("TakeInput", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, new object[0])) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                // This code should not run if we are not holding the hammer, the placement menu is open or we're not pointing at something
                if (!__instance.InPlaceMode() || Hud.IsPieceSelectionVisible()) {
                    return;
                }
                PieceTable current_piece_table = PlayerHotkeyPatch.GetPieceTable(__instance);
                List<List<Piece>> availablePieces = PlayerHotkeyPatch.GetAvailablePieces(current_piece_table);

                Piece currentHoverPiece = PlayerHotkeyPatch.GetPiece(__instance);
                if (currentHoverPiece == null) {
                    return;
                }
                // Loop over all available pieces until we have the same as what we hover over
                for (int x = 0; x < availablePieces.Count(); ++x) {
                    List<Piece> category = availablePieces.ElementAt(x);
                    for (int y = 0; y < category.Count(); ++y) {
                        Piece elem = category.ElementAt(y);
                        if (elem.m_name == currentHoverPiece.m_name) {
                            // Set the selected category and selected piece in the piece table of the player
                            current_piece_table.SetCategory((int)elem.m_category);
                            // TODO: Check what the length of the grid row is
                            current_piece_table.SetSelected(new Vector2Int(y % 10, y / 10));

                            // Update the placement ghost
                            MethodInfo setupPlacementGhostRef = typeof(Player).GetMethod("SetupPlacementGhost", BindingFlags.NonPublic | BindingFlags.Instance);
                            setupPlacementGhostRef.Invoke(__instance, new object[] { });
                        }
                    }
                }
            }
        }
    }
}

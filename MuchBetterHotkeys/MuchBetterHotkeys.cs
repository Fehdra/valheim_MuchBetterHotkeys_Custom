namespace MuchBetterHotkeys
{
    using BepInEx;
    using BepInEx.Configuration;
    using HarmonyLib;
    using UnityEngine;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Linq;

    [BepInPlugin(MID, "Much Better Hotkeys", pluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class MuchBetterHotkeys : BaseUnityPlugin
    {
        private const string modName = "MuchBetterHotkeys";
        private const string MID = "org.enta.plugins.muchbetterhotkeys";
        private const string pluginVersion = "0.1.0";
        private static string SettingsPath = Path.Combine(Paths.ConfigPath, "MuchBetterHotkeys.cfg");
        public static ConfigFile Settings = null;

        // Awake is called once when both the game and the plug-in are loaded
        void Awake() {
            var harmony = new Harmony(MID);

            // Skip patching if it is the wrong version
            MuchBetterHotkeys.Settings = new ConfigFile(MuchBetterHotkeys.SettingsPath, true);
            if (MuchBetterHotkeys.Settings != null) {
                this.LoadConfig();
            } else {
                Logger.LogError("Failed to setup MuchBetterHotkeys.cfg. BepInEx ConfigFile returned null. Using defaults");
            }

            if (!MuchBetterHotkeys.enabledMod) {
                Logger.LogInfo(modName + " has been disabled in the config");
                return;
            }

            harmony.PatchAll();
        }

        // Token: 0x06000003 RID: 3 RVA: 0x0000208D File Offset: 0x0000028D
        private T BindParameter<T>(T param, string key, string description) {
            return MuchBetterHotkeys.Settings.Bind<T>("MuchBetterHotkeys", key, param, description).Value;
        }

        public static bool enabledMod = true;
        public static bool interactWhileBuilding = true;
        public static KeyCode SwitchHotbarHotkey = KeyCode.Z;
        public static KeyCode QuickSelectBuildHotkey = KeyCode.Q;

        // Token: 0x06000004 RID: 4 RVA: 0x000020A8 File Offset: 0x000002A8
        private void LoadConfig() {
            MuchBetterHotkeys.enabledMod = this.BindParameter<bool>(MuchBetterHotkeys.enabledMod, "Enable", "Whether or not to enable this mod");
            MuchBetterHotkeys.interactWhileBuilding = this.BindParameter<bool>(MuchBetterHotkeys.interactWhileBuilding, "InteractWhileBuilding", "Whether or not to turn on interaction while holding the hammer or the hoe");
            string value = this.BindParameter<string>(MuchBetterHotkeys.SwitchHotbarHotkey.ToString(), "SwitchHotbarHotkey", "Key codes can be found here - https://docs.unity3d.com/ScriptReference/KeyCode.html");
            try {
                MuchBetterHotkeys.SwitchHotbarHotkey = (KeyCode)Enum.Parse(typeof(KeyCode), value, true);
            } catch (Exception) {
                base.Logger.LogError("Failed to parse Switching Hotbars key, using default 'z'");
            }
            value = this.BindParameter<string>(MuchBetterHotkeys.QuickSelectBuildHotkey.ToString(), "QuickSelectBuildHotkey", "Key codes can be found here - https://docs.unity3d.com/ScriptReference/KeyCode.html");
            try {
                MuchBetterHotkeys.QuickSelectBuildHotkey = (KeyCode)Enum.Parse(typeof(KeyCode), value, true);
            } catch (Exception) {
                base.Logger.LogError("Failed to parse Quick Select Build key, using default 'q'");
            }
        }

        void OnDestroy() {
            var harmony = new Harmony(MID);
            harmony.UnpatchSelf();
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

        private static bool switching_hotbars = false;

        private static void SwitchHotbar(Player player) {
            // TODO: Currently tools don't seem to switch places
            // TODO: Feathers moved over my tool once and now the tool is gone
            if (switching_hotbars) {
                return;
            }
            switching_hotbars = true;
            ref Inventory inv = ref ((Humanoid)player).m_inventory;
            int width = inv.GetWidth();
            for (int x = 0; x < width; x++) {
                ItemDrop.ItemData row1Item = inv.GetItemAt(x, 0);
                ItemDrop.ItemData row2Item = inv.GetItemAt(x, 1);
                ((Humanoid)player).RemoveFromEquipQueue(row1Item);
                ((Humanoid)player).RemoveFromEquipQueue(row2Item);
                if (row2Item != null) {
                    // Row 2 -> Row 1
                    if (row1Item != null) {
                        inv.RemoveItem(row1Item);
                    }
                    inv.MoveItemToThis(inv, row2Item, row2Item.m_stack, x, 0);
                }
                if (row1Item != null) {
                    // Row 1 -> Row 2
                    // TODO: This generates a "Item is not in this container message"
                    inv.MoveItemToThis(inv, row1Item, row1Item.m_stack, x, 1);
                    //inv.(row1Item, row1Item.m_stack, x, 1);
                }
                inv.Changed();
            }
            switching_hotbars = false;
            return;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static bool Prefix_Update(Player __instance) {
            if (Player.m_localPlayer != __instance || !(bool)typeof(Player).GetMethod("TakeInput", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, new object[0])) {
                return true;
            }

            if (Hud.IsPieceSelectionVisible() && __instance.InPlaceMode()) {
                KeyCode[] keycodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
                for (int idx = 0; idx < keycodes.Length; idx++) {
                    KeyCode curKeyCode = keycodes[idx];
                    if (Input.GetKeyDown(curKeyCode)) {
                        __instance.SetSelectedPiece(new Vector2Int(idx, 0));
                        Hud.instance.TogglePieceSelection();
                        return false;
                    }
                }
            }

            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {
                if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                    __instance.m_placeRotation -= 4;
                    return false;
                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                    __instance.m_placeRotation += 4;
                    return false;
                }
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static void Postfix_Update(Player __instance, ref int ___m_placeRotation) {
            // We should be the local player and we should be able to take input
            if (Player.m_localPlayer != __instance || !(bool)typeof(Player).GetMethod("TakeInput", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, new object[0])) {
                return;
            }

            //if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Alpha1)) {
            if (Input.GetKeyDown(MuchBetterHotkeys.SwitchHotbarHotkey)) {
                PlayerHotkeyPatch.SwitchHotbar(__instance);
                return;
            }

            if (Input.GetKeyDown(MuchBetterHotkeys.QuickSelectBuildHotkey)) {
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
                bool objectFound = false;
                // Loop over all available pieces until we have the same as what we hover over
                for (int x = 0; x < availablePieces.Count() && !objectFound; ++x) {
                    List<Piece> category = availablePieces.ElementAt(x);
                    for (int y = 0; y < category.Count() && !objectFound; ++y) {
                        Piece elem = category.ElementAt(y);
                        if (elem.m_name == currentHoverPiece.m_name) {
                            // Set the selected category and selected piece in the piece table of the player
                            current_piece_table.SetCategory((int)elem.m_category);

                            // TODO: Check what the length of the grid row is
                            // current_piece_table.SetSelected(new Vector2Int(y % 10, y / 10));
                            __instance.SetSelectedPiece(new Vector2Int(y % 10, y / 10));

                            // Set rotation of piece to be equal to that of the hovered piece
                            __instance.m_placeRotation = (int)(Math.Round(currentHoverPiece.transform.localRotation.eulerAngles.y / 22.5f));

                            // Update the placement ghost
                            MethodInfo setupPlacementGhostRef = typeof(Player).GetMethod("SetupPlacementGhost", BindingFlags.NonPublic | BindingFlags.Instance);
                            setupPlacementGhostRef.Invoke(__instance, new object[] { });

                            // Update the requirements UI
                            Hud.instance.m_hoveredPiece = null;

                            objectFound = true;
                        }
                    }
                }
            }
            return;
        }

        private static MethodInfo InPlaceModeRef = AccessTools.Method(typeof(Character), "InPlaceMode", null, null);

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Hud), "Awake")]
        private static void MoveHealthRoot(Hud __instance) {
            Vector3 currentPosition = __instance.m_pieceHealthRoot.localPosition;
            __instance.m_pieceHealthRoot.localPosition = new Vector3(currentPosition.x, currentPosition.y + 20f, currentPosition.z);
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Player), "UpdateHover")]
        private static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions) {
            if (MuchBetterHotkeys.interactWhileBuilding) {
                return instructions;
            }
            // TODO: This might remove the entire inPlaceMode functionality
            List<CodeInstruction> list = Enumerable.ToList<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Calls(PlayerHotkeyPatch.InPlaceModeRef)) {
                    list[i - 1].opcode = OpCodes.Nop;
                    list[i] = new CodeInstruction(OpCodes.Ldc_I4_0, null);
                }
            }
            return Enumerable.AsEnumerable<CodeInstruction>(list);
        }
    }
}

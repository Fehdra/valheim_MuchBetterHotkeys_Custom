namespace MuchBetterHotkeys
{

    using System;
    using BepInEx;
    using HarmonyLib;
    using UnityEngine;
    public partial class PlayerHotkeyPatch
    {
        private static bool TransferItemToHotbarHotkey(Player player) {
            return true;
            // TODO
            if (!InventoryGui.IsVisible()) {
                return true;
            }
            KeyCode[] keycodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
            for (int idx = 0; idx < keycodes.Length; idx++) {
            }
            return true;
        }
    }
}

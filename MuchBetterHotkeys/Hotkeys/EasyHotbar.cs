using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MuchBetterHotkeys
{
    public partial class PlayerHotkeyPatch
    {
        private static bool EasyHotbar(Player player) {
            if (!MuchBetterHotkeys.easyHotbarPrefix.Value.IsPressed()) {
                return true;
            }
            KeyCode[] keycodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
            for (int idx = 0; idx < keycodes.Length; idx++) {
                KeyCode curKeyCode = keycodes[idx];
                if (Input.GetKeyDown(curKeyCode)) {
                    player.UseHotbarItem(idx + 5);
                    return false;
                }
            }
            return true;
        }
    }
}

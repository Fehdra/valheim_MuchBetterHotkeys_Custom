using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MuchBetterHotkeys
{
    class EasyHotbar
    {
        private static MethodInfo Hotbar1 = AccessTools.Method(typeof(Input), "GetKeyDown", new System.Type[] { typeof(int), }, null);

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Player), "Update")]
        private static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions) {
            if (!MuchBetterHotkeys.settings.interactWhileBuilding) {
                return instructions;
            }
            // TODO: This might remove the entire inPlaceMode functionality
            List<CodeInstruction> list = Enumerable.ToList<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Calls()) {
                }
            }
            return Enumerable.AsEnumerable<CodeInstruction>(list);
        }
    }
}

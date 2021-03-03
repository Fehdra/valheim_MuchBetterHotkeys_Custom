using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace MuchBetterHotkeys
{
    class InteractWhileBuild : BaseUnityPlugin
    {

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
            if (!MuchBetterHotkeys.settings.interactWhileBuilding) {
                return instructions;
            }
            // TODO: This might remove the entire inPlaceMode functionality
            List<CodeInstruction> list = Enumerable.ToList<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Calls(InteractWhileBuild.InPlaceModeRef)) {
                    list[i - 1].opcode = OpCodes.Nop;
                    list[i] = new CodeInstruction(OpCodes.Ldc_I4_0, null);
                }
            }
            return Enumerable.AsEnumerable<CodeInstruction>(list);
        }
    }
}

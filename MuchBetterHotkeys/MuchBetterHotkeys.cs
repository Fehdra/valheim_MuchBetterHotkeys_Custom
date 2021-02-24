namespace MuchBetterHotkeys
{
    using BepInEx;
    using HarmonyLib;
    using UnityEngine;
    using System;
    using System.Reflection;

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


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static void Postfix_Update(Player __instance, GameObject ___m_hovering) {

            if (Player.m_localPlayer != __instance && (bool)typeof(Player).GetMethod("TakeInput", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, new object[0]) && __instance.InPlaceMode()) {
                return;
            }


            if (Input.GetKeyDown(KeyCode.Q)) {
                // MethodInfo UpdateHover = typeof(Player).GetMethod("UpdateHover", BindingFlags.NonPublic | BindingFlags.Instance);
                // UpdateHover.Invoke(__instance, new object[0]);
                Debug.Log(PlayerHotkeyPatch.GetPiece(__instance));
                // Debug.Log(__instance.GetHoveringPiece());
            }
        }
    }
}

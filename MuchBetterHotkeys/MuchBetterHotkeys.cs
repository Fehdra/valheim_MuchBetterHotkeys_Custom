namespace MuchBetterHotkeys
{
    using BepInEx;
    using HarmonyLib;

    [BepInPlugin(MID, modName, pluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class MuchBetterHotkeys : BaseUnityPlugin
    {
        private const string MID = "org.enta.plugins.muchbetterhotkeys";
        private const string modName = "Much Better Hotkeys";
        private const string pluginVersion = "1.0.0";
        private static Settings m_settings = null;

        // Awake is called once when both the game and the plug-in are loaded
        void Awake() {
            var harmony = new Harmony(MID);

            // TODO: Skip patching if it is the wrong version or print warning
            MuchBetterHotkeys.m_settings = new Settings();
            if (MuchBetterHotkeys.m_settings == null) {
                Logger.LogError("Failed to initialize settings for " + modName);
                return;
            }
            PlayerHotkeyPatch.m_settings = MuchBetterHotkeys.m_settings;

            if (!MuchBetterHotkeys.m_settings.enabledMod.Value) {
                Logger.LogInfo(modName + " has been disabled in the mod config");
                return;
            }

            harmony.PatchAll();
        }

        void OnDestroy() {
            var harmony = new Harmony(MID);
            harmony.UnpatchSelf();
        }

        public static Settings settings {
            get {
                return MuchBetterHotkeys.m_settings;
            }
        }
    }
}
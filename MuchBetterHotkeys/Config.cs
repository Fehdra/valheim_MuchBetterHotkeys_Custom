namespace MuchBetterHotkeys
{

    using BepInEx;
    using BepInEx.Configuration;
    using System.IO;
    using UnityEngine;

    public class Settings
    {
        private static string SettingsPath = Path.Combine(Paths.ConfigPath, "MuchBetterHotkeys.cfg");
        public static ConfigFile ConfigFile = null;


        public Settings() {
            Settings.ConfigFile = new ConfigFile(Settings.SettingsPath, true);
            if (Settings.ConfigFile != null) {
                this.LoadConfig();
            } else {
                Debug.LogError("Failed to setup MuchBetterHotkeys.cfg. BepInEx ConfigFile returned null. Using defaults");
            }
        }

        private static string GENERAL_SETTINGS = "General";

        public ConfigEntry<bool> enabledMod { get; set; }
        public ConfigEntry<bool> interactWhileBuilding { get; set; }
        public ConfigEntry<KeyboardShortcut> switchHotbarHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> quickSelectBuildHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> quickRepairHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> quickEquipHammerHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> quickOpenSkillsHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> quickOpenTextsHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> buildRotationSnapHotkey { get; set; }
        public ConfigEntry<KeyboardShortcut> easyHotbarPrefix { get; set; }
        public ConfigEntry<KeyboardShortcut> quickEquipTorchHotkey { get; set; }

        private void LoadConfig() {
            this.enabledMod = ConfigFile.Bind<bool>(GENERAL_SETTINGS, "Enable", true, "Whether or not to enable this mod");
            this.interactWhileBuilding = ConfigFile.Bind<bool>(GENERAL_SETTINGS, "InteractWhileBuilding", true, "Whether or not to turn on interaction while holding the hammer, the hoe or any tool with a menu");

            switchHotbarHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "SwitchHotbarHotkey", new KeyboardShortcut(KeyCode.Z), "Swaps the 1st and 2nd row of your inventory \nKey codes can be found here - https://docs.unity3d.com/ScriptReference/KeyCode.html");
            quickSelectBuildHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickSelectBuildHotkey", new KeyboardShortcut(KeyCode.Q), "Press when building to select the object and rotation of the object you're pointing at. Rather than selecting it from the HUD");
            quickRepairHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickRepairHotkey", new KeyboardShortcut(KeyCode.V), "While building, press this key to repair the object you're aiming at");
            quickEquipHammerHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipHammerHotkey", new KeyboardShortcut(KeyCode.B), "Find the hammer with the lowest durability in your inventory and equip it");
            quickEquipTorchHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipTorchHotkey", new KeyboardShortcut(KeyCode.T), "Find the torch with the lowest durability in your inventory and equip it");
            quickOpenSkillsHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickOpenSkillsHotkey", new KeyboardShortcut(KeyCode.P), "Open the skills menu");
            quickOpenTextsHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickOpenTextsHotkey", new KeyboardShortcut(KeyCode.L), "Open the log that holds all information about statuses and previous messages");
            buildRotationSnapHotkey = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "BuildRotationSnapHotkey", new KeyboardShortcut(KeyCode.LeftControl), "Hold this key while scrolling to rotate objects 90 degrees in build mode");
            easyHotbarPrefix = ConfigFile.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EasyHotbarPrefix", new KeyboardShortcut(KeyCode.LeftAlt), "Hold this prefix together with 1-4 in order to use hotbar items 5-8");
        }
    }
}
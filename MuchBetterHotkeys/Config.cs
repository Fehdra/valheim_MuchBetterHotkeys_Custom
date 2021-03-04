namespace MuchBetterHotkeys
{

    using BepInEx;
    using BepInEx.Configuration;
    using System;
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

        public bool enabledMod = true;
        public bool interactWhileBuilding = true;
        public KeyCode SwitchHotbarHotkey = KeyCode.Z;
        public KeyCode QuickSelectBuildHotkey = KeyCode.Q;
        public KeyCode QuickRepairHotkey = KeyCode.V;
        public KeyCode QuickEquipHammerHotkey = KeyCode.B;
        public KeyCode QuickEquipTorchHotkey = KeyCode.T;
        public KeyCode BuildRotationSnapHotkey = KeyCode.LeftControl;

        private T BindParameter<T>(T param, string key, string description) {
            return Settings.ConfigFile.Bind<T>("MuchBetterHotkeys", key, param, description).Value;
        }

        private void ParseKey(string key, ref KeyCode defaultKey, string description) {
            string keyurl = "\nKey codes can be found here - https://docs.unity3d.com/ScriptReference/KeyCode.html";
            string value = this.BindParameter<string>(defaultKey.ToString(), key, description + keyurl);
            try {
                defaultKey = (KeyCode)Enum.Parse(typeof(KeyCode), value, true);
            } catch (Exception) {
                Debug.LogError("Failed to parse " + key + ", using default " + defaultKey.ToString());
            }
        }

        private void LoadConfig() {
            this.enabledMod = this.BindParameter<bool>(enabledMod, "Enable", "Whether or not to enable this mod");
            this.interactWhileBuilding = this.BindParameter<bool>(interactWhileBuilding, "InteractWhileBuilding", "Whether or not to turn on interaction while holding the hammer, the hoe or any tool with a menu");

            ParseKey("SwitchHotbarHotkey", ref this.SwitchHotbarHotkey, "Swaps the 1st and 2nd row of your inventory");
            ParseKey("QuickSelectBuildHotkey", ref this.QuickSelectBuildHotkey, "Press when building to select the object and rotation of the object you're pointing at. Rather than selecting it from the HUD");
            ParseKey("QuickRepairHotkey", ref this.QuickRepairHotkey, "While building, press this key to repair the object you're aiming at");
            ParseKey("QuickEquipHammerHotkey", ref this.QuickEquipHammerHotkey, "Find the hammer with the lowest durability in your inventory and equip it");
            ParseKey("QuickEquipTorchHotkey", ref this.QuickEquipTorchHotkey, "Find the torch with the lowest durability in your inventory and equip it");
            ParseKey("BuildRotationSnapHotkey", ref this.BuildRotationSnapHotkey, "Hold this key while scrolling to rotate objects 90 degrees in build mode");
        }
    }
}
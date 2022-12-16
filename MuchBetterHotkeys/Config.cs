namespace MuchBetterHotkeys {

	using BepInEx;
	using BepInEx.Configuration;
	using UnityEngine;

	public partial class MuchBetterHotkeys : BaseUnityPlugin {
		private static string GENERAL_SETTINGS = "General";

		public static ConfigEntry<bool> enabledMod { get; set; }
		public static ConfigEntry<bool> interactWhileBuilding { get; set; }
		public static ConfigEntry<KeyboardShortcut> switchHotbarHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickSelectBuildHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickRepairHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickEquipHammerHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickOpenSkillsHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickOpenTextsHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> buildRotationSnapHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> easyHotbarPrefix { get; set; }
		public static ConfigEntry<KeyboardShortcut> scrollPieceSelectionPrefix { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickEquipTorchHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickEquipPickaxeHotkey { get; set; }
		public static ConfigEntry<KeyboardShortcut> quickEquipAxeHotkey { get; set; }

		// Extra
		public static bool buildHelperModActive = false;

		private void LoadConfig() {
			enabledMod = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable", true, "Whether or not to enable this mod");
			interactWhileBuilding = base.Config.Bind<bool>(GENERAL_SETTINGS, "InteractWhileBuilding", true, "Whether or not to turn on interaction while holding the hammer, the hoe or any tool with a menu");

			switchHotbarHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "SwitchHotbarHotkey", new KeyboardShortcut(KeyCode.Z), "Swaps the 1st and 2nd row of your inventory \nKey codes can be found here - https://docs.unity3d.com/ScriptReference/KeyCode.html");
			quickSelectBuildHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickSelectBuildHotkey", new KeyboardShortcut(KeyCode.Q), "Press when building to select the object and rotation of the object you're pointing at. Rather than selecting it from the HUD");
			quickRepairHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickRepairHotkey", new KeyboardShortcut(KeyCode.V), "While building, press this key to repair the object you're aiming at");
			quickEquipHammerHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipHammerHotkey", new KeyboardShortcut(KeyCode.B), "Find the hammer with the lowest durability in your inventory and equip it");
			quickEquipTorchHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipTorchHotkey", new KeyboardShortcut(KeyCode.T), "Find the torch with the lowest durability in your inventory and equip it");
			quickOpenSkillsHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickOpenSkillsHotkey", new KeyboardShortcut(KeyCode.P), "Open the skills menu");
			quickOpenTextsHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickOpenTextsHotkey", new KeyboardShortcut(KeyCode.L), "Open the log that holds all information about statuses and previous messages");
			buildRotationSnapHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "BuildRotationSnapHotkey", new KeyboardShortcut(KeyCode.LeftControl), "Hold this key while scrolling to rotate objects 90 degrees in build mode");
			easyHotbarPrefix = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EasyHotbarPrefix", new KeyboardShortcut(KeyCode.LeftAlt), "Hold this prefix together with 1-4 in order to use hotbar items 5-8");
			scrollPieceSelectionPrefix = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "ScrollPieceSelectionPrefix", new KeyboardShortcut(KeyCode.LeftAlt), "Hold this prefix together with scrolling to quickly switch between items in a hammer/hoe/cultivator menu");
			quickEquipPickaxeHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipPickaxeHotkey", new KeyboardShortcut(KeyCode.Q), "Find the best quality, but lowest durability, pickaxe when looking at its respective resource and pressing this button. Equip it.");
			quickEquipAxeHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "QuickEquipAxeHotkey", new KeyboardShortcut(KeyCode.Q), "Find the best quality, but lowest durability, axe when looking at its respective resource and pressing this button. Equip it.");
		}
	}
}
namespace MuchBetterHotkeys
{
	using BepInEx;
	using HarmonyLib;
	using UnityEngine;

	[BepInPlugin(MID, modName, pluginVersion)]
	[BepInProcess("valheim.exe")]
	[BepInProcess("valheim_server.exe")]
	// [BepInDependency("Xenofell.BuildHelper", BepInDependency.DependencyFlags.SoftDependency)]
	public partial class MuchBetterHotkeys : BaseUnityPlugin
	{
		private const string MID = "org.enta.plugins.muchbetterhotkeys";
		private const string modName = "Much Better Hotkeys";
		private const string pluginVersion = "1.1.0";

		// Awake is called once when both the game and the plug-in are loaded
		void Awake()
		{
			var harmony = new Harmony(MID);

			// TODO: Skip patching if it is the wrong version or print warning
			this.LoadConfig();

			if (!MuchBetterHotkeys.enabledMod.Value)
			{
				Logger.LogInfo(modName + " has been disabled in the mod config");
				return;
			}

			// TODO: Create a link to BuildHelper
			// CheckSoftDependencies();

			harmony.PatchAll();
		}

		public void CheckSoftDependencies()
		{
			// Set base values
			buildHelperModActive = false;

			// Check soft dependencies
			GameObject manager = GameObject.Find("BepInEx_Manager");
			BaseUnityPlugin[] componentsInChildren = manager.GetComponentsInChildren<BaseUnityPlugin>();
			foreach (BaseUnityPlugin value in componentsInChildren)
			{
				if (value.Info.Metadata.GUID == "Xenofell.BuildHelper")
				{
					buildHelperModActive = true;
				}
			}

		}

		void OnDestroy()
		{
			var harmony = new Harmony(MID);
			harmony.UnpatchSelf();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;

namespace MuchBetterHotkeys
{
    [BepInPlugin("org.bepinex.plugins.muchbetterhotkeys", "Much Better Hotkeys", "1.0.0.0")]
    [BepInProcess("valheim.exe")]
    public class MuchBetterHotkeys : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            // Game's version in format "major.minor.patch"
            string gameVersion = Version.GetVersionString();
            // Version(s) you target
            string[] supportedGameVersions = { "0.145.6" };

            // Skip patching if it's the wrong version
            if (Array.IndexOf(supportedGameVersions, gameVersion) == -1)
            {
                Logger.LogWarning($"Mod MOD_NAME disabled, incompatible game version {gameVersion}");
                return;
            }

            var harmony = new Harmony("someuniquestring");
            harmony.PatchAll();
        }
    }

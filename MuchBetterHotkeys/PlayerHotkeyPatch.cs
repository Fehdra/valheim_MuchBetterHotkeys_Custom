<<<<<<< HEAD
namespace MuchBetterHotkeys
{

    using BepInEx;
    using HarmonyLib;
    using System;
    using UnityEngine;

    [HarmonyPatch]
    public partial class PlayerHotkeyPatch : BaseUnityPlugin
    {

        public static Settings m_settings = null;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static bool Prefix_Update(Player __instance) {
            // We should be the local player and we should be able to take input
            if (Player.m_localPlayer != __instance) {
                return true;
            }

            // Quickly transfer items to hotbar using a hover and 1-9
            if (!PlayerHotkeyPatch.TransferItemToHotbarHotkey(__instance)) {
                return false;
            }

            if (!__instance.TakeInput()) {
                return true;
            }


            // Easy Hotbar, making it more accessible to reach the higher numbers of the hotbar (5-8)
            if (!PlayerHotkeyPatch.EasyHotbar(__instance)) {
                return false;
            }

            // Rotate 90 degree angles
            if (Input.GetKey(PlayerHotkeyPatch.m_settings.BuildRotationSnapHotkey)) {
                if (!PlayerHotkeyPatch.BuildRotationSnap(__instance)) {
                    return false;
                }
            }

            // Piece Selection hotkey
            if (Hud.IsPieceSelectionVisible() && __instance.InPlaceMode()) {
                if (!PlayerHotkeyPatch.QuickPieceSelection(__instance)) {
                    return false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) && __instance.GetHoverObject() != null && !PlayerHotkeyPatch.QuickEquipAxe(__instance)) {
                return false;
            }

            if (Input.GetKeyDown(KeyCode.Q) && __instance.GetHoverObject() != null && !PlayerHotkeyPatch.QuickEquipPickaxe(__instance)) {
                return false;
            }

            return true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Update")]
        private static void Postfix_Update(Player __instance, ref int ___m_placeRotation) {
            // We should be the local player and we should be able to take input
            if (Player.m_localPlayer != __instance || !__instance.TakeInput()) {
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.SwitchHotbarHotkey)) {
                PlayerHotkeyPatch.SwitchHotbar(__instance);
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickRepairHotkey)) {
                PlayerHotkeyPatch.QuickRepair(__instance);
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickEquipHammerHotkey)) {
                PlayerHotkeyPatch.QuickEquipHammer(__instance);
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickOpenSkillsHotkey)) {
                PlayerHotkeyPatch.QuickOpenSkills();
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickOpenTextsHotkey)) {
                PlayerHotkeyPatch.QuickOpenTexts();
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickEquipTorchHotkey)) {
                PlayerHotkeyPatch.QuickEquipTorch(__instance);
                return;
            }

            if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickSelectBuildHotkey)) {
                PlayerHotkeyPatch.QuickSelectBuild(__instance);
                return;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(InventoryGui), "OnRightClickItem", new Type[] { typeof(InventoryGrid), typeof(ItemDrop.ItemData), typeof(Vector2i) })]
        private static bool Prefix_OnRightClickItem(InventoryGui __instance, InventoryGrid grid, ItemDrop.ItemData item, Vector2i pos) {
            // TODO
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && grid.isActiveAndEnabled) {
                __instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, item.m_stack / 2);
                Debug.Log(__instance.m_dragGo);
                return false;
            }
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && grid.isActiveAndEnabled) {
                if (__instance.m_dragItem != null) {
                    // Add 1 if we do rmb more often
                    // TODO
                    //__instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, 1);
                } else {
                    __instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, 1);
                    Debug.Log(__instance.m_dragGo);
                }
                return false;
            }
            return true;
        }
    }
=======
namespace MuchBetterHotkeys {

	using BepInEx;
	using HarmonyLib;
	using System;
	using UnityEngine;

	[HarmonyPatch]
	public partial class PlayerHotkeyPatch : BaseUnityPlugin {

		public static Settings m_settings = null;

		[HarmonyPrefix]
		[HarmonyPatch(typeof(Player), "Update")]
		private static bool Prefix_Update(Player __instance) {
			// We should be the local player and we should be able to take input
			if (Player.m_localPlayer != __instance) {
				return true;
			}

			// Quickly transfer items to hotbar using a hover and 1-9
			if (!PlayerHotkeyPatch.TransferItemToHotbarHotkey(__instance)) {
				return false;
			}

			if (!__instance.TakeInput()) {
				return true;
			}


			// Easy Hotbar, making it more accessible to reach the higher numbers of the hotbar (5-8)
			if (!PlayerHotkeyPatch.EasyHotbar(__instance)) {
				return false;
			}

			// Rotate 90 degree angles
			if (Input.GetKey(PlayerHotkeyPatch.m_settings.BuildRotationSnapHotkey)) {
				if (!PlayerHotkeyPatch.BuildRotationSnap(__instance)) {
					return false;
				}
			}

			// Piece Selection hotkey
			if (Hud.IsPieceSelectionVisible() && __instance.InPlaceMode()) {
				if (!PlayerHotkeyPatch.QuickPieceSelection(__instance)) {
					return false;
				}
			}

			if (Input.GetKeyDown(KeyCode.Q) && __instance.GetHoverObject() != null) {
				PlayerHotkeyPatch.QuickEquipAxe(__instance);
				PlayerHotkeyPatch.QuickEquipPickaxe(__instance);
			}
			return true;
		}


		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "Update")]
		private static void Postfix_Update(Player __instance, ref int ___m_placeRotation) {
			// We should be the local player and we should be able to take input
			if (Player.m_localPlayer != __instance || !__instance.TakeInput()) {
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.SwitchHotbarHotkey)) {
				PlayerHotkeyPatch.SwitchHotbar(__instance);
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickRepairHotkey)) {
				PlayerHotkeyPatch.QuickRepair(__instance);
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickEquipHammerHotkey)) {
				PlayerHotkeyPatch.QuickEquipHammer(__instance);
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickOpenSkillsHotkey)) {
				PlayerHotkeyPatch.QuickOpenSkills();
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickOpenTextsHotkey)) {
				PlayerHotkeyPatch.QuickOpenTexts();
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickEquipTorchHotkey)) {
				PlayerHotkeyPatch.QuickEquipTorch(__instance);
				return;
			}

			if (Input.GetKeyDown(PlayerHotkeyPatch.m_settings.QuickSelectBuildHotkey)) {
				PlayerHotkeyPatch.QuickSelectBuild(__instance);
				return;
			}
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(InventoryGui), "OnRightClickItem", new Type[] { typeof(InventoryGrid), typeof(ItemDrop.ItemData), typeof(Vector2i) })]
		private static bool Prefix_OnRightClickItem(InventoryGui __instance, InventoryGrid grid, ItemDrop.ItemData item, Vector2i pos) {
			// TODO
			if (( Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ) && grid.isActiveAndEnabled) {
				__instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, item.m_stack / 2);
				Debug.Log(__instance.m_dragGo);
				return false;
			}
			if (( Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) ) && grid.isActiveAndEnabled) {
				if (__instance.m_dragItem != null) {
					// Add 1 if we do rmb more often
					// TODO
					//__instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, 1);
				} else {
					__instance.SetupDragItem(item, Player.m_localPlayer.m_inventory, 1);
					Debug.Log(__instance.m_dragGo);
				}
				return false;
			}
			return true;
		}
	}
>>>>>>> e7888f27eea7f000d5fb6267e63e3c93269de302
}
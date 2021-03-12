namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {

        public static string[] axeCompareTexts = new string[] { "Log", "Beech", "Birch", "Oak", "Ancient tree" };
        private static bool QuickEquipAxe(Player player) {
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            string hoverText = hoverable.GetHoverText();
            foreach (string compareText in axeCompareTexts) {
                if (compareText == hoverText) {
                    PlayerHotkeyPatch.QuickEquipHammer(player);
                }
            }
            return true;
        }
    }
}

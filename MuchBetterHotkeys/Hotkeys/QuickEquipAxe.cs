namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {
        private static bool QuickEquipAxe(Player player) {
            Debug.Log(" Not Implemented Yet ");
            return false;
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            string hoverText = hoverable.GetHoverText();
            string[] compareTexts = new string[] { "Log", "Beech", "Birch", "Oak", "Ancient tree" };
            foreach (string compareText in compareTexts) {
                if (compareText == hoverText) {
                    PlayerHotkeyPatch.QuickEquipHammer(player);
                }
            }
            return false;
        }
    }
}

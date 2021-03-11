namespace MuchBetterHotkeys
{
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {
        private static bool QuickEquipPickaxe(Player player) {
            Debug.Log(" Not Implemented Yet ");
            return false;
            GameObject hoverObject = player.GetHoverObject();
            Hoverable hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            string hoverText = hoverable.GetHoverText();
            string[] compareTexts = new string[] { "Rock", "Tin deposit", "Muddy scraps", "Copper deposit", "Obsidian", "Silver vein" };
            foreach (string compareText in compareTexts) {
                if (compareText == hoverText) {
                    PlayerHotkeyPatch.QuickEquipHammer(player);
                }
            }
            return false;
        }
    }
}

namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerHotkeyPatch
    {
        private static void QuickOpenSkills() {
            InventoryGui igui = InventoryGui.instance;
            igui.Show(null);
            igui.OnOpenSkills();
        }
    }
}

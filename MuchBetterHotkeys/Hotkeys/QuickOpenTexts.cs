namespace MuchBetterHotkeys
{

    using System;
    using System.Collections.Generic;

    public partial class PlayerHotkeyPatch
    {
        private static void QuickOpenTexts() {
            InventoryGui igui = InventoryGui.instance;
            igui.Show(null);
            igui.OnOpenTexts();
        }
    }
}

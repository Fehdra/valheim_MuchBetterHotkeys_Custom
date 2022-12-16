namespace MuchBetterHotkeys {

	public partial class PlayerHotkeyPatch {
		private static void QuickOpenTexts() {
			InventoryGui igui = InventoryGui.instance;
			igui.Show(null);
			igui.OnOpenTexts();
		}
	}
}

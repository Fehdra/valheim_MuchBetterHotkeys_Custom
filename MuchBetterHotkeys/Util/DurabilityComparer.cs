namespace MuchBetterHotkeys {

	using System.Collections.Generic;

	public class DurabilityComparer : IComparer<ItemDrop.ItemData> {
		int IComparer<ItemDrop.ItemData>.Compare(ItemDrop.ItemData x, ItemDrop.ItemData y) {
			if (x.m_durability == 0) {
				return 1;
			} else if (y.m_durability == 0) {
				return -1;
			}
			return x.m_durability > y.m_durability ? 1 : -1;
		}
	}

}
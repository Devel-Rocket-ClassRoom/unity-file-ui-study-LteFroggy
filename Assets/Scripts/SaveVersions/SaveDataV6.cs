using System;
using System.Collections.Generic;

namespace SaveVersions {
	public class SaveDataV6 : SaveDataV5 {
		public override int Version => 6;
		
		public CharacterFilteringOption CharacterFilteringOption = CharacterFilteringOption.None;
		public CharacterSortingOption CharacterSortingOption = CharacterSortingOption.CreatedAscending;
		public InventoryFilteringOption InventoryFilteringOption = InventoryFilteringOption.None;
		public InventorySortingOption InventorySortingOption = InventorySortingOption.CreatedAscending;
		public List<CharacterSaveData> CharacterList = new List<CharacterSaveData>();
		
		public override SaveData VersionUp() {
			throw new InvalidOperationException();
		}
	}
}
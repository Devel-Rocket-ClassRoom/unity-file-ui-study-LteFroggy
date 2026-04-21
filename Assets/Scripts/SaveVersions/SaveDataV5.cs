using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaveVersions;

public class SaveDataV5 : SaveDataV4 {
	public override int Version => 5;
	
	[JsonConverter(typeof(StringEnumConverter))]
	public InventoryFilteringOption filteringOption;
	[JsonConverter(typeof(StringEnumConverter))]
	public InventorySortingOption sortingOption;
	
	public override SaveData VersionUp() {
		return new SaveDataV6() {
			InventoryFilteringOption = filteringOption,
			InventorySortingOption = sortingOption,
			Name = Name,
			Gold = Gold,
			ItemList = ItemList,
		};
	}
}
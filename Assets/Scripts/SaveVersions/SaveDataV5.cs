using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class SaveDataV5 : SaveDataV4 {
	public override int Version => 5;
	
	
	[JsonConverter(typeof(StringEnumConverter))]
	public InventoryFilteringOption filteringOption;
	[JsonConverter(typeof(StringEnumConverter))]
	public InventorySortingOption sortingOption;
	
	public override SaveData VersionUp() {
		throw new InvalidOperationException();
	}
}
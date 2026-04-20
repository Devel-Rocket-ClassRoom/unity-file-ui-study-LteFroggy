using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ItemDataConverter : JsonConverter<ItemData> {
	public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer) {
		writer.WriteStartObject();
		writer.WritePropertyName("itemId");
		writer.WriteValue(value.Id);
		writer.WriteEndObject();
	}

	public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue,
		JsonSerializer serializer) {
		
		JObject obj = JObject.Load(reader);
		
		return DataTableManager.ItemTable.Get((string)obj["itemId"]);
	}
}
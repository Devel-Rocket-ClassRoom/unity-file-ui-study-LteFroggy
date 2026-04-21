using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CharacterDataConverter : JsonConverter<CharacterData> {
	public override void WriteJson(JsonWriter writer, CharacterData value, JsonSerializer serializer) {
		writer.WriteStartObject();
		writer.WritePropertyName("characterId");
		writer.WriteValue(value.Id);
		writer.WriteEndObject();
	}

	public override CharacterData ReadJson(JsonReader reader, Type objectType, CharacterData existingValue, bool hasExistingValue,
		JsonSerializer serializer) {
		
		JObject obj = JObject.Load(reader);
		
		return DataTableManager.CharacterTable.Get((string)obj["characterId"]);
	}
}
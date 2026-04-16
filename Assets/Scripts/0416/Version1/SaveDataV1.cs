public class SaveDataV1 : SaveData {
	public string PlayerName { get; set; } = string.Empty;
	
	public override int Version => 1;
	
	public override SaveData VersionUp() {
		return new SaveDataV2(PlayerName);
	}
}
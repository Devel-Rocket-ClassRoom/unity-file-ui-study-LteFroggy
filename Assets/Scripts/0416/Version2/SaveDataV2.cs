public class SaveDataV2 : SaveData {
	public override int Version => 2;
	
	public string Name { get; set; } = "Unknown";
	public int Gold { get; set; } = 0;
	
	public override SaveData VersionUp() {
		return new SaveDataV3(this);
	}
	
	public SaveDataV2() {}
	
	// V1 -> V2 마이그레이션 시 사용
	public SaveDataV2(string name) {
		Name = name;
	}

	// V2 -> V3 마이그레이션 시 사용
	protected SaveDataV2(SaveDataV2 saveData) {
		Name = saveData.Name;
		Gold = saveData.Gold;
	}

	public override string ToString() {
		return $"{Name}의 소지금 : {Gold}G";
	}
}
using UnityEngine;

public abstract class SaveData {
	public abstract int Version { get; }
	
	public abstract SaveData VersionUp();
}



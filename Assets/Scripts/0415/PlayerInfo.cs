using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo {
	public string playerName;
	public int lives;
	public float health;
	public Vector3 position;
	
	public Dictionary<string, int> scores = new Dictionary<string, int>() {
		{"Stage1", 100},
		{"Stage2", 200},
		{"Stage3", 300}
	};
}
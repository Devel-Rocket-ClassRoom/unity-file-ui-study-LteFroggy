using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[Serializable]
public class PlayerState {
	public string playerName;
	public int lives;
	public float health;
	public override string ToString() {
		return $"{playerName} / {lives} / {health}"; 
	}
	
	[JsonConverter(typeof(Vector3Converter))]
	public Vector3 position;
}
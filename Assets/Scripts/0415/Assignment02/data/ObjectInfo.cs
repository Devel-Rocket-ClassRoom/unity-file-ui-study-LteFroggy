using UnityEngine;

[System.Serializable]
public class ObjectInfo {
	public ObjectShape shape;
	public Vector3 pos;
	public Quaternion rot;
	public Vector3 scale;
	public Color color;

	public override string ToString() {
		return $"Pos : {pos} / Rot : {rot} / Scale : {scale} / color : {color}";
	}
}
using Newtonsoft.Json;
using UnityEngine;

public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString() {
        return $"Pos : {pos} / Rot : {rot} / Scale : {scale} / color : {color}";
    }
}

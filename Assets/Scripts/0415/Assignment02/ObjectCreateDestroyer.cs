using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ObjectCreateDestroyer : MonoBehaviour {
	private readonly float _locXMin = -17f;
	private readonly float _locXMax = 17f;
	private readonly float _locZMin = -32f;
	private readonly float _locZMax = 32f;

	[Header("=== 도형 생성, 삭제 버튼 할당 ===")]
	[SerializeField] private Button _createButton;
	[SerializeField] private Button _clearButton;
	
	private void Awake() {
		Initialize();
	}
	
	private void Initialize() {
		_createButton.onClick.AddListener(CreateRandomObject10Times);
		_clearButton.onClick.AddListener(DestoryAllObject);
	}
	
	private void CreateRandomObject10Times() {
		// 10개 랜덤한 도형 정보 생성
		for (int i = 0; i < 10; i++) {
			ObjectShape shape = (ObjectShape)Random.Range(0, Enum.GetValues(typeof(ObjectShape)).Length);
			Vector3 pos = new Vector3(Random.Range(_locXMin, _locXMax), 0f,  Random.Range(_locZMin, _locZMax));
			Quaternion rot = Random.rotation;
			Vector3 scale = new Vector3(Random.Range(0f, 5f),  Random.Range(0f, 5f), Random.Range(0f, 5f));
			Color color = Random.ColorHSV();
			color.a = 1f;
			
			ObjectInfo objInfo = new ObjectInfo() {
				shape = shape,
				pos = pos,
				rot = rot,
				scale = scale,
				color = color
			};
			
			// 도형 생성 함수 호출
			CreateObject(objInfo);
		}
	}
	
	public void CreateObject(ObjectInfo objInfo) {
		// shape에 따른 Instance 생성
		GameObject instance = objInfo.shape switch {
			ObjectShape.Cube => GameObject.CreatePrimitive(PrimitiveType.Cube),
			ObjectShape.Sphere => GameObject.CreatePrimitive(PrimitiveType.Sphere),
			ObjectShape.Cylinder => GameObject.CreatePrimitive(PrimitiveType.Cylinder),
			ObjectShape.Capsule => GameObject.CreatePrimitive(PrimitiveType.Capsule),
			_ => throw new ArgumentOutOfRangeException()
		};
		
		// 태그 및 부모 정한 후, 초기화 함수 호출
		instance.AddComponent<ObjectMeta>();
		instance.tag = Tags.SaveTarget;
		instance.transform.parent = transform;
		instance.GetComponent<ObjectMeta>().Initialize(objInfo);
	}
	
	// 모든 도형 삭제
	public void DestoryAllObject() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag(Tags.SaveTarget);
		
		foreach (var obj in objects) { Destroy(obj); }
	}
}
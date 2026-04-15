using UnityEngine;

public class ObjectMeta : MonoBehaviour {
	private ObjectInfo meta = new ObjectInfo();
	
	// 저장할 정보 직접 반환하도록
	public ObjectInfo SaveInfo => meta;
	
	// 도형 정보가 오면, 저장 후 직접 자신에게 적용
	public void Initialize(ObjectInfo objInfo) {
		meta = objInfo;
		
		transform.position = objInfo.pos;
		transform.rotation = objInfo.rot;
		transform.localScale = objInfo.scale;
		GetComponent<Renderer>().material.color = objInfo.color;
	}
}
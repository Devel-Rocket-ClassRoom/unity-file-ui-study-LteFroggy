using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour {
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			SaveDataManager.Save(0);
		}
		
		if  (Input.GetKeyDown(KeyCode.Alpha1)) {
			if (!SaveDataManager.Load(0)){
				return;
			}
			
			Debug.Log(SaveDataManager.Data);
		}
	}
}

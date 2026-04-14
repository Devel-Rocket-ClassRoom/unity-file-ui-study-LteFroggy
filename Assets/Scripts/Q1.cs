using System.IO;
using UnityEngine;

// Q1: 키보드/마우스 입력으로 파일 생성, 선택, 복사, 삭제를
// 씬의 큐브 오브젝트(FileVisualizor)와 매핑해 시각적으로 다루는 예제.
public class Q1 : MonoBehaviour
{
    private string _directoryPath = "SaveData";
    private string _fileName = "data";
    private int _id = 1;
    private string _ext = ".txt";

    private FileVisualizor _selectedFileVisualizor;
    private FileVisualizor _copiedFileVisualizor;

    private float _distance = 2f;

    private void Update()
    {
        // Space: 새 텍스트 파일을 만들고, 해당 파일을 가리키는 큐브를 생성.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string directoryPath = Path.Combine(Application.persistentDataPath, _directoryPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, _fileName + _id + _ext);
            File.WriteAllText(filePath, $"Hello, World! {_id}번째 파일");
            Debug.Log($"[{_id}] 경로: \"{filePath}\"");
            _id++;

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position += Vector3.right * _distance * _id;
            FileVisualizor fv = go.AddComponent<FileVisualizor>();
            fv.Path = filePath;
        }

        // 마우스 좌클릭: 레이캐스트로 큐브를 선택해 현재 파일 대상으로 지정.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _selectedFileVisualizor = hit.collider.gameObject.GetComponent<FileVisualizor>();
            }
        }

        // Ctrl + C / Ctrl + V: 선택한 파일 참조를 복사하고, 실제 파일 복제본(_COPY)을 생성.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_selectedFileVisualizor == null) return;

                _copiedFileVisualizor = _selectedFileVisualizor;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (_copiedFileVisualizor == null) return;

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = _copiedFileVisualizor.transform.position + Vector3.up * _distance;
                FileVisualizor fv = go.AddComponent<FileVisualizor>();
                fv.Path = _copiedFileVisualizor.Path.Replace(_ext, $"_COPY{_ext}");
                File.Copy(_copiedFileVisualizor.Path, fv.Path);
                _copiedFileVisualizor = fv;
            }
        }

        // Delete: 현재 선택된 파일을 디스크와 씬에서 모두 삭제.
        if (Input.GetKey(KeyCode.Delete))
        {
            if (_selectedFileVisualizor == null) return;

            File.Delete(_selectedFileVisualizor.Path);
            Destroy(_selectedFileVisualizor.gameObject);
        }
    }
}

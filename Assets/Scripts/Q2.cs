using System.IO;
using UnityEngine;

// Q2: 텍스트 파일을 만든 뒤 XOR(단일 바이트 키)로 암호화/복호화를 수행하고,
// 복호화 결과가 원본과 일치하는지 확인하는 예제.
public class Q2 : MonoBehaviour
{
    private string _directoryPath = "Data";
    private string _fileName = "secret";
    private string _ext = ".txt";
    private byte _key = 0xAB;

    private void Update()
    {
        // Space: 원본 텍스트 파일(secret.txt) 생성.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string dirPath = Path.Combine(Application.persistentDataPath, _directoryPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = Path.Combine(dirPath, _fileName + _ext);
            string content = "Hello, Unity World!";

            File.WriteAllText(filePath, content);
            Debug.Log($"원본: {content}\n경로: \"{filePath}\"");
        }

        // E: 원본 파일을 바이트 단위로 읽어 XOR 키를 적용해 encrypt.dat 생성.
        if (Input.GetKeyDown(KeyCode.E))
        {
            string dirPath = Path.Combine(Application.persistentDataPath, _directoryPath);
            string originPath = Path.Combine(dirPath, _fileName + _ext);
            if (File.Exists(originPath))
            {
                string encryptPath = Path.Combine(dirPath, "encrypt.dat");

                using (FileStream of = File.OpenRead(originPath))
                using (FileStream ef = File.Create(encryptPath))
                {
                    int b = of.ReadByte();
                    while (b != -1)
                    {
                        ef.WriteByte((byte)(b ^ _key));
                        b = of.ReadByte();
                    }
                    Debug.Log($"암호화 완료 (파일 크기: {ef.Length} bytes)");
                }
            }
        }

        // D: encrypt.dat를 같은 XOR 키로 다시 처리해 decrypted.txt를 만들고 원본과 비교.
        if (Input.GetKeyDown(KeyCode.D))
        {
            string dirPath = Path.Combine(Application.persistentDataPath, _directoryPath);
            string originPath = Path.Combine(dirPath, _fileName + _ext);
            string encryptPath = Path.Combine(dirPath, "encrypt.dat");
            string decryptPath = Path.Combine(dirPath, "decrypted" + _ext);
            if (File.Exists(originPath) && File.Exists(encryptPath))
            {
                using (FileStream ef = File.OpenRead(encryptPath))
                using (FileStream df = File.Create(decryptPath))
                {
                    int b = ef.ReadByte();
                    while (b != -1)
                    {
                        df.WriteByte((byte)(b ^ _key));
                        b = ef.ReadByte();
                    }
                    Debug.Log($"복호화 완료");
                    Debug.Log($"복호화 결과: {df.Length}");
                }

                string origin = File.ReadAllText(originPath);
                string decrypted = File.ReadAllText(decryptPath);
                Debug.Log($"원본과 일치: {origin == decrypted}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Q3: key=value 형식의 설정 파일(.cfg)을 생성/로드해 Dictionary에 보관하고,
// 런타임에서 설정값을 출력하거나 일부 값을 수정 후 파일에 다시 저장하는 예제.
public class Q3 : MonoBehaviour
{
    private string _directoryPath = "Configs";
    private string _fileName = "settings";
    private string _ext = ".cfg";

    private Dictionary<string, string> _configs;

    private void Start()
    {
        // 최초 실행 시 Configs/settings.cfg가 없으면 기본 설정 파일을 생성.
        string dirPath = Path.Combine(Application.persistentDataPath, _directoryPath);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        string cfgPath = Path.Combine(dirPath, _fileName + _ext);
        if (!File.Exists(cfgPath))
        {
            using (FileStream fs = File.Create(cfgPath))
            using (StreamWriter sw = new(fs))
            {
                sw.WriteLine("master_volume=80");
                sw.WriteLine("bgm_volume=70");
                sw.WriteLine("sfx_volume=90");
                sw.WriteLine("language=kr");
                sw.WriteLine("show_damage=true");
            }
            Debug.Log($"세팅 파일 생성: {cfgPath}");
        }

        // 설정 파일을 줄 단위로 읽어 key=value를 Dictionary로 파싱.
        _configs = new();
        using (StreamReader sr = File.OpenText(cfgPath))
        {
            while (sr.Peek() > -1)
            {
                string line = sr.ReadLine();
                string[] keyValue = line.Split('=');
                _configs.Add(keyValue[0], keyValue[1]);
            }
        }
        Debug.Log($"세팅 로드 완료: {cfgPath}");
    }

    private void Update()
    {
        // Space: 현재 메모리에 로드된 설정값을 로그로 출력.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var sb = new StringBuilder();
            sb.AppendLine("--- 파일 내용 ---");
            foreach (var config in _configs)
            {
                sb.AppendLine($"{config.Key}={config.Value}");
            }
            Debug.Log(sb.ToString());
        }

        // S: 일부 설정을 변경하고, 전체 설정을 파일에 다시 기록(덮어쓰기).
        if (Input.GetKeyDown(KeyCode.S))
        {
            var sb = new StringBuilder();
            sb.AppendLine("--- 변경 전 ---");
            sb.AppendLine($"bgm_volume = {_configs["bgm_volume"]}");
            sb.AppendLine($"language = {_configs["language"]}");
            sb.AppendLine("--- 변경 후 저장 ---");

            _configs["bgm_volume"] = "50";
            _configs["language"] = "en";

            string cfgPath = Path.Combine(Application.persistentDataPath, _directoryPath, _fileName + _ext);
            using (FileStream fs = File.Create(cfgPath))
            using (StreamWriter sw = new(fs))
            {
                sw.WriteLine($"master_volume={_configs["master_volume"]}");
                sw.WriteLine($"bgm_volume={_configs["bgm_volume"]}");
                sw.WriteLine($"sfx_volume={_configs["sfx_volume"]}");
                sw.WriteLine($"language={_configs["language"]}");
                sw.WriteLine($"show_damage={_configs["show_damage"]}");
            }

            sb.AppendLine($"bgm_volume = {_configs["bgm_volume"]}");
            sb.AppendLine($"language = {_configs["language"]}");

            Debug.Log(sb.ToString());
        }
    }
}

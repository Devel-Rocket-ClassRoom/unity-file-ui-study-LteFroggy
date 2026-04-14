using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    public static readonly string Unknown = "키 없음";

    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> _table = new();

    public override void Load(string fileName)
    {
        _table.Clear();

        var path = string.Format(FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        var dataList = LoadCSV<Data>(textAsset.text);
        foreach (var data in dataList)
        {
            if (!_table.ContainsKey(data.Id))
            {
                _table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogError($"키 중복: {data.Id}");
            }
        }
    }

    public string Get(string key)
    {
        if (!_table.ContainsKey(key))
        {
            return Unknown;
        }

        return _table[key];
    }
}
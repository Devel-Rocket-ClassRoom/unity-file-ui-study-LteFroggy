using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> _tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    
    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);

#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DataTableIds.StringTableIds[(int)lang]);
    }
#endif


    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DatableIds.String);
        _tables.Add(DatableIds.String, stringTable);
#else
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            _tables.Add(id, stringTable);
        }
#endif
        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        _tables.Add(DataTableIds.Item, itemTable);
    }

    public static void ChangeLanguage(Languages lang)
    {
        var newId = DataTableIds.StringTableIds[(int)lang];
        if (_tables.ContainsKey(newId)) return;

        string oldId = string.Empty;
        foreach (var id in DataTableIds.StringTableIds)
        {
            if (_tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }
        var stringTable = _tables[oldId];
        stringTable.Load(newId);
        _tables.Remove(oldId);
        _tables.Add(newId, stringTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!_tables.ContainsKey(id))
        {
            Init();
        }

        return _tables[id] as T;
    }
}
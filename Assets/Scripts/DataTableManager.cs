using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> _tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    
    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);
    
    public static CharacterTable CharacterTable => Get<CharacterTable>(DataTableIds.Character);

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

    private static void Init() {
        foreach (var id in DataTableIds.StringTableIds) {
            var stringTable = new StringTable();
            stringTable.Load(id);
            _tables.Add(id, stringTable);
        }
        
        // 아이템 테이블 추가
        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        _tables.Add(DataTableIds.Item, itemTable);
        
        // 캐릭터 테이블 추가
        CharacterTable characterTable = new CharacterTable();
        characterTable.Load(DataTableIds.Character);
        _tables.Add(DataTableIds.Character, characterTable);
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
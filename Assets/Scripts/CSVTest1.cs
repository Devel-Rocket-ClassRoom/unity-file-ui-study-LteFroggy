using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using UnityEngine;

public class CSVData
{
    [Name("Id")] public string Id { get; set; }
    [Name("String")] public string String { get; set; }
}

public class CSVTest1 : MonoBehaviour
{
    [SerializeField] private TextAsset _textAsset;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var textAsset = Resources.Load<TextAsset>("DataTables/StringTableKr");
            string csv = textAsset.text;

            using (StringReader reader = new(csv))
            using (CsvReader csvReader = new(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();
                foreach (var record in records)
                {
                    Debug.Log($"{record.Id}: {record.String}");
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using UnityEngine;

public abstract class DataTable
{
    public static readonly string FormatPath = "DataTables/{0}";

    public abstract void Load(string fileName);

    public static List<T> LoadCSV<T>(string csvText)
    {
        using (StringReader reader = new(csvText))
        using (CsvReader csvReader = new(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<T>();

            return records.ToList();
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using SQLite;

public class DatabaseSeeder
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseSeeder(SQLiteAsyncConnection db)
    {
        _database = db;
    }

    public async Task SeedFromJson(string jsonFilePath)
    {
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found: {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        var commands = JsonConvert.DeserializeObject<List<CommandsTable>>(json);

        if (commands != null && commands.Count > 0)
        {
            await _database.InsertAllAsync(commands);
            Debug.Log("Database seeded from JSON file.");
        }
    }
}

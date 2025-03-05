using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SQLite;

public class DatabaseManager
{
    private readonly SQLiteAsyncConnection _database;
    private const int CURRENT_DB_VERSION = 2; // Обновляй при изменениях

    public DatabaseManager(string dbPath)
    {
        if (IsMigrationNeeded(dbPath))
        {
            DeleteDatabase(dbPath);
        }

        _database = new SQLiteAsyncConnection(dbPath);
        InitializeDatabase();
    }

    private bool IsMigrationNeeded(string dbPath)
    {
        int savedVersion = PlayerPrefs.GetInt("DB_VERSION", 0);
        return savedVersion < CURRENT_DB_VERSION || !File.Exists(dbPath);
    }

    private void DeleteDatabase(string dbPath)
    {
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
            Debug.Log("Old database deleted for migration.");
        }
    }

    private async void InitializeDatabase()
    {
        try
        {
            await _database.CreateTableAsync<CommandsTable>();
            await _database.CreateTableAsync<PackagesTable>();
            await _database.CreateTableAsync<RecordsTable>();

            await SeedDatabase(); // Заполняем базу начальными данными
            PlayerPrefs.SetInt("DB_VERSION", CURRENT_DB_VERSION);
            PlayerPrefs.Save();

            Debug.Log("Database initialized successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Database initialization failed: {ex.Message}");
        }
    }

    private async Task SeedDatabase()
    {
        var count = await _database.Table<CommandsTable>().CountAsync();
        if (count == 0)
        {
            string jsonPath = Path.Combine(Application.streamingAssetsPath, "commands.json");
            var seeder = new DatabaseSeeder(_database);
            await seeder.SeedFromJson(jsonPath);
        }
    }

    public SQLiteAsyncConnection GetConnection() => _database;

}
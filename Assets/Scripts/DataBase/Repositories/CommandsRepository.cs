using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using SQLite;

public class CommandsRepository : MonoBehaviour
{
    private SQLiteAsyncConnection _db;
    
    public void Awake()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "game.db");
        _db = new DatabaseManager(dbPath).GetConnection();
    }

    public async Task<int> AddCommandAsync(CommandsTable command)
    {
        try
        {
            return await _db.InsertAsync(command);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error inserting command: {ex.Message}");
            return -1;
        }
    }

    public async Task<List<CommandsTable>> GetCommandsAsync(int[] difficulties, int offset = 0, int limit = 10)
    {
        try
        {
            return await _db.Table<CommandsTable>()
                .Where(cmd => difficulties.Contains(cmd.Difficulty))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving commands: {ex.Message}");
            return new List<CommandsTable>();
        }
    }

    public async Task<List<CommandsTable>> GetAllCommandsAsync()
    {
        try
        {
            return await _db.Table<CommandsTable>().ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving commands: {ex.Message}");
            return new List<CommandsTable>();
        }
    }
}

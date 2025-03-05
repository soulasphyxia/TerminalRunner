using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IRecordsRepository
{
    Task<List<RecordsTable>> GetAllRecordsAsync();
    Task<RecordsTable> GetRecordByIdAsync(int id);
    Task AddRecordAsync(RecordsTable record);
    Task UpdateRecordAsync(RecordsTable record);
    Task DeleteRecordAsync(int id);
}

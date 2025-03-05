using SQLite;

public class RecordsTable
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [NotNull]
    public int Score { get; set; }
    [NotNull]
    public float CorrectSymbolsPercentage { get; set; }
    [NotNull]
    public int LivesLost { get; set; }
}
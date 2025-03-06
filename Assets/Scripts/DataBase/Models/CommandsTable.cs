using SQLite;

public class CommandsTable
{

    [PrimaryKey, AutoIncrement]
    public ulong ID { get; set; }
    [NotNull, Unique]
    public string Name { get; set; }
    [NotNull]
    public string Description { get; set; }
    [NotNull]
    public int Difficulty { get; set; }
    [NotNull]
    public string Animation { get; set; }
    [NotNull]
    public bool isUsersMade {get; set; }
}
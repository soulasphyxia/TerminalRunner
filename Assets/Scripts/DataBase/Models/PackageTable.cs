using SQLite;

public class PackagesTable
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [NotNull]
    public string CommandName { get; set; }
    [NotNull]
    public string PackageName { get; set; }
}

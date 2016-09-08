using SQLite;

namespace Dustbuster.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(string db);
        SQLiteConnection GetConnection();
    }
}

using SQLite;

namespace Dustbuster
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(string db);
        //SQLiteConnection GetConnection();
    }
}

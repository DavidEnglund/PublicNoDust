using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using SQLite;
using System.Runtime.CompilerServices;
using Dustbuster.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]

namespace Dustbuster.iOS
{
    public class SQLiteIOS : ISQLite
    {

        public SQLiteConnection GetConnection(string dbName)
        {
            //should make db filenames a string resource or something
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string dbFileName = dbName;

            var dbPath = Path.Combine(documentsPath, dbFileName);

            if (dbName == "ProductDB.db3")
            {
                if (!File.Exists(dbPath))
                {
                    File.Copy("ProductDB.db3", dbPath);
                }
            }
            else if (dbName == "JobDB.db3")
            {
                //if (!File.Exists(dbPath)) File.Create(dbPath);
                if (!File.Exists(dbPath))
                {
                    File.Copy("JobDB.db3", dbPath);
                }
            }

            //to create a new db everytime use this - for debugging only 
            //File.Create(dbPath);

            return new SQLiteConnection(dbPath);
        }
    }
}
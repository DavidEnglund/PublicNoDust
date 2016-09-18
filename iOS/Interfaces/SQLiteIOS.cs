using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Dustbuster.Interfaces;
using SQLite;
using System.Runtime.CompilerServices;
using Dustbuster.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]

namespace Dustbuster.iOS
{
    public class SQLiteIOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            //see override below to accomodate multiple databases

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder

            var productDBfilename = "ProductDB.db3";
            var jobDBfilename = "JobDB.db3";

            var productDBpath = Path.Combine(documentsPath, productDBfilename);
            var jobDBpath = Path.Combine(documentsPath, jobDBfilename);

            //to create a new db everytime use this - for debugging only 
            File.Create(productDBpath);
            File.Create(jobDBpath);


            var conn = new SQLiteConnection(productDBpath);

            return conn;
        }

        public SQLiteConnection GetConnection(string dbName)
        {
            //should make db filenames a string resource or something
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder


            string dbFileName = dbName;

            var dbPath = Path.Combine(documentsPath, dbFileName);

            //to create a new db everytime use this - for debugging only 
            File.Create(dbPath);

            return new SQLiteConnection(dbPath);
        }
    }
}
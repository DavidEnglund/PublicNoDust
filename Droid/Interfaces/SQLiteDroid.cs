using System;
using System.IO;
using Xamarin.Forms;
using SQLite;
using Dustbuster.Droid;

[assembly: Dependency(typeof(SQLiteDroid))]

namespace Dustbuster.Droid
{
    public class SQLiteDroid : ISQLite
    {

        public SQLiteConnection GetConnection(string dbName)
        {
            //should make db filenames a string resource or something
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); //Documents folder
            //string documentsPath = global::Android.OS.Environment.ExternalStorageDirectory.Path; //public folder
            string dbFileName = dbName;
            var dbPath = Path.Combine(documentsPath, dbFileName);
            
            if (dbName == "ProductDB.db3")
            {
                if (!File.Exists(dbPath))
                {
                    var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.ProductDB);  // RESOURCE NAME ###

                    // create a write stream
                    FileStream writeStream = new FileStream(dbPath, FileMode.OpenOrCreate, FileAccess.Write);
                    // write to the stream
                    ReadWriteStream(s, writeStream);
                }
            }
            else if (dbName == "JobDB.db3")
            {
                //if (!File.Exists(dbPath)) File.Create(dbPath);
                if (!File.Exists(dbPath))
                {
                    var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.JobDB);  // RESOURCE NAME ###

                    // create a write stream
                    FileStream writeStream = new FileStream(dbPath, FileMode.OpenOrCreate, FileAccess.Write);
                    // write to the stream
                    ReadWriteStream(s, writeStream);
                }
            }


            //to create a new db everytime use this - for debugging only 
            //File.Create(dbPath);

            return new SQLiteConnection(dbPath);
        }

        /// <summary>
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// </summary>
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}
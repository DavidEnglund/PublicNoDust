﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Dustbuster
{
    public class DBMetaData
    {
        [PrimaryKey]
        public int key { get; set; } //this must be 1
        public int DBVersion { get; set; }

        public DBMetaData()
        {

        }

        public DBMetaData(int i)
        {
            key = 1;
            DBVersion = i;
        }
    }
}

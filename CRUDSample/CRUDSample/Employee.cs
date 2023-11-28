using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDSample
{
    public class Employee
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Location { get; set; }
        public string Participant { get; set; }
        public string Notes { get; set; }
    }
}

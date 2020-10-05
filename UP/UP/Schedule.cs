using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP {
    public class Schedule {
        public string Discipline { get; set; }
        public int Number { get; set; }
        public int Day { get; set; }
        public int Group { get; set; }

        public Schedule(OleDbDataReader reader) {
            Discipline = reader.GetString(0);
            Number = reader.GetInt32(1);
            Day = reader.GetInt32(2);
            Group = reader.GetInt32(3);
        }
    }
}

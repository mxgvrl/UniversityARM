using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP {
    public class Disciplines {
        public int Id { get; set; }
        public string Discipline { get; set; }
        public string Group { get; set; }
        public string Teacher { get; set; }

        public Disciplines(OleDbDataReader reader) {
            Id = reader.GetInt32(0);
            Discipline = reader.GetString(1);
            Group = reader.GetString(2);
            Teacher = reader.GetString(3);
        }

        public override string ToString() =>
            $"Название - {Discipline}, Группа - {Group}, Преподаватель - {Teacher}";
    }
}

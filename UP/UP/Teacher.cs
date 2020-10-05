using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP {
    public class Teacher {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Xp { get; set; }

        public Teacher(OleDbDataReader reader) {
            Id = reader.GetInt32(0);
            Name = reader.GetString(1);
            Xp = reader.GetInt32(2);
        }

        public override string ToString() =>
            $"id - {Id}, ФИО - {Name}, Стаж работы - {Xp}";
    }
}

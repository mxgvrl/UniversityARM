using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP {
    public class Group {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Speciality { get; set; }

        public Group(OleDbDataReader reader) {
            Id = reader.GetInt32(0);
            Title = reader.GetString(1);
            Year = reader.GetInt32(2);
            Speciality = reader.GetString(3);
        }

        public override string ToString() =>
            $"id - {Id}, Название - {Title}, Текущий курс - {Year}, Специальность - {Speciality}";
    }
}

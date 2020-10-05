using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP {
    public class Student {
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public int Group { get; set; }

        public Student(int id, string full_Name, int group) {
            Id = id;
            Full_Name = full_Name;
            Group = group;
        }

        public Student(OleDbDataReader reader) {
            Id = reader.GetInt32(0);
            Full_Name = reader.GetString(1);
            Group = reader.GetInt32(2);
        }

        public override string ToString() => $"id - {Id}, ФИО - {Full_Name}, id Группы - {Group}";
    }
}

using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP {
    public partial class Form1 : MaterialForm {
        public Form1() {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber900, Primary.Grey900, Primary.Grey900, Accent.DeepOrange700, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e) {
            updateStudents();
            updateSpeciality();
            updateGroups();
            updateTeachers();
            updateDisciplines();
        }

        private void button1_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "INSERT INTO students VALUES (@id, @full_name, @group)");
                    command.Parameters.AddWithValue("id", int.Parse(student_id.Text));
                    command.Parameters.AddWithValue("full_name", student_name.Text);
                    command.Parameters.AddWithValue("group", int.Parse(student_group_id.Text));
                    command.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateStudents();
        }

        private void CreateConnection(IDbConnection dbConnection, string connectString, Action<IDbConnection> action) {
            dbConnection.ConnectionString = connectString;
            try {
                dbConnection.Open();
                action?.Invoke(dbConnection);
            }
            catch (Exception e) {

                throw e;
            }
            finally {
                dbConnection.Close();
            }


        }

        private void updateStudents(int groudId = 0) {
            listBox1.Items.Clear();
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select * from students");

                OleDbDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()) {
                    var student = new Student(reader);
                    if (groudId == 0 || student.Group == groudId) {
                        listBox1.Items.Add(student.ToString());
                    }
                }
                    

            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }
        
        private OleDbConnection connect = new OleDbConnection();

        private static OleDbCommand CreateCommand(IDbConnection x, string text) {
            OleDbCommand dbCommand = new OleDbCommand();

            dbCommand.CommandText = text;
            if (!(x is OleDbConnection))
                throw new ArgumentException();
            dbCommand.Connection = x as OleDbConnection;
            return dbCommand;
        }

        private void button2_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "DELETE FROM students WHERE id = @id");
                    command.Parameters.AddWithValue("id", int.Parse(student_id.Text));
                    command.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateStudents();
        }

        private void button3_Click(object sender, EventArgs e) {
            updateStudents(int.Parse(student_group_id.Text));
        }

        private void button4_Click(object sender, EventArgs e) {
            updateStudents(0);
        }

        private void tabPage2_Click(object sender, EventArgs e) {

        }

        private void button8_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "INSERT INTO speciality VALUES (@id, @title, @years)");
                    command.Parameters.AddWithValue("id", int.Parse(speciality_id.Text));
                    command.Parameters.AddWithValue("title", speciality_title.Text);
                    command.Parameters.AddWithValue("years", int.Parse(tab.Text));
                    command.ExecuteNonQuery();
                }
                catch(Exception exception) {
                    Console.WriteLine(exception);
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateSpeciality();
        }

        private void button7_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "DELETE FROM speciality WHERE id = @id");
                    command.Parameters.AddWithValue("id", int.Parse(speciality_id.Text));
                    command.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateSpeciality();
        }

        private void updateSpeciality() {
            listBox2.Items.Clear();
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select * from speciality");
                OleDbDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()) {
                    var speciality = new Speciality(reader);
                    listBox2.Items.Add(speciality.ToString());
                }
            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }

        private void updateGroups() {
            listBox3.Items.Clear();
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select groups.id, groups.title, groups.study_year, speciality.title from groups inner join speciality on groups.speciality=speciality.id;");
                OleDbDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()) {
                    var group = new Group(reader);
                    listBox3.Items.Add(group.ToString());
                }
            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }

        private void button6_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "INSERT INTO groups VALUES (@id, @title, @years, @speciality)");
                    command.Parameters.AddWithValue("id", int.Parse(groups_id.Text));
                    command.Parameters.AddWithValue("title", groups_title.Text);
                    command.Parameters.AddWithValue("years", int.Parse(groups_year.Text));
                    command.Parameters.AddWithValue("speciality", int.Parse(group_speciality_id.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception exception) {
                    Console.WriteLine(exception);
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateGroups();
        }

        private void button5_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "DELETE FROM groups WHERE id = @id");
                    command.Parameters.AddWithValue("id", int.Parse(groups_id.Text));
                    command.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateGroups();
        }

        private void updateTeachers() {
            listBox4.Items.Clear();
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select * from teachers");
                OleDbDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()) {
                    var teacher = new Teacher(reader);
                    listBox4.Items.Add(teacher.ToString());
                }
            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }
        private void button10_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "INSERT INTO teachers VALUES (@id, @full_name, @xp)");
                    command.Parameters.AddWithValue("id", int.Parse(teacher_id.Text));
                    command.Parameters.AddWithValue("full_name", teacher_name.Text);
                    command.Parameters.AddWithValue("xp", int.Parse(teacher_xp.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception exception) {
                    Console.WriteLine(exception);
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateTeachers();
        }

        private void button9_Click(object sender, EventArgs e) {
            CreateConnection(connect, Properties.Settings.Default.db, (connection) => {
                try {
                    var command = CreateCommand(connection, "DELETE FROM teachers WHERE id = @id");
                    command.Parameters.AddWithValue("id", int.Parse(teacher_id.Text));
                    command.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Неверный формат ввода!");
                }
            });
            updateTeachers();
        }

        private void button11_Click(object sender, EventArgs e) {
            var list = new List<DataGridView> {
                monday, tuesday, wednesday, thursday, friday
            };
            foreach (var item in list) {
                item.Rows.Clear();
            }
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select disciplines.discipline_name, schedule.pair_number, schedule.day, disciplines.group from schedule inner join disciplines on schedule.discipline=disciplines.id");
                OleDbDataReader reader = dbCommand.ExecuteReader();
                var schedule = new List<Schedule>();
                while (reader.Read()) {
                    schedule.Add(new Schedule(reader));
                }
                var group = int.Parse(schedule_group.Text);
                schedule = schedule.Where(it => it.Group == group).ToList();
                foreach (var item in schedule) {
                    list[item.Day - 1].Rows.Add(item.Discipline);
                }
            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }

        private void updateDisciplines() {
            listBox5.Items.Clear();
            Action<IDbConnection> action = (x) => {
                OleDbCommand dbCommand = CreateCommand(x, "select disciplines.id, disciplines.discipline_name, groups.title, teachers.full_name from (disciplines inner join groups on disciplines.group=groups.id) inner join teachers on disciplines.teacher=teachers.id");
                OleDbDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()) {
                    listBox5.Items.Add(new Disciplines(reader).ToString());
                }
            };
            CreateConnection(connect, Properties.Settings.Default.db, action);
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }
    }
}

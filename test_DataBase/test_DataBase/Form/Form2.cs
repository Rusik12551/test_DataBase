using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test_DataBase
{
    public partial class Form2 : Form
    {
        enum RoWState // Состояние данных
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        DataBase database = new DataBase();
        int selectedRow;

        public Form2()
        {
            InitializeComponent();
        }

        private void CreateColumns()// Вывод данных из БД
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("type_of", "Тип Товаров");
            dataGridView1.Columns.Add("count_of", "Количество");
            dataGridView1.Columns.Add("postavka", "Поставщик");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumns2()// Вывод данных из БД
        {
            dataGridView2.Columns.Add("id", "id");
            dataGridView2.Columns.Add("surname", "Фомилия");
            dataGridView2.Columns.Add("name", "Имя");
            dataGridView2.Columns.Add("middlename", "Отчество");
            dataGridView2.Columns.Add("address", "Адрес");
            dataGridView2.Columns.Add("purchase", "Покупка");
            dataGridView2.Columns.Add("amount", "Количество");
            dataGridView2.Columns.Add("price", "Цена");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)// Передача состояний данных
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4));
        }

        private void ReadSingleRow2(DataGridView dgw, IDataRecord record)// Передача состояний данных
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetInt32(6), record.GetInt32(7));
        }

        private void RefreshDataGrid(DataGridView dgw)// Выводит данные в таблицу
        {
            dgw.Rows.Clear();
            string queryString = "select * from test_db";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void RefreshDataGrid2(DataGridView dgw)// Выводит данные в таблицу
        {
            dgw.Rows.Clear();
            string queryString = "select * from Buyers";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow2(dgw, reader);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)// Выводит данные в таблицу
        {
            CreateColumns();
            CreateColumns2();
            RefreshDataGrid2(dataGridView2);
            RefreshDataGrid(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)// Переход на другую форму
        {
            Form4 frm_sing = new Form4();
            frm_sing.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)// Переход на другую форму
        {
            Form5 frm_sing = new Form5();
            frm_sing.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)// Переход на другую форму
        {
            Form4 frm_sing = new Form4();
            frm_sing.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)// Переход на другую форму
        {
            Form7 frm_sing = new Form7();
            frm_sing.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)// Переход на другую форму
        {
            Form1 frm_sing = new Form1();
            frm_sing.Show();
            this.Hide();
        }
    }
}

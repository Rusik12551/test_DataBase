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
    public partial class Form4 : Form
    {
        enum RowState// Состояние данных
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        public Form4()
        {
            InitializeComponent();
        }

        DataBase database = new DataBase();
        int selectedRow;

        private void CreateColumns()// Вывод данных из БД
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("type_of", "Тип Товаров");
            dataGridView1.Columns.Add("count_of", "Количество");
            dataGridView1.Columns.Add("postavka", "Поставщик");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)// Передача состояний данных
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), RowState.ModifiedNew);
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

        private void button1_Click(object sender, EventArgs e)// Переход на другую форму
        {
            Form6 frm_sing = new Form6();
            frm_sing.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)// Переход на другую форму
        {
            Form2 frm_sing = new Form2();
            frm_sing.Show();
            this.Hide();
        }

        private void Form4_Load_1(object sender, EventArgs e)// Выводит данные в таблицу
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void Search(DataGridView dgw) // Поиск
        {
            dgw.Rows.Clear();
            string searchString = $"select * from test_db where concat (id, type_of, count_of, postavka, price) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(searchString, database.getConnection());
            database.openConnection();
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)// Поиск
        {
            Search(dataGridView1);
        }

        private void deletRow()// Удаление
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {

                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;

        }

        private new void Update() // Обновление
        {
            database.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;
                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from test_db where id = {id}";
                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var type = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var count = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var postavka = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridView1.Rows[index].Cells[4].Value.ToString();

                    var changeQuery = $"update test_db set type_of ='{type}', count_of = '{count}', postavka = '{postavka}', price = '{price}' where id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }

            }
            database.closeConnection();
        }

        private void button3_Click_1(object sender, EventArgs e)// Удаление
        {
            deletRow();
        }

        private void button5_Click(object sender, EventArgs e)// Сохранение
        {
            Update();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)// Заносит данные в текст бокс для редактирования
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
            }
        }

        private void Change()// Изменение информации
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox2.Text;
            var type = textBox3.Text;
            var count = textBox4.Text;
            var postavka = textBox5.Text;
            int price;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox6.Text, out price))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(id, type, count, postavka, price);

                    dataGridView1.Rows[selectedRowIndex].Cells[5].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Цена должна иметь числовой формат");
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)// Изменение информации
        {
            Change();
        }

        private void button4_Click(object sender, EventArgs e)// Обновление таблицы
        {
            RefreshDataGrid(dataGridView1);
        }
    }
}

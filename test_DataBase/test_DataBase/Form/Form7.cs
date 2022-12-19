using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_DataBase
{
    public partial class Form7 : Form
    {
        enum RowState// Состояние данных
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        public Form7()
        {
            InitializeComponent();
        }

        DataBase database = new DataBase();
        int selectedRow;

        private void CreateColumns()// Вывод данных из БД
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("surname", "Фомилия");
            dataGridView1.Columns.Add("name", "Имя");
            dataGridView1.Columns.Add("middlename", "Отчество");
            dataGridView1.Columns.Add("address", "Адрес");
            dataGridView1.Columns.Add("purchase", "Покупка");
            dataGridView1.Columns.Add("amount", "Количество");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)// Передача состояний данных
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetInt32(6), record.GetInt32(7), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)// Выводит данные в таблицу
        {
            dgw.Rows.Clear();
            string queryString = "select * from Buyers";

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
            Form5 frm_sing = new Form5();
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

        private void Search(DataGridView dgw)// Поиск
        {
            dgw.Rows.Clear();
            string searchString = $"select * from Buyers where concat (id, surname, name, middlename, address, purchase, amount, price) like '%" + textBox1.Text + "%'";
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

                dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
                return;

            }
            dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;

        }

        private new void Update()// Обновление
        {
            database.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[8].Value;
                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Buyers where id = {id}";
                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var surname = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var middlename = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var address = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var purchase = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var amount = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var price = dataGridView1.Rows[index].Cells[7].Value.ToString();

                    var changeQuery = $"update Buyers set surname ='{surname}', name = '{name}', middlename = '{middlename}', address = '{address}', purchase = '{purchase}', amount = '{amount}', price = '{price}' where id = '{id}'";

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
                textBox7.Text = row.Cells[5].Value.ToString();
                textBox8.Text = row.Cells[6].Value.ToString();
                textBox9.Text = row.Cells[7].Value.ToString();
            }
        }

        private void Change()// Изменение информации
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox2.Text;
            var surname = textBox3.Text;
            var name = textBox4.Text;
            var middlename = textBox5.Text;
            var address = textBox6.Text;
            var purchase = textBox7.Text;
            var amount = textBox8.Text;
            int price;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox9.Text, out price))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(id, surname, name, middlename, address, purchase, amount, price);

                    dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState.Modified;

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

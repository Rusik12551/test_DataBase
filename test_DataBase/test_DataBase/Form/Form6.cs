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
    public partial class Form6 : Form
    {
        DataBase database = new DataBase();

        public Form6()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)// Вывод данных из БД
        {
            database.openConnection();
            int id = Convert.ToInt32(textBox1.Text);
            var type = textBox3.Text;
            var count = textBox4.Text;
            var postav = textBox5.Text;
            int price;

            if (int.TryParse(textBox6.Text, out price))// Запрос на заполение БД
            {
                var addQuery = $"insert into test_db (id, type_of, count_of, postavka, price) values ('{id}','{type}','{count}','{postav}','{price}')";
                var command = new SqlCommand(addQuery, database.getConnection());
                command.ExecuteNonQuery();
                MessageBox.Show("Запесь успешно создана!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Цена должна иметь числововое значение!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)// Переход на другую форму

        {
            Form4 frm_sing = new Form4();
            frm_sing.Show();
            this.Hide();
        }
    }
}

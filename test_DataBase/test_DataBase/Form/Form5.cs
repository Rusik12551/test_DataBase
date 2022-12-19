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
    public partial class Form5 : Form
    {
        DataBase database = new DataBase();

        public Form5()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)// Вывод данных из БД
        {
            database.openConnection();
            int id = Convert.ToInt32(textBox1.Text);
            var surname = textBox2.Text;
            var name = textBox3.Text;
            var middlename = textBox4.Text;
            var address = textBox5.Text;
            var purchase = textBox6.Text;
            var amount = textBox7.Text;
            int price;

            if (int.TryParse(textBox8.Text, out price)) // Запрос на заполение БД
            {
                var addQuery = $"insert into Buyers (id, surname, name, middlename, address, purchase, amount, price) values ('{id}','{surname}','{name}','{middlename}','{address}' ,'{purchase}','{amount}','{price}')";
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
            Form7 frm_sing = new Form7();
            frm_sing.Show();
            this.Hide();
        }
    }
}
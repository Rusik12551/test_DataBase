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
using System.Text.RegularExpressions;


namespace test_DataBase
{
    public partial class Form1 : Form
    {
        DataBase database = new DataBase();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)// Скрытие прароля
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)// Переход на другую форму, после входа в аккаунт
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Проверка регистрации
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2 frm1 = new Form2();
                this.Hide();
                frm1.ShowDialog();

            }
            else
                MessageBox.Show("Токого аккаунта не существует!", "Аккаунт не существует!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e) // переход на другую форму
        {
            Form3 frm_sing = new Form3();
            frm_sing.Show();
            this.Hide();
        }


    }
}

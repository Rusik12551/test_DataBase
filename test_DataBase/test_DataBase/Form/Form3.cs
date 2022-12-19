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
    public partial class Form3 : Form
    {

        DataBase database = new DataBase();

        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form3_Load(object sender, EventArgs e) // Скрытие прароля
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e) // регистрация пользователя
        {
            int id = Convert.ToInt32(textBox3.Text);
            var login = textBox1.Text;
            var password = textBox2.Text;
            //Запрос к БД
            string querysrting = $"insert into register(id_user,login_user, password_user) values('{id}','{login}','{password}')";
            SqlCommand command = new SqlCommand(querysrting, database.getConnection());

            database.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                Form1 frm_login = new Form1();
                this.Hide();
                frm_login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            database.openConnection();
        }

        private Boolean checkuser()// регистрация пользователя
        {
            int id = Convert.ToInt32(textBox3.Text);
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where id_user = '{id}' login_user = '{loginUser}' and password_user = '{passUser}";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)// Переход на другую форму
        {
            Form1 frm_sing = new Form1();
            frm_sing.Show();
            this.Hide();
        }
    }
}

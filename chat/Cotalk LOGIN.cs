using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;

namespace chat
{
    public partial class login_form : Form
    {

        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";


        public login_form()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }

        public event EventHandler<int> login_mem_num;

        private void ID_textbox_Click(object sender, EventArgs e)
        {
            if (ID_textbox.Text == "ID")
            {
                ID_textbox.Text = string.Empty;
            }
        }

        private void PWD_textbox_Click(object sender, EventArgs e)
        {
            if (PWD_textbox.Text == "Password")
            {
                PWD_textbox.Text = string.Empty;
                PWD_textbox.PasswordChar = '●';
            }
        }

        private void ID_textbox_Leave(object sender, EventArgs e)
        {
            if (ID_textbox.Text == string.Empty)
            {
                ID_textbox.Text = "ID";
            }
        }

        private void PWD_textbox_Leave(object sender, EventArgs e)
        {
            if (PWD_textbox.Text == string.Empty)
            {
                PWD_textbox.PasswordChar = default(char);
                PWD_textbox.Text = "Password";
            }

        }

        private void PWD_show_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (PWD_show_checkbox.Checked == true ||(PWD_show_checkbox.Checked == false && PWD_textbox.Text == "Password"))
            {
                PWD_textbox.PasswordChar = default(char);
            }
            else 
            {
                PWD_textbox.PasswordChar = '●';
            }
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            if(ID_textbox.Text == "ID" && PWD_textbox.Text == "Password")
            {
                MessageBox.Show("아이디와 비밀번호를 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ID_textbox.Text == "ID")
            {
                MessageBox.Show("아이디를 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (PWD_textbox.Text == "Password")
            {
                MessageBox.Show("비밀번호를 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();
                    string loginQuery = $"SELECT mem_num FROM member_profile WHERE id = '{ID_textbox.Text}' AND pwd = '{PWD_textbox.Text}'";

                    using (MySqlCommand login_cmd = new MySqlCommand(loginQuery, mysql))
                    {
                        object result = login_cmd.ExecuteScalar();

                        if (result != null)
                        {
                            DialogResult login_result = MessageBox.Show("로근인  성공\n로그인을 진행하시겠습니까?", "로그인", MessageBoxButtons.YesNo);
                            if(login_result == DialogResult.Yes)
                            {
                                login_mem_num?.Invoke(this, Convert.ToInt32(result));
                                DialogResult = DialogResult.OK;
                                Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("아이디와 비밀번호를 다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            
        }

        private void ID_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (ID_textbox.Text == string.Empty)
                {
                    MessageBox.Show("아이디를 입력하세요.");
                }
                else
                {
                    login_button_Click(sender, e);
                }
            }
        }

        private void PWD_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PWD_textbox.Text == string.Empty)
                {
                    MessageBox.Show("비밀번호를 입력하세요.");
                }
                else
                {
                    login_button_Click(sender, e);
                }
            }
        }

        private void signin_button_Click(object sender, EventArgs e)
        {
            Co_talk_signin sign_in = new Co_talk_signin();
            sign_in.Show();
        }

        private void id_pwd_find_Click(object sender, EventArgs e)
        {
            id_pwd_find find = new id_pwd_find();
            find.Show();
        }

        private void PWD_textbox_Enter(object sender, EventArgs e)
        {
            PWD_textbox_Click(sender, e);
        }

        private void PWD_textbox_TextChanged(object sender, EventArgs e)
        {
            if (PWD_show_checkbox.Checked == true)
            {
                PWD_textbox.PasswordChar = default(char);
            }
            else
            {
                PWD_textbox.PasswordChar = '●';
            }
        }
    }
}

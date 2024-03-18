using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using Mysqlx.Crud;

namespace chat
{
    public partial class id_pwd_find : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";


        public id_pwd_find()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }

        private void id_find_tell_num_textbox_TextChanged(object sender, EventArgs e)
        {
            id_find_tell_num_textbox.Text = new string(id_find_tell_num_textbox.Text.Where(c => char.IsDigit(c) || c == '-').ToArray());



            if (id_find_tell_num_textbox.Text != "- 빼고 입력")
            {
                string tell_result;
                if (id_find_tell_num_textbox.Text.Length > 13)
                {
                    MessageBox.Show("전화번호를 제대로 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tell_result = id_find_tell_num_textbox.Text.Substring(0, 13);
                }
                else
                {
                    List<char> num_list = new List<char>(id_find_tell_num_textbox.Text);
                    num_list.RemoveAll(c => c == '-');
                    tell_result = String.Join("", num_list);

                    if (tell_result.Length >= 4 && tell_result.Length < 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, tell_result.Length - 3)}";
                    }
                    else if (tell_result.Length >= 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, 4)}-{tell_result.Substring(7, tell_result.Length - 7)}";
                    }

                }
                id_find_tell_num_textbox.Text = tell_result;
                id_find_tell_num_textbox.SelectionStart = id_find_tell_num_textbox.Text.Length;
                id_find_tell_num_textbox.ScrollToCaret();
            }
        }

        private void id_find_tell_num_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // int만 입력 받기
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 숫자 또는 백스페이스가 아닌 경우 입력을 취소
                e.Handled = true;
            }
        }

        private void pwd_find_tell_num_textbox_TextChanged(object sender, EventArgs e)
        {
            pwd_find_tell_num_textbox.Text = new string(pwd_find_tell_num_textbox.Text.Where(c => char.IsDigit(c) || c == '-').ToArray());



            if (pwd_find_tell_num_textbox.Text != "- 빼고 입력")
            {
                string tell_result;
                if (pwd_find_tell_num_textbox.Text.Length > 13)
                {
                    MessageBox.Show("전화번호를 제대로 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tell_result = pwd_find_tell_num_textbox.Text.Substring(0, 13);
                }
                else
                {
                    List<char> num_list = new List<char>(pwd_find_tell_num_textbox.Text);
                    num_list.RemoveAll(c => c == '-');
                    tell_result = String.Join("", num_list);

                    if (tell_result.Length >= 4 && tell_result.Length < 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, tell_result.Length - 3)}";
                    }
                    else if (tell_result.Length >= 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, 4)}-{tell_result.Substring(7, tell_result.Length - 7)}";
                    }

                }
                pwd_find_tell_num_textbox.Text = tell_result;
                pwd_find_tell_num_textbox.SelectionStart = pwd_find_tell_num_textbox.Text.Length;
                pwd_find_tell_num_textbox.ScrollToCaret();
            }
        }

        private void pwd_find_tell_num_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // int만 입력 받기
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 숫자 또는 백스페이스가 아닌 경우 입력을 취소
                e.Handled = true;
            }
        }

        private void id_find_button_Click(object sender, EventArgs e)
        {
            if (id_find_name_textbox.Text == string.Empty && id_find_tell_num_textbox.Text == string.Empty)
            {
                MessageBox.Show("이름과 전화번호 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (id_find_name_textbox.Text == string.Empty)
            {
                MessageBox.Show("이름 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (id_find_tell_num_textbox.Text == string.Empty)
            {
                MessageBox.Show("전화번호 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();

                    
                    string selectIdQuery = $"SELECT id FROM member_profile WHERE name = '{id_find_name_textbox.Text}' AND tel_num = '{id_find_tell_num_textbox.Text}'";

                    using (MySqlCommand idCmd = new MySqlCommand(selectIdQuery, mysql))
                    {
                        object result = idCmd.ExecuteScalar();

                        if (result != null)
                        {
                            string memberId = Convert.ToString(result);
                            MessageBox.Show($"찾은 사용자의 ID는 {memberId} 입니다.", "찾기 성공", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("가입하지 않은 이름과 전화번호 정보입니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void pwd_find_button_Click(object sender, EventArgs e)
        {
            if ((pwd_find_name_textbox.Text == string.Empty && pwd_find_tell_num_textbox.Text == string.Empty && pwd_find_id_textbox.Text == string.Empty)
                || (pwd_find_name_textbox.Text == string.Empty && pwd_find_tell_num_textbox.Text == string.Empty)
                || (pwd_find_name_textbox.Text == string.Empty && pwd_find_id_textbox.Text == string.Empty)
                || (pwd_find_tell_num_textbox.Text == string.Empty && pwd_find_id_textbox.Text == string.Empty))
            {
                MessageBox.Show("정보를 모두 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pwd_find_name_textbox.Text == string.Empty)
            {
                MessageBox.Show("이름 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pwd_find_id_textbox.Text == string.Empty)
            {
                MessageBox.Show("아이디 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pwd_find_tell_num_textbox.Text == string.Empty)
            {
                MessageBox.Show("전화번호 정보를 기입하지 않았습니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();

                    // member_profile 테이블에서 ID_signin_textbox.Text 값이 몇개가 있는지 확인
                    string selectpwdQuery = $"SELECT pwd FROM member_profile WHERE name = '{pwd_find_name_textbox.Text}' AND tel_num = '{pwd_find_tell_num_textbox.Text}' AND id = '{pwd_find_id_textbox.Text}'";
                    string ifidQuery = $"SELECT pwd FROM member_profile WHERE id = '{pwd_find_id_textbox.Text}'";

                    using (MySqlCommand idCmd = new MySqlCommand(ifidQuery, mysql))
                    {
                        object result = idCmd.ExecuteScalar();

                        if (result != null)
                        {
                            using (MySqlCommand pwdCmd = new MySqlCommand(selectpwdQuery, mysql))
                            {
                                object pwd_result = pwdCmd.ExecuteScalar();
                                if (pwd_result != null)
                                {
                                    string memberpwd = Convert.ToString(pwd_result);
                                    MessageBox.Show($"찾은 사용자의 Password는 {memberpwd} 입니다.", "찾기 성공", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    MessageBox.Show("가입한 아이디와 맞지 않는 이름과 전화번호 정보입니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("가입하지 않은 아이디 정보입니다.\n다시 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
    }
}

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
using System.Management;
using Mysqlx.Session;

namespace chat
{
    public partial class chat_room_list : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";

        private readonly System.Windows.Forms.Timer timer;
        private int mem_num;
        private List<int> room_num = new List<int>();

        void mem_amount(int i, int amount)
        {
            // 참가자 수를 표시 1:1 대화면 표시 X
            i++;
            if (amount > 1)
            {
                amount++;
                if (i == 1)
                {
                    mem_amount_1.Text = $"{amount}명";
                }
                else if (i == 2)
                {
                    mem_amount_2.Text = $"{amount}명";
                }
                else if (i == 3)
                {
                    mem_amount_3.Text = $"{amount} 명";
                }
                else if (i == 4)
                {
                    mem_amount_4.Text = $"{amount} 명";
                }
                else
                {
                    mem_amount_5.Text = $"{amount} 명";
                }
            }
            else
            {
                if (i == 1)
                {
                    mem_amount_1.Text = string.Empty;
                }
                else if (i == 2)
                {
                    mem_amount_2.Text = string.Empty;
                }
                else if (i == 3)
                {
                    mem_amount_3.Text = string.Empty;
                }
                else if (i == 4)
                {
                    mem_amount_4.Text = string.Empty;
                }
                else
                {
                    mem_amount_5.Text = string.Empty;
                }
            }
        }

        void user_label(int i, int amount ,string name)
        {
            // 참가자 수를 표시 1:1 대화면 표시 X
            i++;
            if (amount > 1)
            {
                if (i == 1)
                {
                    cr_1_user.Text = $"{name}...";
                }
                else if (i == 2)
                {
                    cr_2_user.Text = $"{name}...";
                }
                else if (i == 3)
                {
                    cr_3_user.Text = $"{name}...";
                }
                else if (i == 4)
                {
                    cr_4_user.Text = $"{name}...";
                }
                else
                {
                    cr_5_user.Text = $"{name}...";
                }
            }
            else
            {
                if (i == 1)
                {
                    cr_1_user.Text = name;
                }
                else if (i == 2)
                {
                    cr_2_user.Text = name;
                }
                else if (i == 3)
                {
                    cr_3_user.Text = name;
                }
                else if (i == 4)
                {
                    cr_4_user.Text = name;
                }
                else
                {
                    cr_5_user.Text = name;
                }
            }
        }
        
        void chat_label(int i, string value)
        {
            i++;
            if (value.Length > 14)
            {
                value = $"{value.Substring(0,14)}...";
            }


            if (i == 1)
            {
                cr_1_text.Text = $"{value}";
            }
            else if (i == 2)
            {
                cr_2_text.Text = $"{value}";
            }
            else if (i == 3)
            {
                cr_3_text.Text = $"{value}";
            }
            else if (i == 4)
            {
                cr_4_text.Text = $"{value}";
            }
            else
            {
                cr_5_text.Text = $"{value}";
            }
        }

        void button_init()
        {
            if (cr_1_user.Text == string.Empty)
            {
                chat_room_1.Text = "대화 상대 없음";
                chat_room_1.Enabled = false;
            }
            if (cr_2_user.Text == string.Empty)
            {
                chat_room_2.Text = "대화 상대 없음";
                chat_room_2.Enabled = false;
            }
            if (cr_3_user.Text == string.Empty)
            {
                chat_room_3.Text = "대화 상대 없음";
                chat_room_3.Enabled = false;
            }
            if (cr_4_user.Text == string.Empty)
            {
                chat_room_4.Text = "대화 상대 없음";
                chat_room_4.Enabled = false;
            }
            if (cr_5_user.Text == string.Empty)
            {
                chat_room_5.Text = "대화 상대 없음";
                chat_room_5.Enabled = false;
            }

        }

        void label_init()
        {
            cr_1_user.Text = string.Empty;
            cr_2_user.Text = string.Empty;
            cr_3_user.Text = string.Empty;
            cr_4_user.Text = string.Empty;
            cr_5_user.Text = string.Empty;

            mem_amount_1.Text = string.Empty;
            mem_amount_2.Text = string.Empty;
            mem_amount_3.Text = string.Empty;
            mem_amount_4.Text = string.Empty;
            mem_amount_5.Text = string.Empty;

            cr_1_text.Text = string.Empty;
            cr_2_text.Text = string.Empty;
            cr_3_text.Text = string.Empty;
            cr_4_text.Text = string.Empty;
            cr_5_text.Text = string.Empty;

            chat_room_1.Text = string.Empty;
            chat_room_2.Text = string.Empty;
            chat_room_3.Text = string.Empty;
            chat_room_4.Text = string.Empty;
            chat_room_5.Text = string.Empty;

            chat_room_1.Enabled = true;
            chat_room_2.Enabled = true;
            chat_room_3.Enabled = true;
            chat_room_4.Enabled = true;
            chat_room_5.Enabled = true;
        }

        void chat_list_F5(int mem_num)
        {
            label_init();
            
            string room_list = $"SELECT crn.room_num, MAX(cd.send_time) AS latest_send_time FROM chat_room_num crn LEFT JOIN chat_db cd ON crn.room_num = cd.room_num  WHERE crn.user_mem_num = '{mem_num}' GROUP BY crn.room_num ORDER BY latest_send_time DESC LIMIT 5;";

            if(user_find_textbox.Text != "참여자 찾기" && user_find_textbox.Text != string.Empty)
            {
                room_list = $"SELECT crn.room_num, MAX(cd.send_time) AS latest_send_time FROM (SELECT room_num, user_mem_num FROM chat_room_num crn JOIN member_profile mp ON mp.mem_num = crn.user_mem_num WHERE mp.name = '{user_find_textbox.Text}' or crn.user_mem_num = {mem_num}) crn LEFT JOIN chat_db cd ON crn.room_num = cd.room_num JOIN member_profile mp ON mp.mem_num = crn.user_mem_num WHERE mp.name = '{user_find_textbox.Text}' GROUP BY crn.room_num ORDER BY latest_send_time DESC LIMIT 5;";
            }

            room_num.Clear();

            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();

                using (MySqlCommand room_list_cmd = new MySqlCommand(room_list, mysql))
                {
                    using (MySqlDataReader read_room_num = room_list_cmd.ExecuteReader())
                    {
                        // 결과에서 값들을 읽어와 리스트에 추가
                        while (read_room_num.Read())
                        {
                            int roomNumber = Convert.ToInt32(read_room_num["room_num"]);
                            room_num.Add(roomNumber);
                        }
                    }
                }
                // 채팅방 인원수 구현
                for (int i = 0; i < room_num.Count; i++)
                {
                    string room_member = $"SELECT name FROM member_profile mp JOIN chat_room_num crr ON mp.mem_num = crr.user_mem_num WHERE crr.room_num = '{room_num[i]}' AND crr.user_mem_num != '{mem_num}'";
                    List<string> room_member_list = new List<string>();
                    using (MySqlCommand room_member_cmd = new MySqlCommand(room_member, mysql))
                    {
                        using (MySqlDataReader read_mem_list = room_member_cmd.ExecuteReader())
                        {

                            if (read_mem_list.HasRows)
                            {
                                // 결과에서 값들을 읽어와 리스트에 추가
                                while (read_mem_list.Read())
                                {
                                    string result = read_mem_list["name"].ToString();
                                    room_member_list.Add(result);
                                }
                            }
                            else
                            {
                                room_member_list.Add("no data found");
                            }                                
                        }
                    }
                    
                    mem_amount(i, room_member_list.Count);
                    user_label(i, room_member_list.Count, room_member_list[0]);
                }
                // 채팅방 마지막 대화 불러오기
                for (int i = 0; i < room_num.Count; i++)
                {
                    string last_chat_q = $"SELECT chat_msg FROM chat_db WHERE room_num = '{room_num[i]}' ORDER BY send_time DESC LIMIT 1";
                    using (MySqlCommand load_lastchat_cmd = new MySqlCommand(last_chat_q, mysql))
                    {
                        using (MySqlDataReader load_last_chat = load_lastchat_cmd.ExecuteReader())
                        {
                            string chat_result;
                            if (load_last_chat.Read())
                            {
                                chat_result = load_last_chat["chat_msg"].ToString();
                            }
                            else
                            {
                                chat_result = "최근 대화 없음";
                            }
                            chat_label(i, chat_result);
                        }
                    }
                }
            }
            button_init();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            chat_list_F5(mem_num);
        }



        public chat_room_list(int data)
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
            this.mem_num = data;


            timer = new System.Windows.Forms.Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
        }

        private void chat_room_1_Click(object sender, EventArgs e)
        {
            chat_form chat = new chat_form(room_num[0],mem_num);
            chat.Show();
        }

        private void chat_room_2_Click(object sender, EventArgs e)
        {
            chat_form chat = new chat_form(room_num[1], mem_num);
            chat.Show();
        }

        private void chat_room_3_Click(object sender, EventArgs e)
        {
            chat_form chat = new chat_form(room_num[2], mem_num);
            chat.Show();
        }

        private void chat_room_4_Click(object sender, EventArgs e)
        {
            chat_form chat = new chat_form(room_num[3], mem_num);
            chat.Show();
        }

        private void chat_room_5_Click(object sender, EventArgs e)
        {
            chat_form chat = new chat_form(room_num[4], mem_num);
            chat.Show();
        }

        private void chat_room_list_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void chat_room_list_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void friend_button_Click(object sender, EventArgs e)
        {
            user_profile up = new user_profile();
            up.Show();
        }

        private void user_find_textbox_Click(object sender, EventArgs e)
        {
            if(user_find_textbox.Text == "참여자 찾기")
            {
                user_find_textbox.Text = string.Empty;
                user_find_textbox.ForeColor = SystemColors.WindowText;
            }
        }

        private void user_find_textbox_Leave(object sender, EventArgs e)
        {
            if (user_find_textbox.Text == string.Empty || user_find_textbox.Text == "")
            {
                user_find_textbox.Text = "참여자 찾기";
                user_find_textbox.ForeColor = SystemColors.WindowFrame;
            }
        }

        private void user_find_textbox_TextChanged(object sender, EventArgs e)
        {
            //user_find_textbox_Click(sender, e);
        }

        private void create_chat_room_button_Click(object sender, EventArgs e)
        {
            chat_mem new_room = new chat_mem("room_list", 0, mem_num);
            new_room.Show();
        }

        private void cr_1_text_Click(object sender, EventArgs e)
        {
            chat_room_1_Click(sender, e);
        }

        private void cr_1_user_Click(object sender, EventArgs e)
        {
            chat_room_1_Click(sender, e);
        }

        private void mem_amount_1_Click(object sender, EventArgs e)
        {
            chat_room_1_Click(sender, e);
        }

        private void cr_2_text_Click(object sender, EventArgs e)
        {
            chat_room_2_Click(sender, e);
        }

        private void cr_2_user_Click(object sender, EventArgs e)
        {
            chat_room_2_Click(sender, e);
        }

        private void mem_amount_2_Click(object sender, EventArgs e)
        {
            chat_room_2_Click(sender, e);
        }

        private void cr_3_text_Click(object sender, EventArgs e)
        {
            chat_room_3_Click(sender, e);
        }

        private void cr_3_user_Click(object sender, EventArgs e)
        {
            chat_room_3_Click(sender, e);
        }

        private void mem_amount_3_Click(object sender, EventArgs e)
        {
            chat_room_3_Click(sender, e);
        }

        private void cr_4_text_Click(object sender, EventArgs e)
        {
            chat_room_4_Click(sender, e);
        }

        private void cr_4_user_Click(object sender, EventArgs e)
        {
            chat_room_4_Click(sender, e);
        }

        private void mem_amount_4_Click(object sender, EventArgs e)
        {
            chat_room_4_Click(sender, e);
        }

        private void cr_5_text_Click(object sender, EventArgs e)
        {
            chat_room_5_Click(sender, e);
        }

        private void cr_5_user_Click(object sender, EventArgs e)
        {
            chat_room_5_Click(sender, e);
        }

        private void mem_amount_5_Click(object sender, EventArgs e)
        {
            chat_room_5_Click(sender, e);
        }
    }
}

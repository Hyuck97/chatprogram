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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System.Timers;


namespace chat
{
    public partial class chat_form : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";


        private readonly System.Windows.Forms.Timer timer;
        private int room_num;
        private int host_num;
        private string host_name;
        private int my_sql_chat_row_count = 0;
        private int current_query_row = 0;
        List<string> room_member_list = new List<string>();

        void show_member()
        {
           room_member_list.Clear();

            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                string room_member = $"SELECT name FROM member_profile mp JOIN chat_room_num crr ON mp.mem_num = crr.user_mem_num WHERE crr.room_num = '{room_num}' AND crr.user_mem_num != '{host_num}'";
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

                if (String.Join(", ", room_member_list).Length <= 26 )
                {
                    show_memeber_list.Text = String.Join(", ", room_member_list);
                    this.Text = String.Join(", ", room_member_list);
                }
                else
                {
                    for (int i = 0; i < room_member_list.Count; i++)
                    {
                        List<string> rangelist = room_member_list.GetRange(0, i + 1);
                        if (String.Join(", ", rangelist).Length > 26)
                        {
                            rangelist = room_member_list.GetRange(0, i);
                            string members = $"{String.Join(", ", rangelist)}...";
                            show_memeber_list.Text = members;
                            this.Text = members;
                            break;
                        }
                    }
                    
                }
            }
        }

        void what_is_host_name()
        {
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                string host_nm = $"SELECT name FROM member_profile Where mem_num = '{host_num}'";

                using (MySqlCommand host_nm_cmd = new MySqlCommand(host_nm, mysql))
                {
                    using (MySqlDataReader read_host_nm = host_nm_cmd.ExecuteReader())
                    {
                        read_host_nm.Read();
                        string result = read_host_nm["name"].ToString();
                        host_name = result;
                    }
                }
            }
        }

        void chat_msg_list_init()
        {
            chat_value_richtextbox.SelectionCharOffset = 1;
            chat_value_richtextbox.SelectionIndent = 3;
            chat_value_richtextbox.SelectionRightIndent = 3;

        }

        void load_chat_msg(int i)
        {
            
            string sender;
            string msg_result;
            string date_resut;

            string msg_sql = $"SELECT * FROM (SELECT send_who_name, chat_msg, send_time FROM chat_db Where room_num = '{room_num}' ORDER BY send_time DESC LIMIT {i}) AS subquery ORDER BY send_time ASC;";
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand msg_cmd = new MySqlCommand(msg_sql, mysql))
                {
                    using (MySqlDataReader read_msg = msg_cmd.ExecuteReader())
                    {
                        while (read_msg.Read())
                        {
                            my_sql_chat_row_count++;
                            sender = read_msg["send_who_name"].ToString();
                            msg_result = read_msg["chat_msg"].ToString();
                            date_resut = read_msg["send_time"].ToString();

                            string msg = $"\n<{sender}> [{date_resut}] -> " +
                                         $"\n{msg_result}";
                            chat_value_richtextbox.AppendText(msg);
                            chat_value_richtextbox.AppendText("\n");

                        }
                    }
                }
            }
        }

        void load_chat_history()
        {
            string sender;
            string msg_result;
            string date_resut;

            string msg_sql = $"SELECT send_who_name, chat_msg, send_time FROM chat_db Where room_num = '{room_num}' ORDER BY send_time ASC";
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand msg_cmd = new MySqlCommand(msg_sql, mysql))
                {
                    using (MySqlDataReader read_msg = msg_cmd.ExecuteReader())
                    {
                        while (read_msg.Read())
                        {
                            my_sql_chat_row_count++;
                            sender = read_msg["send_who_name"].ToString();
                            msg_result = read_msg["chat_msg"].ToString();
                            date_resut = read_msg["send_time"].ToString();

                            string msg = $"\n<{sender}> [{date_resut}] -> " +
                                         $"\n{msg_result}";
                            chat_value_richtextbox.AppendText(msg);
                            chat_value_richtextbox.AppendText("\n");
                        }
                    }
                }
            }

            

            
        }

        private int count_chat_query()
        {
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                string query = $"SELECT COUNT(*) FROM chat_db WHERE room_num = {room_num}";
                using (MySqlCommand command = new MySqlCommand(query, mysql))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (current_query_row != count_chat_query())
            {
                current_query_row = count_chat_query();
                if(current_query_row >= my_sql_chat_row_count)
                {
                    load_chat_msg(current_query_row - my_sql_chat_row_count);
                }
                show_member();
            }
        }


        public chat_form(int room_data,int host_num_data)
        {
            InitializeComponent();
            chat_msg_list_init();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
            room_num = room_data;
            host_num = host_num_data;
            show_member();
            what_is_host_name();
            load_chat_history();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            string insertQuery = string.Format("INSERT INTO chat_db (room_num, send_who_num, send_who_name, chat_msg, send_time) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');", room_num, host_num, host_name, chat_send_richtextbox.Text, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                // INSERT 쿼리 실행
               
                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, mysql))
                {
                    insertCmd.ExecuteNonQuery();
                }
            }

            chat_send_richtextbox.Text = "";
        }

        private void chat_form_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void chat_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

        private void chat_send_richtextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                e.SuppressKeyPress = true;
                send_button_Click(sender, e);
            }
        }

        private void chat_value_richtextbox_TextChanged(object sender, EventArgs e)
        {
            chat_value_richtextbox.SelectionStart = chat_value_richtextbox.Text.Length;
            chat_value_richtextbox.ScrollToCaret();
        }

        private void show_memeber_list_Click(object sender, EventArgs e)
        {
            string members = String.Join(",\n", room_member_list);
            MessageBox.Show($"<대화방 참여자>\n{host_name},\n{members}", "참여자 목록" , MessageBoxButtons.OK);
        }

        private void chat_plus_member_button_Click(object sender, EventArgs e)
        {
            chat_mem new_mem = new chat_mem("chat_room", room_num, host_num);
            new_mem.Show();
        }
    }
}

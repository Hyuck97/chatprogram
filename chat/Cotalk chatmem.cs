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
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;

namespace chat
{
    public partial class chat_mem : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";

        

        private string from_where;
        private int room_num;
        private int host_num;
        private string host_name;
        List<int> sel_index = new List<int>();


        void init(string from_where)
        {
            if (from_where == "room_list")
            {
                activate_button.Text = "새로운 채팅방 생성";
                this.Text = "Co Talk New Chat Room";
            }
            else
            {
                activate_button.Text = "대화방 참가자 추가";
                this.Text = "Co Talk Add Member";
            }
        }

        void list_show()
        {
            string user_sql = $"SELECT name, age, id, class_name FROM member_profile WHERE mem_num != {host_num}";

            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand profile_cmd = new MySqlCommand(user_sql, mysql))
                {
                    using (MySqlDataReader read_profile = profile_cmd.ExecuteReader())
                    {
                        while (read_profile.Read())
                        {
                            string name = read_profile["name"].ToString();
                            string age = read_profile["age"].ToString();
                            string id = read_profile["id"].ToString();
                            string class_name = read_profile["class_name"].ToString();

                            //checkedlist item 추가
                            string value = $" {name}({age}) [{id}] <{class_name}>";
                            mem_show_checkedlistbox.Items.Add(value, false);
                        }
                    }
                }
            }
        }

        void check_list_init(int room_num)
        {
            string read_n_q = $"SELECT name, age, id, class_name from member_profile mp JOIN chat_room_num crn ON mp.mem_num = crn.user_mem_num WHERE crn.room_num = {room_num}";
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand profile_cmd = new MySqlCommand(read_n_q, mysql))
                {
                    using (MySqlDataReader read_profile = profile_cmd.ExecuteReader())
                    {
                        while (read_profile.Read())
                        {
                            string name = read_profile["name"].ToString();
                            string age = read_profile["age"].ToString();
                            string id = read_profile["id"].ToString();
                            string class_name = read_profile["class_name"].ToString();

                            //checkedlist item 찾기
                            string value = $" {name}({age}) [{id}] <{class_name}>";
                            int itemIndex = mem_show_checkedlistbox.FindString(value);
                            if (itemIndex != ListBox.NoMatches)
                            {
                                // 아이템이 발견되었을 때 선택
                                mem_show_checkedlistbox.Items.RemoveAt(itemIndex);
                            }
                        }
                    }
                }
            }
        }

        void chat_room_act(int room_num)
        {

            List<string> new_mem_list = new List<string>();

            int mem_num;
            
            // mysql chat_room_num db 에 추가
            for (int i = 0; i < sel_index.Count; i++)
            {
                string item_text = mem_show_checkedlistbox.Items[sel_index[i]].ToString();
                string pattern = @"\[([^\]]*)\]";

                Match match = Regex.Match(item_text, pattern);

                string id = match.Groups[1].Value;
                string find_mn_sql = $"SELECT mem_num, name FROM member_profile WHERE id = '{id}'";

                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();
                    
                    
                    
                    using (MySqlCommand profile_cmd = new MySqlCommand(find_mn_sql, mysql))
                    {
                        using (MySqlDataReader read_profile = profile_cmd.ExecuteReader())
                        {
                            read_profile.Read();
                            mem_num = Convert.ToInt32(read_profile["mem_num"]);
                            new_mem_list.Add(read_profile["name"].ToString());
                        }
                    }
                    string insert_q = string.Format("INSERT INTO chat_room_num (room_num, user_mem_num) VALUES ('{0}', '{1}');", room_num, mem_num);
                    using (MySqlCommand insertCmd = new MySqlCommand(insert_q, mysql))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                }

            }

            string new_mem = string.Join(", ", new_mem_list);
            string value = $"[{new_mem}]을 초대하였습니다.";

            string insertQuery = string.Format("INSERT INTO chat_db (room_num, send_who_num, send_who_name, chat_msg, send_time) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');", room_num, host_num, host_name, value, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                // INSERT 쿼리 실행

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, mysql))
                {
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        void room_list_act(int new_room_num_data)
        {
            List<string> new_mem_list = new List<string>();

            int mem_num;

            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                string insert_q = string.Format("INSERT INTO chat_room_num (room_num, user_mem_num) VALUES ('{0}', '{1}');", new_room_num_data, host_num);
                using (MySqlCommand insertCmd = new MySqlCommand(insert_q, mysql))
                {
                    insertCmd.ExecuteNonQuery();
                    new_mem_list.Add(host_name);
                }
            }

            // mysql chat_room_num db 에 추가
            for (int i = 0; i < sel_index.Count; i++)
            {
                string item_text = mem_show_checkedlistbox.Items[sel_index[i]].ToString();
                string pattern = @"\[([^\]]*)\]";

                Match match = Regex.Match(item_text, pattern);

                string id = match.Groups[1].Value;
                string find_mn_sql = $"SELECT mem_num, name FROM member_profile WHERE id = '{id}'";

                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();

                    using (MySqlCommand profile_cmd = new MySqlCommand(find_mn_sql, mysql))
                    {
                        using (MySqlDataReader read_profile = profile_cmd.ExecuteReader())
                        {
                            read_profile.Read();
                            mem_num = Convert.ToInt32(read_profile["mem_num"]);
                            new_mem_list.Add(read_profile["name"].ToString());
                        }
                    }
                    string insert_q = string.Format("INSERT INTO chat_room_num (room_num, user_mem_num) VALUES ('{0}', '{1}');", new_room_num_data, mem_num);
                    using (MySqlCommand insertCmd = new MySqlCommand(insert_q, mysql))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                }

            }

            string new_mem = string.Join(", ", new_mem_list);
            string value = $"[{host_name}님이 대화방을 생성하였습니다.\n[{new_mem}]";

            string insertQuery = string.Format("INSERT INTO chat_db (room_num, send_who_num, send_who_name, chat_msg, send_time) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');", new_room_num_data, host_num, host_name, value, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                // INSERT 쿼리 실행

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, mysql))
                {
                    insertCmd.ExecuteNonQuery();
                }
            }

        }

        int get_new_room_num()
        {
            int new_num;
            string get_rn_sql = $"SELECT room_num FROM chat_project.chat_room_num ORDER BY room_num DESC LIMIT 1";
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand room_num_cmd = new MySqlCommand(get_rn_sql, mysql))
                {
                    using (MySqlDataReader read = room_num_cmd.ExecuteReader())
                    {
                        read.Read();
                        new_num = Convert.ToInt32(read["room_num"]) + 1;
                    }
                }
            }

            return new_num;
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





        public chat_mem(string from_where_data, int room_num_data, int host_num_data)
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
            mem_show_checkedlistbox.Items.Clear();
            from_where = from_where_data;
            room_num = room_num_data;
            host_num = host_num_data;
            what_is_host_name();

            // 채팅방 목록에서 -> room_list / 채팅방에서 -> chat_room
            list_show();
            init(from_where);
            if (from_where == "chat_room")
            {
                // 채팅 방에서 참가자를 추가할 경우 기존 채팅방에 참가자는 리스트에서 제거
                check_list_init(room_num);
            }
        }

        private void activate_button_Click(object sender, EventArgs e)
        {
            sel_index = mem_show_checkedlistbox.CheckedIndices.Cast<int>().ToList();
            
            if(from_where == "chat_room")
            {
                chat_room_act(room_num);
            }
            else
            {
                int new_room_num = get_new_room_num();
                room_list_act(new_room_num);

                chat_form chat = new chat_form(new_room_num, host_num);
                chat.Show();
            }
            this.Close();
        }
    }
}

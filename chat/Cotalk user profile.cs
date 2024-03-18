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
    public partial class user_profile : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";


        public user_profile()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);

            show_user_list.BeginUpdate();
            ListViewItem item;

            string user_sql = $"SELECT name, age, tel_num, id, class_name FROM member_profile";
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();
                using (MySqlCommand profile_cmd = new MySqlCommand(user_sql, mysql))
                {
                    using (MySqlDataReader read_profile = profile_cmd.ExecuteReader())
                    {
                        while(read_profile.Read())
                        {
                            string name = read_profile["name"].ToString();
                            string age = read_profile["age"].ToString();
                            string tell_num = read_profile["tel_num"].ToString();
                            string id = read_profile["id"].ToString();
                            string class_name = read_profile["class_name"].ToString();

                            item = new ListViewItem($"{name} [{age}]");
                            item.SubItems.Add(tell_num);
                            item.SubItems.Add(id);
                            item.SubItems.Add(class_name);

                            show_user_list.Items.Add(item);

                        }
                    }
                }
            }

            show_user_list.EndUpdate();

        }
    }
}

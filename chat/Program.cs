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

namespace chat
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int mem_num = 0;
            login_form start_login = new login_form();

            start_login.login_mem_num += (sender, login_mem_num_data) =>
            {
                mem_num = login_mem_num_data;
            };

            if (start_login.ShowDialog() == DialogResult.OK)
            {
                chat_room_list login = new chat_room_list(mem_num);
                Application.Run(login);
            }
        }
    }
}

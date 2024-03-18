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
    public partial class Co_talk_signin : Form
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "chat_project"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "1234"; //계정 비밀번호
        string _connectionAddress = "";



        public Co_talk_signin()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }


        private void agree_richtextbox_MouseEnter(object sender, EventArgs e)
        {
            // 개인정보 내용 스크롤 모두 내려야 이용 동의 체크박스 활성화
            int currentPosition = agree_richtextbox.GetPositionFromCharIndex(agree_richtextbox.GetFirstCharIndexFromLine(agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.SelectionStart))).Y;

            int visibleLines = agree_richtextbox.ClientSize.Height / agree_richtextbox.Font.Height;
            int totalLines = agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.TextLength) + 1;

            int lastVisibleLine = agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.GetCharIndexFromPosition(new Point(1, agree_richtextbox.ClientSize.Height - 2)));

            if (lastVisibleLine + visibleLines >= totalLines)
            {
                agree_checkbox.Enabled = true;
            }
        }

        private void PWD_signin_textbox_Click(object sender, EventArgs e)
        {
            if (PWD_signin_textbox.Text == "영문, 숫자, 특수기호 포함 8글자 이상")
            {
                PWD_signin_textbox.Text = string.Empty;
                PWD_signin_textbox.ForeColor = SystemColors.WindowText;
                if (show_pwd_checkbox.Checked == false)
                {
                    PWD_signin_textbox.PasswordChar = '●';
                } 
                else
                {
                    PWD_signin_textbox.PasswordChar = default(char);
                }
            }
        }

        private void PWD_signin_textbox_Leave(object sender, EventArgs e)
        {
            if(PWD_signin_textbox.Text == string.Empty)
            {
                PWD_signin_textbox.PasswordChar = default(char);
                PWD_signin_textbox.ForeColor = SystemColors.WindowFrame;
                PWD_signin_textbox.Text = "영문, 숫자, 특수기호 포함 8글자 이상";
            }
        }

        private void tell_num_textbox_Click(object sender, EventArgs e)
        {
            if (tell_num_textbox.Text == "- 빼고 입력")
            {
                tell_num_textbox.Text = string.Empty;
                tell_num_textbox.ForeColor = SystemColors.WindowText;
            }
        }

        private void tell_num_textbox_Leave(object sender, EventArgs e)
        {
            if (tell_num_textbox.Text == string.Empty)
            {
                tell_num_textbox.ForeColor = SystemColors.WindowFrame;
                tell_num_textbox.Text = "- 빼고 입력";
            }
        }

        private void show_pwd_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (show_pwd_checkbox.Checked == true)
            {
                
                PWD_signin_textbox.PasswordChar = default(char);
                PWD2_signin_textbox.PasswordChar = default(char);

            }
            else
            {
                if (PWD_signin_textbox.Text != "영문, 숫자, 특수기호 포함 8글자 이상")
                {
                    PWD_signin_textbox.PasswordChar = '●';
                }
                PWD2_signin_textbox.PasswordChar = '●';
            }
        }

        private void tell_num_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // int만 입력 받기
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 숫자 또는 백스페이스가 아닌 경우 입력을 취소
                e.Handled = true;
            }
        }

        private void ran_pwd_button_Click(object sender, EventArgs e)
        {
            //생성된 랜덤 pwd를 보여주기 위해
            show_pwd_checkbox.Checked = true;
            PWD_signin_textbox.PasswordChar = default(char);
            PWD2_signin_textbox.PasswordChar = default(char);

            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            char[] specialSymbols = { '!', '@', '#', '$', '%', '^', '&', '*' };

            // 영문 알파벳 배열
            char[] uppercaseAlphabet = new char[26];
            char[] lowercaseAlphabet = new char[26];

            for (int i = 0; i < 26; i++)
            {
                uppercaseAlphabet[i] = (char)('A' + i);
                lowercaseAlphabet[i] = (char)('a' + i);
            }

            Random random = new Random();

            
            int randomlength = random.Next(13, 18);
            int symbollength = random.Next(2, 4);
            int alphabetlength = random.Next(3, randomlength - symbollength - 3 + 1);
            int numlength = randomlength - symbollength - alphabetlength;   // 숫자 기본으로 2개이상 포함

            string[] ranpwd = new string[randomlength];

            for (int i = 0; i < randomlength; i++)
            {
                int rantype = random.Next(1,5);     // 1 = 소문자, 2 = 대문자, 3 = 숫자, 4 = 특수 기호
                if (rantype == 1)
                {
                    if (alphabetlength == 0)
                    {
                        i--;
                    }
                    else
                    {
                        alphabetlength--;
                        int ranindex = random.Next(0,26);
                        ranpwd[i] = lowercaseAlphabet[ranindex].ToString();
                    }
                }
                else if (rantype == 2)
                {
                    if (alphabetlength == 0)
                    {
                        i--;
                    }
                    else
                    {
                        alphabetlength--;
                        int ranindex = random.Next(0, 26);
                        ranpwd[i] = uppercaseAlphabet[ranindex].ToString();
                    }
                }
                else if (rantype == 3)
                {
                    if (numlength == 0)
                    {
                        i--;
                    }
                    else
                    {
                        numlength--;
                        int ranindex = random.Next(0, 10);
                        ranpwd[i] = numbers[ranindex].ToString();
                    }
                }
                else
                {
                    if (symbollength == 0)
                    {
                        i--;
                    }
                    else
                    {
                        symbollength--;
                        int ranindex = random.Next(0, 8);
                        ranpwd[i] = specialSymbols[ranindex].ToString();
                    }
                }
            }

            string result = string.Join("", ranpwd);

            PWD_signin_textbox.ForeColor = SystemColors.WindowText;
            PWD_signin_textbox.Text = result;
            PWD2_signin_textbox.Text = result;

        }

        private bool IsAllowedCharacter(char inputChar, Keys modifierKeys)
        {
            char[] allowedSymbols = { '!', '@', '#', '$', '%', '^', '&', '*' };
            char[] uppercaseAlphabet = new char[26];
            char[] lowercaseAlphabet = new char[26];
            for (int i = 0; i < 26; i++)
            {
                uppercaseAlphabet[i] = (char)('A' + i);
                lowercaseAlphabet[i] = (char)('a' + i);
            }


            bool isAlphabet = char.IsLetter(inputChar);
            
            return (char.IsDigit(inputChar) || allowedSymbols.Contains(inputChar) || uppercaseAlphabet.Contains(inputChar) || lowercaseAlphabet.Contains(inputChar) || inputChar == '\b') ||
           (char.IsControl(inputChar) && modifierKeys == Keys.Control);
        }

        private bool IsAllowedCharacter_ID(char inputChar, Keys modifierKeys)
        {
            char[] uppercaseAlphabet = new char[26];
            char[] lowercaseAlphabet = new char[26];
            for (int i = 0; i < 26; i++)
            {
                uppercaseAlphabet[i] = (char)('A' + i);
                lowercaseAlphabet[i] = (char)('a' + i);
            }


            bool isAlphabet = char.IsLetter(inputChar);

            return (char.IsDigit(inputChar) || uppercaseAlphabet.Contains(inputChar) || lowercaseAlphabet.Contains(inputChar) || inputChar == '\b') ||
           (char.IsControl(inputChar) && modifierKeys == Keys.Control);
        }

        private void PWD_signin_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys modifierKeys = Control.ModifierKeys;

            if (!IsAllowedCharacter(e.KeyChar, modifierKeys))
            {
                e.Handled = true;
            }
        }

        private void PWD2_signin_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys modifierKeys = Control.ModifierKeys;

            if (!IsAllowedCharacter(e.KeyChar, modifierKeys))
            {
                e.Handled = true;
            }
        }

        private void name_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        static bool HasLowerCase(char[] array)
        {
            // 배열에 소문자가 하나라도 있는지 확인
            return array.Any(char.IsLower);
        }
        static bool HasUpperCase(char[] array)
        {
            // 배열에 대문자가 하나라도 있는지 확인
            return array.Any(char.IsUpper);
        }
        static bool HasNumCase(char[] array)
        {
            // 배열에 숫자가 하나라도 있는지 확인
            return array.Any(char.IsNumber);
        }
        static bool HasSymbolCase(char[] array)
        {
            // 배열에 특수 문자가 하나라도 있는지 확인
            char[] specialSymbols = { '!', '@', '#', '$', '%', '^', '&', '*' };
            return array.Any(symbol => specialSymbols.Contains(symbol));
        }


        private void PWD_signin_textbox_TextChanged(object sender, EventArgs e)
        {
            


            // 일치 여부 표시
            if (PWD_signin_textbox.Text == PWD2_signin_textbox.Text)
            {
                if (PWD2_signin_textbox.Text != string.Empty || PWD_signin_textbox.Text != string.Empty)
                {
                    pwd_same.Text = "일치 여부 : ○";
                }
            }
            else
            {
                pwd_same.Text = "일치 여부 : X";
            }

            // 안전도 표시
            // 소문자 / 대문자 / 숫자 / 특수문자 / 10글자 이상
            char[] pwdarr1 = PWD_signin_textbox.Text.ToCharArray();
            int safety = 0;

            bool hasLowerCase = HasLowerCase(pwdarr1);
            if (hasLowerCase)
            {
                safety++;
            }

            bool hasUpperCase = HasUpperCase(pwdarr1);
            if (hasUpperCase)
            {
                safety++;
            }

            bool hasNumCase = HasNumCase(pwdarr1);
            if (hasNumCase)
            {
                safety++;
            }

            bool hasSymbolCase = HasSymbolCase(pwdarr1);
            if (hasSymbolCase)
            {
                safety++;
            }

            if (pwdarr1.GetLength(0) >= 10)
            {
                safety++;
            }

            if (PWD_signin_textbox.Text == "영문, 숫자, 특수기호 포함 8글자 이상")
            {
                safety = 0;
            }


            //안전도 출력
            if (safety == 0)
            {
                PWD_sequrity.Text = "안전도 : ○○○○○";
            }
            else if (safety == 1)
            {
                PWD_sequrity.Text = "안전도 : ●○○○○";
            }
            else if (safety == 2)
            {
                PWD_sequrity.Text = "안전도 : ●●○○○";
            }
            else if (safety == 3)
            {
                PWD_sequrity.Text = "안전도 : ●●●○○";
            }
            else if (safety == 4)
            {
                PWD_sequrity.Text = "안전도 : ●●●●○";
            }
            else
            {
                PWD_sequrity.Text = "안전도 : ●●●●●";
            }
        }

        private void PWD2_signin_textbox_TextChanged(object sender, EventArgs e)
        {
            if (PWD_signin_textbox.Text == PWD2_signin_textbox.Text)
            {
                if (PWD2_signin_textbox.Text != string.Empty || PWD_signin_textbox.Text != string.Empty)
                {
                    pwd_same.Text = "일치 여부 : ○";
                }
            }
            else
            {
                pwd_same.Text = "일치 여부 : X";
            }
        }

        private void ID_check_Click(object sender, EventArgs e)
        {
            string idToCheck = ID_signin_textbox.Text;
            bool insert = true;
            using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
            {
                mysql.Open();

                // member_profile 테이블에서 ID_signin_textbox.Text 값이 몇개가 있는지 확인
                string selectQuery = $"SELECT COUNT(*) FROM member_profile WHERE id = '{idToCheck}'";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, mysql))
                {
                    int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        // ID 값이 존재하면 경고
                        MessageBox.Show("이미 존재하는 ID입니다. 다른 ID를 사용해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        insert = false;
                    }
                    else
                    {
                        // ID 값이 존재하지 않으면 계속
                        DialogResult result = MessageBox.Show("사용 가능한 ID입니다. 사용하시겠습니까?", "중복 확인", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            // Yes 버튼이 눌렸을 경우
                            ID_check_show_checkbox.Checked = true;
                            ID_signin_textbox.Enabled = false;
                            ID_check.Enabled = false;
                            insert = true;
                        }
                        else
                        {
                            // No 버튼이 눌렸을 경우
                            // 여기에 원하는 동작을 추가하세요
                            insert = false;
                        }
                    }
                }
                if (insert)
                {
                    //ID 먼저 생성
                    string name = "a";
                    string tell_num = "a";
                    string pwd = "a";
                    string class_name = "a";
                    string birth = dateTimePicker1.Value.ToString("yyyyMMdd");
                    int age = 1;
                    string id = ID_signin_textbox.Text;

                    // INSERT 쿼리 실행
                    string insertQuery = string.Format("INSERT INTO member_profile (name, age, tel_num, birth, id, pwd, class_name) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');", name, age, tell_num, birth, id, pwd, class_name);
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, mysql))
                    {
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }   // 아이디 중복 확인

        private void signin_button_Click(object sender, EventArgs e)
        {
            string name = name_textbox.Text;
            string tell_num = tell_num_textbox.Text.ToString();
            string birth = dateTimePicker1.Value.ToString("yyyyMMdd");
            int age = DateTime.Today.Year - Convert.ToInt32(dateTimePicker1.Value.ToString("yyyy")) + 1;
            string id = ID_signin_textbox.Text;
            string pwd = PWD_signin_textbox.Text;
            string class_name = class_combobox.Text;

            if (ID_check_show_checkbox.Checked == false)
            {
                MessageBox.Show("ID 중복 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pwd_same.Text == "일치 여부 : X")
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다. 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (PWD_sequrity.Text == "안전도 : ○○○○○" || PWD_sequrity.Text == "안전도 : ●○○○○" || PWD_sequrity.Text == "안전도 : ●●○○○" || PWD_sequrity.Text == "안전도 : ●●●○○")
            {
                MessageBox.Show("비밀번호가 안전하지 않습니다. 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (name_textbox.Text == string.Empty)
            {
                MessageBox.Show("이름을 입력하지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tell_num_textbox.Text == "")
            {
                MessageBox.Show("전화번호를 확인해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (agree_checkbox.Checked == false)
            {
                MessageBox.Show("개인정보 수집 및 이용 동의에 체크해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                DialogResult result = MessageBox.Show("위 정보로 가입하시겠습니까?", "가입 확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Yes 버튼이 눌렸을 경우
                    using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                    {
                        mysql.Open();

                        // UPDATE 쿼리를 작성하여 특정 ID에 대한 데이터 업데이트
                        string updateQuery = $"UPDATE member_profile SET name = '{name}', age = {age}, tel_num = '{tell_num}', birth = '{birth}' , pwd = '{pwd}', class_name = '{class_name}' WHERE id = '{id}'";

                        using (MySqlCommand command = new MySqlCommand(updateQuery, mysql))
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("회원가입이 완료되었습니다.", "가입 완료", MessageBoxButtons.OK);
                    this.Close();
                }
            }
            
        }

        private void Co_talk_signin_FormClosing(object sender, FormClosingEventArgs e)
        {
            string idToCheck = ID_signin_textbox.Text;

            // 중복확인 상태에서 가입하지 않은 쿼리 삭제
            if (ID_check_show_checkbox.Checked == true)
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();

                   
                    string deleteQuery = $"DELETE FROM member_profile WHERE id = '{idToCheck}' AND pwd = 'a'";

                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, mysql))
                    {
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                    }
                }
            }
        }

       
        private void tell_num_textbox_TextChanged(object sender, EventArgs e)
        {

            tell_num_textbox.Text = new string(tell_num_textbox.Text.Where(c => char.IsDigit(c) || c == '-').ToArray());



            if (tell_num_textbox.Text != "- 빼고 입력")
            {
                string tell_result;
                if (tell_num_textbox.Text.Length > 13)
                {
                    MessageBox.Show("전화번호를 제대로 입력해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tell_result = tell_num_textbox.Text.Substring(0, 13);
                }
                else
                {
                    List<char> num_list = new List<char>(tell_num_textbox.Text);
                    num_list.RemoveAll( c => c=='-');
                    tell_result = String.Join("", num_list);

                    if (tell_result.Length >= 4  && tell_result.Length < 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, tell_result.Length-3)}";
                    }
                    else if (tell_result.Length >= 8)
                    {
                        tell_result = $"{tell_result.Substring(0, 3)}-{tell_result.Substring(3, 4)}-{tell_result.Substring(7,tell_result.Length-7)}";
                    }

                }
                tell_num_textbox.Text = tell_result;
                tell_num_textbox.SelectionStart = tell_num_textbox.Text.Length;
                tell_num_textbox.ScrollToCaret();
            }
        }

        private void Co_talk_signin_MouseEnter(object sender, EventArgs e)
        {
            // 개인정보 내용 스크롤 모두 내려야 이용 동의 체크박스 활성화
            int currentPosition = agree_richtextbox.GetPositionFromCharIndex(agree_richtextbox.GetFirstCharIndexFromLine(agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.SelectionStart))).Y;

            int visibleLines = agree_richtextbox.ClientSize.Height / agree_richtextbox.Font.Height;
            int totalLines = agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.TextLength) + 1;

            int lastVisibleLine = agree_richtextbox.GetLineFromCharIndex(agree_richtextbox.GetCharIndexFromPosition(new Point(1, agree_richtextbox.ClientSize.Height - 2)));

            if (lastVisibleLine + visibleLines >= totalLines)
            {
                agree_checkbox.Enabled = true;
            }
        }

        private void ID_signin_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys modifierKeys = Control.ModifierKeys;

            if (!IsAllowedCharacter_ID(e.KeyChar, modifierKeys))
            {
                e.Handled = true;
            }
        }
    }
}

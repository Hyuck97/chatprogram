namespace chat
{
    partial class login_form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login_form));
            this.ID_textbox = new System.Windows.Forms.TextBox();
            this.PWD_textbox = new System.Windows.Forms.TextBox();
            this.login_logo = new System.Windows.Forms.PictureBox();
            this.PWD_show_checkbox = new System.Windows.Forms.CheckBox();
            this.login_button = new System.Windows.Forms.Button();
            this.signin_button = new System.Windows.Forms.Button();
            this.id_pwd_find = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.login_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // ID_textbox
            // 
            this.ID_textbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ID_textbox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ID_textbox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ID_textbox.Location = new System.Drawing.Point(70, 330);
            this.ID_textbox.Name = "ID_textbox";
            this.ID_textbox.Size = new System.Drawing.Size(240, 29);
            this.ID_textbox.TabIndex = 1;
            this.ID_textbox.Text = "ID";
            this.ID_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ID_textbox.Click += new System.EventHandler(this.ID_textbox_Click);
            this.ID_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ID_textbox_KeyDown);
            this.ID_textbox.Leave += new System.EventHandler(this.ID_textbox_Leave);
            // 
            // PWD_textbox
            // 
            this.PWD_textbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PWD_textbox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PWD_textbox.Location = new System.Drawing.Point(70, 380);
            this.PWD_textbox.Name = "PWD_textbox";
            this.PWD_textbox.Size = new System.Drawing.Size(240, 29);
            this.PWD_textbox.TabIndex = 2;
            this.PWD_textbox.Text = "Password";
            this.PWD_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PWD_textbox.Click += new System.EventHandler(this.PWD_textbox_Click);
            this.PWD_textbox.TextChanged += new System.EventHandler(this.PWD_textbox_TextChanged);
            this.PWD_textbox.Enter += new System.EventHandler(this.PWD_textbox_Enter);
            this.PWD_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PWD_textbox_KeyDown);
            this.PWD_textbox.Leave += new System.EventHandler(this.PWD_textbox_Leave);
            // 
            // login_logo
            // 
            this.login_logo.Image = ((System.Drawing.Image)(resources.GetObject("login_logo.Image")));
            this.login_logo.Location = new System.Drawing.Point(12, 12);
            this.login_logo.Name = "login_logo";
            this.login_logo.Size = new System.Drawing.Size(360, 290);
            this.login_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.login_logo.TabIndex = 0;
            this.login_logo.TabStop = false;
            // 
            // PWD_show_checkbox
            // 
            this.PWD_show_checkbox.AutoSize = true;
            this.PWD_show_checkbox.Location = new System.Drawing.Point(200, 415);
            this.PWD_show_checkbox.Name = "PWD_show_checkbox";
            this.PWD_show_checkbox.Size = new System.Drawing.Size(110, 19);
            this.PWD_show_checkbox.TabIndex = 3;
            this.PWD_show_checkbox.Text = "Show password";
            this.PWD_show_checkbox.UseVisualStyleBackColor = true;
            this.PWD_show_checkbox.CheckedChanged += new System.EventHandler(this.PWD_show_checkbox_CheckedChanged);
            // 
            // login_button
            // 
            this.login_button.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.login_button.Location = new System.Drawing.Point(90, 450);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(200, 35);
            this.login_button.TabIndex = 4;
            this.login_button.Text = "로그인";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // signin_button
            // 
            this.signin_button.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.signin_button.Location = new System.Drawing.Point(90, 509);
            this.signin_button.Name = "signin_button";
            this.signin_button.Size = new System.Drawing.Size(200, 35);
            this.signin_button.TabIndex = 5;
            this.signin_button.Text = "회원가입";
            this.signin_button.UseVisualStyleBackColor = true;
            this.signin_button.Click += new System.EventHandler(this.signin_button_Click);
            // 
            // id_pwd_find
            // 
            this.id_pwd_find.AutoSize = true;
            this.id_pwd_find.Location = new System.Drawing.Point(119, 571);
            this.id_pwd_find.Name = "id_pwd_find";
            this.id_pwd_find.Size = new System.Drawing.Size(148, 15);
            this.id_pwd_find.TabIndex = 6;
            this.id_pwd_find.Text = "<아이디 / 비밀번호 찾기>";
            this.id_pwd_find.Click += new System.EventHandler(this.id_pwd_find_Click);
            // 
            // login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(384, 611);
            this.Controls.Add(this.id_pwd_find);
            this.Controls.Add(this.signin_button);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.PWD_show_checkbox);
            this.Controls.Add(this.PWD_textbox);
            this.Controls.Add(this.ID_textbox);
            this.Controls.Add(this.login_logo);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "login_form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "CO Talk LOGIN";
            ((System.ComponentModel.ISupportInitialize)(this.login_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ID_textbox;
        private System.Windows.Forms.TextBox PWD_textbox;
        private System.Windows.Forms.PictureBox login_logo;
        private System.Windows.Forms.CheckBox PWD_show_checkbox;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button signin_button;
        private System.Windows.Forms.Label id_pwd_find;
    }
}


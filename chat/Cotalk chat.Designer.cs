namespace chat
{
    partial class chat_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chat_form));
            this.chat_send_richtextbox = new System.Windows.Forms.RichTextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.chat_value_richtextbox = new System.Windows.Forms.RichTextBox();
            this.show_memeber_list = new System.Windows.Forms.Label();
            this.chat_plus_member_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chat_send_richtextbox
            // 
            this.chat_send_richtextbox.Location = new System.Drawing.Point(12, 479);
            this.chat_send_richtextbox.Name = "chat_send_richtextbox";
            this.chat_send_richtextbox.Size = new System.Drawing.Size(280, 70);
            this.chat_send_richtextbox.TabIndex = 0;
            this.chat_send_richtextbox.Text = "";
            this.chat_send_richtextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chat_send_richtextbox_KeyDown);
            // 
            // send_button
            // 
            this.send_button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.send_button.Location = new System.Drawing.Point(297, 479);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(75, 70);
            this.send_button.TabIndex = 1;
            this.send_button.Text = "보내기";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // chat_value_richtextbox
            // 
            this.chat_value_richtextbox.Location = new System.Drawing.Point(12, 45);
            this.chat_value_richtextbox.Name = "chat_value_richtextbox";
            this.chat_value_richtextbox.Size = new System.Drawing.Size(360, 410);
            this.chat_value_richtextbox.TabIndex = 2;
            this.chat_value_richtextbox.Text = "";
            this.chat_value_richtextbox.TextChanged += new System.EventHandler(this.chat_value_richtextbox_TextChanged);
            // 
            // show_memeber_list
            // 
            this.show_memeber_list.AutoSize = true;
            this.show_memeber_list.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.show_memeber_list.Location = new System.Drawing.Point(12, 16);
            this.show_memeber_list.Name = "show_memeber_list";
            this.show_memeber_list.Size = new System.Drawing.Size(43, 17);
            this.show_memeber_list.TabIndex = 3;
            this.show_memeber_list.Text = "label1";
            this.show_memeber_list.Click += new System.EventHandler(this.show_memeber_list_Click);
            // 
            // chat_plus_member_button
            // 
            this.chat_plus_member_button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chat_plus_member_button.Location = new System.Drawing.Point(289, 12);
            this.chat_plus_member_button.Name = "chat_plus_member_button";
            this.chat_plus_member_button.Size = new System.Drawing.Size(83, 25);
            this.chat_plus_member_button.TabIndex = 4;
            this.chat_plus_member_button.Text = "대화 초대";
            this.chat_plus_member_button.UseVisualStyleBackColor = true;
            this.chat_plus_member_button.Click += new System.EventHandler(this.chat_plus_member_button_Click);
            // 
            // chat_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.chat_plus_member_button);
            this.Controls.Add(this.show_memeber_list);
            this.Controls.Add(this.chat_value_richtextbox);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.chat_send_richtextbox);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "chat_form";
            this.Text = "names~";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chat_form_FormClosing);
            this.Load += new System.EventHandler(this.chat_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chat_send_richtextbox;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.RichTextBox chat_value_richtextbox;
        private System.Windows.Forms.Label show_memeber_list;
        private System.Windows.Forms.Button chat_plus_member_button;
    }
}
namespace chat
{
    partial class user_profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(user_profile));
            this.show_user_list = new System.Windows.Forms.ListView();
            this.Title = new System.Windows.Forms.Label();
            this.tell_num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name_age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.class_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // show_user_list
            // 
            this.show_user_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_age,
            this.tell_num,
            this.ID,
            this.class_name});
            this.show_user_list.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.show_user_list.GridLines = true;
            this.show_user_list.HideSelection = false;
            this.show_user_list.Location = new System.Drawing.Point(17, 64);
            this.show_user_list.Name = "show_user_list";
            this.show_user_list.Size = new System.Drawing.Size(650, 480);
            this.show_user_list.TabIndex = 0;
            this.show_user_list.UseCompatibleStateImageBehavior = false;
            this.show_user_list.View = System.Windows.Forms.View.Details;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Title.Location = new System.Drawing.Point(249, 12);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(192, 37);
            this.Title.TabIndex = 1;
            this.Title.Text = "Co Talk Users";
            // 
            // tell_num
            // 
            this.tell_num.Text = "전화번호";
            this.tell_num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tell_num.Width = 147;
            // 
            // ID
            // 
            this.ID.Text = "아이디";
            this.ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ID.Width = 107;
            // 
            // name_age
            // 
            this.name_age.Text = "이름[나이]";
            this.name_age.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.name_age.Width = 113;
            // 
            // class_name
            // 
            this.class_name.Text = "수강과목";
            this.class_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.class_name.Width = 278;
            // 
            // user_profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.show_user_list);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "user_profile";
            this.Text = "Co Talk User Profile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView show_user_list;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ColumnHeader name_age;
        private System.Windows.Forms.ColumnHeader tell_num;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader class_name;
    }
}
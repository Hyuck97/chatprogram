namespace chat
{
    partial class chat_mem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chat_mem));
            this.activate_button = new System.Windows.Forms.Button();
            this.mem_show_checkedlistbox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // activate_button
            // 
            this.activate_button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.activate_button.Location = new System.Drawing.Point(18, 381);
            this.activate_button.Name = "activate_button";
            this.activate_button.Size = new System.Drawing.Size(350, 35);
            this.activate_button.TabIndex = 0;
            this.activate_button.Text = "button1";
            this.activate_button.UseVisualStyleBackColor = true;
            this.activate_button.Click += new System.EventHandler(this.activate_button_Click);
            // 
            // mem_show_checkedlistbox
            // 
            this.mem_show_checkedlistbox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mem_show_checkedlistbox.FormattingEnabled = true;
            this.mem_show_checkedlistbox.HorizontalScrollbar = true;
            this.mem_show_checkedlistbox.Location = new System.Drawing.Point(18, 21);
            this.mem_show_checkedlistbox.Name = "mem_show_checkedlistbox";
            this.mem_show_checkedlistbox.Size = new System.Drawing.Size(350, 344);
            this.mem_show_checkedlistbox.TabIndex = 1;
            // 
            // chat_mem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(384, 431);
            this.Controls.Add(this.mem_show_checkedlistbox);
            this.Controls.Add(this.activate_button);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "chat_mem";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button activate_button;
        private System.Windows.Forms.CheckedListBox mem_show_checkedlistbox;
    }
}
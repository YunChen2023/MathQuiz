namespace StudentApp
{
    partial class StudentForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SF_Answer_TextBox = new System.Windows.Forms.TextBox();
            this.SF_Submit_Button = new System.Windows.Forms.Button();
            this.SF_Que_TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SF_Exit_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "Student";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SF_Answer_TextBox
            // 
            this.SF_Answer_TextBox.Location = new System.Drawing.Point(165, 106);
            this.SF_Answer_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SF_Answer_TextBox.Name = "SF_Answer_TextBox";
            this.SF_Answer_TextBox.Size = new System.Drawing.Size(226, 20);
            this.SF_Answer_TextBox.TabIndex = 7;
            // 
            // SF_Submit_Button
            // 
            this.SF_Submit_Button.Location = new System.Drawing.Point(322, 166);
            this.SF_Submit_Button.Margin = new System.Windows.Forms.Padding(2);
            this.SF_Submit_Button.Name = "SF_Submit_Button";
            this.SF_Submit_Button.Size = new System.Drawing.Size(69, 21);
            this.SF_Submit_Button.TabIndex = 4;
            this.SF_Submit_Button.Text = "Submit";
            this.SF_Submit_Button.UseVisualStyleBackColor = true;
            this.SF_Submit_Button.Click += new System.EventHandler(this.SF_Submit_Button_Click);
            // 
            // SF_Que_TextBox
            // 
            this.SF_Que_TextBox.Location = new System.Drawing.Point(165, 60);
            this.SF_Que_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SF_Que_TextBox.Name = "SF_Que_TextBox";
            this.SF_Que_TextBox.Size = new System.Drawing.Size(226, 20);
            this.SF_Que_TextBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Your Answer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Question:";
            // 
            // SF_Exit_Button
            // 
            this.SF_Exit_Button.Location = new System.Drawing.Point(327, 357);
            this.SF_Exit_Button.Margin = new System.Windows.Forms.Padding(2);
            this.SF_Exit_Button.Name = "SF_Exit_Button";
            this.SF_Exit_Button.Size = new System.Drawing.Size(69, 21);
            this.SF_Exit_Button.TabIndex = 8;
            this.SF_Exit_Button.Text = "Exit";
            this.SF_Exit_Button.UseVisualStyleBackColor = true;
            this.SF_Exit_Button.Click += new System.EventHandler(this.SF_Exit_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SF_Answer_TextBox);
            this.groupBox1.Controls.Add(this.SF_Submit_Button);
            this.groupBox1.Controls.Add(this.SF_Que_TextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 62);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(428, 200);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter your answer and click SUBMIT";
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SF_Exit_Button);
            this.Controls.Add(this.groupBox1);
            this.Name = "StudentForm";
            this.Text = "Student";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StudentApp_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SF_Answer_TextBox;
        private System.Windows.Forms.Button SF_Submit_Button;
        private System.Windows.Forms.TextBox SF_Que_TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SF_Exit_Button;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


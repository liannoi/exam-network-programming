namespace Exam.Client
{
    partial class Dashboard
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
            this.carButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // carButton
            // 
            this.carButton.Location = new System.Drawing.Point(12, 12);
            this.carButton.Name = "carButton";
            this.carButton.Size = new System.Drawing.Size(140, 60);
            this.carButton.TabIndex = 0;
            this.carButton.UseVisualStyleBackColor = true;
            this.carButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CarButton_KeyUp);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 640);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(120, 30);
            this.startButton.TabIndex = 0;
            this.startButton.TabStop = false;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.carButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard - Client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button carButton;
        private System.Windows.Forms.Button startButton;
    }
}


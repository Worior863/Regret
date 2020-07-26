namespace RoboApp
{
    partial class Form2
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Kontynuacja = new System.Windows.Forms.Button();
            this.Szukanie = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 28);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(73, 122);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(204, 44);
            this.progressBar1.TabIndex = 1;
            // 
            // Kontynuacja
            // 
            this.Kontynuacja.Location = new System.Drawing.Point(138, 197);
            this.Kontynuacja.Name = "Kontynuacja";
            this.Kontynuacja.Size = new System.Drawing.Size(75, 23);
            this.Kontynuacja.TabIndex = 2;
            this.Kontynuacja.Text = "Kontynuuj";
            this.Kontynuacja.UseVisualStyleBackColor = true;
            this.Kontynuacja.Click += new System.EventHandler(this.Kontynuacja_Click);
            // 
            // Szukanie
            // 
            this.Szukanie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Szukanie_DoWork);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 278);
            this.Controls.Add(this.Kontynuacja);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Kontynuacja;
        private System.ComponentModel.BackgroundWorker Szukanie;
    }
}
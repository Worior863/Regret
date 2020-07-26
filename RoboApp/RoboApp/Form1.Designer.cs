namespace RoboApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Punkt_kon = new System.Windows.Forms.TextBox();
            this.War_kierunek = new System.Windows.Forms.TextBox();
            this.War_lewo = new System.Windows.Forms.TextBox();
            this.War_prawo = new System.Windows.Forms.TextBox();
            this.Sim = new System.Windows.Forms.CheckBox();
            this.Arcade = new System.Windows.Forms.CheckBox();
            this.W_górę = new System.ComponentModel.BackgroundWorker();
            this.Wlewo = new System.ComponentModel.BackgroundWorker();
            this.Wprawo = new System.ComponentModel.BackgroundWorker();
            this.Wdół = new System.ComponentModel.BackgroundWorker();
            this.Stop = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Tajmer = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TextBoxy = new System.ComponentModel.BackgroundWorker();
            this.Oddaj = new System.Windows.Forms.Button();
            this.Przywłaszczenie = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Obraz = new System.Windows.Forms.PictureBox();
            this.Szukaj = new System.Windows.Forms.Button();
            this.Guzik_Jazdy = new System.Windows.Forms.Button();
            this.Stany_Robotów_Wizu = new System.ComponentModel.BackgroundWorker();
            this.Stany_robotów_String = new System.ComponentModel.BackgroundWorker();
            this.Kol_Stan = new System.Windows.Forms.PictureBox();
            this.Zwalnianie_przy_jeździe_w_przód = new System.ComponentModel.BackgroundWorker();
            this.Zwalnianie_przy_jeździe_w_tył = new System.ComponentModel.BackgroundWorker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Odświeżanie = new System.Windows.Forms.Button();
            this.Jazda_do_celu = new System.ComponentModel.BackgroundWorker();
            this.PR_KĄ_PP = new System.Windows.Forms.TextBox();
            this.PR_LIN_PP = new System.Windows.Forms.TextBox();
            this.XT = new System.Windows.Forms.TextBox();
            this.YT = new System.Windows.Forms.TextBox();
            this.THT = new System.Windows.Forms.TextBox();
            this.XPP = new System.Windows.Forms.TextBox();
            this.YPP = new System.Windows.Forms.TextBox();
            this.THPP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Auto = new System.Windows.Forms.CheckBox();
            this.Sterowanie_manualne = new System.Windows.Forms.CheckBox();
            this.Sterowanie = new System.Windows.Forms.GroupBox();
            this.Tył = new RoboApp.OvalPictureBox();
            this.Prawo = new RoboApp.OvalPictureBox();
            this.Lewo = new RoboApp.OvalPictureBox();
            this.Prosto = new RoboApp.OvalPictureBox();
            this.Nie8 = new RoboApp.OvalPictureBox();
            this.Nie7 = new RoboApp.OvalPictureBox();
            this.Nie6 = new RoboApp.OvalPictureBox();
            this.Nie5 = new RoboApp.OvalPictureBox();
            this.Nie4 = new RoboApp.OvalPictureBox();
            this.Nie3 = new RoboApp.OvalPictureBox();
            this.Nie2 = new RoboApp.OvalPictureBox();
            this.Nie1 = new RoboApp.OvalPictureBox();
            this.OK8 = new RoboApp.OvalPictureBox();
            this.OK7 = new RoboApp.OvalPictureBox();
            this.OK6 = new RoboApp.OvalPictureBox();
            this.X8 = new RoboApp.OvalPictureBox();
            this.X7 = new RoboApp.OvalPictureBox();
            this.X6 = new RoboApp.OvalPictureBox();
            this.Robo8 = new RoboApp.OvalPictureBox();
            this.Robo7 = new RoboApp.OvalPictureBox();
            this.Rob6 = new RoboApp.OvalPictureBox();
            this.OK4 = new RoboApp.OvalPictureBox();
            this.OK5 = new RoboApp.OvalPictureBox();
            this.OK1 = new RoboApp.OvalPictureBox();
            this.OK3 = new RoboApp.OvalPictureBox();
            this.OK2 = new RoboApp.OvalPictureBox();
            this.X1 = new RoboApp.OvalPictureBox();
            this.X2 = new RoboApp.OvalPictureBox();
            this.X3 = new RoboApp.OvalPictureBox();
            this.X4 = new RoboApp.OvalPictureBox();
            this.X5 = new RoboApp.OvalPictureBox();
            this.Rob5 = new RoboApp.OvalPictureBox();
            this.Robo4 = new RoboApp.OvalPictureBox();
            this.Robo3 = new RoboApp.OvalPictureBox();
            this.Robo2 = new RoboApp.OvalPictureBox();
            this.Robo1 = new RoboApp.OvalPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obraz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kol_Stan)).BeginInit();
            this.Sterowanie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tył)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prawo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lewo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rob6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rob5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo1)).BeginInit();
            this.SuspendLayout();
            // 
            // Punkt_kon
            // 
            this.Punkt_kon.Enabled = false;
            this.Punkt_kon.Location = new System.Drawing.Point(266, 12);
            this.Punkt_kon.Name = "Punkt_kon";
            this.Punkt_kon.Size = new System.Drawing.Size(144, 20);
            this.Punkt_kon.TabIndex = 1;
            // 
            // War_kierunek
            // 
            this.War_kierunek.Enabled = false;
            this.War_kierunek.Location = new System.Drawing.Point(79, 4);
            this.War_kierunek.Name = "War_kierunek";
            this.War_kierunek.Size = new System.Drawing.Size(162, 20);
            this.War_kierunek.TabIndex = 3;
            // 
            // War_lewo
            // 
            this.War_lewo.Enabled = false;
            this.War_lewo.Location = new System.Drawing.Point(79, 45);
            this.War_lewo.Name = "War_lewo";
            this.War_lewo.Size = new System.Drawing.Size(76, 20);
            this.War_lewo.TabIndex = 8;
            // 
            // War_prawo
            // 
            this.War_prawo.Enabled = false;
            this.War_prawo.Location = new System.Drawing.Point(161, 45);
            this.War_prawo.Name = "War_prawo";
            this.War_prawo.Size = new System.Drawing.Size(80, 20);
            this.War_prawo.TabIndex = 9;
            // 
            // Sim
            // 
            this.Sim.AutoSize = true;
            this.Sim.BackColor = System.Drawing.Color.Transparent;
            this.Sim.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Sim.Location = new System.Drawing.Point(79, 84);
            this.Sim.Name = "Sim";
            this.Sim.Size = new System.Drawing.Size(66, 17);
            this.Sim.TabIndex = 10;
            this.Sim.Text = "Simulate";
            this.Sim.UseVisualStyleBackColor = false;
            this.Sim.CheckedChanged += new System.EventHandler(this.Sim_CheckedChanged);
            // 
            // Arcade
            // 
            this.Arcade.AutoSize = true;
            this.Arcade.BackColor = System.Drawing.Color.Transparent;
            this.Arcade.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Arcade.Location = new System.Drawing.Point(181, 84);
            this.Arcade.Name = "Arcade";
            this.Arcade.Size = new System.Drawing.Size(60, 17);
            this.Arcade.TabIndex = 11;
            this.Arcade.Text = "Arcade";
            this.Arcade.UseVisualStyleBackColor = false;
            this.Arcade.CheckedChanged += new System.EventHandler(this.Arcade_CheckedChanged);
            // 
            // W_górę
            // 
            this.W_górę.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wgórę_DoWork);
            // 
            // Wlewo
            // 
            this.Wlewo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Wlewo_DoWork);
            // 
            // Wprawo
            // 
            this.Wprawo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wprawo_DoWork);
            // 
            // Wdół
            // 
            this.Wdół.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wdół_DoWork);
            // 
            // Stop
            // 
            this.Stop.BackgroundImage = global::RoboApp.Properties.Resources.stip;
            this.Stop.Location = new System.Drawing.Point(119, 249);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(80, 80);
            this.Stop.TabIndex = 13;
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pictureBox2.BackgroundImage = global::RoboApp.Properties.Resources.stacyjka;
            this.pictureBox2.Location = new System.Drawing.Point(65, 180);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(280, 560);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // Tajmer
            // 
            this.Tajmer.Enabled = false;
            this.Tajmer.Location = new System.Drawing.Point(416, 778);
            this.Tajmer.Name = "Tajmer";
            this.Tajmer.Size = new System.Drawing.Size(144, 20);
            this.Tajmer.TabIndex = 22;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TextBoxy
            // 
            this.TextBoxy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TextBoxy_DoWork);
            // 
            // Oddaj
            // 
            this.Oddaj.Location = new System.Drawing.Point(12, 148);
            this.Oddaj.Name = "Oddaj";
            this.Oddaj.Size = new System.Drawing.Size(117, 25);
            this.Oddaj.TabIndex = 30;
            this.Oddaj.Text = "Oddanie robota";
            this.Oddaj.UseVisualStyleBackColor = true;
            this.Oddaj.Click += new System.EventHandler(this.Oddaj_Click);
            // 
            // Przywłaszczenie
            // 
            this.Przywłaszczenie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Przywłaszczenie_DoWork);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::RoboApp.Properties.Resources.podpis;
            this.label1.Location = new System.Drawing.Point(85, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 36;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Image = global::RoboApp.Properties.Resources.podpis;
            this.label2.Location = new System.Drawing.Point(225, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 37;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::RoboApp.Properties.Resources.podpis;
            this.label3.Location = new System.Drawing.Point(85, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 38;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::RoboApp.Properties.Resources.podpis;
            this.label4.Location = new System.Drawing.Point(225, 431);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 39;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Image = global::RoboApp.Properties.Resources.podpis;
            this.label5.Location = new System.Drawing.Point(85, 572);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 40;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Obraz
            // 
            this.Obraz.BackgroundImage = global::RoboApp.Properties.Resources.black_sand_paper_texture_53876_88601;
            this.Obraz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Obraz.Location = new System.Drawing.Point(416, 12);
            this.Obraz.Name = "Obraz";
            this.Obraz.Size = new System.Drawing.Size(760, 760);
            this.Obraz.TabIndex = 42;
            this.Obraz.TabStop = false;
            this.Obraz.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Obraz_MouseClick);
            // 
            // Szukaj
            // 
            this.Szukaj.Location = new System.Drawing.Point(12, 12);
            this.Szukaj.Name = "Szukaj";
            this.Szukaj.Size = new System.Drawing.Size(117, 40);
            this.Szukaj.TabIndex = 43;
            this.Szukaj.Text = "Szukanie robotów";
            this.Szukaj.UseVisualStyleBackColor = true;
            this.Szukaj.Click += new System.EventHandler(this.Szukaj_Click);
            // 
            // Guzik_Jazdy
            // 
            this.Guzik_Jazdy.Location = new System.Drawing.Point(12, 103);
            this.Guzik_Jazdy.Name = "Guzik_Jazdy";
            this.Guzik_Jazdy.Size = new System.Drawing.Size(117, 39);
            this.Guzik_Jazdy.TabIndex = 44;
            this.Guzik_Jazdy.Text = "Jazda Manualna";
            this.Guzik_Jazdy.UseVisualStyleBackColor = true;
            this.Guzik_Jazdy.Click += new System.EventHandler(this.Guzik_Jazdy_Click);
            // 
            // Stany_Robotów_Wizu
            // 
            this.Stany_Robotów_Wizu.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Stany_Robotów_Wizu_DoWork);
            // 
            // Stany_robotów_String
            // 
            this.Stany_robotów_String.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Stany_robotów_String_DoWork);
            // 
            // Kol_Stan
            // 
            this.Kol_Stan.Location = new System.Drawing.Point(331, 81);
            this.Kol_Stan.Name = "Kol_Stan";
            this.Kol_Stan.Size = new System.Drawing.Size(65, 61);
            this.Kol_Stan.TabIndex = 47;
            this.Kol_Stan.TabStop = false;
            // 
            // Zwalnianie_przy_jeździe_w_przód
            // 
            this.Zwalnianie_przy_jeździe_w_przód.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Zwalnianie_przy_jeździe_w_przód_DoWork);
            // 
            // Zwalnianie_przy_jeździe_w_tył
            // 
            this.Zwalnianie_przy_jeździe_w_tył.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Zwalnianie_przy_jeździe_w_tył_DoWork);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Image = global::RoboApp.Properties.Resources.podpis;
            this.label6.Location = new System.Drawing.Point(225, 572);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 58;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Image = global::RoboApp.Properties.Resources.podpis;
            this.label7.Location = new System.Drawing.Point(85, 713);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 59;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Image = global::RoboApp.Properties.Resources.podpis;
            this.label8.Location = new System.Drawing.Point(225, 713);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 17);
            this.label8.TabIndex = 60;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Odświeżanie
            // 
            this.Odświeżanie.Location = new System.Drawing.Point(12, 58);
            this.Odświeżanie.Name = "Odświeżanie";
            this.Odświeżanie.Size = new System.Drawing.Size(117, 39);
            this.Odświeżanie.TabIndex = 70;
            this.Odświeżanie.Text = "Odświeżanie przeszkód";
            this.Odświeżanie.UseVisualStyleBackColor = true;
            this.Odświeżanie.Click += new System.EventHandler(this.Odświeżanie_Click);
            // 
            // Jazda_do_celu
            // 
            this.Jazda_do_celu.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Jazda_do_celu_DoWork);
            // 
            // PR_KĄ_PP
            // 
            this.PR_KĄ_PP.Enabled = false;
            this.PR_KĄ_PP.Location = new System.Drawing.Point(129, 761);
            this.PR_KĄ_PP.Name = "PR_KĄ_PP";
            this.PR_KĄ_PP.Size = new System.Drawing.Size(144, 20);
            this.PR_KĄ_PP.TabIndex = 71;
            // 
            // PR_LIN_PP
            // 
            this.PR_LIN_PP.Enabled = false;
            this.PR_LIN_PP.Location = new System.Drawing.Point(129, 796);
            this.PR_LIN_PP.Name = "PR_LIN_PP";
            this.PR_LIN_PP.Size = new System.Drawing.Size(144, 20);
            this.PR_LIN_PP.TabIndex = 72;
            // 
            // XT
            // 
            this.XT.Enabled = false;
            this.XT.Location = new System.Drawing.Point(1196, 473);
            this.XT.Name = "XT";
            this.XT.Size = new System.Drawing.Size(99, 20);
            this.XT.TabIndex = 79;
            // 
            // YT
            // 
            this.YT.Enabled = false;
            this.YT.Location = new System.Drawing.Point(1311, 473);
            this.YT.Name = "YT";
            this.YT.Size = new System.Drawing.Size(113, 20);
            this.YT.TabIndex = 80;
            // 
            // THT
            // 
            this.THT.Enabled = false;
            this.THT.Location = new System.Drawing.Point(1439, 473);
            this.THT.Name = "THT";
            this.THT.Size = new System.Drawing.Size(113, 20);
            this.THT.TabIndex = 81;
            // 
            // XPP
            // 
            this.XPP.Enabled = false;
            this.XPP.Location = new System.Drawing.Point(1196, 523);
            this.XPP.Name = "XPP";
            this.XPP.Size = new System.Drawing.Size(99, 20);
            this.XPP.TabIndex = 82;
            // 
            // YPP
            // 
            this.YPP.Enabled = false;
            this.YPP.Location = new System.Drawing.Point(1311, 523);
            this.YPP.Name = "YPP";
            this.YPP.Size = new System.Drawing.Size(113, 20);
            this.YPP.TabIndex = 83;
            // 
            // THPP
            // 
            this.THPP.Enabled = false;
            this.THPP.Location = new System.Drawing.Point(1439, 523);
            this.THPP.Name = "THPP";
            this.THPP.Size = new System.Drawing.Size(113, 20);
            this.THPP.TabIndex = 84;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(181, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 85;
            this.label9.Text = "Wybrany punkt";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(1340, 447);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 86;
            this.label10.Text = "Układ stołu";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(1340, 556);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 87;
            this.label11.Text = "Układ PP";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(566, 778);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 88;
            this.label12.Text = "Tajmer";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(281, 764);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 89;
            this.label13.Text = "Prędkość kątowa PP";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(279, 799);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 13);
            this.label14.TabIndex = 90;
            this.label14.Text = "Prędkość liniowa PP";
            // 
            // Auto
            // 
            this.Auto.AutoSize = true;
            this.Auto.BackColor = System.Drawing.Color.Transparent;
            this.Auto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Auto.Location = new System.Drawing.Point(146, 53);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(148, 17);
            this.Auto.TabIndex = 92;
            this.Auto.Text = "Sterowanie automatyczne";
            this.Auto.UseVisualStyleBackColor = false;
            this.Auto.CheckedChanged += new System.EventHandler(this.Auto_CheckedChanged);
            // 
            // Sterowanie_manualne
            // 
            this.Sterowanie_manualne.AutoSize = true;
            this.Sterowanie_manualne.BackColor = System.Drawing.Color.Transparent;
            this.Sterowanie_manualne.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Sterowanie_manualne.Location = new System.Drawing.Point(146, 80);
            this.Sterowanie_manualne.Name = "Sterowanie_manualne";
            this.Sterowanie_manualne.Size = new System.Drawing.Size(128, 17);
            this.Sterowanie_manualne.TabIndex = 91;
            this.Sterowanie_manualne.Text = "Sterowanie manualne";
            this.Sterowanie_manualne.UseVisualStyleBackColor = false;
            this.Sterowanie_manualne.CheckedChanged += new System.EventHandler(this.Sterowanie_manualne_CheckedChanged);
            // 
            // Sterowanie
            // 
            this.Sterowanie.BackColor = System.Drawing.Color.Transparent;
            this.Sterowanie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Sterowanie.Controls.Add(this.Stop);
            this.Sterowanie.Controls.Add(this.Arcade);
            this.Sterowanie.Controls.Add(this.Sim);
            this.Sterowanie.Controls.Add(this.War_prawo);
            this.Sterowanie.Controls.Add(this.War_lewo);
            this.Sterowanie.Controls.Add(this.Tył);
            this.Sterowanie.Controls.Add(this.Prawo);
            this.Sterowanie.Controls.Add(this.Lewo);
            this.Sterowanie.Controls.Add(this.Prosto);
            this.Sterowanie.Controls.Add(this.War_kierunek);
            this.Sterowanie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Sterowanie.Location = new System.Drawing.Point(1221, 9);
            this.Sterowanie.Name = "Sterowanie";
            this.Sterowanie.Size = new System.Drawing.Size(318, 435);
            this.Sterowanie.TabIndex = 93;
            this.Sterowanie.TabStop = false;
            this.Sterowanie.Text = "groupBox1";
            // 
            // Tył
            // 
            this.Tył.BackColor = System.Drawing.Color.DarkGray;
            this.Tył.Image = ((System.Drawing.Image)(resources.GetObject("Tył.Image")));
            this.Tył.Location = new System.Drawing.Point(119, 335);
            this.Tył.Name = "Tył";
            this.Tył.Size = new System.Drawing.Size(80, 80);
            this.Tył.TabIndex = 7;
            this.Tył.TabStop = false;
            this.Tył.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tył_MouseDown);
            this.Tył.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tył_MouseUp);
            // 
            // Prawo
            // 
            this.Prawo.BackColor = System.Drawing.Color.DarkGray;
            this.Prawo.Image = ((System.Drawing.Image)(resources.GetObject("Prawo.Image")));
            this.Prawo.Location = new System.Drawing.Point(205, 249);
            this.Prawo.Name = "Prawo";
            this.Prawo.Size = new System.Drawing.Size(80, 80);
            this.Prawo.TabIndex = 6;
            this.Prawo.TabStop = false;
            this.Prawo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Prawo_MouseDown);
            this.Prawo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Prawo_MouseUp);
            // 
            // Lewo
            // 
            this.Lewo.BackColor = System.Drawing.Color.DarkGray;
            this.Lewo.Image = ((System.Drawing.Image)(resources.GetObject("Lewo.Image")));
            this.Lewo.Location = new System.Drawing.Point(33, 249);
            this.Lewo.Name = "Lewo";
            this.Lewo.Size = new System.Drawing.Size(80, 80);
            this.Lewo.TabIndex = 5;
            this.Lewo.TabStop = false;
            this.Lewo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lewo_MouseDown);
            this.Lewo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Lewo_MouseUp);
            // 
            // Prosto
            // 
            this.Prosto.BackColor = System.Drawing.Color.DarkGray;
            this.Prosto.Image = ((System.Drawing.Image)(resources.GetObject("Prosto.Image")));
            this.Prosto.Location = new System.Drawing.Point(119, 163);
            this.Prosto.Name = "Prosto";
            this.Prosto.Size = new System.Drawing.Size(80, 80);
            this.Prosto.TabIndex = 4;
            this.Prosto.TabStop = false;
            this.Prosto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Prosto_MouseDown);
            this.Prosto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Prosto_MouseUp);
            // 
            // Nie8
            // 
            this.Nie8.BackColor = System.Drawing.Color.Transparent;
            this.Nie8.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie8.Location = new System.Drawing.Point(235, 630);
            this.Nie8.Name = "Nie8";
            this.Nie8.Size = new System.Drawing.Size(80, 80);
            this.Nie8.TabIndex = 68;
            this.Nie8.TabStop = false;
            // 
            // Nie7
            // 
            this.Nie7.BackColor = System.Drawing.Color.Transparent;
            this.Nie7.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie7.Location = new System.Drawing.Point(95, 630);
            this.Nie7.Name = "Nie7";
            this.Nie7.Size = new System.Drawing.Size(80, 80);
            this.Nie7.TabIndex = 67;
            this.Nie7.TabStop = false;
            // 
            // Nie6
            // 
            this.Nie6.BackColor = System.Drawing.Color.Transparent;
            this.Nie6.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie6.Location = new System.Drawing.Point(235, 489);
            this.Nie6.Name = "Nie6";
            this.Nie6.Size = new System.Drawing.Size(80, 80);
            this.Nie6.TabIndex = 66;
            this.Nie6.TabStop = false;
            // 
            // Nie5
            // 
            this.Nie5.BackColor = System.Drawing.Color.Transparent;
            this.Nie5.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie5.Location = new System.Drawing.Point(95, 489);
            this.Nie5.Name = "Nie5";
            this.Nie5.Size = new System.Drawing.Size(80, 80);
            this.Nie5.TabIndex = 65;
            this.Nie5.TabStop = false;
            // 
            // Nie4
            // 
            this.Nie4.BackColor = System.Drawing.Color.Transparent;
            this.Nie4.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie4.Location = new System.Drawing.Point(235, 348);
            this.Nie4.Name = "Nie4";
            this.Nie4.Size = new System.Drawing.Size(80, 80);
            this.Nie4.TabIndex = 64;
            this.Nie4.TabStop = false;
            // 
            // Nie3
            // 
            this.Nie3.BackColor = System.Drawing.Color.Transparent;
            this.Nie3.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie3.Location = new System.Drawing.Point(95, 348);
            this.Nie3.Name = "Nie3";
            this.Nie3.Size = new System.Drawing.Size(80, 80);
            this.Nie3.TabIndex = 63;
            this.Nie3.TabStop = false;
            // 
            // Nie2
            // 
            this.Nie2.BackColor = System.Drawing.Color.Transparent;
            this.Nie2.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie2.Location = new System.Drawing.Point(235, 207);
            this.Nie2.Name = "Nie2";
            this.Nie2.Size = new System.Drawing.Size(80, 80);
            this.Nie2.TabIndex = 62;
            this.Nie2.TabStop = false;
            // 
            // Nie1
            // 
            this.Nie1.BackColor = System.Drawing.Color.Transparent;
            this.Nie1.BackgroundImage = global::RoboApp.Properties.Resources._2;
            this.Nie1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Nie1.Location = new System.Drawing.Point(95, 207);
            this.Nie1.Name = "Nie1";
            this.Nie1.Size = new System.Drawing.Size(80, 80);
            this.Nie1.TabIndex = 61;
            this.Nie1.TabStop = false;
            // 
            // OK8
            // 
            this.OK8.BackColor = System.Drawing.Color.Transparent;
            this.OK8.BackgroundImage = global::RoboApp.Properties.Resources._12321;
            this.OK8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK8.Location = new System.Drawing.Point(235, 630);
            this.OK8.Name = "OK8";
            this.OK8.Size = new System.Drawing.Size(80, 80);
            this.OK8.TabIndex = 57;
            this.OK8.TabStop = false;
            // 
            // OK7
            // 
            this.OK7.BackColor = System.Drawing.Color.Transparent;
            this.OK7.BackgroundImage = global::RoboApp.Properties.Resources._12321;
            this.OK7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK7.Location = new System.Drawing.Point(95, 630);
            this.OK7.Name = "OK7";
            this.OK7.Size = new System.Drawing.Size(80, 80);
            this.OK7.TabIndex = 56;
            this.OK7.TabStop = false;
            // 
            // OK6
            // 
            this.OK6.BackColor = System.Drawing.Color.Transparent;
            this.OK6.BackgroundImage = global::RoboApp.Properties.Resources._12321;
            this.OK6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK6.Location = new System.Drawing.Point(235, 489);
            this.OK6.Name = "OK6";
            this.OK6.Size = new System.Drawing.Size(80, 80);
            this.OK6.TabIndex = 55;
            this.OK6.TabStop = false;
            // 
            // X8
            // 
            this.X8.BackColor = System.Drawing.Color.Transparent;
            this.X8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X8.Image = ((System.Drawing.Image)(resources.GetObject("X8.Image")));
            this.X8.Location = new System.Drawing.Point(235, 630);
            this.X8.Name = "X8";
            this.X8.Size = new System.Drawing.Size(80, 80);
            this.X8.TabIndex = 54;
            this.X8.TabStop = false;
            this.X8.Click += new System.EventHandler(this.X8_Click);
            // 
            // X7
            // 
            this.X7.BackColor = System.Drawing.Color.Transparent;
            this.X7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X7.Image = ((System.Drawing.Image)(resources.GetObject("X7.Image")));
            this.X7.Location = new System.Drawing.Point(95, 630);
            this.X7.Name = "X7";
            this.X7.Size = new System.Drawing.Size(80, 80);
            this.X7.TabIndex = 53;
            this.X7.TabStop = false;
            this.X7.Click += new System.EventHandler(this.X7_Click);
            // 
            // X6
            // 
            this.X6.BackColor = System.Drawing.Color.Transparent;
            this.X6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X6.Image = ((System.Drawing.Image)(resources.GetObject("X6.Image")));
            this.X6.Location = new System.Drawing.Point(235, 489);
            this.X6.Name = "X6";
            this.X6.Size = new System.Drawing.Size(80, 80);
            this.X6.TabIndex = 52;
            this.X6.TabStop = false;
            this.X6.Click += new System.EventHandler(this.X6_Click);
            // 
            // Robo8
            // 
            this.Robo8.BackColor = System.Drawing.Color.DarkGray;
            this.Robo8.Image = ((System.Drawing.Image)(resources.GetObject("Robo8.Image")));
            this.Robo8.Location = new System.Drawing.Point(235, 630);
            this.Robo8.Name = "Robo8";
            this.Robo8.Size = new System.Drawing.Size(80, 80);
            this.Robo8.TabIndex = 51;
            this.Robo8.TabStop = false;
            this.Robo8.Click += new System.EventHandler(this.Robo8_Click);
            // 
            // Robo7
            // 
            this.Robo7.BackColor = System.Drawing.Color.DarkGray;
            this.Robo7.Image = ((System.Drawing.Image)(resources.GetObject("Robo7.Image")));
            this.Robo7.Location = new System.Drawing.Point(95, 630);
            this.Robo7.Name = "Robo7";
            this.Robo7.Size = new System.Drawing.Size(80, 80);
            this.Robo7.TabIndex = 50;
            this.Robo7.TabStop = false;
            this.Robo7.Click += new System.EventHandler(this.Robo7_Click);
            // 
            // Rob6
            // 
            this.Rob6.BackColor = System.Drawing.Color.DarkGray;
            this.Rob6.Image = ((System.Drawing.Image)(resources.GetObject("Rob6.Image")));
            this.Rob6.Location = new System.Drawing.Point(235, 489);
            this.Rob6.Name = "Rob6";
            this.Rob6.Size = new System.Drawing.Size(80, 80);
            this.Rob6.TabIndex = 49;
            this.Rob6.TabStop = false;
            this.Rob6.Click += new System.EventHandler(this.Rob6_Click);
            // 
            // OK4
            // 
            this.OK4.BackColor = System.Drawing.Color.Transparent;
            this.OK4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK4.Image = ((System.Drawing.Image)(resources.GetObject("OK4.Image")));
            this.OK4.Location = new System.Drawing.Point(235, 348);
            this.OK4.Name = "OK4";
            this.OK4.Size = new System.Drawing.Size(80, 80);
            this.OK4.TabIndex = 35;
            this.OK4.TabStop = false;
            // 
            // OK5
            // 
            this.OK5.BackColor = System.Drawing.Color.Transparent;
            this.OK5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK5.Image = ((System.Drawing.Image)(resources.GetObject("OK5.Image")));
            this.OK5.Location = new System.Drawing.Point(95, 489);
            this.OK5.Name = "OK5";
            this.OK5.Size = new System.Drawing.Size(80, 80);
            this.OK5.TabIndex = 34;
            this.OK5.TabStop = false;
            // 
            // OK1
            // 
            this.OK1.BackColor = System.Drawing.Color.Transparent;
            this.OK1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK1.Image = ((System.Drawing.Image)(resources.GetObject("OK1.Image")));
            this.OK1.Location = new System.Drawing.Point(95, 207);
            this.OK1.Name = "OK1";
            this.OK1.Size = new System.Drawing.Size(80, 80);
            this.OK1.TabIndex = 33;
            this.OK1.TabStop = false;
            // 
            // OK3
            // 
            this.OK3.BackColor = System.Drawing.Color.Transparent;
            this.OK3.BackgroundImage = global::RoboApp.Properties.Resources._12321;
            this.OK3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK3.Location = new System.Drawing.Point(95, 348);
            this.OK3.Name = "OK3";
            this.OK3.Size = new System.Drawing.Size(80, 80);
            this.OK3.TabIndex = 32;
            this.OK3.TabStop = false;
            // 
            // OK2
            // 
            this.OK2.BackColor = System.Drawing.Color.Transparent;
            this.OK2.BackgroundImage = global::RoboApp.Properties.Resources._12321;
            this.OK2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK2.Location = new System.Drawing.Point(235, 207);
            this.OK2.Name = "OK2";
            this.OK2.Size = new System.Drawing.Size(80, 80);
            this.OK2.TabIndex = 31;
            this.OK2.TabStop = false;
            // 
            // X1
            // 
            this.X1.BackColor = System.Drawing.Color.Transparent;
            this.X1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X1.Image = ((System.Drawing.Image)(resources.GetObject("X1.Image")));
            this.X1.Location = new System.Drawing.Point(95, 207);
            this.X1.Name = "X1";
            this.X1.Size = new System.Drawing.Size(80, 80);
            this.X1.TabIndex = 29;
            this.X1.TabStop = false;
            this.X1.Click += new System.EventHandler(this.ovalPictureBox14_Click);
            // 
            // X2
            // 
            this.X2.BackColor = System.Drawing.Color.Transparent;
            this.X2.BackgroundImage = global::RoboApp.Properties.Resources.iks;
            this.X2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X2.Location = new System.Drawing.Point(235, 207);
            this.X2.Name = "X2";
            this.X2.Size = new System.Drawing.Size(80, 80);
            this.X2.TabIndex = 28;
            this.X2.TabStop = false;
            this.X2.Click += new System.EventHandler(this.ovalPictureBox13_Click);
            // 
            // X3
            // 
            this.X3.BackColor = System.Drawing.Color.Transparent;
            this.X3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X3.Image = ((System.Drawing.Image)(resources.GetObject("X3.Image")));
            this.X3.Location = new System.Drawing.Point(95, 348);
            this.X3.Name = "X3";
            this.X3.Size = new System.Drawing.Size(80, 80);
            this.X3.TabIndex = 27;
            this.X3.TabStop = false;
            this.X3.Click += new System.EventHandler(this.ovalPictureBox12_Click);
            // 
            // X4
            // 
            this.X4.BackColor = System.Drawing.Color.Transparent;
            this.X4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X4.Image = ((System.Drawing.Image)(resources.GetObject("X4.Image")));
            this.X4.Location = new System.Drawing.Point(235, 348);
            this.X4.Name = "X4";
            this.X4.Size = new System.Drawing.Size(80, 80);
            this.X4.TabIndex = 26;
            this.X4.TabStop = false;
            this.X4.Click += new System.EventHandler(this.ovalPictureBox10_Click);
            // 
            // X5
            // 
            this.X5.BackColor = System.Drawing.Color.Transparent;
            this.X5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.X5.Image = ((System.Drawing.Image)(resources.GetObject("X5.Image")));
            this.X5.Location = new System.Drawing.Point(95, 489);
            this.X5.Name = "X5";
            this.X5.Size = new System.Drawing.Size(80, 80);
            this.X5.TabIndex = 25;
            this.X5.TabStop = false;
            this.X5.Click += new System.EventHandler(this.ovalPictureBox11_Click);
            // 
            // Rob5
            // 
            this.Rob5.BackColor = System.Drawing.Color.DarkGray;
            this.Rob5.Image = ((System.Drawing.Image)(resources.GetObject("Rob5.Image")));
            this.Rob5.Location = new System.Drawing.Point(95, 489);
            this.Rob5.Name = "Rob5";
            this.Rob5.Size = new System.Drawing.Size(80, 80);
            this.Rob5.TabIndex = 18;
            this.Rob5.TabStop = false;
            this.Rob5.Click += new System.EventHandler(this.ovalPictureBox9_Click);
            // 
            // Robo4
            // 
            this.Robo4.BackColor = System.Drawing.Color.DarkGray;
            this.Robo4.Image = ((System.Drawing.Image)(resources.GetObject("Robo4.Image")));
            this.Robo4.Location = new System.Drawing.Point(235, 348);
            this.Robo4.Name = "Robo4";
            this.Robo4.Size = new System.Drawing.Size(80, 80);
            this.Robo4.TabIndex = 17;
            this.Robo4.TabStop = false;
            this.Robo4.Click += new System.EventHandler(this.ovalPictureBox8_Click);
            // 
            // Robo3
            // 
            this.Robo3.BackColor = System.Drawing.Color.DarkGray;
            this.Robo3.Image = ((System.Drawing.Image)(resources.GetObject("Robo3.Image")));
            this.Robo3.Location = new System.Drawing.Point(95, 348);
            this.Robo3.Name = "Robo3";
            this.Robo3.Size = new System.Drawing.Size(80, 80);
            this.Robo3.TabIndex = 16;
            this.Robo3.TabStop = false;
            this.Robo3.Click += new System.EventHandler(this.ovalPictureBox7_Click);
            // 
            // Robo2
            // 
            this.Robo2.BackColor = System.Drawing.Color.White;
            this.Robo2.BackgroundImage = global::RoboApp.Properties.Resources.robo;
            this.Robo2.Location = new System.Drawing.Point(235, 207);
            this.Robo2.Name = "Robo2";
            this.Robo2.Size = new System.Drawing.Size(80, 80);
            this.Robo2.TabIndex = 15;
            this.Robo2.TabStop = false;
            this.Robo2.Click += new System.EventHandler(this.ovalPictureBox6_Click);
            // 
            // Robo1
            // 
            this.Robo1.BackColor = System.Drawing.Color.DarkGray;
            this.Robo1.Image = ((System.Drawing.Image)(resources.GetObject("Robo1.Image")));
            this.Robo1.Location = new System.Drawing.Point(95, 207);
            this.Robo1.Name = "Robo1";
            this.Robo1.Size = new System.Drawing.Size(80, 80);
            this.Robo1.TabIndex = 14;
            this.Robo1.TabStop = false;
            this.Robo1.Click += new System.EventHandler(this.ovalPictureBox5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.Sterowanie);
            this.Controls.Add(this.Auto);
            this.Controls.Add(this.Sterowanie_manualne);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.THPP);
            this.Controls.Add(this.YPP);
            this.Controls.Add(this.XPP);
            this.Controls.Add(this.THT);
            this.Controls.Add(this.YT);
            this.Controls.Add(this.XT);
            this.Controls.Add(this.PR_LIN_PP);
            this.Controls.Add(this.PR_KĄ_PP);
            this.Controls.Add(this.Odświeżanie);
            this.Controls.Add(this.Nie8);
            this.Controls.Add(this.Nie7);
            this.Controls.Add(this.Nie6);
            this.Controls.Add(this.Nie5);
            this.Controls.Add(this.Nie4);
            this.Controls.Add(this.Nie3);
            this.Controls.Add(this.Nie2);
            this.Controls.Add(this.Nie1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OK8);
            this.Controls.Add(this.OK7);
            this.Controls.Add(this.OK6);
            this.Controls.Add(this.X8);
            this.Controls.Add(this.X7);
            this.Controls.Add(this.X6);
            this.Controls.Add(this.Robo8);
            this.Controls.Add(this.Robo7);
            this.Controls.Add(this.Rob6);
            this.Controls.Add(this.Kol_Stan);
            this.Controls.Add(this.Guzik_Jazdy);
            this.Controls.Add(this.Szukaj);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK4);
            this.Controls.Add(this.OK5);
            this.Controls.Add(this.OK1);
            this.Controls.Add(this.OK3);
            this.Controls.Add(this.OK2);
            this.Controls.Add(this.Oddaj);
            this.Controls.Add(this.X1);
            this.Controls.Add(this.X2);
            this.Controls.Add(this.X3);
            this.Controls.Add(this.X4);
            this.Controls.Add(this.X5);
            this.Controls.Add(this.Tajmer);
            this.Controls.Add(this.Rob5);
            this.Controls.Add(this.Robo4);
            this.Controls.Add(this.Robo3);
            this.Controls.Add(this.Robo2);
            this.Controls.Add(this.Robo1);
            this.Controls.Add(this.Punkt_kon);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Obraz);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "RoboApp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obraz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kol_Stan)).EndInit();
            this.Sterowanie.ResumeLayout(false);
            this.Sterowanie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tył)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prawo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lewo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nie1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rob6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OK2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rob5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Robo1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Punkt_kon;
        private System.Windows.Forms.TextBox War_kierunek;
        private OvalPictureBox Prosto;
        private OvalPictureBox Lewo;
        private OvalPictureBox Prawo;
        private OvalPictureBox Tył;
        private System.Windows.Forms.TextBox War_lewo;
        private System.Windows.Forms.TextBox War_prawo;
        private System.Windows.Forms.CheckBox Sim;
        private System.Windows.Forms.CheckBox Arcade;
        private System.ComponentModel.BackgroundWorker W_górę;
        private System.ComponentModel.BackgroundWorker Wlewo;
        private System.ComponentModel.BackgroundWorker Wprawo;
        private System.ComponentModel.BackgroundWorker Wdół;
        private System.Windows.Forms.Button Stop;
        private OvalPictureBox Robo1;
        private OvalPictureBox Robo2;
        private OvalPictureBox Robo3;
        private OvalPictureBox Robo4;
        private OvalPictureBox Rob5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox Tajmer;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker TextBoxy;
        private OvalPictureBox X5;
        private OvalPictureBox X4;
        private OvalPictureBox X3;
        private OvalPictureBox X2;
        private OvalPictureBox X1;
        private System.Windows.Forms.Button Oddaj;
        private System.ComponentModel.BackgroundWorker Przywłaszczenie;
        private OvalPictureBox OK2;
        private OvalPictureBox OK3;
        private OvalPictureBox OK1;
        private OvalPictureBox OK5;
        private OvalPictureBox OK4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox Obraz;
        private System.Windows.Forms.Button Szukaj;
        private System.Windows.Forms.Button Guzik_Jazdy;
        private System.ComponentModel.BackgroundWorker Stany_Robotów_Wizu;
        private System.ComponentModel.BackgroundWorker Stany_robotów_String;
        private System.Windows.Forms.PictureBox Kol_Stan;
        private System.ComponentModel.BackgroundWorker Zwalnianie_przy_jeździe_w_przód;
        private System.ComponentModel.BackgroundWorker Zwalnianie_przy_jeździe_w_tył;
        private OvalPictureBox Rob6;
        private OvalPictureBox Robo7;
        private OvalPictureBox Robo8;
        private OvalPictureBox X6;
        private OvalPictureBox X7;
        private OvalPictureBox X8;
        private OvalPictureBox OK6;
        private OvalPictureBox OK7;
        private OvalPictureBox OK8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private OvalPictureBox Nie1;
        private OvalPictureBox Nie2;
        private OvalPictureBox Nie3;
        private OvalPictureBox Nie4;
        private OvalPictureBox Nie5;
        private OvalPictureBox Nie6;
        private OvalPictureBox Nie7;
        private OvalPictureBox Nie8;
        private System.Windows.Forms.Button Odświeżanie;
        private System.ComponentModel.BackgroundWorker Jazda_do_celu;
        private System.Windows.Forms.TextBox PR_KĄ_PP;
        private System.Windows.Forms.TextBox PR_LIN_PP;
        private System.Windows.Forms.TextBox XT;
        private System.Windows.Forms.TextBox YT;
        private System.Windows.Forms.TextBox THT;
        private System.Windows.Forms.TextBox XPP;
        private System.Windows.Forms.TextBox YPP;
        private System.Windows.Forms.TextBox THPP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox Auto;
        private System.Windows.Forms.CheckBox Sterowanie_manualne;
        private System.Windows.Forms.GroupBox Sterowanie;
    }
}


namespace ModManager
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.heroic = new System.Windows.Forms.Button();
            this.steam = new System.Windows.Forms.Button();
            this.epicgames = new System.Windows.Forms.Button();
            this.rockstarlauncher = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GamePath = new System.Windows.Forms.LinkLabel();
            this.discord = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.modinstall = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(45, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Modları Aç";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(274, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 31);
            this.button2.TabIndex = 1;
            this.button2.Text = "Modları Kapat";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // heroic
            // 
            this.heroic.Location = new System.Drawing.Point(337, 192);
            this.heroic.Name = "heroic";
            this.heroic.Size = new System.Drawing.Size(104, 32);
            this.heroic.TabIndex = 2;
            this.heroic.Text = "Heroic Launcher";
            this.heroic.UseVisualStyleBackColor = true;
            this.heroic.Click += new System.EventHandler(this.Heroic_Click);
            // 
            // steam
            // 
            this.steam.Location = new System.Drawing.Point(45, 192);
            this.steam.Name = "steam";
            this.steam.Size = new System.Drawing.Size(75, 32);
            this.steam.TabIndex = 3;
            this.steam.Text = "Steam";
            this.steam.UseVisualStyleBackColor = true;
            this.steam.Click += new System.EventHandler(this.Steam_Click);
            // 
            // epicgames
            // 
            this.epicgames.Location = new System.Drawing.Point(128, 192);
            this.epicgames.Name = "epicgames";
            this.epicgames.Size = new System.Drawing.Size(75, 32);
            this.epicgames.TabIndex = 4;
            this.epicgames.Text = "Epic Games";
            this.epicgames.UseVisualStyleBackColor = true;
            this.epicgames.Click += new System.EventHandler(this.Epicgames_Click);
            // 
            // rockstarlauncher
            // 
            this.rockstarlauncher.Location = new System.Drawing.Point(209, 192);
            this.rockstarlauncher.Name = "rockstarlauncher";
            this.rockstarlauncher.Size = new System.Drawing.Size(122, 32);
            this.rockstarlauncher.TabIndex = 5;
            this.rockstarlauncher.Text = "Rockstar Launcher";
            this.rockstarlauncher.UseVisualStyleBackColor = true;
            this.rockstarlauncher.Click += new System.EventHandler(this.Rockstarlauncher_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(42, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Oyunu başlatmak için kullandığınız başlatıcıyı(launcher) seçin.";
            // 
            // GamePath
            // 
            this.GamePath.AutoSize = true;
            this.GamePath.BackColor = System.Drawing.Color.Transparent;
            this.GamePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.GamePath.Location = new System.Drawing.Point(228, 393);
            this.GamePath.Name = "GamePath";
            this.GamePath.Size = new System.Drawing.Size(178, 22);
            this.GamePath.TabIndex = 8;
            this.GamePath.TabStop = true;
            this.GamePath.Text = "Game Path · Tarık B.";
            this.GamePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // discord
            // 
            this.discord.AutoSize = true;
            this.discord.BackColor = System.Drawing.Color.Transparent;
            this.discord.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.discord.Location = new System.Drawing.Point(228, 360);
            this.discord.Name = "discord";
            this.discord.Size = new System.Drawing.Size(199, 22);
            this.discord.TabIndex = 9;
            this.discord.TabStop = true;
            this.discord.Text = "discord.gg/3TKaVsUjaJ";
            this.discord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Discord_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::ModManager.Properties.Resources.GAME_PATH_FOTO;
            this.pictureBox1.Location = new System.Drawing.Point(45, 283);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 144);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // modinstall
            // 
            this.modinstall.Location = new System.Drawing.Point(209, 92);
            this.modinstall.Name = "modinstall";
            this.modinstall.Size = new System.Drawing.Size(68, 23);
            this.modinstall.TabIndex = 10;
            this.modinstall.Text = "Mod Kur";
            this.modinstall.UseVisualStyleBackColor = true;
            this.modinstall.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ModManager.Properties.Resources.index;
            this.ClientSize = new System.Drawing.Size(496, 473);
            this.Controls.Add(this.modinstall);
            this.Controls.Add(this.discord);
            this.Controls.Add(this.GamePath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rockstarlauncher);
            this.Controls.Add(this.epicgames);
            this.Controls.Add(this.steam);
            this.Controls.Add(this.heroic);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RDR2 Mod Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button heroic;
        private System.Windows.Forms.Button steam;
        private System.Windows.Forms.Button epicgames;
        private System.Windows.Forms.Button rockstarlauncher;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel GamePath;
        private System.Windows.Forms.LinkLabel discord;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button modinstall;
    }
}


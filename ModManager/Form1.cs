using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace ModManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Utility.CleanUpOtherVersions();
            InitializeComponent();
            CheckForUpdates();
        }
        public static class AppInfo
        {
            public static string Version = "1.1.2";
        }
        private async void CheckForUpdates()
        {
            DisableControls(this);
            try
            {
                string apiUrl = "https://api.sypnex.net/mmversion";
                string latestVersion = await GetLatestVersion(apiUrl);
                if (latestVersion != AppInfo.Version)
                {
                    DialogResult result = MessageBox.Show("Yeni bir güncelleme bulunmaktadır. Şimdi güncellemek ister misiniz?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        await Utility.Update(latestVersion);
                        System.Environment.Exit(0);
                    }
                    else
                    { System.Environment.Exit(0); }
                }
                else
                { EnableControls(this); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> GetLatestVersion(string apiUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new HttpRequestException($"HTTP hata kodu: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Veri alımı sırasında hata oluştu: {ex.Message}", ex);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Utility.OpenMods();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Utility.CloseMods();
        }

        private void Epicgames_Click(object sender, EventArgs e)
        {
            string url = "com.epicgames.launcher://apps/b30b6d1b4dfd4dcc93b5490be5e094e5%3A22a7b503221442daa2fb16ad37b6ccbf%3AHeather?action=launch&silent=true";
            Utility.RunProgram(url);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/@GamePathTB/videos");
        }

        private void Discord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/3TKaVsUjaJ");
        }

        private void Steam_Click(object sender, EventArgs e)
        {
            string url = "steam://rungameid/1174180";
            Utility.RunProgram(url);
        }

        private void Rockstarlauncher_Click(object sender, EventArgs e)
        {
            Utility.RunProgram(null);
        }

        private void Heroic_Click(object sender, EventArgs e)
        {
            string url = "heroic://launch/legendary/Heather";
            Utility.RunProgram(url);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DisableControls(this);
            Modinstaller Modinstaller = new Modinstaller();
            Modinstaller.Show();
            Modinstaller.FormClosed += (s, args) =>
            {
                EnableControls(this);
            };
        }
        private void DisableControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                DisableControls(c);
            }

            control.Enabled = false;
        }
        private void EnableControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                EnableControls(c);
            }

            control.Enabled = true;
        }
    }
}

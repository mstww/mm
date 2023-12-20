using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using Newtonsoft.Json;

namespace ModManager
{
    public partial class Modinstaller : Form
    {
        public Modinstaller()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Utility.MODInstall("https://api.sypnex.net/file?q=download&filename=scripthook", scripthookinstall);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Utility.MODInstall("https://api.sypnex.net/file?q=download&filename=tryama", turkishpatchinstall);
        }

        private void Turkishpatchremove_Click(object sender, EventArgs e)
        {
            Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=tryama", turkishpatchremove);
        }

        private void Scripthookremove_Click(object sender, EventArgs e)
        {
            Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=scripthook", scripthookremove);
        }

        private void Lmlinstall_Click(object sender, EventArgs e)
        {
            Utility.MODInstall("https://api.sypnex.net/file?q=download&filename=lml", Lmlinstall);
        }

        private void Lmlremove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu işlem lml klasörü içerisindeki modları tamamen silecektir devam etmek istiyormusunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=lml", Lmlremove);
            }
        }

        private void FontFixInstall_Click(object sender, EventArgs e)
        {
            Utility.MODInstall("https://api.sypnex.net/file?q=download&filename=fontfix", FontFixInstall);
        }

        private void FontFixUninstall_Click(object sender, EventArgs e)
        {
            Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=fontfix", FontFixUninstall);
        }

        private void FontFixNewInstall_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nexusmods.com/reddeadredemption2/mods/3078");
            MessageBox.Show("Nexus Mods'dan indirdiğin RAR dosyası içerisini lml klasörü içerisine çıkartın. Daha sonra oyuna girebilirsiniz. (LENNYS MOD LOADER YÜKLÜ OLMALIDIR.)", "Kurulum", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FontFixNewUninstall_Click(object sender, EventArgs e)
        {
            Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=trfont", FontFixNewUninstall);
        }

        private void LmlCRInstall_Click(object sender, EventArgs e)
        {
            Utility.MODInstall("https://api.sypnex.net/file?q=download&filename=lmlcr", LmlCRInstall);
        }

        private void LmlCRUninstall_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu işlem lml klasörü içerisindeki modları tamamen silecektir devam etmek istiyormusunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Utility.MODUninstall("https://api.sypnex.net/file?q=filemap&filename=lmlcr", LmlCRUninstall);
            }
        }
    }
}
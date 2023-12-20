using Newtonsoft.Json;
using SharpCompress.Archives.Rar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
public static class Utility
{
    static void RunCommand(string fileName, string arguments)
    {
        ProcessStartInfo psi = new ProcessStartInfo(fileName)
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = fileName,
            Arguments = arguments
        };

        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();
        }
    }
    public static void RunProgram(string url)
    {
        try
        {
            if (!string.IsNullOrEmpty(url))
            {
                RunCommand("cmd.exe", $"/C start {url}");
            }
            else
            {
                Process.Start("PlayRDR2.exe");
            }
        }
        catch (Exception error)
        {
            MessageBox.Show($"Hata oluştu: {error}");
        }
    }

    public static void OpenMods()
    {
        try
        {
            ModManagerConfig config = ConfigManager.LoadConfig();
            string folderPath = config.RDR2InstallLocation;
            string[] fileLists = Directory.GetFiles(folderPath);
            int changes = 0;
            int find = 0;
            foreach (string filePath in fileLists)
            {
                string fileName = Path.GetFileName(filePath);
                string[] oldNames = { "dinput8dll", "versiondll", "ScriptHookRDR2dll", "SimpleHookdll", "ModManager.Coredll", "NativeInteropdll", "EasyLoad64dll", "EasyHook64dll", "EasyHookdll" };
                string[] newNames = { "dinput8.dll", "version.dll", "ScriptHookRDR2.dll", "SimpleHook.dll", "ModManager.Core.dll", "NativeInterop.dll", "EasyLoad64.dll", "EasyHook64.dll", "EasyHook.dll" };
                for (int i = 0; i < oldNames.Length && i < newNames.Length; i++)
                {
                    string oldName = oldNames[i];
                    string newName = newNames[i];
                    if (fileName.Contains(oldName))
                    {
                        string newFileName = fileName.Replace(oldName, newName);
                        string newFilePath = Path.Combine(folderPath, newFileName);

                        File.Move(filePath, newFilePath);
                        changes++;
                    }
                    else if (fileName.Contains(newName))
                    {
                        find++;
                    }
                }
            }
            if (changes == 0 && find == 0)
            {
                MessageBox.Show("Modlar etkinleştirilemedi uygulamanın RDR2 klasörü içerisinde olduğuna eminmisin? Ya da sadece scripthook ve lennys mod loader kurulu değil.");
            }
            else if (changes == 0)
            {
                MessageBox.Show("Modlar zaten etkinleştirilmiş.");
            }
            else
            {
                MessageBox.Show("Modlar etkinleştirildi.");
            }

        }
        catch (Exception error)
        {
            MessageBox.Show($"Hata oluştu: {error}");
        }
    }
    public static void CloseMods()
    {
        try
        {
            ModManagerConfig config = ConfigManager.LoadConfig();
            string folderPath = config.RDR2InstallLocation;
            string[] fileLists = Directory.GetFiles(folderPath);
            int changes = 0;
            int find = 0;
            foreach (string filePath in fileLists)
            {
                string fileName = Path.GetFileName(filePath);
                string[] newNames = { "dinput8dll", "versiondll", "ScriptHookRDR2dll", "SimpleHookdll", "ModManager.Coredll", "NativeInteropdll", "EasyLoad64dll", "EasyHook64dll", "EasyHookdll" };
                string[] oldNames = { "dinput8.dll", "version.dll", "ScriptHookRDR2.dll", "SimpleHook.dll", "ModManager.Core.dll", "NativeInterop.dll", "EasyLoad64.dll", "EasyHook64.dll", "EasyHook.dll" };
                for (int i = 0; i < oldNames.Length && i < newNames.Length; i++)
                {
                    string oldName = oldNames[i];
                    string newName = newNames[i];
                    if (fileName.Contains(oldName))
                    {
                        string newFileName = fileName.Replace(oldName, newName);
                        string newFilePath = Path.Combine(folderPath, newFileName);

                        File.Move(filePath, newFilePath);
                        changes++;
                    }
                    else if (fileName.Contains(newName))
                    {
                        find++;
                    }
                }
            }
            if (changes == 0 && find == 0)
            {
                MessageBox.Show("Modlar kapatılamadı uygulamanın RDR2 klasörü içerisinde olduğuna eminmisin? Ya da sadece scripthook ve lennys mod loader kurulu değil.");
            }
            else if (changes == 0)
            {
                MessageBox.Show("Modlar zaten kapatılmış.");
            }
            else
            {
                MessageBox.Show("Modlar kapatıldı.");
            }
        }
        catch (Exception error)
        {
            MessageBox.Show($"Hata oluştu: {error}");
        }
    }
    public static async void MODInstall(string apiURL, Button button)
    {
        try
        {
            ModManagerConfig config = ConfigManager.LoadConfig();
            string extractPath = config.RDR2InstallLocation;
            Random random = new Random();
            int randomNumber = random.Next(100, 9999);
            string downloadedFileName = $"rdr2modinstaller-{randomNumber}.rar";
            button.Text = "Kuruluyor...";
            button.Enabled = false;
            string downloadedFilePath = Path.Combine(Path.GetTempPath(), downloadedFileName);

            using (HttpClient client = new HttpClient())
            {
                byte[] fileData = await client.GetByteArrayAsync(apiURL);
                File.WriteAllBytes(downloadedFilePath, fileData);
            }

            using (var archive = RarArchive.Open(downloadedFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    string entryFullPath = Path.Combine(extractPath, entry.Key);

                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(entryFullPath);
                    }
                    else
                    {
                        string entryDirectory = Path.GetDirectoryName(entryFullPath);
                        if (!Directory.Exists(entryDirectory))
                        {
                            Directory.CreateDirectory(entryDirectory);
                        }

                        if (!entry.IsDirectory)
                        {
                            using (Stream entryStream = entry.OpenEntryStream())
                            using (FileStream fileStream = File.Open(entryFullPath, FileMode.Create, FileAccess.Write))
                            {
                                entryStream.CopyTo(fileStream);
                            }

                            if (entry.LastModifiedTime.HasValue)
                            {
                                File.SetLastWriteTime(entryFullPath, entry.LastModifiedTime.Value);
                            }
                            else
                            {
                                File.SetLastWriteTime(entryFullPath, DateTime.Now);
                            }
                        }
                    }
                }
            }
            button.Text = "Kur";
            button.Enabled = true;
            MessageBox.Show("Başarıyla kuruldu!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }

    public static async void MODUninstall(string apiURL, Button button)
    {
        try
        {
            ModManagerConfig config = ConfigManager.LoadConfig();
            string uninstallLocation = config.RDR2InstallLocation;
            button.Text = "Kaldırılıyor...";
            button.Enabled = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonFileMap = await client.GetStringAsync(apiURL);

                var fileMap = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonFileMap);

                if (fileMap.ContainsKey("files"))
                {
                    foreach (var fileName in fileMap["files"])
                    {
                        string fullPath = Path.Combine(uninstallLocation, fileName);

                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        else if (Directory.Exists(fullPath))
                        {
                            Directory.Delete(fullPath, true);
                        }
                    }
                    button.Text = "Kaldır";
                    button.Enabled = true;
                    MessageBox.Show("Başarıyla kaldırıldı!");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }
    public static async Task Update(string version)
    {
        try
        {
            string apiURL = "https://api.sypnex.net/file?q=download&filename=ModManager";
            string tempDownloadPath = Path.Combine(Path.GetTempPath(), "ModManagerUpdate.rar");
            string currentExePath = Application.ExecutablePath;

            using (HttpClient client = new HttpClient())
            {
                byte[] fileData = await client.GetByteArrayAsync(apiURL);
                File.WriteAllBytes(tempDownloadPath, fileData);
            }

            string newFileName = $"ModManagerV{version}.exe";

            string newFilePath = Path.Combine(Path.GetDirectoryName(currentExePath), newFileName);

            using (var archive = SharpCompress.Archives.Rar.RarArchive.Open(tempDownloadPath))
            {
                foreach (var entry in archive.Entries)
                {
                    using (Stream entryStream = entry.OpenEntryStream())
                    using (FileStream fileStream = File.Open(newFilePath, FileMode.Create, FileAccess.Write))
                    {
                        entryStream.CopyTo(fileStream);
                    }

                    if (entry.LastModifiedTime.HasValue)
                    {
                        File.SetLastWriteTime(newFilePath, entry.LastModifiedTime.Value);
                    }
                    else
                    {
                        File.SetLastWriteTime(newFilePath, DateTime.Now);
                    }
                }
            }
            Process.Start(newFilePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }
    public static async void CleanUpOtherVersions()
    {
        try
        {
            await Task.Delay(2000);
            string currentExePath = Application.ExecutablePath;
            string currentExeName = Path.GetFileName(currentExePath);
            string exeDirectory = Path.GetDirectoryName(currentExePath);
            string[] files = Directory.GetFiles(exeDirectory, "ModManagerV*.exe");

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);

                if (!string.Equals(fileName, currentExeName, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(filePath);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }
    public class ModManagerConfig
    {
        [Obfuscation(Exclude = true, Feature = "renaming")]
        public string RDR2InstallLocation { get; set; }
    }

    public class ConfigManager
    {
        private static readonly string ConfigFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RDR2ModManager",
            "ModManagerConfigV2.json"
        );
        private static ModManagerConfig _cachedConfig;
        public static ModManagerConfig LoadConfig()
        {
            if (_cachedConfig != null)
            {
                return _cachedConfig;
            }

            try
            {
                string configJson = File.ReadAllText(ConfigFilePath);
                _cachedConfig = JsonConvert.DeserializeObject<ModManagerConfig>(configJson);
            }
            catch (Exception)
            {
                _cachedConfig = new ModManagerConfig
                {
                    RDR2InstallLocation = ""
                };
                string selectedFilePath;
                do
                {
                    selectedFilePath = GetSelectedFilePath();

                    if (!string.IsNullOrEmpty(selectedFilePath))
                    {
                        _cachedConfig.RDR2InstallLocation = selectedFilePath;
                        SaveConfig(_cachedConfig);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("RDR2 Klasörünü seçmek zorundasın eğer seçmek istemiyorsan uygulamayı kullanamazsın. Eğer kullanmak istemiyorsan iptal butonuna tıkla.", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.Cancel)
                        {
                            Application.Exit();
                            break;
                        }
                    }
                } while (string.IsNullOrEmpty(selectedFilePath));
            }
            return _cachedConfig;
        }

        public static void SaveConfig(ModManagerConfig config)
        {
            string configDirectory = Path.GetDirectoryName(ConfigFilePath);

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, configJson);
        }
        private static string GetSelectedFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "RDR2 Executable|RDR2.exe";
                openFileDialog.Title = "RDR2 Dosya Konumunu Seç";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            return null;
        }
    }
}

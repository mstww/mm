using Newtonsoft.Json;
using SharpCompress.Archives.Rar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
public static class Utility
{
    public static void RunProgram(string url)
    {
        try
        {
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(url);
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
            string folderPath = Directory.GetCurrentDirectory();
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
                MessageBox.Show("Modlar etkinleştirilemedi uygulamanın RDR2 klasörü içerisinde olduğuna eminmisin?");
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
            string folderPath = Directory.GetCurrentDirectory();
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
                MessageBox.Show("Modlar kapatılamadı uygulamanın RDR2 klasörü içerisinde olduğuna eminmisin?");
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
            // Kullanıcıdan bir dosya seçmesini iste
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PlayRDR2 Executable|PlayRDR2.exe";
                openFileDialog.Title = "Select PlayRDR2.exe File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın yolu
                    string selectedFilePath = openFileDialog.FileName;
                    Random random = new Random();
                    int randomNumber = random.Next(100, 9999); // 100 ile 999 arasında rastgele bir sayı
                    string downloadedFileName = $"rdr2modinstaller-{randomNumber}.rar";
                    // API'den dosyayı indir
                    button.Text = "Kuruluyor...";
                    button.Enabled = false;
                    string downloadedFilePath = Path.Combine(Path.GetTempPath(), downloadedFileName);

                    using (HttpClient client = new HttpClient())
                    {
                        byte[] fileData = await client.GetByteArrayAsync(apiURL);
                        File.WriteAllBytes(downloadedFilePath, fileData);
                    }

                    // RAR dosyasını çıkart
                    string extractPath = Path.GetDirectoryName(selectedFilePath); // Oyunun bulunduğu konum
                    using (var archive = RarArchive.Open(downloadedFilePath))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            // Dosyanın tam yolu
                            string entryFullPath = Path.Combine(extractPath, entry.Key);

                            // Klasörü oluştur
                            if (entry.IsDirectory)
                            {
                                Directory.CreateDirectory(entryFullPath);
                            }
                            else
                            {
                                // Klasörleri oluştur
                                string entryDirectory = Path.GetDirectoryName(entryFullPath);
                                if (!Directory.Exists(entryDirectory))
                                {
                                    Directory.CreateDirectory(entryDirectory);
                                }

                                // Dosyayı çıkart
                                if (!entry.IsDirectory)
                                {
                                    using (Stream entryStream = entry.OpenEntryStream())
                                    using (FileStream fileStream = File.Open(entryFullPath, FileMode.Create, FileAccess.Write))
                                    {
                                        entryStream.CopyTo(fileStream);
                                    }

                                    // Dosyanın değiştirilme tarihini orijinal tarihine ayarla
                                    if (entry.LastModifiedTime.HasValue)
                                    {
                                        File.SetLastWriteTime(entryFullPath, entry.LastModifiedTime.Value);
                                    }
                                    else
                                    {
                                        // Eğer LastModifiedTime null ise, dosyanın değiştirilme tarihini şu anki zamana ayarla
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
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }

    /*public static async void MODInstall(string apiURL, Button button)
    {
        try
        {
            // Kullanıcıdan bir dosya seçmesini iste
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PlayRDR2 Executable|PlayRDR2.exe";
                openFileDialog.Title = "Select PlayRDR2.exe File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın yolu
                    string selectedFilePath = openFileDialog.FileName;
                    Random random = new Random();
                    int randomNumber = random.Next(100, 9999); // 100 ile 999 arasında rastgele bir sayı
                    string downloadedFileName = $"rdr2modinstaller-{randomNumber}.rar";
                    // API'den dosyayı indir
                    button.Text = "Kuruluyor...";
                    button.Enabled = false;
                    string downloadedFilePath = Path.Combine(Path.GetTempPath(), downloadedFileName);

                    using (HttpClient client = new HttpClient())
                    {
                        byte[] fileData = await client.GetByteArrayAsync(apiURL);
                        File.WriteAllBytes(downloadedFilePath, fileData);
                    }

                    // RAR dosyasını çıkart
                    string extractPath = Path.GetDirectoryName(selectedFilePath); // Oyunun bulunduğu konum
                    using (var archive = RarArchive.Open(downloadedFilePath))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            // Dosyanın tam yolu
                            string entryFullPath = Path.Combine(extractPath, entry.Key);

                            // Klasörleri oluştur
                            string entryDirectory = Path.GetDirectoryName(entryFullPath);
                            if (!Directory.Exists(entryDirectory))
                            {
                                Directory.CreateDirectory(entryDirectory);
                            }

                            // Dosyayı çıkart
                            if (!entry.IsDirectory)
                            {
                                using (Stream entryStream = entry.OpenEntryStream())
                                using (FileStream fileStream = File.Open(entryFullPath, FileMode.Create, FileAccess.Write))
                                {
                                    entryStream.CopyTo(fileStream);
                                }

                                // Dosyanın değiştirilme tarihini orijinal tarihine ayarla
                                if (entry.LastModifiedTime.HasValue)
                                {
                                    File.SetLastWriteTime(entryFullPath, entry.LastModifiedTime.Value);
                                }
                                else
                                {
                                    // Eğer LastModifiedTime null ise, dosyanın değiştirilme tarihini şu anki zamana ayarla
                                    File.SetLastWriteTime(entryFullPath, DateTime.Now);
                                }
                            }
                        }
                    }
                    button.Text = "Kur";
                    button.Enabled = true;
                    MessageBox.Show("Başarıyla kuruldu!");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata oluştu: {ex.Message}");
        }
    }*/

    public static async void MODUninstall(string apiURL, Button button)
    {
        try
        {
            // Kullanıcıdan bir dosya seçmesini iste
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PlayRDR2 Executable|PlayRDR2.exe";
                openFileDialog.Title = "Select PlayRDR2.exe File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın klasörü
                    string uninstallLocation = Path.GetDirectoryName(openFileDialog.FileName);
                    button.Text = "Kaldırılıyor...";
                    button.Enabled = false;
                    // API'den dosya haritasını al
                    using (HttpClient client = new HttpClient())
                    {
                        string jsonFileMap = await client.GetStringAsync(apiURL);

                        // JSON verisini bir nesneye dönüştür
                        var fileMap = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonFileMap);

                        // "files" dizisindeki her bir dosyayı işle
                        if (fileMap.ContainsKey("files"))
                        {
                            foreach (var fileName in fileMap["files"])
                            {
                                // Dosyanın tam yolu
                                string fullPath = Path.Combine(uninstallLocation, fileName);

                                // Dosyayı ve dizini kaldır
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

            // RAR dosyasını indir
            using (HttpClient client = new HttpClient())
            {
                byte[] fileData = await client.GetByteArrayAsync(apiURL);
                File.WriteAllBytes(tempDownloadPath, fileData);
            }

            // Yeni dosyanın adını belirle
            string newFileName = $"ModManagerV{version}.exe";

            // Yeni dosyanın tam yolu
            string newFilePath = Path.Combine(Path.GetDirectoryName(currentExePath), newFileName);

            // ModManager.exe'yi çıkartma
            using (var archive = SharpCompress.Archives.Rar.RarArchive.Open(tempDownloadPath))
            {
                foreach (var entry in archive.Entries)
                {
                    // Dosyayı çıkart ve yeni adını kullan
                    using (Stream entryStream = entry.OpenEntryStream())
                    using (FileStream fileStream = File.Open(newFilePath, FileMode.Create, FileAccess.Write))
                    {
                        entryStream.CopyTo(fileStream);
                    }

                    // Dosyanın değiştirilme tarihini orijinal tarihine ayarla
                    if (entry.LastModifiedTime.HasValue)
                    {
                        File.SetLastWriteTime(newFilePath, entry.LastModifiedTime.Value);
                    }
                    else
                    {
                        // Eğer LastModifiedTime null ise, dosyanın değiştirilme tarihini şu anki zamana ayarla
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

                // Sadece mevcut uygulamadan farklı dosyaları sil
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
}

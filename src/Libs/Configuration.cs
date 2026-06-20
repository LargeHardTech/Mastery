using System.Text.Json;

namespace Config
{
    public class Config
    {
       
        public int Height { get; set; } = 700;
        public int Width { get; set; } = 1200;
        public int X { get; set; } = 100;
        public int Y { get; set; } = 100;
        public bool IsMaximized { get; set; } = false;
        public bool LoggedIn { get; set; } = false;


        private const string AppName = "Mastery";

        private static readonly string FolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "." + AppName);

        private static readonly string ConfigFilePath = Path.Combine(
            FolderPath, "config.json");

        public static Config Load()
        {
            while (true)
            {
                try
                {
                    string jsonString = File.ReadAllText(ConfigFilePath);
                    return JsonSerializer.Deserialize<Config>(jsonString) ?? new Config();
                }
                catch (Exception ex)
                {
                    if (!ShowRetry("加载配置文件失败:\n" + ex.Message + "\n\n重试将重新初始化配置文件"))
                    {
                        throw new InvalidOperationException("无法加载配置文件", ex);
                    }

                    Init(true);
                }
            }
        }
        
        public static void Init(bool reset = false)
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);
                if (reset || !File.Exists(ConfigFilePath))
                {
                    new Config().Save();
                }
            }
            catch (Exception ex)
            {
                ShowError("初始化配置文件失败:\n" + ex.Message);
                throw;
            }
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                using var writer = File.CreateText(ConfigFilePath);
                writer.Write(jsonString);
            }
            catch (Exception ex)
            {
                ShowError("保存配置失败:\n" + ex.Message);
                throw;
            }
        }

        private static void ShowError(string text) =>
            MessageBox.Show(text, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static bool ShowRetry(string text) => 
            MessageBox.Show(text, AppName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            == DialogResult.Retry;
    }
}

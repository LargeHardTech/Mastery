using Mastery.src.Forms;

namespace Mastery
{
    internal static class Program
    {
        public static Config.Config cfg = null!;
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();  

            try
            {
                Config.Config.Init();
                cfg = Config.Config.Load();
                Application.Run(new MainForm());
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Mastery",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}

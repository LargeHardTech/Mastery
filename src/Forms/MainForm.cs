using System.Windows.Forms;

namespace Mastery.src.Forms
{
    public partial class MainForm : Form
    {
        private readonly Config.Config _cfg;

        public MainForm()
        {
            InitializeComponent();
            _cfg = Program.cfg;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                _cfg.Width = this.Size.Width;
                _cfg.Height = this.Size.Height;
                _cfg.X = this.Location.X;
                _cfg.Y = this.Location.Y;
                _cfg.IsMaximized = (this.WindowState == FormWindowState.Maximized);
            }
            _cfg.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //设置窗口大小和位置
            this.Size = new System.Drawing.Size(_cfg.Width, _cfg.Height);
            this.Location = new System.Drawing.Point(_cfg.X, _cfg.Y);
            this.WindowState = _cfg.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            

            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

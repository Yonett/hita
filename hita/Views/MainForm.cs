using hita.Controllers;

namespace hita.Views
{
    public partial class MainForm : Form
    {
        private readonly NSProblemController Controller = new();
        private const ushort PanelCollapsedSize = 60;
        private const ushort ProblemParametersPanelSize = 270;
        private const ushort SolverParametersPanelSize = 340;
        private const ushort PaintParametersPanelSize = 60;
        public MainForm()
        {
            InitializeComponent();
            Console.WriteLine(this.ClientSize.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Height == PanelCollapsedSize)
                flowLayoutPanel1.Height = ProblemParametersPanelSize;
            else
                flowLayoutPanel1.Height = PanelCollapsedSize;
        }
    }
}

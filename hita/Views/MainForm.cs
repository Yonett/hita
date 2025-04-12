using hita.Controllers;

namespace hita.Views
{
    public enum PaintTargets
    {
        Temperature,
        Current,
        Eddy,
        Vx,
        Vy,
    }
    public partial class MainForm : Form
    {
        private readonly NSProblemController Controller = new();
        private const ushort PanelCollapsedSize = 60;
        private const ushort ProblemParametersPanelSize = 270;
        private const ushort SolverParametersPanelSize = 340;
        private const ushort PaintParametersPanelSize = 200;

        public MainForm()
        {
            InitializeComponent();
            LHComboBox.SelectedIndex = 5;
        }

        #region Buttons Click Handlers
        private void problemSettingsCollapseButton_Click(object sender, EventArgs e)
        {
            if (problemSettingsPanel.Height == PanelCollapsedSize)
                problemSettingsPanel.Height = ProblemParametersPanelSize;
            else
                problemSettingsPanel.Height = PanelCollapsedSize;
        }

        private void solverSettingsCollapseButton_Click(object sender, EventArgs e)
        {
            if (solverSettingsPanel.Height == PanelCollapsedSize)
                solverSettingsPanel.Height = SolverParametersPanelSize;
            else
                solverSettingsPanel.Height = PanelCollapsedSize;
        }

        private void paintSettingsCollapseButton_Click(object sender, EventArgs e)
        {
            if (paintSettingsPanel.Height == PanelCollapsedSize)
                paintSettingsPanel.Height = PaintParametersPanelSize;
            else
                paintSettingsPanel.Height = PanelCollapsedSize;
        }
        #endregion
    }
}

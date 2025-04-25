using hita.Controllers;
using hita.Geometric;
using OpenTK.Graphics.OpenGL;

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
        private static readonly NSProblemController Controller = new();

        private const ushort PanelCollapsedSize = 60;
        private const ushort ProblemParametersPanelSize = 270;
        private const ushort SolverParametersPanelSize = 340;
        private const ushort PaintParametersPanelSize = 200;

        private readonly Color ActivePaintTargetBackground = Color.FromArgb(41, 128, 185);
        private readonly Color ActivePaintTargetForeground = Color.FromArgb(255, 255, 255);
        private readonly Color InactivePaintTargetBackground = Color.FromArgb(149, 165, 166);
        private readonly Color InactivePaintTargetForeground = Color.FromArgb(44, 62, 80);

        private PaintTargets PaintTarget = PaintTargets.Temperature;

        private List<double> Values = new List<double>();
        private List<Element> Elements = new List<Element>();
        private List<Line> Lines = new List<Line>();

        public double ValueMax = 0.0;
        public double ValueMin = 0.0;

        private double GLScale { get; set; } = 1.0;
        private double GLTranslateX { get; set; } = 0.0;
        private double GLTranslateY { get; set; } = 0.0;
        private double GLMousePosX { get; set; } = 0.0;
        private double GLMousePosY { get; set; } = 0.0;

        public MainForm()
        {
            InitializeComponent();
            LHComboBox.SelectedIndex = 5;
        }

        #region Collapse Buttons Click Handlers
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

        #region Paint Targets

        private void TemperatureLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Temperature)
            {
                UpdatePaintTargetControl();
                PaintTarget = PaintTargets.Temperature;
                temperatureLabel.BackColor = ActivePaintTargetBackground;
                temperatureLabel.ForeColor = ActivePaintTargetForeground;
                Values = Controller.TemperatureValues;
            }
        }
        private void CurrentLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Current)
            {
                UpdatePaintTargetControl();
                PaintTarget = PaintTargets.Current;
                currentLabel.BackColor = ActivePaintTargetBackground;
                currentLabel.ForeColor = ActivePaintTargetForeground;
                Values = Controller.CurrentValues;
            }
        }
        private void EddyLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Eddy)
            {
                UpdatePaintTargetControl();
                PaintTarget = PaintTargets.Eddy;
                eddyLabel.BackColor = ActivePaintTargetBackground;
                eddyLabel.ForeColor = ActivePaintTargetForeground;
                Values = Controller.EddyValues;
            }
        }
        private void VxLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Vx)
            {
                UpdatePaintTargetControl();
                PaintTarget = PaintTargets.Vx;
                VxLabel.BackColor = ActivePaintTargetBackground;
                VxLabel.ForeColor = ActivePaintTargetForeground;
                Values = Controller.VxValues;
            }
        }
        private void VyLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Vy)
            {
                UpdatePaintTargetControl();
                PaintTarget = PaintTargets.Vy;
                VyLabel.BackColor = ActivePaintTargetBackground;
                VyLabel.ForeColor = ActivePaintTargetForeground;
                Values = Controller.VyValues;
            }
        }

        private void UpdatePaintTargetControl()
        {
            switch (PaintTarget)
            {
                case PaintTargets.Temperature:
                    temperatureLabel.BackColor = InactivePaintTargetBackground;
                    temperatureLabel.ForeColor = InactivePaintTargetForeground;
                    break;
                case PaintTargets.Current:
                    currentLabel.BackColor = InactivePaintTargetBackground;
                    currentLabel.ForeColor = InactivePaintTargetForeground;
                    break;
                case PaintTargets.Eddy:
                    eddyLabel.BackColor = InactivePaintTargetBackground;
                    eddyLabel.ForeColor = InactivePaintTargetForeground;
                    break;
                case PaintTargets.Vx:
                    VxLabel.BackColor = InactivePaintTargetBackground;
                    VxLabel.ForeColor = InactivePaintTargetForeground;
                    break;
                case PaintTargets.Vy:
                    VyLabel.BackColor = InactivePaintTargetBackground;
                    VyLabel.ForeColor = InactivePaintTargetForeground;
                    break;
            }
        }

        #endregion

        #region Paint
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();

            GL.LoadIdentity();

            GL.ClearColor(1.0f, 0.889f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Scale(GLScale, GLScale, 1);
            GL.Translate(GLTranslateX, GLTranslateY, 0);

            GL.Color3(0, 0, 0);

            GL.Begin(PrimitiveType.Triangles);

                GL.Vertex2(0.5, 0.5);
                GL.Vertex2(1.0, 0.5);
                GL.Vertex2(0.0, 0.0);

            GL.End();

            glControl.SwapBuffers();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GLMousePosX = e.X;
                GLMousePosY = e.Y;
            }
        }
        private void glControl_Load(object sender, EventArgs e)
        {
            int w = glControl.ClientSize.Width;
            int h = glControl.ClientSize.Height;
            double ww = w;
            double hh = h;
            double ratio = 1.0;
            GL.Viewport(0, 0, w, h);

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            if (w > h)
            {
                ratio = ww / hh;
                GL.Ortho(-2.0 * ratio, 2.0 * ratio, -2.0, 2.0, 1.0, -1.0);
            }
            else
            {
                ratio = hh / ww;
                GL.Ortho(-2.0, 2.0, -2.0 * ratio, 2.0 * ratio, 1.0, -1.0);
            }
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();
        }
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GLTranslateX += (e.X - GLMousePosX) * 0.01 / GLScale;
                GLTranslateY -= (e.Y - GLMousePosY) * 0.01 / GLScale;
                glControl.Refresh();
                GLMousePosX = e.X;
                GLMousePosY = e.Y;
            }
        }
        private void glControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                GLScale *= 1.2;
            else
                GLScale *= 0.9;

            if (GLScale < 0.02)
                GLScale = 0.02;

            glControl.Refresh();
        }

        #endregion

        private async void startCalculationButton_Click(object sender, EventArgs e)
        {
            startCalculationButton.Enabled = false;
            Controller.NumberOfIsolines = (int)isolinesNumberNumeric.Value;
            Controller.SetParams
                (
                    Gr: Convert.ToDouble(GrNumeric.Value),
                    Pr: Convert.ToDouble(PrNumeric.Value),
                    Width: Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[0]),
                    Height: Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[1]),
                    WidthNodesCount: Convert.ToInt32(horizontalNodesNumeric.Value),
                    HeightNodesCount: Convert.ToInt32(verticalNodesNumeric.Value),
                    SlaeMaxIter: Convert.ToInt32(maxIterNumeric.Value),
                    WParam: Convert.ToDouble(wNumeric.Value)
                );

            await Task.Run(() =>
            {
                Controller.SolveProblem();
                Controller.ReadResults();
            });            

            startCalculationButton.Enabled = true;

            switch (PaintTarget)
            {
                case PaintTargets.Temperature:
                    Values = Controller.TemperatureValues;
                    break;
                case PaintTargets.Current:
                    Values = Controller.CurrentValues;
                    break;
                case PaintTargets.Eddy:
                    Values = Controller.EddyValues;
                    break;
                case PaintTargets.Vx:
                    Values = Controller.VxValues;
                    break;
                case PaintTargets.Vy:
                    Values = Controller.VyValues;
                    break;
            }
        }
    }
}

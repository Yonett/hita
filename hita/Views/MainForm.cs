using System.Globalization;
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
        private List<Node> Nodes = new List<Node>();
        private List<Line> Lines = new List<Line>();

        public double ValueMax = 0.0;
        public double ValueMin = 0.0;
        public double ValuesRange = 0.0;
        public double HSVValue = 0.0;
        public double XMax = 1.0;
        public double YMax = 1.0;

        private double GLScale { get; set; } = 1.0;
        private double GLTranslateX { get; set; } = 0.0;
        private double GLTranslateY { get; set; } = 0.0;
        private double GLMousePosX { get; set; } = 0.0;
        private double GLMousePosY { get; set; } = 0.0;

        public MainForm()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-EN", false);
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
                Lines = Controller.TemperatueLines;

                ValueMax = Controller.TemperatureMax;
                ValueMin = Controller.TemperatureMin;
                ValuesRange = Controller.TemperatureRange;
                glControl.Invalidate();
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
                Lines = Controller.CurrentLines;

                ValueMax = Controller.CurrentMax;
                ValueMin = Controller.CurrentMin;
                ValuesRange = Controller.CurrentRange;
                glControl.Invalidate();
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
                Lines = Controller.EddyLines;

                ValueMax = Controller.EddyMax;
                ValueMin = Controller.EddyMin;
                ValuesRange = Controller.EddyRange;
                glControl.Invalidate();
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
                Lines = Controller.VxLines;

                ValueMax = Controller.VxMax;
                ValueMin = Controller.VxMin;
                ValuesRange = Controller.VxRange;
                glControl.Invalidate();
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
                Lines = Controller.VyLines;

                ValueMax = Controller.VyMax;
                ValueMin = Controller.VyMin;
                ValuesRange = Controller.VyRange;
                glControl.Invalidate();
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

        public void GetColorByFuncValue(float value)
        {
            float r = 0, g = 0, b = 0;
            float h = value / 360;

            int i = (int)Math.Floor(h * 6);
            float f = h * 6 - i;
            float q = (1 - f);
            float t = (1 - q);

            switch (i % 6)
            {
                case 0: r = 1; g = t; b = 0; break;
                case 1: r = q; g = 1; b = 0; break;
                case 2: r = 0; g = 1; b = t; break;
                case 3: r = 0; g = q; b = 1; break;
                case 4: r = t; g = 0; b = 1; break;
                case 5: r = 1; g = 0; b = q; break;
            }
            GL.Color3(r, g, b);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();

            GL.LoadIdentity();

            GL.Enable(EnableCap.LineSmooth);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Scale(GLScale, GLScale, 1);
            GL.Translate(GLTranslateX, GLTranslateY, 0);

            GL.Color3(0, 0, 0);

            foreach (Element elem in Elements)
            {
                GL.Begin(PrimitiveType.Quads);
                HSVValue = 240 * (1.0 - Values[elem.node_numbers[0]] / ValuesRange);
                GetColorByFuncValue((float)HSVValue);
                GL.Vertex2(Nodes[elem.node_numbers[0]].x, Nodes[elem.node_numbers[0]].y);

                HSVValue = 240 * (1.0 - Values[elem.node_numbers[1]] / ValuesRange);
                GetColorByFuncValue((float)HSVValue);
                GL.Vertex2(Nodes[elem.node_numbers[1]].x, Nodes[elem.node_numbers[1]].y);

                HSVValue = 240 * (1.0 - Values[elem.node_numbers[3]] / ValuesRange);
                GetColorByFuncValue((float)HSVValue);
                GL.Vertex2(Nodes[elem.node_numbers[3]].x, Nodes[elem.node_numbers[3]].y);

                HSVValue = 240 * (1.0 - Values[elem.node_numbers[2]] / ValuesRange);
                GetColorByFuncValue((float)HSVValue);
                GL.Vertex2(Nodes[elem.node_numbers[2]].x, Nodes[elem.node_numbers[2]].y);
                GL.End();
            }

            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.LineWidth(1);

            GL.Begin(PrimitiveType.Lines);

            foreach (Line line in Lines)
            {
                GL.Vertex2(line.M.x, line.M.y);
                GL.Vertex2(line.N.x, line.N.y);
            }

            GL.End();

            GL.LineWidth(2);

            GL.Begin(PrimitiveType.LineLoop);

                GL.Vertex2(0.0, 0.0);
                GL.Vertex2(XMax, 0.0);
                GL.Vertex2(XMax, YMax);
                GL.Vertex2(0.0, YMax);

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

            Controller.NumberOfIsolines = (int)isolinesNumberNumeric.Value + 1;
            Controller.xs = (int)horizontalNodesNumeric.Value;
            Controller.ys = (int)verticalNodesNumeric.Value;

            Controller.SetParams
                (
                    Gr: Convert.ToDouble(GrNumeric.Value),
                    Pr: Convert.ToDouble(PrNumeric.Value),
                    Width: XMax,
                    Height: YMax,
                    WidthNodesCount: Convert.ToInt32(horizontalNodesNumeric.Value) - 1,
                    HeightNodesCount: Convert.ToInt32(verticalNodesNumeric.Value) - 1,
                    SlaeMaxIter: Convert.ToInt32(maxIterNumeric.Value),
                    WParam: Convert.ToDouble(wNumeric.Value)
                );

            await Task.Run(() =>
            {
                Controller.SolveProblem();
            });

            Elements = Controller.Elements;
            Nodes = Controller.Nodes;

            switch (PaintTarget)
            {
                case PaintTargets.Temperature:
                    Values = Controller.TemperatureValues;
                    Lines = Controller.TemperatueLines;
                    ValueMax = Controller.TemperatureMax;
                    ValueMin = Controller.TemperatureMin;
                    ValuesRange = Controller.TemperatureRange;
                    break;
                case PaintTargets.Current:
                    Values = Controller.CurrentValues;
                    Lines = Controller.CurrentLines;
                    ValueMax = Controller.CurrentMax;
                    ValueMin = Controller.CurrentMin;
                    ValuesRange = Controller.CurrentRange;
                    break;
                case PaintTargets.Eddy:
                    Values = Controller.EddyValues;
                    Lines = Controller.EddyLines;
                    ValueMax = Controller.EddyMax;
                    ValueMin = Controller.EddyMin;
                    ValuesRange = Controller.EddyRange;
                    break;
                case PaintTargets.Vx:
                    Values = Controller.VxValues;
                    Lines = Controller.VxLines;
                    ValueMax = Controller.VxMax;
                    ValueMin = Controller.VxMin;
                    ValuesRange = Controller.VxRange;
                    break;
                case PaintTargets.Vy:
                    Values = Controller.VyValues;
                    Lines = Controller.VyLines;
                    ValueMax = Controller.VyMax;
                    ValueMin = Controller.VyMin;
                    ValuesRange = Controller.VyRange;
                    break;
            }
            startCalculationButton.Enabled = true;
            glControl.Refresh();
        }

        private void LHComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            XMax = Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[0]);
            YMax = Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[1]);
            glControl.Refresh();
        }
    }
}

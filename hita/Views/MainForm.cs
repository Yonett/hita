using hita.Controllers;
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
        private readonly NSProblemController Controller = new();
        private const ushort PanelCollapsedSize = 60;
        private const ushort ProblemParametersPanelSize = 270;
        private const ushort SolverParametersPanelSize = 340;
        private const ushort PaintParametersPanelSize = 200;
        private PaintTargets PaintTarget = PaintTargets.Temperature;
        private List<double> Values = new List<double>();
        private List<Node> Nodes = new List<Node>();

        private readonly Color ActivePaintTargetBackground = Color.FromArgb(41, 128, 185);
        private readonly Color ActivePaintTargetForeground = Color.FromArgb(255, 255, 255);
        private readonly Color InactivePaintTargetBackground = Color.FromArgb(149, 165, 166);
        private readonly Color InactivePaintTargetForeground = Color.FromArgb(44, 62, 80);

        private readonly Thread? CalculationThread;

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

            glControl.SwapBuffers();
        }
        #endregion





        private async void startCalculationButton_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("L/H: {0}", LHComboBox.SelectedItem!.ToString());
            //Console.WriteLine("L: {0}", Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[0]));
            //Console.WriteLine("H: {0}", Convert.ToDouble(LHComboBox.SelectedItem!.ToString()!.Split(":")[1]));
            //Console.WriteLine("Pr: {0}", PrNumeric.Value);
            //Console.WriteLine("Gr: {0}", GrNumeric.Value);
            //Console.WriteLine("Nodes horizontally: {0}", horizontalNodesNumeric.Value);
            //Console.WriteLine("Nodes vertically: {0}", verticalNodesNumeric.Value);
            //Console.WriteLine("MaxIter: {0}", maxIterNumeric.Value);
            //Console.WriteLine("w: {0}", wNumeric.Value);
            startCalculationButton.Enabled = false;
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
            await Controller.SolveProblem();
            startCalculationButton.Enabled = true;
        }
    }

    public class Node
    {
        public double x = 0.0;
        public double y = 0.0;
        public Node(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Node() { }
    }
    public class Line
    {
        public Node M = new Node();
        public Node N = new Node();
        public Line(Node M, Node N)
        {
            this.M = M;
            this.N = N;
        }
        public Line() { }
    }
    public class Element
    {
        public int[] node_numbers = new int[4];
        public Element() { }
        public Element(int a, int b, int c, int d)
        {
            this.node_numbers[0] = a;
            this.node_numbers[1] = b;
            this.node_numbers[2] = c;
            this.node_numbers[3] = d;
        }
    }
    public class Triangle
    {
        public Node[] nodes = new Node[3];
        public double[] funcs = new double[3];
        public Triangle() { }
    }
}

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

        private Color ActivePaintTargetBackground = Color.FromArgb(41, 128, 185);
        private Color InactivePaintTargetBackground = Color.FromArgb(149, 165, 166);

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
                UpdatePaintTarget();
                PaintTarget = PaintTargets.Temperature;
                TemperatureLabel.BackColor = ActivePaintTargetBackground;
            }
        }
        private void CurrentLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Current)
            {
                UpdatePaintTarget();
                PaintTarget = PaintTargets.Current;
                CurrentLabel.BackColor = ActivePaintTargetBackground;
            }
        }
        private void EddyLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Eddy)
            {
                UpdatePaintTarget();
                PaintTarget = PaintTargets.Eddy;
                EddyLabel.BackColor = ActivePaintTargetBackground;
            }
        }
        private void VxLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Vx)
            {
                UpdatePaintTarget();
                PaintTarget = PaintTargets.Vx;
                VxLabel.BackColor = ActivePaintTargetBackground;
            }
        }
        private void VyLabel_Click(object sender, EventArgs e)
        {
            if (PaintTarget != PaintTargets.Vy)
            {
                UpdatePaintTarget();
                PaintTarget = PaintTargets.Vy;
                VyLabel.BackColor = ActivePaintTargetBackground;
            }
        }

        private void UpdatePaintTarget()
        {
            switch (PaintTarget)
            {
                case PaintTargets.Temperature:
                    TemperatureLabel.BackColor = InactivePaintTargetBackground;
                    break;
                case PaintTargets.Current:
                    CurrentLabel.BackColor = InactivePaintTargetBackground;
                    break;
                case PaintTargets.Eddy:
                    EddyLabel.BackColor = InactivePaintTargetBackground;
                    break;
                case PaintTargets.Vx:
                    VxLabel.BackColor = InactivePaintTargetBackground;
                    break;
                case PaintTargets.Vy:
                    VyLabel.BackColor = InactivePaintTargetBackground;
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

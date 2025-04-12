namespace hita.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            glControl = new OpenTK.GLControl.GLControl();
            settingsPanel = new Panel();
            paintSettingsPanel = new FlowLayoutPanel();
            paintSettingsCollapsePanel = new FlowLayoutPanel();
            paintSettingsCollapseButton = new Button();
            paintSettingsCollapseLabel = new Label();
            flowLayoutPanel14 = new FlowLayoutPanel();
            paintTargetLabel = new Label();
            flowLayoutPanel16 = new FlowLayoutPanel();
            TemperatureLabel = new Label();
            CurrentLabel = new Label();
            EddyLabel = new Label();
            VxLabel = new Label();
            VyLabel = new Label();
            isolinesNumberPanel = new FlowLayoutPanel();
            isolinesNumberLabel = new Label();
            isolinesNumberNumeric = new NumericUpDown();
            solverSettingsPanel = new FlowLayoutPanel();
            solverSettingsCollapsePanel = new FlowLayoutPanel();
            solverSettingsCollapseButton = new Button();
            solverSettingsCollapseLabel = new Label();
            flowLayoutPanel5 = new FlowLayoutPanel();
            label6 = new Label();
            numericUpDown3 = new NumericUpDown();
            flowLayoutPanel6 = new FlowLayoutPanel();
            label7 = new Label();
            numericUpDown4 = new NumericUpDown();
            flowLayoutPanel10 = new FlowLayoutPanel();
            label8 = new Label();
            numericUpDown5 = new NumericUpDown();
            flowLayoutPanel11 = new FlowLayoutPanel();
            label9 = new Label();
            numericUpDown6 = new NumericUpDown();
            problemSettingsPanel = new FlowLayoutPanel();
            problemSettingsCollapsePanel = new FlowLayoutPanel();
            problemSettingsCollapseButton = new Button();
            problemSettingsCollapseLabel = new Label();
            LHPanel = new FlowLayoutPanel();
            LHLabel = new Label();
            LHComboBox = new ComboBox();
            PrPanel = new FlowLayoutPanel();
            PrLabel = new Label();
            PrNumeric = new NumericUpDown();
            GrPanel = new FlowLayoutPanel();
            GrLabel = new Label();
            GrNumeric = new NumericUpDown();
            actionPanel = new FlowLayoutPanel();
            startCalculationButton = new Button();
            cancelCalculationButton = new Button();
            progressBar = new ProgressBar();
            settingsPanel.SuspendLayout();
            paintSettingsPanel.SuspendLayout();
            paintSettingsCollapsePanel.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel16.SuspendLayout();
            isolinesNumberPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)isolinesNumberNumeric).BeginInit();
            solverSettingsPanel.SuspendLayout();
            solverSettingsCollapsePanel.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            flowLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            flowLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            problemSettingsPanel.SuspendLayout();
            problemSettingsCollapsePanel.SuspendLayout();
            LHPanel.SuspendLayout();
            PrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PrNumeric).BeginInit();
            GrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrNumeric).BeginInit();
            actionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // glControl
            // 
            glControl.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl.APIVersion = new Version(3, 3, 0, 0);
            glControl.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl.IsEventDriven = true;
            glControl.Location = new Point(400, 0);
            glControl.Name = "glControl";
            glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
            glControl.SharedContext = null;
            glControl.Size = new Size(784, 761);
            glControl.TabIndex = 0;
            // 
            // settingsPanel
            // 
            settingsPanel.AutoScroll = true;
            settingsPanel.BackColor = Color.FromArgb(247, 241, 227);
            settingsPanel.Controls.Add(paintSettingsPanel);
            settingsPanel.Controls.Add(solverSettingsPanel);
            settingsPanel.Controls.Add(problemSettingsPanel);
            settingsPanel.Location = new Point(0, 0);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(400, 661);
            settingsPanel.TabIndex = 1;
            // 
            // paintSettingsPanel
            // 
            paintSettingsPanel.BackColor = Color.FromArgb(247, 241, 227);
            paintSettingsPanel.Controls.Add(paintSettingsCollapsePanel);
            paintSettingsPanel.Controls.Add(flowLayoutPanel14);
            paintSettingsPanel.Controls.Add(isolinesNumberPanel);
            paintSettingsPanel.Dock = DockStyle.Top;
            paintSettingsPanel.Location = new Point(0, 610);
            paintSettingsPanel.Margin = new Padding(0);
            paintSettingsPanel.Name = "paintSettingsPanel";
            paintSettingsPanel.Size = new Size(383, 200);
            paintSettingsPanel.TabIndex = 2;
            // 
            // paintSettingsCollapsePanel
            // 
            paintSettingsCollapsePanel.Controls.Add(paintSettingsCollapseButton);
            paintSettingsCollapsePanel.Controls.Add(paintSettingsCollapseLabel);
            paintSettingsCollapsePanel.Location = new Point(10, 10);
            paintSettingsCollapsePanel.Margin = new Padding(10, 10, 10, 0);
            paintSettingsCollapsePanel.Name = "paintSettingsCollapsePanel";
            paintSettingsCollapsePanel.Size = new Size(350, 40);
            paintSettingsCollapsePanel.TabIndex = 0;
            // 
            // paintSettingsCollapseButton
            // 
            paintSettingsCollapseButton.Cursor = Cursors.Hand;
            paintSettingsCollapseButton.FlatStyle = FlatStyle.Flat;
            paintSettingsCollapseButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            paintSettingsCollapseButton.ForeColor = Color.FromArgb(61, 61, 61);
            paintSettingsCollapseButton.Location = new Point(0, 0);
            paintSettingsCollapseButton.Margin = new Padding(0);
            paintSettingsCollapseButton.Name = "paintSettingsCollapseButton";
            paintSettingsCollapseButton.Size = new Size(40, 40);
            paintSettingsCollapseButton.TabIndex = 2;
            paintSettingsCollapseButton.Text = "V";
            paintSettingsCollapseButton.UseVisualStyleBackColor = true;
            paintSettingsCollapseButton.Click += paintSettingsCollapseButton_Click;
            // 
            // paintSettingsCollapseLabel
            // 
            paintSettingsCollapseLabel.Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            paintSettingsCollapseLabel.ForeColor = Color.FromArgb(61, 61, 61);
            paintSettingsCollapseLabel.Location = new Point(50, 0);
            paintSettingsCollapseLabel.Margin = new Padding(10, 0, 0, 0);
            paintSettingsCollapseLabel.Name = "paintSettingsCollapseLabel";
            paintSettingsCollapseLabel.Size = new Size(300, 40);
            paintSettingsCollapseLabel.TabIndex = 3;
            paintSettingsCollapseLabel.Text = "Параметры отрисовки";
            paintSettingsCollapseLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.Controls.Add(paintTargetLabel);
            flowLayoutPanel14.Controls.Add(flowLayoutPanel16);
            flowLayoutPanel14.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel14.Location = new Point(10, 65);
            flowLayoutPanel14.Margin = new Padding(10, 15, 10, 5);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Size = new Size(350, 60);
            flowLayoutPanel14.TabIndex = 1;
            flowLayoutPanel14.WrapContents = false;
            // 
            // paintTargetLabel
            // 
            paintTargetLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            paintTargetLabel.ForeColor = Color.FromArgb(61, 61, 61);
            paintTargetLabel.Location = new Point(0, 0);
            paintTargetLabel.Margin = new Padding(0);
            paintTargetLabel.Name = "paintTargetLabel";
            paintTargetLabel.Size = new Size(350, 24);
            paintTargetLabel.TabIndex = 1;
            paintTargetLabel.Text = "Цель отрисовки";
            paintTargetLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel16
            // 
            flowLayoutPanel16.Controls.Add(TemperatureLabel);
            flowLayoutPanel16.Controls.Add(CurrentLabel);
            flowLayoutPanel16.Controls.Add(EddyLabel);
            flowLayoutPanel16.Controls.Add(VxLabel);
            flowLayoutPanel16.Controls.Add(VyLabel);
            flowLayoutPanel16.Location = new Point(0, 24);
            flowLayoutPanel16.Margin = new Padding(0);
            flowLayoutPanel16.Name = "flowLayoutPanel16";
            flowLayoutPanel16.Size = new Size(350, 36);
            flowLayoutPanel16.TabIndex = 2;
            // 
            // TemperatureLabel
            // 
            TemperatureLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            TemperatureLabel.Location = new Point(0, 0);
            TemperatureLabel.Margin = new Padding(0);
            TemperatureLabel.Name = "TemperatureLabel";
            TemperatureLabel.Size = new Size(70, 36);
            TemperatureLabel.TabIndex = 3;
            TemperatureLabel.Text = "T";
            TemperatureLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CurrentLabel
            // 
            CurrentLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            CurrentLabel.Location = new Point(70, 0);
            CurrentLabel.Margin = new Padding(0);
            CurrentLabel.Name = "CurrentLabel";
            CurrentLabel.Size = new Size(70, 36);
            CurrentLabel.TabIndex = 4;
            CurrentLabel.Text = "ψ";
            CurrentLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EddyLabel
            // 
            EddyLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            EddyLabel.Location = new Point(140, 0);
            EddyLabel.Margin = new Padding(0);
            EddyLabel.Name = "EddyLabel";
            EddyLabel.Size = new Size(70, 36);
            EddyLabel.TabIndex = 5;
            EddyLabel.Text = "ω";
            EddyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VxLabel
            // 
            VxLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            VxLabel.Location = new Point(210, 0);
            VxLabel.Margin = new Padding(0);
            VxLabel.Name = "VxLabel";
            VxLabel.Size = new Size(70, 36);
            VxLabel.TabIndex = 6;
            VxLabel.Text = "Vx";
            VxLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VyLabel
            // 
            VyLabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            VyLabel.Location = new Point(280, 0);
            VyLabel.Margin = new Padding(0);
            VyLabel.Name = "VyLabel";
            VyLabel.Size = new Size(70, 36);
            VyLabel.TabIndex = 7;
            VyLabel.Text = "Vy";
            VyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // isolinesNumberPanel
            // 
            isolinesNumberPanel.Controls.Add(isolinesNumberLabel);
            isolinesNumberPanel.Controls.Add(isolinesNumberNumeric);
            isolinesNumberPanel.FlowDirection = FlowDirection.TopDown;
            isolinesNumberPanel.Location = new Point(10, 135);
            isolinesNumberPanel.Margin = new Padding(10, 5, 10, 5);
            isolinesNumberPanel.Name = "isolinesNumberPanel";
            isolinesNumberPanel.Size = new Size(350, 60);
            isolinesNumberPanel.TabIndex = 2;
            // 
            // isolinesNumberLabel
            // 
            isolinesNumberLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            isolinesNumberLabel.ForeColor = Color.FromArgb(61, 61, 61);
            isolinesNumberLabel.Location = new Point(0, 0);
            isolinesNumberLabel.Margin = new Padding(0);
            isolinesNumberLabel.Name = "isolinesNumberLabel";
            isolinesNumberLabel.Size = new Size(350, 24);
            isolinesNumberLabel.TabIndex = 1;
            isolinesNumberLabel.Text = "Кол-во изолиний";
            isolinesNumberLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // isolinesNumberNumeric
            // 
            isolinesNumberNumeric.BorderStyle = BorderStyle.FixedSingle;
            isolinesNumberNumeric.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            isolinesNumberNumeric.ForeColor = Color.FromArgb(61, 61, 61);
            isolinesNumberNumeric.Location = new Point(0, 24);
            isolinesNumberNumeric.Margin = new Padding(0);
            isolinesNumberNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            isolinesNumberNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            isolinesNumberNumeric.Name = "isolinesNumberNumeric";
            isolinesNumberNumeric.Size = new Size(350, 27);
            isolinesNumberNumeric.TabIndex = 3;
            isolinesNumberNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // solverSettingsPanel
            // 
            solverSettingsPanel.BackColor = Color.FromArgb(247, 241, 227);
            solverSettingsPanel.Controls.Add(solverSettingsCollapsePanel);
            solverSettingsPanel.Controls.Add(flowLayoutPanel5);
            solverSettingsPanel.Controls.Add(flowLayoutPanel6);
            solverSettingsPanel.Controls.Add(flowLayoutPanel10);
            solverSettingsPanel.Controls.Add(flowLayoutPanel11);
            solverSettingsPanel.Dock = DockStyle.Top;
            solverSettingsPanel.Location = new Point(0, 270);
            solverSettingsPanel.Margin = new Padding(0);
            solverSettingsPanel.Name = "solverSettingsPanel";
            solverSettingsPanel.Size = new Size(383, 340);
            solverSettingsPanel.TabIndex = 1;
            // 
            // solverSettingsCollapsePanel
            // 
            solverSettingsCollapsePanel.Controls.Add(solverSettingsCollapseButton);
            solverSettingsCollapsePanel.Controls.Add(solverSettingsCollapseLabel);
            solverSettingsCollapsePanel.Location = new Point(10, 10);
            solverSettingsCollapsePanel.Margin = new Padding(10, 10, 10, 0);
            solverSettingsCollapsePanel.Name = "solverSettingsCollapsePanel";
            solverSettingsCollapsePanel.Size = new Size(350, 40);
            solverSettingsCollapsePanel.TabIndex = 0;
            // 
            // solverSettingsCollapseButton
            // 
            solverSettingsCollapseButton.Cursor = Cursors.Hand;
            solverSettingsCollapseButton.FlatStyle = FlatStyle.Flat;
            solverSettingsCollapseButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            solverSettingsCollapseButton.ForeColor = Color.FromArgb(61, 61, 61);
            solverSettingsCollapseButton.Location = new Point(0, 0);
            solverSettingsCollapseButton.Margin = new Padding(0);
            solverSettingsCollapseButton.Name = "solverSettingsCollapseButton";
            solverSettingsCollapseButton.Size = new Size(40, 40);
            solverSettingsCollapseButton.TabIndex = 2;
            solverSettingsCollapseButton.Text = "V";
            solverSettingsCollapseButton.UseVisualStyleBackColor = true;
            solverSettingsCollapseButton.Click += solverSettingsCollapseButton_Click;
            // 
            // solverSettingsCollapseLabel
            // 
            solverSettingsCollapseLabel.Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            solverSettingsCollapseLabel.ForeColor = Color.FromArgb(61, 61, 61);
            solverSettingsCollapseLabel.Location = new Point(50, 0);
            solverSettingsCollapseLabel.Margin = new Padding(10, 0, 0, 0);
            solverSettingsCollapseLabel.Name = "solverSettingsCollapseLabel";
            solverSettingsCollapseLabel.Size = new Size(300, 40);
            solverSettingsCollapseLabel.TabIndex = 3;
            solverSettingsCollapseLabel.Text = "Параметры решателя";
            solverSettingsCollapseLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(label6);
            flowLayoutPanel5.Controls.Add(numericUpDown3);
            flowLayoutPanel5.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel5.Location = new Point(10, 65);
            flowLayoutPanel5.Margin = new Padding(10, 15, 10, 5);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(350, 60);
            flowLayoutPanel5.TabIndex = 1;
            // 
            // label6
            // 
            label6.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label6.ForeColor = Color.FromArgb(61, 61, 61);
            label6.Location = new Point(0, 0);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(350, 24);
            label6.TabIndex = 1;
            label6.Text = "Кол-во узлов сетки по горизонтали";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown3
            // 
            numericUpDown3.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown3.ForeColor = Color.FromArgb(61, 61, 61);
            numericUpDown3.Location = new Point(0, 24);
            numericUpDown3.Margin = new Padding(0);
            numericUpDown3.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(350, 27);
            numericUpDown3.TabIndex = 3;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(label7);
            flowLayoutPanel6.Controls.Add(numericUpDown4);
            flowLayoutPanel6.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel6.Location = new Point(10, 135);
            flowLayoutPanel6.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(350, 60);
            flowLayoutPanel6.TabIndex = 2;
            // 
            // label7
            // 
            label7.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label7.ForeColor = Color.FromArgb(61, 61, 61);
            label7.Location = new Point(0, 0);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(350, 24);
            label7.TabIndex = 1;
            label7.Text = "Кол-во узлов сетки по вертикали";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown4
            // 
            numericUpDown4.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown4.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown4.ForeColor = Color.FromArgb(61, 61, 61);
            numericUpDown4.Location = new Point(0, 24);
            numericUpDown4.Margin = new Padding(0);
            numericUpDown4.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(350, 27);
            numericUpDown4.TabIndex = 3;
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.Controls.Add(label8);
            flowLayoutPanel10.Controls.Add(numericUpDown5);
            flowLayoutPanel10.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel10.Location = new Point(10, 205);
            flowLayoutPanel10.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            flowLayoutPanel10.Size = new Size(350, 60);
            flowLayoutPanel10.TabIndex = 3;
            // 
            // label8
            // 
            label8.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label8.ForeColor = Color.FromArgb(61, 61, 61);
            label8.Location = new Point(0, 0);
            label8.Margin = new Padding(0);
            label8.Name = "label8";
            label8.Size = new Size(350, 24);
            label8.TabIndex = 1;
            label8.Text = "Максимальное число итераций";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown5
            // 
            numericUpDown5.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown5.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown5.ForeColor = Color.FromArgb(61, 61, 61);
            numericUpDown5.Location = new Point(0, 24);
            numericUpDown5.Margin = new Padding(0);
            numericUpDown5.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(350, 27);
            numericUpDown5.TabIndex = 3;
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.Controls.Add(label9);
            flowLayoutPanel11.Controls.Add(numericUpDown6);
            flowLayoutPanel11.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel11.Location = new Point(10, 275);
            flowLayoutPanel11.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Size = new Size(350, 60);
            flowLayoutPanel11.TabIndex = 4;
            // 
            // label9
            // 
            label9.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label9.ForeColor = Color.FromArgb(61, 61, 61);
            label9.Location = new Point(0, 0);
            label9.Margin = new Padding(0);
            label9.Name = "label9";
            label9.Size = new Size(350, 24);
            label9.TabIndex = 1;
            label9.Text = "Коэффициент релаксации";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown6
            // 
            numericUpDown6.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown6.DecimalPlaces = 2;
            numericUpDown6.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown6.ForeColor = Color.FromArgb(61, 61, 61);
            numericUpDown6.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown6.Location = new Point(0, 24);
            numericUpDown6.Margin = new Padding(0);
            numericUpDown6.Maximum = new decimal(new int[] { 100, 0, 0, 131072 });
            numericUpDown6.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(350, 27);
            numericUpDown6.TabIndex = 3;
            numericUpDown6.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // problemSettingsPanel
            // 
            problemSettingsPanel.BackColor = Color.FromArgb(247, 241, 227);
            problemSettingsPanel.Controls.Add(problemSettingsCollapsePanel);
            problemSettingsPanel.Controls.Add(LHPanel);
            problemSettingsPanel.Controls.Add(PrPanel);
            problemSettingsPanel.Controls.Add(GrPanel);
            problemSettingsPanel.Dock = DockStyle.Top;
            problemSettingsPanel.Location = new Point(0, 0);
            problemSettingsPanel.Margin = new Padding(0);
            problemSettingsPanel.Name = "problemSettingsPanel";
            problemSettingsPanel.Size = new Size(383, 270);
            problemSettingsPanel.TabIndex = 0;
            // 
            // problemSettingsCollapsePanel
            // 
            problemSettingsCollapsePanel.Controls.Add(problemSettingsCollapseButton);
            problemSettingsCollapsePanel.Controls.Add(problemSettingsCollapseLabel);
            problemSettingsCollapsePanel.Location = new Point(10, 10);
            problemSettingsCollapsePanel.Margin = new Padding(10, 10, 10, 0);
            problemSettingsCollapsePanel.Name = "problemSettingsCollapsePanel";
            problemSettingsCollapsePanel.Size = new Size(350, 40);
            problemSettingsCollapsePanel.TabIndex = 0;
            // 
            // problemSettingsCollapseButton
            // 
            problemSettingsCollapseButton.Cursor = Cursors.Hand;
            problemSettingsCollapseButton.FlatStyle = FlatStyle.Flat;
            problemSettingsCollapseButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            problemSettingsCollapseButton.ForeColor = Color.FromArgb(61, 61, 61);
            problemSettingsCollapseButton.Location = new Point(0, 0);
            problemSettingsCollapseButton.Margin = new Padding(0);
            problemSettingsCollapseButton.Name = "problemSettingsCollapseButton";
            problemSettingsCollapseButton.Size = new Size(40, 40);
            problemSettingsCollapseButton.TabIndex = 0;
            problemSettingsCollapseButton.Text = "V";
            problemSettingsCollapseButton.UseVisualStyleBackColor = true;
            problemSettingsCollapseButton.Click += problemSettingsCollapseButton_Click;
            // 
            // problemSettingsCollapseLabel
            // 
            problemSettingsCollapseLabel.Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            problemSettingsCollapseLabel.ForeColor = Color.FromArgb(61, 61, 61);
            problemSettingsCollapseLabel.Location = new Point(50, 0);
            problemSettingsCollapseLabel.Margin = new Padding(10, 0, 0, 0);
            problemSettingsCollapseLabel.Name = "problemSettingsCollapseLabel";
            problemSettingsCollapseLabel.Size = new Size(300, 40);
            problemSettingsCollapseLabel.TabIndex = 1;
            problemSettingsCollapseLabel.Text = "Параметры задачи";
            problemSettingsCollapseLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LHPanel
            // 
            LHPanel.Controls.Add(LHLabel);
            LHPanel.Controls.Add(LHComboBox);
            LHPanel.FlowDirection = FlowDirection.TopDown;
            LHPanel.Location = new Point(10, 65);
            LHPanel.Margin = new Padding(10, 15, 10, 5);
            LHPanel.Name = "LHPanel";
            LHPanel.Size = new Size(350, 60);
            LHPanel.TabIndex = 1;
            // 
            // LHLabel
            // 
            LHLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LHLabel.ForeColor = Color.FromArgb(61, 61, 61);
            LHLabel.Location = new Point(0, 0);
            LHLabel.Margin = new Padding(0);
            LHLabel.Name = "LHLabel";
            LHLabel.Size = new Size(350, 24);
            LHLabel.TabIndex = 0;
            LHLabel.Text = "Отношение L/H";
            LHLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LHComboBox
            // 
            LHComboBox.BackColor = Color.FromArgb(247, 241, 227);
            LHComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LHComboBox.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LHComboBox.ForeColor = Color.FromArgb(61, 61, 61);
            LHComboBox.FormattingEnabled = true;
            LHComboBox.ItemHeight = 18;
            LHComboBox.Items.AddRange(new object[] { "10 : 1", "8 : 1", "6 : 1", "4 : 1", "2 : 1", "1 : 1", "1 : 2", "1 : 4", "1 : 6", "1 : 8", "1 : 10" });
            LHComboBox.Location = new Point(0, 24);
            LHComboBox.Margin = new Padding(0);
            LHComboBox.Name = "LHComboBox";
            LHComboBox.Size = new Size(350, 26);
            LHComboBox.TabIndex = 1;
            // 
            // PrPanel
            // 
            PrPanel.Controls.Add(PrLabel);
            PrPanel.Controls.Add(PrNumeric);
            PrPanel.FlowDirection = FlowDirection.TopDown;
            PrPanel.Location = new Point(10, 135);
            PrPanel.Margin = new Padding(10, 5, 10, 5);
            PrPanel.Name = "PrPanel";
            PrPanel.Size = new Size(350, 60);
            PrPanel.TabIndex = 2;
            // 
            // PrLabel
            // 
            PrLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            PrLabel.ForeColor = Color.FromArgb(61, 61, 61);
            PrLabel.Location = new Point(0, 0);
            PrLabel.Margin = new Padding(0);
            PrLabel.Name = "PrLabel";
            PrLabel.Size = new Size(350, 24);
            PrLabel.TabIndex = 1;
            PrLabel.Text = "Число Прандтля";
            PrLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PrNumeric
            // 
            PrNumeric.BorderStyle = BorderStyle.FixedSingle;
            PrNumeric.DecimalPlaces = 2;
            PrNumeric.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            PrNumeric.ForeColor = Color.FromArgb(61, 61, 61);
            PrNumeric.Location = new Point(0, 24);
            PrNumeric.Margin = new Padding(0);
            PrNumeric.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            PrNumeric.Name = "PrNumeric";
            PrNumeric.Size = new Size(350, 27);
            PrNumeric.TabIndex = 2;
            // 
            // GrPanel
            // 
            GrPanel.Controls.Add(GrLabel);
            GrPanel.Controls.Add(GrNumeric);
            GrPanel.FlowDirection = FlowDirection.TopDown;
            GrPanel.Location = new Point(10, 205);
            GrPanel.Margin = new Padding(10, 5, 10, 5);
            GrPanel.Name = "GrPanel";
            GrPanel.Size = new Size(350, 60);
            GrPanel.TabIndex = 3;
            // 
            // GrLabel
            // 
            GrLabel.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            GrLabel.ForeColor = Color.FromArgb(61, 61, 61);
            GrLabel.Location = new Point(0, 0);
            GrLabel.Margin = new Padding(0);
            GrLabel.Name = "GrLabel";
            GrLabel.Size = new Size(350, 24);
            GrLabel.TabIndex = 1;
            GrLabel.Text = "Число Грасгофа";
            GrLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GrNumeric
            // 
            GrNumeric.BorderStyle = BorderStyle.FixedSingle;
            GrNumeric.DecimalPlaces = 2;
            GrNumeric.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            GrNumeric.ForeColor = Color.FromArgb(61, 61, 61);
            GrNumeric.Location = new Point(0, 24);
            GrNumeric.Margin = new Padding(0);
            GrNumeric.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            GrNumeric.Name = "GrNumeric";
            GrNumeric.Size = new Size(350, 27);
            GrNumeric.TabIndex = 3;
            // 
            // actionPanel
            // 
            actionPanel.BackColor = Color.FromArgb(247, 241, 227);
            actionPanel.Controls.Add(startCalculationButton);
            actionPanel.Controls.Add(cancelCalculationButton);
            actionPanel.Controls.Add(progressBar);
            actionPanel.Location = new Point(0, 660);
            actionPanel.Margin = new Padding(0);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(400, 101);
            actionPanel.TabIndex = 2;
            // 
            // startCalculationButton
            // 
            startCalculationButton.FlatStyle = FlatStyle.Flat;
            startCalculationButton.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            startCalculationButton.ForeColor = Color.FromArgb(61, 61, 61);
            startCalculationButton.Location = new Point(10, 10);
            startCalculationButton.Margin = new Padding(10, 10, 5, 5);
            startCalculationButton.Name = "startCalculationButton";
            startCalculationButton.Size = new Size(185, 35);
            startCalculationButton.TabIndex = 0;
            startCalculationButton.Text = "Начать вычисления";
            startCalculationButton.UseVisualStyleBackColor = true;
            // 
            // cancelCalculationButton
            // 
            cancelCalculationButton.FlatStyle = FlatStyle.Flat;
            cancelCalculationButton.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            cancelCalculationButton.ForeColor = Color.FromArgb(61, 61, 61);
            cancelCalculationButton.Location = new Point(205, 10);
            cancelCalculationButton.Margin = new Padding(5, 10, 10, 10);
            cancelCalculationButton.Name = "cancelCalculationButton";
            cancelCalculationButton.Size = new Size(185, 35);
            cancelCalculationButton.TabIndex = 1;
            cancelCalculationButton.Text = "Прервать вычисления";
            cancelCalculationButton.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(10, 55);
            progressBar.Margin = new Padding(10, 0, 10, 10);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(380, 35);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(actionPanel);
            Controls.Add(settingsPanel);
            Controls.Add(glControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "hita";
            settingsPanel.ResumeLayout(false);
            paintSettingsPanel.ResumeLayout(false);
            paintSettingsCollapsePanel.ResumeLayout(false);
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel16.ResumeLayout(false);
            isolinesNumberPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)isolinesNumberNumeric).EndInit();
            solverSettingsPanel.ResumeLayout(false);
            solverSettingsCollapsePanel.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            flowLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            flowLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            flowLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            problemSettingsPanel.ResumeLayout(false);
            problemSettingsCollapsePanel.ResumeLayout(false);
            LHPanel.ResumeLayout(false);
            PrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PrNumeric).EndInit();
            GrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GrNumeric).EndInit();
            actionPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl;
        private Panel settingsPanel;
        private FlowLayoutPanel problemSettingsPanel;
        private FlowLayoutPanel problemSettingsCollapsePanel;
        private FlowLayoutPanel LHPanel;
        private FlowLayoutPanel PrPanel;
        private FlowLayoutPanel GrPanel;
        private FlowLayoutPanel solverSettingsPanel;
        private FlowLayoutPanel solverSettingsCollapsePanel;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel11;
        private Button problemSettingsCollapseButton;
        private Button solverSettingsCollapseButton;
        private Label solverSettingsCollapseLabel;
        private Label problemSettingsCollapseLabel;
        private Label LHLabel;
        private ComboBox LHComboBox;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label PrLabel;
        private Label GrLabel;
        private NumericUpDown PrNumeric;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private NumericUpDown GrNumeric;
        private FlowLayoutPanel paintSettingsPanel;
        private FlowLayoutPanel paintSettingsCollapsePanel;
        private Button paintSettingsCollapseButton;
        private Label paintSettingsCollapseLabel;
        private FlowLayoutPanel flowLayoutPanel14;
        private Label paintTargetLabel;
        private FlowLayoutPanel isolinesNumberPanel;
        private Label isolinesNumberLabel;
        private NumericUpDown isolinesNumberNumeric;
        private FlowLayoutPanel flowLayoutPanel16;
        private Label TemperatureLabel;
        private Label CurrentLabel;
        private Label EddyLabel;
        private Label VxLabel;
        private Label VyLabel;
        private FlowLayoutPanel actionPanel;
        private Button startCalculationButton;
        private Button cancelCalculationButton;
        private ProgressBar progressBar;
    }
}
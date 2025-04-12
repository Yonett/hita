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
            flowLayoutPanel12 = new FlowLayoutPanel();
            flowLayoutPanel13 = new FlowLayoutPanel();
            button3 = new Button();
            label10 = new Label();
            flowLayoutPanel14 = new FlowLayoutPanel();
            label11 = new Label();
            flowLayoutPanel15 = new FlowLayoutPanel();
            label12 = new Label();
            numericUpDown8 = new NumericUpDown();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            button2 = new Button();
            label2 = new Label();
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            button1 = new Button();
            label1 = new Label();
            flowLayoutPanel7 = new FlowLayoutPanel();
            label3 = new Label();
            comboBox1 = new ComboBox();
            flowLayoutPanel8 = new FlowLayoutPanel();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            flowLayoutPanel9 = new FlowLayoutPanel();
            label5 = new Label();
            numericUpDown2 = new NumericUpDown();
            actionPanel = new Panel();
            flowLayoutPanel16 = new FlowLayoutPanel();
            label16 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label17 = new Label();
            settingsPanel.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            flowLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            flowLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            flowLayoutPanel16.SuspendLayout();
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
            settingsPanel.BackColor = SystemColors.ActiveCaption;
            settingsPanel.Controls.Add(flowLayoutPanel12);
            settingsPanel.Controls.Add(flowLayoutPanel2);
            settingsPanel.Controls.Add(flowLayoutPanel1);
            settingsPanel.Location = new Point(0, 0);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(400, 661);
            settingsPanel.TabIndex = 1;
            // 
            // flowLayoutPanel12
            // 
            flowLayoutPanel12.BackColor = SystemColors.ControlDark;
            flowLayoutPanel12.Controls.Add(flowLayoutPanel13);
            flowLayoutPanel12.Controls.Add(flowLayoutPanel14);
            flowLayoutPanel12.Controls.Add(flowLayoutPanel15);
            flowLayoutPanel12.Dock = DockStyle.Top;
            flowLayoutPanel12.Location = new Point(0, 610);
            flowLayoutPanel12.Margin = new Padding(0);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Size = new Size(383, 200);
            flowLayoutPanel12.TabIndex = 2;
            // 
            // flowLayoutPanel13
            // 
            flowLayoutPanel13.Controls.Add(button3);
            flowLayoutPanel13.Controls.Add(label10);
            flowLayoutPanel13.Location = new Point(10, 10);
            flowLayoutPanel13.Margin = new Padding(10, 10, 10, 0);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            flowLayoutPanel13.Size = new Size(350, 40);
            flowLayoutPanel13.TabIndex = 0;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            button3.Location = new Point(0, 0);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(40, 40);
            button3.TabIndex = 2;
            button3.Text = "V";
            button3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.Font = new Font("IBM Plex Sans", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            label10.Location = new Point(50, 0);
            label10.Margin = new Padding(10, 0, 0, 0);
            label10.Name = "label10";
            label10.Size = new Size(300, 40);
            label10.TabIndex = 3;
            label10.Text = "Параметры отрисовки";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.Controls.Add(label11);
            flowLayoutPanel14.Controls.Add(flowLayoutPanel16);
            flowLayoutPanel14.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel14.Location = new Point(10, 65);
            flowLayoutPanel14.Margin = new Padding(10, 15, 10, 5);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Size = new Size(350, 60);
            flowLayoutPanel14.TabIndex = 1;
            flowLayoutPanel14.WrapContents = false;
            // 
            // label11
            // 
            label11.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label11.Location = new Point(0, 0);
            label11.Margin = new Padding(0);
            label11.Name = "label11";
            label11.Size = new Size(350, 24);
            label11.TabIndex = 1;
            label11.Text = "Цель отрисовки";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel15
            // 
            flowLayoutPanel15.Controls.Add(label12);
            flowLayoutPanel15.Controls.Add(numericUpDown8);
            flowLayoutPanel15.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel15.Location = new Point(10, 135);
            flowLayoutPanel15.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel15.Name = "flowLayoutPanel15";
            flowLayoutPanel15.Size = new Size(350, 60);
            flowLayoutPanel15.TabIndex = 2;
            // 
            // label12
            // 
            label12.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label12.Location = new Point(0, 0);
            label12.Margin = new Padding(0);
            label12.Name = "label12";
            label12.Size = new Size(350, 24);
            label12.TabIndex = 1;
            label12.Text = "Кол-во изолиний";
            label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown8
            // 
            numericUpDown8.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown8.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown8.Location = new Point(0, 24);
            numericUpDown8.Margin = new Padding(0);
            numericUpDown8.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(347, 27);
            numericUpDown8.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = SystemColors.ControlDark;
            flowLayoutPanel2.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel6);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel10);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel11);
            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.Location = new Point(0, 270);
            flowLayoutPanel2.Margin = new Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(383, 340);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(button2);
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Location = new Point(10, 10);
            flowLayoutPanel3.Margin = new Padding(10, 10, 10, 0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(350, 40);
            flowLayoutPanel3.TabIndex = 0;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            button2.Location = new Point(0, 0);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(40, 40);
            button2.TabIndex = 2;
            button2.Text = "V";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Font = new Font("IBM Plex Sans", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            label2.Location = new Point(50, 0);
            label2.Margin = new Padding(10, 0, 0, 0);
            label2.Name = "label2";
            label2.Size = new Size(300, 40);
            label2.TabIndex = 3;
            label2.Text = "Параметры решателя";
            label2.TextAlign = ContentAlignment.MiddleLeft;
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
            numericUpDown3.Location = new Point(0, 24);
            numericUpDown3.Margin = new Padding(0);
            numericUpDown3.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(347, 27);
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
            numericUpDown4.Location = new Point(0, 24);
            numericUpDown4.Margin = new Padding(0);
            numericUpDown4.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(347, 27);
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
            numericUpDown5.Location = new Point(0, 24);
            numericUpDown5.Margin = new Padding(0);
            numericUpDown5.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(347, 27);
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
            numericUpDown6.Location = new Point(0, 24);
            numericUpDown6.Margin = new Padding(0);
            numericUpDown6.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(347, 27);
            numericUpDown6.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ControlDark;
            flowLayoutPanel1.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel7);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel8);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel9);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(383, 270);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(button1);
            flowLayoutPanel4.Controls.Add(label1);
            flowLayoutPanel4.Cursor = Cursors.Hand;
            flowLayoutPanel4.Location = new Point(10, 10);
            flowLayoutPanel4.Margin = new Padding(10, 10, 10, 0);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(350, 40);
            flowLayoutPanel4.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(40, 40);
            button1.TabIndex = 0;
            button1.Text = "V";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            label1.Location = new Point(50, 0);
            label1.Margin = new Padding(10, 0, 0, 0);
            label1.Name = "label1";
            label1.Size = new Size(300, 40);
            label1.TabIndex = 1;
            label1.Text = "Параметры задачи";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Controls.Add(label3);
            flowLayoutPanel7.Controls.Add(comboBox1);
            flowLayoutPanel7.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel7.Location = new Point(10, 65);
            flowLayoutPanel7.Margin = new Padding(10, 15, 10, 5);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(350, 60);
            flowLayoutPanel7.TabIndex = 1;
            // 
            // label3
            // 
            label3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.Location = new Point(0, 0);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(350, 24);
            label3.TabIndex = 0;
            label3.Text = "Отношение L/H";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            comboBox1.FormattingEnabled = true;
            comboBox1.ItemHeight = 18;
            comboBox1.Location = new Point(0, 24);
            comboBox1.Margin = new Padding(0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(350, 26);
            comboBox1.TabIndex = 1;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Controls.Add(label4);
            flowLayoutPanel8.Controls.Add(numericUpDown1);
            flowLayoutPanel8.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel8.Location = new Point(10, 135);
            flowLayoutPanel8.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(350, 60);
            flowLayoutPanel8.TabIndex = 2;
            // 
            // label4
            // 
            label4.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label4.Location = new Point(0, 0);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(350, 24);
            label4.TabIndex = 1;
            label4.Text = "Число Прандтля";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown1
            // 
            numericUpDown1.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown1.Location = new Point(0, 24);
            numericUpDown1.Margin = new Padding(0);
            numericUpDown1.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(347, 27);
            numericUpDown1.TabIndex = 2;
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.Controls.Add(label5);
            flowLayoutPanel9.Controls.Add(numericUpDown2);
            flowLayoutPanel9.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel9.Location = new Point(10, 205);
            flowLayoutPanel9.Margin = new Padding(10, 5, 10, 5);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            flowLayoutPanel9.Size = new Size(350, 60);
            flowLayoutPanel9.TabIndex = 3;
            // 
            // label5
            // 
            label5.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label5.Location = new Point(0, 0);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(350, 24);
            label5.TabIndex = 1;
            label5.Text = "Число Грасгофа";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown2
            // 
            numericUpDown2.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            numericUpDown2.Location = new Point(0, 24);
            numericUpDown2.Margin = new Padding(0);
            numericUpDown2.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(347, 27);
            numericUpDown2.TabIndex = 3;
            // 
            // actionPanel
            // 
            actionPanel.Location = new Point(0, 661);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(400, 100);
            actionPanel.TabIndex = 2;
            // 
            // flowLayoutPanel16
            // 
            flowLayoutPanel16.Controls.Add(label16);
            flowLayoutPanel16.Controls.Add(label13);
            flowLayoutPanel16.Controls.Add(label14);
            flowLayoutPanel16.Controls.Add(label15);
            flowLayoutPanel16.Controls.Add(label17);
            flowLayoutPanel16.Location = new Point(0, 24);
            flowLayoutPanel16.Margin = new Padding(0);
            flowLayoutPanel16.Name = "flowLayoutPanel16";
            flowLayoutPanel16.Size = new Size(350, 36);
            flowLayoutPanel16.TabIndex = 2;
            // 
            // label16
            // 
            label16.Location = new Point(0, 0);
            label16.Margin = new Padding(0);
            label16.Name = "label16";
            label16.Size = new Size(70, 36);
            label16.TabIndex = 3;
            label16.Text = "T";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.Location = new Point(70, 0);
            label13.Margin = new Padding(0);
            label13.Name = "label13";
            label13.Size = new Size(70, 36);
            label13.TabIndex = 4;
            label13.Text = "ψ";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.Location = new Point(140, 0);
            label14.Margin = new Padding(0);
            label14.Name = "label14";
            label14.Size = new Size(70, 36);
            label14.TabIndex = 5;
            label14.Text = "ω";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.Location = new Point(210, 0);
            label15.Margin = new Padding(0);
            label15.Name = "label15";
            label15.Size = new Size(70, 36);
            label15.TabIndex = 6;
            label15.Text = "Vx";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.Location = new Point(280, 0);
            label17.Margin = new Padding(0);
            label17.Name = "label17";
            label17.Size = new Size(70, 36);
            label17.TabIndex = 7;
            label17.Text = "Vy";
            label17.TextAlign = ContentAlignment.MiddleCenter;
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
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            flowLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            flowLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            flowLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            flowLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            flowLayoutPanel16.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl;
        private Panel settingsPanel;
        private Panel actionPanel;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel7;
        private FlowLayoutPanel flowLayoutPanel8;
        private FlowLayoutPanel flowLayoutPanel9;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel11;
        private Button button1;
        private Button button2;
        private Label label2;
        private Label label1;
        private Label label3;
        private ComboBox comboBox1;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private NumericUpDown numericUpDown2;
        private FlowLayoutPanel flowLayoutPanel12;
        private FlowLayoutPanel flowLayoutPanel13;
        private Button button3;
        private Label label10;
        private FlowLayoutPanel flowLayoutPanel14;
        private Label label11;
        private FlowLayoutPanel flowLayoutPanel15;
        private Label label12;
        private NumericUpDown numericUpDown8;
        private FlowLayoutPanel flowLayoutPanel16;
        private Label label16;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label17;
    }
}
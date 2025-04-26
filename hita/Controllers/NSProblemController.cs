using System;
using System.Xml.Linq;
using hita.Geometric;
using hita.Models;
using static System.Windows.Forms.LinkLabel;

namespace hita.Controllers
{
    public class NSProblemController
    {
        private readonly NSProblem _nsProblem = new NSProblem();
        public string? Folder { get; set; }
        public int NumberOfIsolines { get; set; } = 1;

        public int xs { get; set; }
        public int ys { get; set; }

        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Element> Elements { get; set; } = new List<Element>();
        private Triangle[] Triangles { get; set; } = new Triangle[8];

        public double TemperatureMax { get; set; } = 0.0;
        public double TemperatureMin { get; set; } = 0.0;
        public double TemperatureRange { get; set; } = 0.0;
        public double CurrentMax { get; set; } = 0.0;
        public double CurrentMin { get; set; } = 0.0;
        public double CurrentRange { get; set; } = 0.0;
        public double EddyMax { get; set; } = 0.0;
        public double EddyMin { get; set; } = 0.0;
        public double EddyRange { get; set; } = 0.0;
        public double VxMax { get; set; } = 0.0;
        public double VxMin { get; set; } = 0.0;
        public double VxRange { get; set; } = 0.0;
        public double VyMax { get; set; } = 0.0;
        public double VyMin { get; set; } = 0.0;
        public double VyRange { get; set; } = 0.0;

        public List<double> TemperatureValues { get; set; } = new List<double>();
        public List<double> CurrentValues { get; set; } = new List<double>();
        public List<double> EddyValues { get; set; } = new List<double>();
        public List<double> VxValues { get; set; } = new List<double>();
        public List<double> VyValues { get; set; } = new List<double>();

        public List<double> TemperatueIsolinesLevels { get; set; } = new List<double>();
        public List<double> CurrentIsolinesLevels { get; set; } = new List<double>();
        public List<double> EddyIsolinesLevels { get; set; } = new List<double>();
        public List<double> VxIsolinesLevels { get; set; } = new List<double>();
        public List<double> VyIsolinesLevels { get; set; } = new List<double>();

        public List<Line> TemperatueLines{ get; set; } = new List<Line>();
        public List<Line> CurrentLines{ get; set; } = new List<Line>();
        public List<Line> EddyLines{ get; set; } = new List<Line>();
        public List<Line> VxLines{ get; set; } = new List<Line>();
        public List<Line> VyLines{ get; set; } = new List<Line>();

        private double eps = 1e-16;

        private static Node SplitEdge(Node A, Node B, double alpha)
        {
            double alpha1 = alpha + 1.0;
            return new Node((A.x + alpha * B.x) / alpha1, (A.y + alpha * B.y) / alpha1);
        }
        
        public void SetParams(double Gr,
                              double Pr,
                              double Width,
                              double Height,
                              int WidthNodesCount,
                              int HeightNodesCount,
                              int SlaeMaxIter,
                              double WParam)
        {
            _nsProblem.Gr = Gr;
            _nsProblem.Pr = Pr;
            _nsProblem.Width = Width;
            _nsProblem.Height = Height;
            _nsProblem.WidthNodesCount = WidthNodesCount;
            _nsProblem.HeightNodesCount = HeightNodesCount;
            _nsProblem.HeatedSide = false;
            _nsProblem.SlaeEps = 1e-16;
            _nsProblem.SlaeMaxIter = SlaeMaxIter;
            _nsProblem.SystemEps = 1e-6;
            _nsProblem.SystemMaxIter = 1000;
            _nsProblem.WParam = WParam;
        }

        public void SolveProblem()
        {
            Folder = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            _nsProblem.Solve(Folder);

            ResetResults();
            ReadResults();
            CalcRanges();
            CalcElements();

            CalcLines(TemperatureValues, TemperatueLines, TemperatueIsolinesLevels);
            CalcLines(CurrentValues, CurrentLines, CurrentIsolinesLevels);
            CalcLines(EddyValues, EddyLines, EddyIsolinesLevels);
            CalcLines(VxValues, VxLines, VxIsolinesLevels);
            CalcLines(VyValues, VyLines, VyIsolinesLevels);
        }

        public void ResetResults()
        {
            Elements.Clear();
            Nodes.Clear();

            TemperatureValues.Clear();
            CurrentValues.Clear();
            EddyValues.Clear();
            VxValues.Clear();
            VyValues.Clear();

            TemperatueIsolinesLevels.Clear();
            CurrentIsolinesLevels.Clear();
            EddyIsolinesLevels.Clear();
            VxIsolinesLevels.Clear();
            VyIsolinesLevels.Clear();

            TemperatueLines.Clear();
            CurrentLines.Clear();
            EddyLines.Clear();
            VxLines.Clear();
            VyLines.Clear();
        }

        public void ReadResults()
        {
            using (StreamReader sr = new StreamReader($"{Folder}\\Nodes.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    Nodes.Add(new Node(
                        Convert.ToDouble(values[0]),
                        Convert.ToDouble(values[1])
                    ));
                }
            }

            using (StreamReader sr = new StreamReader($"{Folder}\\Heat.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    TemperatureValues.Add(Convert.ToDouble(values[2]));
                }
            }

            using (StreamReader sr = new StreamReader($"{Folder}\\Stream.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    CurrentValues.Add(Convert.ToDouble(values[2]));
                }
            }

            using (StreamReader sr = new StreamReader($"{Folder}\\Vortex.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    EddyValues.Add(Convert.ToDouble(values[2]));
                }
            }

            using (StreamReader sr = new StreamReader($"{Folder}\\Vx.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    VxValues.Add(Convert.ToDouble(values[2]));
                }
            }

            using (StreamReader sr = new StreamReader($"{Folder}\\Vy.txt"))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split('\t');

                    VyValues.Add(Convert.ToDouble(values[2]));
                }
            }
        }

        public void CalcRanges()
        {
            double IsolineStep = 0.0;

            TemperatureMax = TemperatureValues.Max();
            TemperatureMin = TemperatureValues.Min();
            TemperatureRange = Math.Abs(TemperatureMax - TemperatureMin);

            IsolineStep = TemperatureRange / NumberOfIsolines;
            for (int i = 1; i < NumberOfIsolines; i++)
                TemperatueIsolinesLevels.Add(TemperatureMin + IsolineStep * i);


            CurrentMax = CurrentValues.Max();
            CurrentMin = CurrentValues.Min();
            CurrentRange = Math.Abs(CurrentMax - CurrentMin);

            IsolineStep = CurrentRange / NumberOfIsolines;
            for (int i = 1; i < NumberOfIsolines; i++)
                CurrentIsolinesLevels.Add(CurrentMin + IsolineStep * i);


            EddyMax = EddyValues.Max();
            EddyMin = EddyValues.Min();
            EddyRange = Math.Abs(EddyMax - EddyMin);

            IsolineStep = EddyRange / NumberOfIsolines;
            for (int i = 1; i < NumberOfIsolines; i++)
                EddyIsolinesLevels.Add(EddyMin + IsolineStep * i);


            VxMax = VxValues.Max();
            VxMin = VxValues.Min();
            VxRange = Math.Abs(VxMax - VxMin);

            IsolineStep = VxRange / NumberOfIsolines;
            for (int i = 1; i < NumberOfIsolines; i++)
                VxIsolinesLevels.Add(VxMin + IsolineStep * i);


            VyMax = VyValues.Max();
            VyMin = VyValues.Min();
            VyRange = Math.Abs(VyMax - VyMin);

            IsolineStep = VyRange / NumberOfIsolines;
            for (int i = 1; i < NumberOfIsolines; i++)
                VyIsolinesLevels.Add(VyMin + IsolineStep * i);
        }

        private void CalcElements()
        {
            for (int j = 0; j < ys - 1; j++)
                for (int i = 0; i < xs - 1; i++)
                    Elements.Add(new Element(i + xs * j,
                                             i + xs * j + 1,
                                             i + xs * (j + 1),
                                             i + xs * (j + 1) + 1));
        }

        public void CalcLines(List<double> values, List<Line> lines, List<double> IsolinesLevels)
        {
            Node M = new Node();
            Node N = new Node();
            double alpha = 0.0;
            foreach (Element element in Elements)
            {
                #region Element Triangles
                // triangle 0
                Triangles[0].nodes[0] = Nodes[element.node_numbers[0]];
                Triangles[0].funcs[0] = values[element.node_numbers[0]];

                Triangles[0].nodes[1] = SplitEdge(Nodes[element.node_numbers[0]], Nodes[element.node_numbers[1]], 1);
                Triangles[0].funcs[1] = (values[element.node_numbers[0]] + values[element.node_numbers[1]]) / 2.0;

                Triangles[0].nodes[2] = SplitEdge(Nodes[element.node_numbers[0]], Nodes[element.node_numbers[2]], 1);
                Triangles[0].funcs[2] = (values[element.node_numbers[0]] + values[element.node_numbers[2]]) / 2.0;
                // ----------
                // triangle 1
                Triangles[1].nodes[0] = Triangles[0].nodes[1];
                Triangles[1].funcs[0] = Triangles[0].funcs[1];

                Triangles[1].nodes[1] = Triangles[0].nodes[2];
                Triangles[1].funcs[1] = Triangles[0].funcs[2];

                Triangles[1].nodes[2] = SplitEdge(Nodes[element.node_numbers[1]], Nodes[element.node_numbers[2]], 1);
                Triangles[1].funcs[2] = (values[element.node_numbers[1]] + values[element.node_numbers[2]]) / 2.0;
                // ----------
                // triangle 2
                Triangles[2].nodes[0] = Triangles[0].nodes[1];
                Triangles[2].funcs[0] = Triangles[0].funcs[1];

                Triangles[2].nodes[1] = Nodes[element.node_numbers[1]];
                Triangles[2].funcs[1] = values[element.node_numbers[1]];

                Triangles[2].nodes[2] = Triangles[1].nodes[2];
                Triangles[2].funcs[2] = Triangles[1].funcs[2];

                //-----------
                // triangle 3
                Triangles[3].nodes[0] = Nodes[element.node_numbers[1]];
                Triangles[3].funcs[0] = values[element.node_numbers[1]];

                Triangles[3].nodes[1] = Triangles[1].nodes[2];
                Triangles[3].funcs[1] = Triangles[1].funcs[2];

                Triangles[3].nodes[2] = SplitEdge(Nodes[element.node_numbers[1]], Nodes[element.node_numbers[3]], 1);
                Triangles[3].funcs[2] = (values[element.node_numbers[1]] + values[element.node_numbers[3]]) / 2.0;
                //-----------
                // triangle 4
                Triangles[4].nodes[0] = Triangles[0].nodes[2];
                Triangles[4].funcs[0] = Triangles[0].funcs[2];

                Triangles[4].nodes[1] = Triangles[1].nodes[2];
                Triangles[4].funcs[1] = Triangles[1].funcs[2];

                Triangles[4].nodes[2] = Nodes[element.node_numbers[2]];
                Triangles[4].funcs[2] = values[element.node_numbers[2]];
                //-----------
                // triangle 5
                Triangles[5].nodes[0] = Triangles[1].nodes[2];
                Triangles[5].funcs[0] = Triangles[1].funcs[2];

                Triangles[5].nodes[1] = Nodes[element.node_numbers[2]];
                Triangles[5].funcs[1] = values[element.node_numbers[2]];

                Triangles[5].nodes[2] = SplitEdge(Nodes[element.node_numbers[2]], Nodes[element.node_numbers[3]], 1);
                Triangles[5].funcs[2] = (values[element.node_numbers[2]] + values[element.node_numbers[3]]) / 2.0;
                //-----------
                // triangle 6
                Triangles[6].nodes[0] = Triangles[1].nodes[2];
                Triangles[6].funcs[0] = Triangles[1].funcs[2];

                Triangles[6].nodes[1] = Triangles[3].nodes[2];
                Triangles[6].funcs[1] = Triangles[3].funcs[2];

                Triangles[6].nodes[2] = Triangles[5].nodes[2];
                Triangles[6].funcs[2] = Triangles[5].funcs[2];
                //-----------
                // triangle 7
                Triangles[7].nodes[0] = Triangles[3].nodes[2];
                Triangles[7].funcs[0] = Triangles[3].funcs[2];

                Triangles[7].nodes[1] = Triangles[5].nodes[2];
                Triangles[7].funcs[1] = Triangles[5].funcs[2];

                Triangles[7].nodes[2] = Nodes[element.node_numbers[3]];
                Triangles[7].funcs[2] = values[element.node_numbers[3]];
                //-----------
                #endregion

                foreach (double isoline in IsolinesLevels)
                    foreach (Triangle triangle in Triangles)
                    {
                        if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 triangle.funcs[2] > isoline)                       // case b
                        {
                            M = triangle.nodes[0];
                            N = triangle.nodes[1];
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[1] > isoline)                       // case b
                        {
                            M = triangle.nodes[0];
                            N = triangle.nodes[2];
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[0] > isoline)                       // case b
                        {
                            M = triangle.nodes[1];
                            N = triangle.nodes[2];
                            lines.Add(new Line(M, N));
                        }
                        //---------------------------
                        else if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 triangle.funcs[2] < isoline)                       // case c
                        {
                            M = triangle.nodes[0];
                            N = triangle.nodes[1];
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[1] < isoline)                       // case c
                        {
                            M = triangle.nodes[0];
                            N = triangle.nodes[2];
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[0] < isoline)                       // case c
                        {
                            M = triangle.nodes[1];
                            N = triangle.nodes[2];
                            lines.Add(new Line(M, N));
                        }
                        //---------------------------
                        else if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 triangle.funcs[1] > isoline &&
                                 triangle.funcs[2] < isoline)                       // case f
                        {
                            M = triangle.nodes[0];

                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[1], triangle.nodes[2], alpha);
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 triangle.funcs[0] > isoline &&
                                 triangle.funcs[2] < isoline)                       // case f
                        {
                            M = triangle.nodes[1];

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[2], alpha);
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[0] > isoline &&
                                 triangle.funcs[1] < isoline)                       // case f
                        {
                            M = triangle.nodes[2];

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[1]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[1], alpha);
                            lines.Add(new Line(M, N));
                        }
                        // ***
                        else if (Math.Abs(triangle.funcs[0] - isoline) < eps &&
                                 triangle.funcs[1] < isoline &&
                                 triangle.funcs[2] > isoline)                       // case f
                        {
                            M = triangle.nodes[0];

                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[1], triangle.nodes[2], alpha);
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[1] - isoline) < eps &&
                                 triangle.funcs[0] < isoline &&
                                 triangle.funcs[2] > isoline)                       // case f
                        {
                            M = triangle.nodes[1];

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[2], alpha);
                            lines.Add(new Line(M, N));
                        }
                        else if (Math.Abs(triangle.funcs[2] - isoline) < eps &&
                                 triangle.funcs[0] < isoline &&
                                 triangle.funcs[1] > isoline)                       // case f
                        {
                            M = triangle.nodes[2];

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[1]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[1], alpha);
                            lines.Add(new Line(M, N));
                        }
                        //---------------------------
                        else if (triangle.funcs[0] > isoline &&
                                 triangle.funcs[1] < isoline &&
                                 triangle.funcs[2] < isoline)           // case g
                        {
                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[1]);
                            M = SplitEdge(triangle.nodes[0], triangle.nodes[1], alpha);

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[2], alpha);

                            lines.Add(new Line(M, N));
                        }
                        else if (triangle.funcs[0] < isoline &&
                                 triangle.funcs[1] > isoline &&
                                 triangle.funcs[2] < isoline)           // case g
                        {
                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[0]);
                            M = SplitEdge(triangle.nodes[1], triangle.nodes[0], alpha);

                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[1], triangle.nodes[2], alpha);

                            lines.Add(new Line(M, N));
                        }
                        else if (triangle.funcs[0] < isoline &&
                                 triangle.funcs[1] < isoline &&
                                 triangle.funcs[2] > isoline)           // case g
                        {
                            alpha = (triangle.funcs[2] - isoline) / (isoline - triangle.funcs[0]);
                            M = SplitEdge(triangle.nodes[2], triangle.nodes[0], alpha);

                            alpha = (triangle.funcs[2] - isoline) / (isoline - triangle.funcs[1]);
                            N = SplitEdge(triangle.nodes[2], triangle.nodes[1], alpha);

                            lines.Add(new Line(M, N));
                        }
                        //---------------------------
                        else if (triangle.funcs[0] < isoline &&
                                 triangle.funcs[1] > isoline &&
                                 triangle.funcs[2] > isoline)           // case h
                        {
                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[1]);
                            M = SplitEdge(triangle.nodes[0], triangle.nodes[1], alpha);

                            alpha = (triangle.funcs[0] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[0], triangle.nodes[2], alpha);

                            lines.Add(new Line(M, N));
                        }
                        else if (triangle.funcs[0] > isoline &&
                                 triangle.funcs[1] < isoline &&
                                 triangle.funcs[2] > isoline)           // case h
                        {
                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[0]);
                            M = SplitEdge(triangle.nodes[1], triangle.nodes[0], alpha);

                            alpha = (triangle.funcs[1] - isoline) / (isoline - triangle.funcs[2]);
                            N = SplitEdge(triangle.nodes[1], triangle.nodes[2], alpha);

                            lines.Add(new Line(M, N));
                        }
                        else if (triangle.funcs[0] > isoline &&
                                 triangle.funcs[1] > isoline &&
                                 triangle.funcs[2] < isoline)           // case h
                        {
                            alpha = (triangle.funcs[2] - isoline) / (isoline - triangle.funcs[0]);
                            M = SplitEdge(triangle.nodes[2], triangle.nodes[0], alpha);

                            alpha = (triangle.funcs[2] - isoline) / (isoline - triangle.funcs[1]);
                            N = SplitEdge(triangle.nodes[2], triangle.nodes[1], alpha);

                            lines.Add(new Line(M, N));
                        }
                        // ------------------------------------------------------------------------------------
                    }
            }
        }

        public NSProblemController()
        {
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangles[i] = new Triangle();
            }

        }
    }
}

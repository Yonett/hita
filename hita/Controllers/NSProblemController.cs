using System;
using hita.Geometric;
using hita.Models;

namespace hita.Controllers
{
    public class NSProblemController
    {
        private readonly NSProblem _nsProblem = new NSProblem();
        public string? Folder { get; set; }
        public int NumberOfIsolines { get; set; } = 1;

        private int xs { get; set; }
        private int ys { get; set; }

        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Element> Elements { get; set; } = new List<Element>();
        private Triangle[] Triangles { get; set; } = new Triangle[8];

        public double TemperatureMax { get; set; } = 0.0;
        public double TemperatureMin { get; set; } = 0.0;
        public double CurrentMax { get; set; } = 0.0;
        public double CurrentMin { get; set; } = 0.0;
        public double EddyMax { get; set; } = 0.0;
        public double EddyMin { get; set; } = 0.0;
        public double VxMax { get; set; } = 0.0;
        public double VxMin { get; set; } = 0.0;
        public double VyMax { get; set; } = 0.0;
        public double VyMin { get; set; } = 0.0;

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

        private double eps = 1e-8;

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
            _nsProblem.SystemMaxIter = 3000;
            _nsProblem.WParam = WParam;
        }

        public void SolveProblem()
        {
            Folder = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            _nsProblem.Solve(Folder);
        }

        public void ReadResults()
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

        public NSProblemController() { }
    }
}

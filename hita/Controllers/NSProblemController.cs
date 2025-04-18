using hita.Models;

namespace hita.Controllers
{
    public class NSProblemController
    {
        private readonly NSProblem _nsProblem = new NSProblem();
        public string? Folder { get; set; }
        public List<double> TemperatureValues { get; set; } = new List<double>();
        public List<double> CurrentValues { get; set; } = new List<double>();
        public List<double> EddyValues { get; set; } = new List<double>();
        public List<double> VxValues { get; set; } = new List<double>();
        public List<double> VyValues { get; set; } = new List<double>();

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

        public async Task SolveProblem()
        {
            Folder = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            await Task.Run(() => _nsProblem.Solve(Folder));
        }

        public NSProblemController() { }
    }
}

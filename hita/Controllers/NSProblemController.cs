using hita.Models;
using hita.Views;

namespace hita.Controllers
{
    public class NSProblemController
    {
        private readonly NSProblem _nsProblem = new NSProblem();
        private readonly MainForm Form;
        public void SetParams()
        {
            _nsProblem.Gr = 1e+3;
            _nsProblem.Pr = 16;
            _nsProblem.Width = 1;
            _nsProblem.Height = 1;
            _nsProblem.WidthNodesCount = 5;
            _nsProblem.HeightNodesCount = 5;
            _nsProblem.HeatedSide = false;
            _nsProblem.SlaeEps = 1e-16;
            _nsProblem.SlaeMaxIter = 1000;
            _nsProblem.SystemEps = 1e-6;
            _nsProblem.SystemMaxIter = 3000;
            _nsProblem.WParam = 0.3;
        }

        public void SolveProblem()
        {
            _nsProblem.Solve();
        }

        public NSProblemController(MainForm Form)
        {
            this.Form = Form;
        }
    }
}

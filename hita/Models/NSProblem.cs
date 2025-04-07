using System.Runtime.InteropServices;

namespace hita.Models
{
    public class NSProblem
    {
        public double Gr { get; set; }
        public double Pr { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public int WidthNodesCount { get; set; }
        public int HeightNodesCount { get; set; }

        public bool HeatedSide { get; set; }

        public double SlaeEps { get; set; }
        public int SlaeMaxIter { get; set; }

        public double SystemEps { get; set; }
        public int SystemMaxIter { get; set; }

        public double WParam { get; set; }

        private string Folder { get; set; } = String.Empty;

        [DllImport("NSProblem.dll", EntryPoint = "createNsProblem")]
        private static extern IntPtr _createNsProblem
            (
            double Gr,
            double Pr,
            double width,
            double height,
            int sptCnt_w,
            int sptCnt_h,
            bool heatedSide,
            double slaeEps,
            int slaeMaxIter,
            double sEps,
            int sMaxIter
            );

        [DllImport("NSProblem.dll", EntryPoint = "deleteNsProblem")]
        private static extern void _deleteNsProblem(IntPtr nsProblemPointer);

        [DllImport("NSProblem.dll", EntryPoint = "solveNsProblem")]
        private static extern bool _solveNsProblem(IntPtr nsProblemPointer, double w);

        [DllImport("NSProblem.dll", EntryPoint = "printResultsNsProblem")]
        private static extern void _printResultsNsProblem(IntPtr nsProblemPointer, string folder);

        public NSProblem() { }

        public bool Solve()
        {
            Folder = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            bool result;
            IntPtr NsProblemPointer = _createNsProblem(Gr,
                                                       Pr,
                                                       Width,
                                                       Height,
                                                       WidthNodesCount,
                                                       HeightNodesCount,
                                                       HeatedSide,
                                                       SlaeEps,
                                                       SlaeMaxIter,
                                                       SystemEps,
                                                       SystemMaxIter);

            if (NsProblemPointer == IntPtr.Zero)
                return false;

            result = _solveNsProblem(NsProblemPointer, WParam);

            _printResultsNsProblem(NsProblemPointer, Folder);

            _deleteNsProblem(NsProblemPointer);

            return result;
        }
    }
}

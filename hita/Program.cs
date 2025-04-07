using hita.Views;
using hita.Controllers;

namespace hita
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            MainForm Form = new MainForm();
            NSProblemController problemController = new NSProblemController(Form);

            problemController.SetParams();
            problemController.SolveProblem();

            ApplicationConfiguration.Initialize();
            Application.Run(Form);
        }
    }
}
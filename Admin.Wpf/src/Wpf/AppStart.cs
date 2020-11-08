
//using ShowMeTheXAML;
using Utility.Wpf;
using Wpf.Common;

namespace Wpf
{
    public class AppStart: WpfApplication
    {
        public AppStart()
        {
            //this.StartupUri = new System.Uri("Demo/Test.xaml", System.UriKind.Relative);
            this.StartupUri = new System.Uri("Login.xaml", System.UriKind.Relative);
        }
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
#if NETCOREAPP3_1
            //XamlDisplay.Init();
#endif
            Wpf.AppStart app = new Wpf.AppStart();
            StartManager.Start();
            app.Run();
        }
    }
}

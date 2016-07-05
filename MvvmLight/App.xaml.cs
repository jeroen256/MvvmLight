using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace MvvmLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static App()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info($"------------------ Starting Application Version {Assembly.GetEntryAssembly().GetName().Version.ToString()}: -------------------------------------------\n");
            DispatcherHelper.Initialize();
        }
    }
}

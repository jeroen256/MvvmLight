using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace MvvmLight
{
    /// <summary>
    /// Description for Mahapps.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the Mahapps class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Workaround for bug: Dialog overlapping titlebar https://github.com/MahApps/MahApps.Metro/issues/2109
            var overlayBox = this.Template.FindName("PART_OverlayBox", this) as FrameworkElement;
            Grid.SetRow(overlayBox, 1);
        }

    }
}
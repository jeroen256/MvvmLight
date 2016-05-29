using System.Windows;
using MvvmLight.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace MvvmLight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ShowView2")
            {
                var view2 = new MahappsWindow();
                view2.Show();
            }
        }
    }
}
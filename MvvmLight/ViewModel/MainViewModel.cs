using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using MvvmLight.Model;
using System.ComponentModel;
using MahApps.Metro.Controls;

namespace MvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataService _dataService;
        private readonly IDialogCoordinator _dialogCoordinator;

        public RelayCommand ShowSettingsWindow { private set; get; }
        public RelayCommand ShowDialog { get { return _showDialog ?? (_showDialog = new RelayCommand(ExecuteShowDialog)); } } RelayCommand _showDialog;
        public RelayCommand ShowProgressDialog { get { return _showProgressDialog ?? (_showProgressDialog = new RelayCommand(ShowProgressDialogCommandExecute)); } } RelayCommand _showProgressDialog;
        public RelayCommand<MetroWindow> LoadCommand { private set; get; }
        MetroWindow metroWindow;
        public RelayCommand<CancelEventArgs> CloseCommand { private set; get; }
        public const string WelcomeTitlePropertyName = "WelcomeTitle";
        public string WelcomeTitle { get { return _welcomeTitle; } set { Set(ref _welcomeTitle, value); } } string _welcomeTitle;
        public string Html { get { return _html; } set { Set(ref _html, value); } } string _html;
        public MainViewModel(IDataService dataService, IDialogCoordinator dialogCoordinator)
        {
            log.Info("Starting MainViewModel.");
            _dialogCoordinator = dialogCoordinator;
            _dataService = dataService;

            ShowSettingsWindow = new RelayCommand(ExecuteShowSettingsWindow);
            LoadCommand = new RelayCommand<MetroWindow>(LoadCommandExecute);
            CloseCommand = new RelayCommand<CancelEventArgs>(CloseCommandExecute);

            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });

            _dataService.GetHtml(
                (html, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    Html = html;
                });
        }

        public void ExecuteShowSettingsWindow()
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        string s;
        async void ExecuteShowDialog()
        {
            var result = await _dialogCoordinator.ShowMessageAsync(this, "Teste", "Teste", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings
            {
                AffirmativeButtonText = "OK",
                NegativeButtonText = "CANCELAR",
                AnimateShow = true,
                AnimateHide = false
            });

            if (result == MessageDialogResult.Negative)
                s = "negative";
            else { s = "not negative";  }
        }
        async void ShowProgressDialogCommandExecute()
        {
            var result = await _dialogCoordinator.ShowProgressAsync(this, "Even geduld..", "Bezig met iets", true);
            result.Canceled += delegate { result.CloseAsync(); };
            result.SetProgress(0.3);
            await Task.Delay(1000);
            result.SetProgress(0.5);
            await Task.Delay(1000);
            result.SetProgress(0.7);
            await Task.Delay(1000);
            result.SetProgress(1);
            if (result.IsOpen) { await result.CloseAsync(); }
            
        }

        async void LoadCommandExecute(MetroWindow metroWindow)
        {
            this.metroWindow = metroWindow;
            var result = await _dialogCoordinator.ShowProgressAsync(this, "Even geduld..", "Bezig met iets", true);
            result.Canceled += delegate { result.CloseAsync(); };
            result.SetProgress(0.3);
            await Task.Delay(100);
            result.SetProgress(0.5);
            await Task.Delay(100);
            result.SetProgress(0.7);
            await Task.Delay(100);
            result.SetProgress(1);
            await result.CloseAsync();
        }

        bool CloseCommandCanExecute = false;
        async void CloseCommandExecute(CancelEventArgs e)
        {
            if (CloseCommandCanExecute == false) {
                e.Cancel = true;
                var result = await _dialogCoordinator.ShowProgressAsync(this, "Even geduld..", "Bezig met afsluiten", true);
                result.Canceled += delegate { result.CloseAsync(); };
                result.SetProgress(0.3);
                await Task.Delay(100);
                result.SetProgress(0.5);
                await Task.Delay(100);
                result.SetProgress(0.7);
                await Task.Delay(100);
                result.SetProgress(1);
                await result.CloseAsync();
                CloseCommandCanExecute = true;
                metroWindow.Close();
            }
        }

        //public override void Cleanup()
        //{
        //    // Clean up if needed
        //    s = "cleaning";
        //    base.Cleanup();
        //}
    }
}
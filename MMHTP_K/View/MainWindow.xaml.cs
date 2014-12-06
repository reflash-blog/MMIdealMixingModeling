using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MMHTP_K.ViewModel;

namespace MMHTP_K.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void MainWindow_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToggleFlyout(0);
        }

        private async void About_OnClick(object sender, RoutedEventArgs e)
        {
            //MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "ok",
                ColorScheme = MetroDialogColorScheme.Theme
            };

            await this.ShowMessageAsync("О программе", "Программа реализует расчет математической модели",
                MessageDialogStyle.Affirmative, mySettings);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void ShowSchemeDialog(object sender, RoutedEventArgs e)
        {
            this.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            var dialog = (BaseMetroDialog)this.Resources["SchemeDialog"];

            await this.ShowMetroDialogAsync(dialog);
            
        }

        private async void HideSchemeDialog(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["SchemeDialog"];
            await this.HideMetroDialogAsync(dialog);
        }
    }
}

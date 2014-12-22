using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MMHTP_K.Model;
using MMHTP_K.View.Helpers;
using MMHTP_K.ViewModel;
using Newtonsoft.Json;

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


        private void InputWindowMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void MainWindow_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToggleFlyout(0);
        }

        private async void About_OnClick(object sender, RoutedEventArgs e)
        {
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

        private async void HelpExampleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            var mainWindowViewModel = this.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null)
                mainWindowViewModel.HelpText = "Для начала работы вы должны ввести данные";
            var dialog = (BaseMetroDialog)this.Resources["HelpDialog"];
            await this.ShowMetroDialogAsync(dialog);
            await Task.Run(() => Thread.Sleep(2500));
            await this.HideMetroDialogAsync(dialog);
            await Helper.ShowFileMenu();
            await Task.Run(() => Thread.Sleep(100));                                            // For Left click to be performed
            if (mainWindowViewModel != null) 
                mainWindowViewModel.HelpText = "Вы можете получить данные из файла";
            await this.ShowMetroDialogAsync(dialog);
            await Task.Run(() => Thread.Sleep(2500));
            await this.HideMetroDialogAsync(dialog);
            await Helper.ShowOpenFileMenu();
            if (mainWindowViewModel != null)
                mainWindowViewModel.HelpText = "Или ввести самостоятельно";
            await this.ShowMetroDialogAsync(dialog);
            await Task.Run(() => Thread.Sleep(2500));
            await this.HideMetroDialogAsync(dialog);
            await Helper.ShowInputFlyout();
        }



    }
}

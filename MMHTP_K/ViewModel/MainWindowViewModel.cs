using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using MMHTP_K.Control;
using MMHTP_K.Model;
using MMHTP_K.View;

namespace MMHTP_K.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private InputViewModel _inputViewModel;
        private ResultData _resultData;

        private bool runned = false;
        public bool Runned { get { return runned; } set { runned = value; RaisePropertyChanged("Runned"); } }   // Progress bar helper
        public InputViewModel InputViewModel                                                                    // Flyout binding
        {
            get { return _inputViewModel ?? (_inputViewModel = new InputViewModel()); }
        }

        public string HelpText { get; set; }                                                      // Help Dialog Text

        public ResultData ResultData                                        // Result binding
        {
            get
            {
                if (_resultData != null) return _resultData;
                _resultData = InitialCollectionInitialization();
                return _resultData;
            }
            set { _resultData = value; RaisePropertyChanged("ResultData"); }
        }

        private static ResultData InitialCollectionInitialization()
        {
            var coll = new ObservableCollection<ResultItem>();
            for (var i = 0.0; i < 10; i+=0.1)
            {
                coll.Add(new ResultItem { Length = i, Volume = Math.Sin(i),Performance = Math.Cos(i)});
            }
            return new ResultData{ResultItems = coll,SeparationFactor = 0};
        }

        #region OpenCommand
        RelayCommand _openCommand = null;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand((p) => OnOpen(p), (p) => CanOpen(p));
                }

                return _openCommand;
            }
        }

        private bool CanOpen(object parameter)
        {
            return true;
        }

        private async void OnOpen(object parameter)
        {
            await Open();
        }

        public async Task Open()
        {
            await InputViewModel.Open();
        }

        #endregion 

        #region CalcCommand
        RelayCommand _calcCommand = null;
        public ICommand CalcCommand
        {
            get
            {
                if (_calcCommand == null)
                {
                    _calcCommand = new RelayCommand((p) => OnCalc(p), (p) => CanCalc(p));
                }

                return _calcCommand;
            }
        }

        private bool CanCalc(object parameter)
        {
            return true;
        }

        private async void OnCalc(object parameter)
        {
            var data = _inputViewModel.InputData;
            var window = parameter as MainWindow;
            if (window == null) return;
            if (data.H <= 0 | data.R <= 0 | data.R > 100 | data.LStart >= data.LEnd | data.LStart <= 0 | data.LEnd > 300)
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "ok",
                    ColorScheme = MetroDialogColorScheme.Theme
                };

                await
                    window.ShowMessageAsync("Ошибка",
                        "Ограничения на ввод:\nH > 0\n0 < R < 100\nLStart < LEnd\nLStart > 0\nLEnd < 300",
                        MessageDialogStyle.Affirmative, mySettings);
                return;
                
            }
            await Calc();
        }

        public async Task Calc()
        {
            Runned = true;
            var model = new MathModel();
            ResultData = await model.Process(_inputViewModel.InputData);
            Runned = false;
        }

        #endregion 

        #region SaveCommand
        RelayCommand _saveCommand = null;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((p) => OnSave(p), (p) => CanSave(p));
                }

                return _saveCommand;
            }
        }

        private bool CanSave(object parameter)
        {
            return true;
        }

        private async void OnSave(object parameter)
        {
            await Save();
        }

        public async Task Save()
        {
            await InputViewModel.Save();
        }

        #endregion 

        #region SaveAsCommand
        RelayCommand _saveAsCommand = null;
        public ICommand SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new RelayCommand((p) => OnSaveAs(p), (p) => CanSaveAs(p));
                }

                return _saveAsCommand;
            }
        }

        private bool CanSaveAs(object parameter)
        {
            return true;
        }

        private async void OnSaveAs(object parameter)
        {
            await SaveAs();
        }

        public async Task SaveAs()
        {
            await InputViewModel.SaveAs();
        }

        #endregion 

        #region HelpCommand
        RelayCommand _helpCommand = null;
        public ICommand HelpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new RelayCommand((p) => OnHelp(p), (p) => CanHelp(p));
                }

                return _helpCommand;
            }
        }

        private bool CanHelp(object parameter)
        {
            return File.Exists(@"help.chm");
        }

        private void OnHelp(object parameter)
        {
            Help();
        }

        public void Help()
        {
            Process.Start(@"help.chm");
        }

        #endregion 
    }

    
    internal class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion // ICommand Members
    }
}

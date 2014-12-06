using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MMHTP_K.Model;

namespace MMHTP_K.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private InputViewModel _inputViewModel;
        private ObservableCollection<ResultData> _resultDatas;

        public InputViewModel InputViewModel
        {
            get { return _inputViewModel ?? (_inputViewModel = new InputViewModel()); }
        }



        public ObservableCollection<ResultData> ResultData
        {
            get
            {
                if (_resultDatas != null) return _resultDatas;
                _resultDatas = InitialCollectionInitialization();
                return _resultDatas;
            }
            set { _resultDatas = value; RaisePropertyChanged("ResultData"); }
        }

        private static ObservableCollection<Model.ResultData> InitialCollectionInitialization()
        {
            var coll = new ObservableCollection<ResultData>();
            for (var i = 0.0; i < 10; i+=0.1)
            {
                coll.Add(new ResultData { Time = i, Concentration = Math.Sin(i) });
            }
            return coll;
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

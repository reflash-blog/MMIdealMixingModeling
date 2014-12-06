using System.Threading.Tasks;
using MMHTP_K.Model;
using MMHTP_K.View;

namespace MMHTP_K.ViewModel
{
    class InputViewModel:ViewModelBase
    {
        private InputData _inputData = new InputData();

        public InputData InputData
        {
            get { return _inputData; }
            set
            {
                _inputData = value;
                RaisePropertyChanged("InputData");
            }
        }



        #region OpenCommand

        public async Task Open()
        {
            InputData = await FileSystemInteraction.OpenFile();
        }

        #endregion

        #region SaveCommand
        

        public async Task Save()
        {
            await FileSystemInteraction.SaveFile(InputData);
        }

        #endregion

        #region SaveAsCommand
        
        public async Task SaveAs()
        {
            await FileSystemInteraction.SaveAsFile(InputData);
        }

        #endregion 
    }
}

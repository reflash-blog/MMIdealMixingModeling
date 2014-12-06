using System.Collections.ObjectModel;
using System.ComponentModel;
using MMHTPCourseProject.Chart;

namespace MMHTPCourseProject.ViewModel
{
    internal class ResultPageDensityViewModel : INotifyPropertyChanged
    {
        private static ResultPageDensityViewModel _resultPageDensityViewModel;

        private ResultPageDensityViewModel()
        {
        }

        public static ResultPageDensityViewModel InitializeResultPageDensityViewModel()
        {
            return _resultPageDensityViewModel ?? (_resultPageDensityViewModel = new ResultPageDensityViewModel());
        }

        public ObservableCollection<DataSeriesInfo> Results { get; set; }


        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}

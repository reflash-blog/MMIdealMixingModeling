using System.Collections.ObjectModel;
using System.ComponentModel;
using MMHTPCourseProject.Chart;

namespace MMHTPCourseProject.ViewModel
{
    internal class ResultPagePressureViewModel : INotifyPropertyChanged
    {
        private static ResultPagePressureViewModel _resultPagePressureViewModel;

        private ResultPagePressureViewModel()
        {
        }

        public static ResultPagePressureViewModel InitializeResultPagePressureViewModel()
        {
            return _resultPagePressureViewModel ?? (_resultPagePressureViewModel = new ResultPagePressureViewModel());
        }

        public ObservableCollection<DataSeriesInfo> Results { get; set; }
        public double DeltaP { get; set; }

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}

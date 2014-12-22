using System.Collections.ObjectModel;

namespace MMHTP_K.Model
{
    public class ResultData
    {
        public double SeparationFactor { get; set; }
        public ObservableCollection<ResultItem> ResultItems { get; set; } 
    }
}

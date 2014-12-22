using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMHTP_K.Model;

namespace MMHTP_K.Control
{
    public class MathModel:IMathModel
    {
        public async Task<ResultData> Process(InputData inputData)
        {
            return await Task.Run(() =>
            {
                var result = new ResultData
                {
                    SeparationFactor = Math.Round(0.85*Math.Pow(inputData.N,2)*inputData.R/900,2)        // Фактор разделения центрифуги
                };
                var resultItems = new ObservableCollection<ResultItem>();
                for (var l = inputData.LStart; l < inputData.LEnd; l += inputData.H)
                {
                    var r0 = 0.71*inputData.R;
                    resultItems.Add(new ResultItem
                    {
                        Length = Math.Round(l,2),                                                                       // Длина
                        Performance = Math.Round(2 * Math.PI * Math.Pow(inputData.N, 2) * l * Math.Pow(r0, 2)/900,2),   // Производительность       
                        Volume = Math.Round(Math.PI * Math.Pow(inputData.R, 2)*l,2)                                     // Объем
                    });
                }
                result.ResultItems = resultItems;
                return result;
            });
        }
    }
}

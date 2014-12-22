using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMHTP_K.Model;

namespace MMHTP_K.Control
{
    public interface IMathModel
    {
        Task<ResultData> Process(InputData inputData);
    }
}

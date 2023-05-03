using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Business.Abstract
{
    public interface ILoggerService
    {
        void WriteLog(string message);
        void ReadLog();
    }
}

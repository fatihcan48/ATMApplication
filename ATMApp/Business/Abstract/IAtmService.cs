using ATMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Business.Abstract
{
    public interface IAtmService
    {
        void EndOfDay();
        User Login();
    }
}

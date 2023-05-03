using ATMApp.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Business.Concrete
{
    public class InMemoryLogger : ILoggerService
    {
        public void ReadLog()
        {
            Console.WriteLine("\n**************** Gün Sonu Raporu *****************\n");
            string path = $@"{Environment.CurrentDirectory}\MemoryLogs\Date_{DateTime.UtcNow.ToShortDateString()}.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void WriteLog(string message)
        {
            string path = $@"{Environment.CurrentDirectory}\MemoryLogs\Date_{DateTime.Now.ToShortDateString()}.txt";

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}

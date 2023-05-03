using ATMApp.Business.Abstract;
using ATMApp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Business.Concrete
{
    public class AtmManager : IAtmService
    {
        private IUserService _userService;
        private ILoggerService _loggerService;

        public AtmManager(IUserService userService, ILoggerService loggerService)
        {
            _userService = userService;
            _loggerService = loggerService;
        }


        public void EndOfDay()
        {
            _loggerService.ReadLog();
        }

        public User Login()
        {
            byte meter = 0;
            Login:
            Console.Write("Lütfen şifrenizi giriniz :");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || !_userService.GetAll().Any(u=>u.Password==password))
            {
                Console.WriteLine("Şifre hatalı! Girişe yönlendiriliyorsunuz..." );

                _loggerService.WriteLog($"{DateTime.UtcNow} --> {_userService.GetByAccountNo("null").AccountNo} -->" +
                   $"Şifre hatalı! Girişe yönlendiriliyorsunuz...");

                Thread.Sleep(1000);
                meter++;
                if (meter == 3 )
                {
                    Console.WriteLine("Hatalı giriş limiti aşıldı programı kapatmak için bir tuşa basınız...");

                    _loggerService.WriteLog($"{DateTime.UtcNow}  --> {_userService.GetByAccountNo("null").AccountNo} -->" +
                        $" Hatalı giriş limiti aşıldı!");
                    
                    Process.GetCurrentProcess().Kill();
                }
                goto Login;
            }

            _loggerService.WriteLog($"{DateTime.UtcNow} --> {_userService.GetByPassword(password).AccountNo} -->" +
                $" Giriş işlemi başarılı...");

            return _userService.GetByPassword(password);
        }

     
        
    }
}

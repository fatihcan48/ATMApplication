using ATMApp.Business.Abstract;
using ATMApp.Business.Concrete;
using ATMApp.DataAccess.EntityFramework;
using ATMApp.DataAccess.InMemory;
using ATMApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ATMApp
{
    public class Program
    {
        static void Main(string[] args)
        {
        
            IUserService userService = new UserManager(new EfUserDal(),new EfLogger());
            IAtmService atmService = new AtmManager(userService, new EfLogger());

        Login:
            Console.WriteLine("\n*******************************************\n");

            var user = atmService.Login();

            Console.WriteLine("\n*******************************************\n");

        Menu:
            Console.WriteLine("1 - Withdrawal (Para Çekme) \n2 - Remitment (Para Gönderme)" +
                "\n3 - Deposit (Para Yatırma) \n4 - Balance Inquiry (Bakiye Sorgulama) " +
                "\n5 - End Of The Day (Gün Sonu Raporu) \n6 - Quit (Çıkış)");

            Console.WriteLine("\n*******************************************\n");

        Choose:
            Console.Write("Lütfen yapmak istediğiniz işlemi seçiniz : ");
            int secim = int.Parse(Console.ReadLine());

            if (secim == 1)
            {
                userService.Withdrawal(user);

            }
            else if (secim == 2)
            {
                userService.Remitment(user);
            }
            else if (secim == 3)
            {
                userService.Deposit(user);
            }
            else if (secim == 4)
            {
                userService.BalanceInquiry(user);
            }
            else if (secim == 5)
            {
                atmService.EndOfDay();
            }
            else if (secim == 6)
            {
                Console.Clear();
                goto Login;
            }
            else
            {
                Console.WriteLine("Hatalı giriş yaptınız!");
                Thread.Sleep(1000);
                goto Choose;
            }

            Console.Write("Yapmak istediğiniz başka bir işlem var mı ?(Y/N): ");
            string again = Console.ReadLine();
            if (again.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Clear();
                goto Menu;
            }
            else
            {
                Console.WriteLine("Çıkış yapılıyor... İyi Günler...");
                Process.GetCurrentProcess().Kill();
            }


            Console.ReadKey();
        }

    }
}
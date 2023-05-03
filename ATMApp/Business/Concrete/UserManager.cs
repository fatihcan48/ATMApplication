using ATMApp.Business.Abstract;
using ATMApp.DataAccess.Abstract;
using ATMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ILoggerService _loggerService;

        public UserManager(IUserDal userDal, ILoggerService loggerService)
        {
            _userDal = userDal;
            _loggerService = loggerService;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void BalanceInquiry(User user)
        {
            var realUser = _userDal.BalanceInquiry(user);
            Console.WriteLine("{0} TL bakiyeniz bulunmaktadır.", realUser.AccountBalance);

            _loggerService.WriteLog(DateTime.UtcNow + " --> " +
                $"{realUser.AccountNo} --> {realUser.AccountBalance} TL bakiyeniz bulunmaktadır.");
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public void Deposit(User user)
        {
            var realUser = GetByPassword(user.Password);

            var amount = CheckEnteredAmountForUser(realUser);
            
            var lastBalance = _userDal.Deposit(realUser, amount);

            var message = $"{realUser.AccountNo} --> Para yatırma işlemi başarılı! Toplam bakiyeniz : {lastBalance} TL";

            Console.WriteLine(message);
            _loggerService.WriteLog(DateTime.UtcNow + " --> " + message);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetByAccountNo(string accountNo)
        {
            return _userDal.GetByAccountNo(accountNo);
        }

        public User GetByPassword(string password)
        {
            return _userDal.GetByPassword(password);
        }

        public void Remitment(User sender)
        {
            var realSender = GetByPassword(sender.Password);

            var amount = CheckEnteredAmountForSender(realSender);

            Console.Write("Lütfen para göndermek istediğiniz hesap numarasını giriniz : ");
            string accountNo = Console.ReadLine();

            User receiver = GetByAccountNo(accountNo);

            if (string.IsNullOrEmpty(accountNo) || receiver == null)
            {
                Console.WriteLine("Lütfen geçerli bir hesap numarası giriniz!");
            }

            var lastBalanceOfSender = _userDal.Remitment(realSender, receiver, amount);

            Console.WriteLine("Para gönderme işleminiz başarılıdır! Kalan bakiyeniz: {0} TL", lastBalanceOfSender);

            var message = $"{realSender.AccountNo} --> {receiver.AccountNo}" + " " + "numaralı hesaba para gönderme işleminiz başarılı..."
                + $"Kalan bakiyeniz: {lastBalanceOfSender} TL";

            _loggerService.WriteLog(DateTime.UtcNow + " --> " + message);
        }

        public void Withdrawal(User user)
        {
            var realUser = GetByPassword(user.Password);

            var amount = CheckEnteredAmountForSender(realUser);

            var lastBalance = _userDal.Withdrawal(realUser,amount);

            var message = $"{realUser.AccountNo} --> Para çekme işlemi başarılı! Toplam bakiyeniz : {lastBalance} TL";

            Console.WriteLine(message);

            _loggerService.WriteLog(DateTime.UtcNow + " --> " + message);
        }

        private decimal CheckEnteredAmountForSender(User user)
        {
        EnterAmount:
            Console.Write("Lütfen tutarı giriniz : ");
            var amount = decimal.Parse(Console.ReadLine());

            if (string.IsNullOrEmpty(amount.ToString()) || amount <= 0 || amount > user.AccountBalance)
            {
                Console.WriteLine("Lütfen geçerli bir miktar giriniz!");
                _loggerService.WriteLog($"{DateTime.UtcNow}  --> {user.AccountNo}  --> Lütfen geçerli bir miktar giriniz!");
                goto EnterAmount;
            }
            return amount;
        }
        private decimal CheckEnteredAmountForUser(User user)
        {
        EnterAmount:
            Console.Write("Lütfen tutarı giriniz : ");
            var amount = decimal.Parse(Console.ReadLine());

            if (string.IsNullOrEmpty(amount.ToString()) || amount <= 0)
            {
                Console.WriteLine("Lütfen geçerli bir miktar giriniz!");
                _loggerService.WriteLog($"{DateTime.UtcNow}  --> {user.AccountNo}  --> Lütfen geçerli bir miktar giriniz!");
                Thread.Sleep(1000);
                goto EnterAmount;
            }
            return amount;
        }
    }
}

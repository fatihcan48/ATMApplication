using ATMApp.DataAccess.Abstract;
using ATMApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.DataAccess.EntityFramework
{
    public class EfUserDal : IUserDal
    {
        public void Add(User user)
        {
            using (var context = new ATMAppContext())
            {
                var addedEntity = context.Entry(user);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                Console.WriteLine("Kullanıcı kaydı başarılı.");
            }
        }

        public User BalanceInquiry(User user)
        {
            using (var context = new ATMAppContext())
            {
                var realUser = context.Set<User>().SingleOrDefault(u => u.AccountNo == user.AccountNo);
                return realUser;
            }
        }

        public void Delete(User user)
        {
            using (var context = new ATMAppContext())
            {
                var willDeleted = context.Entry(user);
                willDeleted.State = EntityState.Deleted;
                context.SaveChanges();
                Console.WriteLine("Kullanıcı silme işlemi başarılı.");
            }
        }

        public decimal Deposit(User user, decimal amount)
        {
            using (var context = new ATMAppContext())
            {
                var realUser = context.Set<User>().SingleOrDefault(u => u.AccountNo == user.AccountNo);
                realUser.AccountBalance += amount;
                context.SaveChanges();
                return realUser.AccountBalance;
            }
        }

        public List<User> GetAll()
        {
            using (var context = new ATMAppContext())
            {
                return context.Set<User>().ToList();
            }
        }

        public User GetByAccountNo(string accountNo)
        {
            using (var context = new ATMAppContext())
            {
                return context.Set<User>().SingleOrDefault(u => u.AccountNo == accountNo);
            }
        }

        public User GetByPassword(string password)
        {
            using (var context = new ATMAppContext())
            {
                return context.Set<User>().SingleOrDefault(u => u.Password == password);
            }
        }

        public decimal Remitment(User sender, User receiver, decimal amount)
        {
            using (var context = new ATMAppContext())
            {
                var realSender = context.Set<User>().SingleOrDefault(u => u.AccountNo == sender.AccountNo);
                var realReceiver = context.Set<User>().SingleOrDefault(u => u.AccountNo == receiver.AccountNo);
                realSender.AccountBalance -= amount;
                realReceiver.AccountBalance += amount;
                context.SaveChanges();
                return realSender.AccountBalance;
            }
        }

        public decimal Withdrawal(User user,decimal amount)
        {
            using (var context = new ATMAppContext())
            {
                var realUser = context.Set<User>().SingleOrDefault(u => u.AccountNo == user.AccountNo);
                realUser.AccountBalance -= amount;
                context.SaveChanges();
                return realUser.AccountBalance;
            }
        }
    }
}

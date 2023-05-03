﻿using ATMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.DataAccess.Abstract
{
    public interface IUserDal
    {
        List<User> GetAll();
        User GetByPassword(string password);
        User GetByAccountNo(string accountNo);
        void Add(User user);
        void Delete(User user);
        decimal Deposit(User user,decimal amount);
        decimal Withdrawal(User user, decimal amount);
        decimal Remitment(User sender, User receiver, decimal amount);
        User BalanceInquiry(User user);
    }
}

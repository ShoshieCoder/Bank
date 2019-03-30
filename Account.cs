using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class Account
    {
        private static int numberOfAcc=0;
        private readonly int accountNumber;
        private readonly Customer accountOwner;
        private int maxMinusAllowed;

        public int AccountNumber
        {
            get
            {
                return this.accountNumber;
            }
        }

        public int Balance { get; private set; }

        public Customer AccountOwner
        {
            get
            {
                return this.accountOwner;
            }
        }

        public int MaxMinusAllowed
        {
            get
            {
                return this.maxMinusAllowed;
            }
        }

        public Account(Customer customer, int monthlyIncome, int balance)
        {
            this.accountOwner = customer;
            this.accountNumber = ++numberOfAcc;
            maxMinusAllowed = monthlyIncome * 3;
            this.Balance = balance;
        }

        public void Add(int amount)
        {
            try
            {
                if (ReferenceEquals(amount,null) || ReferenceEquals(amount,0))
                    throw new BalanceException("invalid value");
            }
            
            catch(BalanceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            this.Balance += amount;
        }

        public void Substract(int amount)
        {
            try
            {
                if (ReferenceEquals(amount, null) || ReferenceEquals(amount, 0) || amount<0)
                    throw new BalanceException("invalid value");

                if (this.Balance - amount < maxMinusAllowed)
                    throw new BalanceException("exceeds maximum minus allowed");
            }

            catch (BalanceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            this.Balance -= amount;
        }

        public static bool operator ==(Account one, Account two)
        {
            if (ReferenceEquals(one, null) && ReferenceEquals(two, null))
                return true;

            if (ReferenceEquals(one, null) || ReferenceEquals(two, null))
                return false;

            return one.accountNumber == two.accountNumber;
        }

        public static bool operator !=(Account one, Account two)
        {
            return !(one.accountNumber == two.accountNumber);
        }

        public override bool Equals(object obj)
        {
            Account other = obj as Account;
            return this.accountNumber == other.accountNumber;
        }

        public override int GetHashCode()
        {
            return this.accountNumber;
        }

        public static Account operator +(Account one, Account two)
        {
            Customer newCustomer = new Customer(one.accountOwner.CustomerID, one.accountOwner.Name, one.accountOwner.PhNumber);
            return new Account(newCustomer, (one.maxMinusAllowed / 3) + (two.maxMinusAllowed / 3), one.Balance + two.Balance);
        }

        public static Account operator +(Account account,int amount)
        {
            try
            {
                if (ReferenceEquals(amount, null) || ReferenceEquals(amount, 0))
                    throw new BalanceException("invalid value");
            }

            catch (BalanceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            account.Add(amount);
            return account; 
        }

        public static Account operator -(Account account, int amount)
        {
            try
            {
                if (ReferenceEquals(amount, null) || ReferenceEquals(amount, 0))
                    throw new BalanceException("invalid value");

                if (account.Balance - amount < account.maxMinusAllowed)
                    throw new BalanceException("Exceeds maximus minus allowed");
            }

            catch (BalanceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            account.Substract(amount);
            return account;
        }

        public override string ToString()
        {
            return $"Account owner:{accountOwner}, Number of account{accountNumber}, Balance:{Balance}, Maximum minus{MaxMinusAllowed}";
        }
    }
}

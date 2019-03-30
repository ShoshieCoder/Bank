using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    class Bank:IBank
    {
        private List<Account> accounts = new List<Account>();
        private List<Customer> customers = new List<Customer>();
        private Dictionary<int, Customer> customerID = new Dictionary<int,Customer>();
        private Dictionary<int, Customer> customerNumber = new Dictionary<int, Customer>();
        private Dictionary<int, Account> accountNumber = new Dictionary<int, Account>();
        private Dictionary<Customer, List<Account>> customerAccounts = new Dictionary<Customer, List<Account>>();
        private int totalMoneyInBank=0;
        private int profits=0;

        public string Name { get; }

        public string Address { get; }

        public int CustomerCount { get; }//cant change value inside functions

        internal Customer GetCustomerByID(int ID)
        {
            if (customerID.TryGetValue(ID, out Customer value))
                return value;
            throw new CustomerNotFoundException("Customer not found");
        }

        internal Customer GetCustomerByNumber(int number)
        {
            if (customerNumber.TryGetValue(number, out Customer value))
                return value;
            throw new CustomerNotFoundException("Customer not found");
        }

        internal Account GetAccountByNumber(int number)
        {
            if (accountNumber.TryGetValue(number, out Account value))
                return value;
            throw new AccountNotFoundException();
        }

        internal List<Account> GetAccountsByCustomer(Customer customer)
        {
            if (customerAccounts.TryGetValue(customer, out List<Account> value))
                return value;
            throw new AccountNotFoundException("Account not found");
        }

        internal void AddNewCustomer(Customer customer)
        {
            if (ReferenceEquals(customer, null))
                throw new CustomerNullException("Customer is null");

            if (customers.Contains(customer))
                throw new CustomerAlreadyExistException($"Customer {customer.Name} already exist");

            customers.Add(customer);
            customerID.Add(customer.CustomerID, customer);
            customerNumber.Add(customer.CustomerNumber, customer);
        }

        internal void OpenNewAccount(Account account, Customer customer)
        {
            if (ReferenceEquals(account, null))
                throw new AccountNullException("Account has no value");

            if (accounts.Contains(account))
                throw new AccountAlreadyExcistException("Account already excist");

            if (!(customers.Contains(customer)))
                throw new CustomerNotFoundException("Customer not found, Please create a new Customer before creating an account");

            //if theres no list of accounts to the customer create a new one 
            if (!(customerAccounts.ContainsKey(customer)))
                customerAccounts.Add(customer, new List<Account>());

            accounts.Add(account);
            accountNumber.Add(account.AccountNumber, account);
            customerAccounts[customer].Add(account);
        }

        internal int Deposit(Account account, int amount)
        {
            if (ReferenceEquals(account, null))
                throw new AccountNullException("Account has no value");

            if (!accounts.Contains(account))
                throw new AccountNotFoundException("Account not found");

            account.Add(amount);
            totalMoneyInBank += amount;
            return account.Balance;
        }

        internal int Withdraw(Account account, int amount)
        {
            if (ReferenceEquals(account, null))
                throw new AccountNullException("Account has no value");

            if (!accounts.Contains(account))
                throw new AccountNotFoundException("Account not found");

            if ((account.Balance - amount) < account.MaxMinusAllowed)
                throw new BalanceException("you poor bish");

            account.Substract(amount);
            totalMoneyInBank -= amount;
            return account.Balance;
        }

        internal int GetCustomerTotalBalance(Customer customer)
        {
            if (ReferenceEquals(customer, null))
                throw new CustomerNullException("Customer has no value");

            if (!customers.Contains(customer))
                throw new CustomerNotFoundException("Customer not found");

            int totalBalance = 0;

            foreach (Account account in customerAccounts[customer])
                totalBalance += account.Balance;

            return totalBalance;
        }

        internal void CloseAccont(Account account, Customer customer)
        {
            if (ReferenceEquals(account, null))
                throw new AccountNullException("Account has no value");

            if (ReferenceEquals(customer, null))
                throw new CustomerNullException("Customer has no value");

            if (!customers.Contains(customer))
                throw new CustomerNotFoundException("Customer not found");

            if (!accounts.Contains(account))
                throw new AccountNotFoundException("No accounts for this customer");

            if (!ReferenceEquals(account.AccountOwner, customer))
                throw new NotSameCustomerException("This account does not belong to this customer");

            customerAccounts[account.AccountOwner].Remove(account);
            accountNumber.Remove(account.AccountNumber);
            accounts.Remove(account);
        }

        internal void ChargeAnnualCommission(float precentage)
        {
            foreach (Account account in accounts)
            {
                int commission;

                if (account.Balance < 0)
                    commission = (int)Math.Abs(account.Balance * (precentage * 2));

                commission = (int)(account.Balance * precentage);
                account.Substract(commission);
                totalMoneyInBank -= commission;
                profits += commission;
            }
        }

        internal void JoinAccount(Account account1,Account account2)
        {
            if (ReferenceEquals(account1, null) || ReferenceEquals(account2, null))
                throw new AccountNullException("One and or both of the accouts are null");

            if (!accounts.Contains(account1) || !accounts.Contains(account2))
                throw new AccountNotFoundException("One and or both of the account dont exist");

            if (!ReferenceEquals(account1.AccountOwner, account2.AccountOwner))
                throw new NotSameCustomerException("These accounts dont belong to the same customer");

            OpenNewAccount(account1 + account2, account1.AccountOwner);
            CloseAccont(account1, account1.AccountOwner);
            CloseAccont(account2, account2.AccountOwner);
        }

        public Bank()
        {

        }

        public Bank(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public override string ToString()
        {
            string result = $"Customers:{customers}";

            foreach (Customer x in customers)
            {
                result = $"{x}\n";
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class Customer
    {
        private static int numberOfCust=0;
        private readonly int customerID;
        private readonly int customerNumber;

        public string Name { get; private set; }
        public int PhNumber { get; private set; }

        public Customer(int id, string name, int phNumber)
        {
            customerNumber = ++numberOfCust;
            this.customerID = id;
            Name = name;
            PhNumber = phNumber;
        }

        public int CustomerID
        {
            get
            {
                return this.customerID;
            }
        }

        public int CustomerNumber
        {
            get
            {
                return this.customerNumber;
            }
        }

        public static bool operator == (Customer one, Customer two)
        {
            if (ReferenceEquals(one, null) && ReferenceEquals(two, null))
                return true;

            if (ReferenceEquals(one, null) || ReferenceEquals(two, null))
                return false;

            return one.customerNumber == two.CustomerNumber;
        }

        public static bool operator != (Customer one, Customer two)
        {
            return !(one.customerNumber == two.customerNumber);
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return this.customerNumber == other.customerNumber;
        }

        public override int GetHashCode()
        {
            return this.customerNumber;
        }

        public override string ToString()
        {
            return $"Customer details Name:{Name}, Phone number:{PhNumber}, ID:{customerID}, Customer Number:{customerNumber}";
        }
    }
}

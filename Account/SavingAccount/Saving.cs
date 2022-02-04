using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingV1._7.Account.SavingAccount
{
    class Saving : Account
    {
        float interest;
        public Saving(string accountName, long accountNumber, string accountType, float balance, float interest) : base(accountName, accountNumber,accountType, balance)
        {
            this.interest = interest;
        }
        public Saving(float interest) : base()
        {
            this.interest = interest;

        }
        //Properties 
        public float Interest { get => interest; set => interest = value; }
        public float Balance { get => balance; set => balance = value; }


    }
}

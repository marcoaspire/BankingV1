using BankingV1._7.Account.CreditAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingV1._7.Account
{
    class Operation
    {
        //DateTime date;
        /*
        string type;
        long accountNumber;
        float currentBalance;
        */

        string operationType;
        Account account;
        float previousBalance;

        public Operation(string operationType, Account account, float previousBalance)
        {
            this.operationType = operationType;
            this.account = account;
            this.previousBalance = previousBalance;
        }
       


        public string OperationType { get => operationType; set => OperationType = value; }
        public float PreviousBalance { get => previousBalance; set => previousBalance = value; }
        
        internal Account Account { get => account; set => account = value; }

        
        public override string ToString()
        {
            if (this.Account.AccountType.Equals("Credit account"))
            {
                Credit credit = (Credit)this.Account;
                Console.WriteLine("Entree {0}- {1}", credit.Limit, credit.Balance);
                return String.Format("{0}\t\t{1}\t{2}\t\t{3}\t\t{4}", this.OperationType, this.Account.AccountNumber,
                this.Account.AccountType, this.PreviousBalance, credit.Limit - credit.Balance); 
            }
            else
                return String.Format("{0}\t\t{1}\t{2}\t\t{3}\t\t{4}", this.OperationType, this.Account.AccountNumber, 
                this.Account.AccountType, this.PreviousBalance, this.Account.Balance);
        }
    }
}

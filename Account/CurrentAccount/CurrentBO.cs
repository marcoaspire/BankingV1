using BankingV1._7.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingV1._7.Account.CurrentAccount
{
    class CurrentBO : AccountBO
    {
        /*
        public override float monthEndBalance(Account account)
        {
            return account.Balance;
        }
        */
        public override float MonthEndBalance(Account account)
        {
            return account.Balance;
        }

        public override Account NewAccount()
        {
            bool validAccount, validBalance = false;
            long accountNumber;
            float balance;

            Current account = new Current(10000);
            account.AccountType = "Current account";


            do
            {
                Console.WriteLine("\nType your account numbers");
                validAccount = Int64.TryParse(Console.ReadLine(), out accountNumber);
                account.AccountNumber = accountNumber;

                //check if it's not in the list



            } while (!validAccount);

            do
            {
                Console.WriteLine("Type account name");
                account.AccountName = Console.ReadLine();
                if (string.IsNullOrEmpty(account.AccountName))
                    Console.WriteLine("Error:Name can not be empty");
            } while (string.IsNullOrEmpty(account.AccountName));

            do
            {
                Console.WriteLine("Type the balance");
                validBalance = float.TryParse(Console.ReadLine(), out balance);
                if (CheckBalance(balance))
                    account.Balance = balance;
            } while (!validBalance || account.Balance < 0);
            Console.WriteLine($"Hello dear user, your have a new {account.AccountType} {account.AccountName} , its account number is {account.AccountNumber} and you have ${account.Balance}");
            return account;
        }
        private float checkBalance(float balance)
        {
            if (balance < 0)
                return 0;
            else
                return balance;
        }

        public override Account Deposit(Account account)
        {
            Current currentAccount = (Current)account;
            bool validDeposit = false;
            float deposit;
            do
            {
                Console.WriteLine("Type the amount you want to deposit");
                validDeposit = float.TryParse(Console.ReadLine(), out deposit);
            } while (!validDeposit || deposit < 0);
            if (deposit > currentAccount.MaxDepositLimit)
                throw new Exception("Deposit can be greater than its limit "+ currentAccount.MaxDepositLimit);
            BankMenu.operations.Add(DateTime.Now, new Operation("Deposit", account, account.Balance));
            //account.Balance += deposit;

            Console.WriteLine($"Now your balance is ${account.Balance}");
            return account;
        }


    }
}

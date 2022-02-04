using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingV1._7.Menu;
namespace BankingV1._7.Account
{
    abstract class AccountBO :IAccountBO
    {
        //public static List<Account> accounts;
        public static LinkedList<Account> accounts;

        //Methods

        public virtual Account Deposit(Account account)
        {
            bool validDeposit = false;
            float deposit;
            do
            {
                Console.WriteLine("Type the amount you want to deposit");
                validDeposit = float.TryParse(Console.ReadLine(), out deposit);
            } while (!validDeposit || deposit < 0);
            BankMenu.operations.Add(DateTime.Now, new Operation("Deposit", (Account)account.Clone(), account.Balance));
            //account.Balance += deposit;

            Console.WriteLine($"Now your balance is ${account.Balance}");
            return account;
        }
        public virtual Account Withdraw(Account account)
        {
            float withdrawal;
            bool validWithdrawal = false;

            do
            {
                Console.WriteLine("Type the amount you want to withdraw");
                validWithdrawal = float.TryParse(Console.ReadLine(), out withdrawal);
            } while (!validWithdrawal);
            if (withdrawal > account.Balance)
            {
                Console.WriteLine($"Your balance is less than {withdrawal} \n Transaction failed");
            }
            else
            {
                account.Balance -= withdrawal;
                Console.WriteLine($"Now your balance is ${account.Balance}");
            }
            return account;
        }

        public virtual float MonthEndBalance(Account account)
        {
            return account.Balance;
        }

        //public abstract Account newAccount(int accountType);

        //interface methods
        public abstract Account NewAccount();

        public void RemoveAccount(Account a)
        {
            Console.WriteLine("entreDelete");
            accounts.Remove(a);
        }

        public void UpdateAccount(Account a)
        {
            String name;
            do
            {
                Console.WriteLine("\nWrite the new name of the account");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                    Console.WriteLine("Error:Name can not be empty");
            } while (string.IsNullOrEmpty(name));


            accounts.Where(account => account == a).ToList().ForEach(s => s.AccountName = name);
            Console.WriteLine("Your change is saved");
        }
        
        public bool CheckBalance(float balance)
        {
            if (balance < 1)
            {
                Console.WriteLine("Balance negativo. Try again");
                return false;
            }
            return true;
        }
    }
}

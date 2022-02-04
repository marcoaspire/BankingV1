using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingV1._7.Account;
using BankingV1._7.Account.CurrentAccount;
using BankingV1._7.Account.CreditAccount;
using BankingV1._7.Account.SavingAccount;
using System.IO;
using System.Threading;

namespace BankingV1._7.Menu
{
    class BankMenu
    {
        public static SortedList<DateTime, Operation> operations = new SortedList<DateTime, Operation>();
        public void DataLoad()
        {
            string path = @"C:\Users\marco.antonio\OneDrive - Aspire Systems (India) Private Limited\Documents\test.txt";
            //structure of the file
            //accountType accountName accounNumber balance
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string[] accounts;
                    string s;
                    //AccountBO.accounts = new List<Account.Account>();
                    AccountBO.accounts = new LinkedList<Account.Account>();
                    Operation op;
                    
                    Account.Account account = null;
                    try
                    {
                        sr.ReadLine();
                        while ((s = sr.ReadLine()) != null)
                        {
                            accounts = s.Split(' ');
                            switch (Int32.Parse(accounts[0]))
                            {
                                case 1:
                                    account = new Current(accounts[1], Int64.Parse(accounts[2]), "Current account", float.Parse(accounts[3]), 10000);
                                    break;
                                case 2:
                                    account = new Saving(accounts[1], Int64.Parse(accounts[2]), "Savings account", float.Parse(accounts[3]), 10);
                                    break;
                                case 3:
                                    account = new Credit(accounts[1], Int64.Parse(accounts[2]), "Credit account", float.Parse(accounts[3]), 20);
                                    break;
                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }
                            if (account != null)
                            {
                                AccountBO.accounts.AddLast(account);

                                Console.WriteLine("nueva cuenta:" + account.Balance);
                                operations.Add(DateTime.Now, new Operation("NewAccount", (Account.Account)account.Clone(),account.Balance));
                                Thread.Sleep(1000);
                            }

                            //AccountBO.accounts.Add(account);
                        }
                        Console.WriteLine("External content loaded successfully!");
                    }
                    catch (PathTooLongException)
                    {
                        Console.WriteLine("'path' exceeds the maxium supported path length.");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("The file cannot be found.");
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("The directory cannot be found.");
                    }
                    catch (DriveNotFoundException)
                    {
                        Console.WriteLine("The drive specified in 'path' is invalid.");
                    }

                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("You do not have permission to create this file.");
                    }
                    catch (EndOfStreamException)
                    {
                        Console.WriteLine("Invalid format");

                    }
                    catch (ArgumentException w)
                    {
                        Console.WriteLine("Invalid format, data muy grande:"+w);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Exception IO:" + ex.HResult);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ERROR:Invalid format");
                    }
                    finally
                    {
                        sr.Close();
                    }


                    Console.WriteLine();
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("The drive specified in 'path' is invalid.");
            }

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to create this file.");
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine("Invalid format");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid format, data muy grande");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Exception IO:" + ex.HResult);
            }
            catch (IndexOutOfRangeException ex)
            {

                Console.WriteLine("External content could not loaded: " + ex);
            }



        }
        public void DisplayMenu()
        {
            int op, choice;
            bool validNumber;
            CurrentBO currentBO = new CurrentBO();
            CreditBO creditBO = new CreditBO();
            SavingBO savingBO = new SavingBO();
            do
            {
                Console.WriteLine("Welcome Banking System");
                Console.WriteLine("1.Open a new bank account");
                Console.WriteLine("2.Display your account details by ID");
                Console.WriteLine("3.Display all your accounts");
                Console.WriteLine("4.Deposit/Pay your credit");
                Console.WriteLine("5.Withdraw/Pay with your credit");
                Console.WriteLine("6.End of the month");
                Console.WriteLine("7.Delete/Change name of your account");
                Console.WriteLine("8.Exit");
                Console.WriteLine("Type an option, please");
                op = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (op)
                {
                    case '1':
                        do
                        {
                            Console.WriteLine("What type of bank account would you like to open?");
                            Console.WriteLine("1-Current account");
                            Console.WriteLine("2-Savings account");
                            Console.WriteLine("3-Credit account");
                            Console.WriteLine("4-Return to menu");
                            validNumber = Int32.TryParse(Console.ReadKey().KeyChar.ToString(), out choice);
                            if (choice > 4 || choice < 1)
                                Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");

                        } while (!validNumber || choice > 4 || choice < 1);
                        if (choice == 4)
                            break;
                        if (AccountBO.accounts is null)
                            AccountBO.accounts = new LinkedList<Account.Account>();
                         //   AccountBO.accounts = new List<Account.Account>();
                        
                        Account.Account newAccount = null;

                        switch (choice)
                        {
                            case 1:
                                newAccount = currentBO.NewAccount();
                                break;
                            case 2:
                                newAccount = savingBO.NewAccount();
                                break;
                            case 3:
                                newAccount = creditBO.NewAccount();
                                break;
                            default:
                                break;
                        }
                        if (!AccountBO.accounts.Contains(newAccount))
                        {
                            AccountBO.accounts.AddLast(newAccount);
                            operations.Add(DateTime.Now, new Operation("NewAccount", (Account.Account)newAccount.Clone(),newAccount.Balance));
                        }
                            //AccountBO.accounts.Add(newAccount); 
                        else
                            Console.WriteLine("Error: We could not open the new account because you alredy have one with that account number");


                        break;
                    case '2':
                        if (AccountBO.accounts != null)
                        {
                            Account.Account accoutFound = this.Find();
                            if (accoutFound != null)
                                Console.WriteLine(accoutFound);
                            else
                                Console.WriteLine("We couldn't find an account with that number");
                        }
                        else
                            Console.WriteLine("You don't have an account with us. Open your account now!");

                        break;
                    case '3':
                        if (AccountBO.accounts != null)
                        {
                            this.ShowAllAcounts();
                        }
                        else
                            Console.WriteLine("You don't have an account with us. Open your account now!");
                        break;
                    case '4':

                        if (AccountBO.accounts != null)
                        {
                            Account.Account accoutFound = this.Find();
                            if (accoutFound != null)
                            {
                                try
                                {
                                    string type = accoutFound.GetType().Name;
                                    Console.WriteLine(accoutFound.GetType().Name);

                                    if (type.Equals("Credit"))
                                    {
                                        creditBO.Deposit((Credit)accoutFound);
                                    }
                                    else if (type.Equals("Current"))
                                    {
                                        currentBO.Deposit(accoutFound);
                                    }
                                    else if (type.Equals("Saving"))
                                    {
                                        savingBO.Deposit(accoutFound);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Transaction has failed. " + e.Message);
                                }
                                


                            }
                            else
                                Console.WriteLine("We couldn’t find account with that number ");


                        }
                        else
                            Console.WriteLine("You don't have an account with us. Open your account now!");
                        break;

                    case '5':
                        if (AccountBO.accounts != null)
                        {
                            Account.Account accoutFound = this.Find();
                            if (accoutFound != null)
                            {
                                string type = accoutFound.GetType().Name;

                                if (type.Equals("Credit"))
                                {
                                    creditBO.Withdraw((Credit)accoutFound);
                                }
                                else if (type.Equals("Current"))
                                {
                                    currentBO.Withdraw(accoutFound);
                                }
                                else if (type.Equals("Saving"))
                                {
                                    savingBO.Withdraw(accoutFound);
                                }
                            }
                            else
                            {
                                Console.WriteLine("We couldn’t find account with that number ");
                            }

                        }
                        else
                            Console.WriteLine("You don't have an account with us. Open your account now!");
                        break;
                    case '6':
                        if (AccountBO.accounts != null)
                        {
                            Account.Account accoutFound = this.Find();
                            if (accoutFound != null)
                            {
                                string type = accoutFound.GetType().Name;
                                Console.WriteLine("Account Statement");

                                if (type.Equals("Credit"))
                                {
                                    accoutFound.Balance = creditBO.MonthEndBalance((Credit)accoutFound);
                                    Console.WriteLine();
                                    Console.WriteLine(accoutFound.ToString());
                                }
                                else if (type.Equals("Current"))
                                {
                                    accoutFound.Balance = currentBO.MonthEndBalance(accoutFound);

                                    Console.WriteLine(accoutFound.ToString());
                                }
                                else if (type.Equals("Saving"))
                                {
                                    accoutFound.Balance = savingBO.MonthEndBalance(accoutFound);
                                    Console.WriteLine(accoutFound.ToString());
                                }
                            }
                            else
                                Console.WriteLine("We couldn’t find account with that number");
                        }
                        else
                            Console.WriteLine("You don't have an account with us. Open your account now!");
                        break;
                    case '7':
                        Account.Account accoutFound2 = this.Find();
                        if (accoutFound2 != null)
                        {
                            
                            do
                            {
                                Console.WriteLine("1- Modify name of your account");
                                Console.WriteLine("2- Delete your account");
                                Console.WriteLine("3- Cancel");
                                validNumber = Int32.TryParse(Console.ReadKey().KeyChar.ToString(), out choice);
                                if (choice > 3 || choice < 1)
                                    Console.WriteLine("Invalid option. Please enter a number between 1 and 3.");

                            } while (!validNumber || choice > 3 || choice < 1);
                            if (choice == 3)
                                break;
                            

                            string type = accoutFound2.GetType().Name;
                            if (type.Equals("Credit"))
                            {
                                Credit account=(Credit)accoutFound2;
                                if (choice == 1)
                                    creditBO.UpdateAccount(account);
                                else
                                    creditBO.RemoveAccount(account);
                            }
                            else if (type.Equals("Current"))
                            {
                                Current account = (Current)accoutFound2;
                                if (choice == 1)
                                    creditBO.UpdateAccount(account);
                                else
                                    currentBO.RemoveAccount(account);
                            }
                            else if (type.Equals("Saving"))
                            {
                                Saving account = (Saving)accoutFound2;
                                if (choice == 1)
                                    creditBO.UpdateAccount(account);
                                else
                                    savingBO.RemoveAccount(account);
                            }
                        }
                        else
                            Console.WriteLine("We couldn’t find account with that number");
                        break;
                    case '8':
                        Console.WriteLine("Thank you for using this system");
                        new OperationBO().PrintOperations();
                        Console.WriteLine("");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 8.");
                        break;

                }
                Console.WriteLine("");

            } while (op != '8');



            Console.ReadKey();
        }

        public Account.Account Find()
        {
            bool validAccount;
            long accountNumber;
            do
            {
                Console.WriteLine("Type the account numbrer");
                validAccount = Int64.TryParse(Console.ReadLine(), out accountNumber);
            } while (!validAccount);
            return AccountBO.accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            //return AccountBO.accounts.Find(a => a.AccountNumber == accountNumber);
        }
        public void ShowAllAcounts()
        {
            Console.WriteLine("Your accounts are:");
            foreach (Account.Account item in AccountBO.accounts)
            {
                Console.WriteLine(item.ToString());
            }

        }

    }
    
}

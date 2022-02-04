using BankingV1._7.Menu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingV1._7.Account
{
    class OperationBO
    {
        public void PrintOperations()
        {
            string fileNameTxt = "operationReceipts.txt";
            Console.WriteLine(fileNameTxt);
            try
            {
                using (StreamWriter sw = File.CreateText(fileNameTxt))
                {
                    sw.WriteLine("Bank");
                    sw.WriteLine(DateTime.Now.ToString("s", CultureInfo.GetCultureInfo("en-US")));
                    sw.WriteLine("All movements that you did were:");
                    sw.WriteLine("Date\t\t\tOperationType\tAccountNumber\t AccountType\tPrevious Balance\tCurrent Balance/Available Credit");
                    foreach (var item in BankMenu.operations)
                    {
                        sw.Write(item.Key + "\t");
                        sw.WriteLine(item.Value.ToString());
                    }
                    sw.Close();
                    Console.WriteLine("You can find your receipts at:" + Path.GetFullPath(fileNameTxt));
                }
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("'path' exceeds the maxium supported path length.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The directory cannot be found.");
            }
            

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to create this file.");
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Invalid format");

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Path is null, verify the path where you can save the report");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid format in the argument");
            }
            





        }
    }
}

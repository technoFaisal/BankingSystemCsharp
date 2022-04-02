using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace BankSystem
{
    public class AccountG
    {
        private string firstName;
        private string lastName;
        private string address;
        private int phone;
        private string email;
        private int accountNo;
        private double balance = 0;
        private List<string> dataRem = new List<string>(); // remaming data i.e. transactions

        public AccountG(int accountNo) // to retreive account info
        {
            this.accountNo = accountNo;
        }

        public AccountG(string firstName, string lastName, string address, int phone, string email) // to create an account
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;

            accountNo = generateAccountNumber(); // generate a unique account number
            saveInfo(); // save all the data
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public List<string> DataRem
        {
            get { return dataRem; }
            set { dataRem = value; }
        }


        public void saveInfo()
        {
            TextWriter writer = new StreamWriter(string.Format("{0}.txt", accountNo), append: false); // save in the accountno.txt file

            writer.WriteLine("First Name |{0}", firstName); // writing the file according to the format
            writer.WriteLine("Last Name |{0}", lastName);
            writer.WriteLine("Address |{0}", address);
            writer.WriteLine("Phone |{0}", phone);
            writer.WriteLine("Email |{0}", email);
            writer.WriteLine("AccountNo |{0}", accountNo);
            writer.WriteLine("Balance |{0}", balance);

            if (dataRem.Count > 0) // for the transactions
            {

                for (int i = 0; i < dataRem.Count; i++)
                {
                    writer.WriteLine(dataRem[i]);
                }
            }

            writer.Close();
        }

        public void loadInfo()
        {

            string[] lines = File.ReadAllLines(string.Format("{0}.txt", accountNo));
            string[] firstNameG = lines[0].Split("|");
            string[] lastNameG = lines[1].Split("|");
            string[] addressG = lines[2].Split("|");
            string[] phoneG = lines[3].Split("|");
            string[] emailG = lines[4].Split("|");
            string[] accountNoG = lines[5].Split("|");
            string[] balanceG = lines[6].Split("|");

            if (lines.Length > 7) // if there is any transaction history
            {
                for (int i = 7; i < lines.Length; i++)
                {
                    dataRem.Add(lines[i]);
                }

            }

            firstName = firstNameG[1]; // loading the variables from the file
            lastName = lastNameG[1];
            address = addressG[1];
            phone = int.Parse(phoneG[1]);
            email = emailG[1];
            balance = double.Parse(balanceG[1]);

        }

        public bool depositAmount(double amount) // deposit new amount
        {
            DateTime thisDay = DateTime.Today;
            if (amount < 0) // negative amount must not be entered
            {
                return false;
            }
            else
            {
                loadInfo(); //load the variables
                balance += amount; // update balance
                saveInfo(); // save again
                TextWriter writer = new StreamWriter(string.Format("{0}.txt", accountNo), append: true); // appending the previous file
                writer.WriteLine("{0} | Deposit | {1} | {2}", thisDay.ToString("d"), amount, balance); // writing the transactions occured
                writer.Close();
                return true;

            }
        }

        public bool withdrawAmount(double amount)
        {
            loadInfo(); // load the variables to perform a check on balance

            DateTime thisDay = DateTime.Today;
            if (amount < 0 || amount > balance) // negative amount must not be entered or amount greater than the available balance
            {
                return false;
            }
            else
            {
                balance -= amount;
                saveInfo(); // saving after appending
                TextWriter writer = new StreamWriter(string.Format("{0}.txt", accountNo), append: true); // appending the previous file
                writer.WriteLine("{0} | Withdraw | {1} | {2}", thisDay.ToString("d"), amount, balance); // writing the transactions occured
                writer.Close();


                return true;
            }

        }

        private int generateAccountNumber() // create a file with all the account numbers
        {
            Random randomNumber = new Random(); // random number generator
            string accountNoLogPath = "accNoLog.txt"; // file to store all the account numbers
            int numberAcc = randomNumber.Next(100000, 10000000); // random number between 6 to 8  digits

            if (File.Exists(accountNoLogPath))  // checking for the file
            {

                bool uniqueAccNum = false;

                while (!uniqueAccNum) // account number must be unique
                {

                    string[] lines = File.ReadAllLines(accountNoLogPath); // check the file with all the account numbers written in it

                    foreach (string line in lines)
                    {
                        if (line.Contains("" + numberAcc))
                        {
                            uniqueAccNum = false;
                            numberAcc = randomNumber.Next(100000, 10000000); // a random 6-8 digit number
                        }
                        else
                        {
                            uniqueAccNum = true;
                        }

                    }

                }

                TextWriter writer = new StreamWriter("accNoLog.txt", append: true); // appending the previous file
                writer.WriteLine(numberAcc);
                writer.Close();
                return numberAcc;
            }
            else // file not exist for the first time
            {
                TextWriter writer = new StreamWriter("accNoLog.txt");
                writer.WriteLine("{0}", numberAcc);
                writer.Close();
                return numberAcc;
            }

        }

        public bool searchAccount(int accountNumber)
        {

            string accountNoLogPath = "accNoLog.txt"; // file to store all the account numbers

            string[] lines = File.ReadAllLines(accountNoLogPath);

            foreach (string line in lines) // check if the log file contains the account number
            {
                if (line.Equals("" + accountNumber))
                {
                    return true;
                }
            }
            return false;
        }

        public void deleteAccount()
        {

            string accountNoLogPath = "accNoLog.txt"; // file to store all the account numbers

            string[] lines = File.ReadAllLines(accountNoLogPath);

            List<String> fileG = new List<String>((System.IO.File.ReadAllLines(accountNoLogPath))); //storing all the account numbers in a list

            for (int i = 0; i < fileG.Count; i++)
            {
                if (fileG[i].Equals("" + accountNo))
                {
                    fileG.RemoveAt(i); // remove the account if found
                }
            }

            File.WriteAllLines(accountNoLogPath, fileG.ToArray()); // updated log gile
            File.Delete(string.Format("{0}.txt", accountNo)); // delete the actual account file

        }

    }

}

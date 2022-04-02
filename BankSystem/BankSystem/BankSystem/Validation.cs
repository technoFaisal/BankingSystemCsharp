using System;
using System.IO;

namespace BankSystem
{

    public class Validation
    {

        public bool checkPhone(string phoneNumber) // take as string apply check store as integer
        {
            int phoneG;
            bool isNumeric = int.TryParse(phoneNumber, out phoneG); // try to parse string to integer
            if (isNumeric && phoneNumber.Length < 11) // apply condition
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkEmail(string email) // take as string apply check store as integer
        {
            string testEmail = email;

            bool containAtSign = testEmail.Contains('@'); // check '@' sign

            if (containAtSign)
            {
                string[] domain = testEmail.Split('@');

                if (domain[1].ToLower().Equals("gmail.com") || domain[1].ToLower().Equals("outlook.com") || domain[1].ToLower().Equals("uts.edu.au")) // look for valid domain name for test purposes only these three are used
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public int validateUser(string username, string password)
        {

            try
            {
                string[] lines = System.IO.File.ReadAllLines("login.txt"); // data of login.txt
                                                                           // Console.SetCursorPosition(12, 16);
                foreach (string line in lines) // check the data set
                {
                    string[] userData = line.Split('|'); // spilt and save username on basis of "|"

                    if (userData[0].Equals(username) && userData[1].Equals(password))
                    {
                        return 1;
                    }

                }

            }
            catch (FileNotFoundException e) // handle the exception
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            return -1;

        }


    }

}

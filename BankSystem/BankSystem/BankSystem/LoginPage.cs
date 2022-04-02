using System;
using System.IO;

namespace BankSystem
{
    public class LoginPage
    {
        public Boolean validUserIdentified = false;

        public LoginPage()
        {
            bool userFound = false;

            while (!userFound)
            {
                string userName = ""; // user name validation string
                string password = ""; // secure string for password to introduce * encryption

                Console.SetCursorPosition(13, 7);
                Console.WriteLine("                  ");
                Console.SetCursorPosition(12, 8);
                Console.WriteLine("                  ");
                Console.SetCursorPosition(13, 7); // user name postion

                ConsoleKeyInfo nextKeyU; // user name check key 
                bool userCompleteCheck = false; // check if tab or enter is pressed
                bool passCompleteCheck = false;

                while (!userCompleteCheck) // accept the user name untill certain criteria is met
                {
                    nextKeyU = Console.ReadKey(true); // read character by character

                    if (nextKeyU.Key == ConsoleKey.Tab || nextKeyU.Key == ConsoleKey.Enter)
                    {
                        userCompleteCheck = true;
                    }
                    else
                    {
                        if (nextKeyU.Key != ConsoleKey.Backspace)
                        {
                            userName = userName + nextKeyU.KeyChar; // append each character to form a string
                            Console.Write(nextKeyU.KeyChar); // display along side accepting from the user
                        }
                        else
                        {
                            if (userName.Length > 0) // to avoid out of bound exception
                            {
                                // erase the last * as well
                                Console.Write(nextKeyU.KeyChar);
                                Console.Write(" ");
                                Console.Write(nextKeyU.KeyChar);
                                userName = userName.Remove(userName.Length - 1);
                            }
                        }

                    }

                }

                Console.SetCursorPosition(12, 8); // password acceptance    
                ConsoleKeyInfo nextKey; // password info ket

                while (!passCompleteCheck)
                {
                    nextKey = Console.ReadKey(true);
                    // read character by character

                    if (nextKey.Key == ConsoleKey.Tab || nextKey.Key == ConsoleKey.Enter)
                    {
                        passCompleteCheck = true;
                    }
                    else
                    {
                        if (nextKey.Key == ConsoleKey.Backspace)
                        {
                            if (password.Length > 0) // to avoid out of bound exception
                            {
                                // erase the last * as well
                                Console.Write(nextKey.KeyChar);
                                Console.Write(" ");
                                Console.Write(nextKey.KeyChar);
                                password = password.Remove(password.Length - 1); // remvoving each bit
                            }


                        }
                        if (nextKey.Key != ConsoleKey.Backspace && password.Length < 14) // password length must be less than 14 characters
                        {
                            password = password + nextKey.KeyChar;
                            Console.Write("*");
                        }

                    }

                }
                Validation validate = new Validation();

                int x = validate.validateUser(userName, password);

                if (x == 1)
                {
                    Console.SetCursorPosition(5, 13);
                    Console.WriteLine("                                             ");
                    Console.SetCursorPosition(5, 13);
                    Console.WriteLine("Valid credentials!... Please Enter");
                    userFound = true;
                    validUserIdentified = true;
                }
                else
                {
                    Console.SetCursorPosition(5, 13);
                    Console.WriteLine("InValid credentials!... Please retry");
                }

                Console.ReadKey();

            }

        }

    }

}



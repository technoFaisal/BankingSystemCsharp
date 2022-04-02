using System;
using System.IO;
using System.Security;


namespace BankSystem
{
    class Screen
    {

        static void Main(string[] args)
        {

            while (true) // to stay in application all the time
            {
                Console.Clear();
                DisplayForm menuG = new DisplayForm();
                menuG.loginPage(); // initiate the login page

                bool exit = false; // recursive loop control

                while (!exit) // recursive menu loop
                {
                    Console.SetCursorPosition(30, 15);
                    int choice = 0; // choice variable for user menu
                    Console.Clear();
                    choice = menuG.mainMenu(); // display menu
                    Console.Clear();
                    switch (choice) // selected options
                    {
                        case 1:
                            menuG.newAccount();
                            break;

                        case 2:
                            menuG.searchAccountDis();
                            break;

                        case 3:
                            menuG.depositDis();
                            break;

                        case 4:
                            menuG.withdrawDis(); break;

                        case 5:
                            menuG.accountStatementDis();
                            break;

                        case 6:
                            menuG.deleteAccountDis();
                            break;

                        case 7:
                            exit = true;
                            Console.WriteLine("Press any key to go back to the login screen....");
                            break;

                        default:
                            Console.WriteLine("Invalid choice: {0}", choice);
                            break;
                    }

                    Console.ReadKey();

                }

            }

        }

    }

}

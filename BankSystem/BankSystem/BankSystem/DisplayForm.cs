using System;
using BankSystem;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BankSystem
{

    class DisplayForm
    {
        private int length = 0;
        private int width = 0;

        public DisplayForm()
        {
            length = 20; // fix length and width
            width = 50;
        }

        public DisplayForm(int length, int width)
        {
            this.length = length;
            this.width = width;
        }

        private void setTextAt(string textG, int posX, int posY) // set text at specific postion
        {

            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(textG);

        }

        private void generateForm(int length, int width) // genereate a simple form structure
        {
            Console.SetCursorPosition(width, length); // MOVE TO SPECIFC POINT


            for (int i = 0; i < width - 1; i++)   // horizontal lines loop
            {
                setTextAt("◄", i + 1, 0);
                setTextAt("►", i + 1, 4);
                setTextAt("◄", i + 1, length);
            }

            for (int i = 0; i < length - 1; i++) // Vertical Lines in the form
            {
                setTextAt("|", 0, i + 1);
                setTextAt("|", width, i + 1);
            }


        }

        private void generateForm(int length, int width, int startPoint) // overloaded method to create multiple forms in a single page
        {
            Console.SetCursorPosition(width, length); // move to specific point
            for (int i = 0; i < width - 1; i++)   // horizontal lines loop
            {
                setTextAt("◄", i + 1, 0 + startPoint);
                setTextAt("►", i + 1, 4 + startPoint); // append the start point
                setTextAt("◄", i + 1, length + startPoint);
            }

            for (int i = 0; i < length - 1; i++) // Vertical Lines in the form
            {
                Console.SetCursorPosition(0, i + 1 + startPoint);
                Console.WriteLine("|");
                Console.SetCursorPosition(width, i + 1 + startPoint);
                Console.WriteLine("|");
            }


        }


        public int mainMenu() //generate main menu form
        {
            generateForm(18, 50);
            setTextAt("Welcome to the Faisal Bank", 12, 2);
            setTextAt("1. Create a new account", 5, 6);
            setTextAt("2. Search for an account", 5, 7);
            setTextAt("3. Deposit", 5, 8);
            setTextAt("4. Withdraw", 5, 9);
            setTextAt("5. A/C Statement", 5, 10);
            setTextAt("6. Delete account", 5, 11);
            setTextAt("7. Exit", 5, 12);
            setTextAt("Enter Your Choice (1-7):", 5, 15);
            Console.SetCursorPosition(30, 15);

            bool correctSelection = false; // limit user for select an option
            int choice = 0; // user choice from the menu

            ConsoleKeyInfo nextKey;
            nextKey = Console.ReadKey(true); // read users option

            while (!correctSelection)
            {

                Console.SetCursorPosition(30, 15);
                Console.Write(nextKey.KeyChar); // overwrite the newly entered character

                if (nextKey.KeyChar > 48 && nextKey.KeyChar < 56) // checking ascii for 1-7
                {
                    choice = Convert.ToInt32(nextKey.KeyChar.ToString());
                    nextKey = Console.ReadKey(true); // read single character

                    if (nextKey.Key == ConsoleKey.Enter)  // check if the character is finalized
                    {
                        correctSelection = true;
                    }
                    if (nextKey.Key == ConsoleKey.Backspace) // if user want to remove the entered character
                    {
                        setTextAt(" ", 30, 15);
                        choice = 0;
                    }

                }
                else
                {
                    nextKey = Console.ReadKey(true);
                    if (nextKey.Key == ConsoleKey.Backspace)
                    {
                        setTextAt(" ", 30, 15);
                    }
                    if (nextKey.Key == ConsoleKey.Enter)
                    {
                        setTextAt("Invalid option selected please select between 1-7", 1, 16);
                        Console.SetCursorPosition(30, 15);
                    }
                }
            }

            return choice;

        }

        public void newAccount() // create new accoaunt menu
        {
            Validation testG = new Validation(); // validation object for phone and email test

            bool done = false; // new account completion check

            while (!done)
            {
                Console.Clear();
                List<string> textFields = new List<string>();

                generateForm(15, 50);
                setTextAt("CREATE A NEW ACCOUNT", 15, 2);
                setTextAt("ENTER THE DETAILS", 5, 6);
                setTextAt("First Name:", 5, 8);
                setTextAt("Last Name:", 5, 9);
                setTextAt("Address:", 5, 10);
                setTextAt("Phone:", 5, 11);
                setTextAt("Email:", 5, 12);

                Console.SetCursorPosition(17, 8); // read data according to the form
                string firstName = Console.ReadLine();
                Console.SetCursorPosition(16, 9);
                string lastName = Console.ReadLine();
                Console.SetCursorPosition(14, 10);
                string address = Console.ReadLine();
                Console.SetCursorPosition(12, 11);
                string phone = Console.ReadLine();

                bool phoneTest = testG.checkPhone(phone);

                while (!phoneTest) // if phone number test doesnt passes
                {
                    setTextAt("Please enter a valid phone number", 8, 13);
                    Console.SetCursorPosition(12, 11);
                    Console.WriteLine("                             ");
                    Console.SetCursorPosition(12, 11);
                    phone = Console.ReadLine();
                    setTextAt("                                 ", 8, 13);
                    phoneTest = testG.checkPhone(phone);
                }

                Console.SetCursorPosition(12, 12);
                string email = Console.ReadLine();
                bool emailTest = testG.checkEmail(email);

                while (!emailTest) // if email number test doesnt passes
                {
                    setTextAt("Please enter a valid email address", 8, 13);
                    Console.SetCursorPosition(12, 12);
                    Console.WriteLine("                            ");
                    Console.SetCursorPosition(12, 12);
                    email = Console.ReadLine();
                    setTextAt("                                     ", 8, 13);
                    emailTest = testG.checkEmail(email);
                }

                setTextAt("Is the information correct (y/n)?", 3, 16);
                Console.SetCursorPosition(37, 16);
                string selectionG = Console.ReadLine(); // finalize the account details

                if (selectionG.Equals("Y") || selectionG.Equals("y"))
                {
                    AccountG newAccount = new AccountG(firstName, lastName, address, Convert.ToInt32(phone), email); // create a new account with the given credentials
                    setTextAt("Account Created Successfuly details will be provided by email", 5, 18);

                    newAccount.loadInfo(); // load the variables for email
                    textFields.Clear();
                    textFields.Add("Account No: " + newAccount.AccountNo);
                    textFields.Add("Account Balance: $" + newAccount.Balance);
                    textFields.Add("First Name: " + newAccount.FirstName);
                    textFields.Add("Last Name: " + newAccount.LastName);
                    textFields.Add("Address: " + newAccount.Address);
                    textFields.Add("Phone: " + newAccount.Phone);
                    textFields.Add("Email: " + newAccount.Email);

                    string body = "";

                    for (int i = 0; i < textFields.Count; i++)
                    {
                        body += textFields[i] + "\n";
                    }

                    done = sendEmail(body, newAccount.Email);

                    setTextAt(String.Format("Your Account Number is: {0}", newAccount.AccountNo), 5, 19);
                    done = true;
                }

            }

        }

        public void loginPage() // login page display
        {
            generateForm(10, 50);
            setTextAt("Welcome To The Faisal Bank", 12, 2); // display the title at left , top 
            setTextAt("Login to start", 18, 5); //  display the subtitle at left , top
            setTextAt("User Name:", 3, 7); // set different locations for texts
            setTextAt("password:", 3, 8);
            setTextAt("                 ", 13, 7); // empty text to clear up the spaces
            setTextAt("                  ", 12, 8);

            LoginPage login = new LoginPage(); // initiate the login page
        }

        public void searchAccountDis() // search an account
        {
            bool done = false; // continuous loop untill user doesnt want to search for the account anymore

            while (!done)
            {
                generateForm(8, 50);
                setTextAt("Enter The Details", 18, 2); // display the title at left , top 
                setTextAt("Account Number:", 3, 6); //  display the subtitle at left , top
                setTextAt("                     ", 19, 6);

                Console.SetCursorPosition(19, 6);
                string userInput = Console.ReadLine();
                int accountNo;
                bool isNumeric = int.TryParse(userInput, out accountNo); // check if only digits are entered
                bool chooseG = false;

                if (isNumeric && userInput.Length <= 10)
                {
                    AccountG account = new AccountG(accountNo); // create an account object with the related account number
                    bool accountFound = account.searchAccount(accountNo); // check if account exists

                    if (accountFound)
                    {
                        account.loadInfo();
                        displayAccountDetails(account, false);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n)?", 3, 26);
                            Console.SetCursorPosition(31, 26);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                setTextAt("press enter to go back to main menu", 1, 28);

                                chooseG = true; // exit the inner loop
                                done = true; // exit the outer loop
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                Console.Clear();
                                done = false;
                                chooseG = true;
                            }

                        }

                    }
                    else  // if account number not found 
                    {
                        setTextAt("Invalid account number", 3, 12);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n) ?", 3, 13);
                            Console.SetCursorPosition(31, 13);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                Console.WriteLine("press enter to go back to main menu");
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                setTextAt("                                     ", 1, 12);
                                setTextAt("                                     ", 1, 13);
                                done = false;
                                chooseG = true;
                            }

                        }

                    }

                }
                else // if account number entered is not in a valid format
                {
                    setTextAt("Invalid account number", 3, 12);

                    while (!chooseG)
                    {
                        setTextAt("check another account(y/n) ?", 3, 13);
                        string choose = Console.ReadLine();

                        if (choose.Equals("n") || choose.Equals("N"))
                        {
                            done = true;
                            Console.WriteLine("press enter to go back to main menu");
                            chooseG = true;
                        }

                        if (choose.Equals("y") || choose.Equals("Y"))
                        {
                            setTextAt("                                     ", 1, 12);
                            setTextAt("                                     ", 1, 13);
                            done = false;
                            chooseG = true;
                        }

                    }

                }

            }

        }


        public void deleteAccountDis()
        {
            int accountNo;
            bool done = false;

            while (!done)
            {
                generateForm(8, 50);
                setTextAt("Delete An Account", 18, 2); // display the title at left , top 
                setTextAt("Enter The Details", 18, 5);
                setTextAt("Account Number:", 3, 7); //  display the subtitle at left , top
                setTextAt("                            ", 19, 7);

                Console.SetCursorPosition(19, 7);
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out accountNo);
                bool chooseG = false;

                if (isNumeric && userInput.Length <= 10)
                {
                    AccountG account = new AccountG(accountNo);
                    bool accountFound = account.searchAccount(accountNo);

                    if (accountFound)
                    {
                        account.loadInfo();
                        displayAccountDetails(account, false);
                        while (!chooseG)
                        {
                            setTextAt("Delete(y/n)?", 3, 26);
                            Console.SetCursorPosition(16, 26);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                setTextAt("press enter to go back to main menu", 1, 28);
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                account.deleteAccount();
                                Console.WriteLine("Account deleted successfully");
                                done = true;
                                chooseG = true;
                            }

                        }

                    }
                    else
                    {
                        setTextAt("Invalid account number", 3, 12);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n)?", 3, 13);
                            Console.SetCursorPosition(31, 13);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                Console.WriteLine("press enter to go back to main menu");
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                setTextAt("                                 ", 1, 12);
                                setTextAt("                                 ", 1, 13);
                                done = false;
                                chooseG = true;
                            }

                        }

                    }

                }
                else
                {
                    setTextAt("Invalid format used", 3, 12);
                    while (!chooseG)
                    {
                        setTextAt("check another account(y/n)?", 3, 12);
                        string choose = Console.ReadLine();

                        if (choose.Equals("n") || choose.Equals("N"))
                        {
                            done = true;
                            Console.WriteLine("press enter to go back to main menu");
                            chooseG = true;
                        }

                        if (choose.Equals("y") || choose.Equals("Y"))
                        {
                            setTextAt("                                 ", 1, 12);
                            setTextAt("                                 ", 1, 13);
                            done = false;
                            chooseG = true;
                        }

                    }

                }

            }

        }


        public void depositDis()
        {
            bool done = false;

            while (!done)
            {
                generateForm(9, 50);
                setTextAt("Deposit", 18, 2);
                setTextAt("Enter The Details", 18, 5); // display the title at left , top 
                setTextAt("Account Number:", 3, 7); //  display the subtitle at left , top
                setTextAt("Amount:", 3, 8);
                setTextAt("                        ", 19, 7);

                Console.SetCursorPosition(19, 7);
                string userInput = Console.ReadLine();
                int accountNo;
                bool isNumeric = int.TryParse(userInput, out accountNo);
                bool chooseG = false;

                if (isNumeric && userInput.Length <= 10)
                {
                    AccountG account = new AccountG(accountNo);
                    bool accountFound = account.searchAccount(accountNo);

                    if (accountFound)
                    {
                        setTextAt("Account Found! Plese Enter the amount", 3, 12);
                        Console.SetCursorPosition(11, 8);
                        string userAmount = Console.ReadLine();
                        double amount;
                        bool isNum = double.TryParse(userAmount, out amount);

                        if (!isNum)
                        {
                            setTextAt("invalid user input for amount", 3, 13);
                            setTextAt("                                     ", 11, 8);
                            setTextAt("press any key to continue ", 3, 14);
                            done = true;
                        }
                        else
                        {
                            bool deposited = account.depositAmount(amount);

                            if (deposited)
                            {
                                done = true;
                                setTextAt("                                     ", 3, 13);

                                setTextAt("Amount deposited", 3, 13);
                                Console.WriteLine("Press any key to continue");
                            }
                            else
                            {
                                setTextAt("                                     ", 3, 13);
                                setTextAt("Amount deposited", 3, 13);
                                Console.WriteLine("Press any key to continue");
                            }
                            done = true;
                        }

                    }
                    else
                    {
                        setTextAt(" Invalid account number", 3, 12);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n)?", 3, 13);
                            Console.SetCursorPosition(31, 13);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                Console.WriteLine("press enter to go back to main menu");
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                setTextAt("                                     ", 1, 12);
                                setTextAt("                                     ", 1, 13);
                                done = false;
                                chooseG = true;
                            }

                        }

                    }

                }
                else
                {
                    setTextAt(" Invalid format used", 3, 12);

                    while (!chooseG)
                    {
                        setTextAt("check another account(y/n)?", 3, 12);
                        Console.SetCursorPosition(31,12);
                        string choose = Console.ReadLine();

                        if (choose.Equals("n") || choose.Equals("N"))
                        {
                            done = true;
                            Console.WriteLine("press enter to go back to main menu");
                            chooseG = true;
                        }

                        if (choose.Equals("y") || choose.Equals("Y"))
                        {
                            setTextAt("                                     ", 1, 12);
                            setTextAt("                                     ", 1, 13);
                            done = false;
                            chooseG = true;
                        }

                    }

                }

            }

        }


        public void withdrawDis()
        {
            bool done = false;

            while (!done)
            {
                generateForm(9, 50);
                setTextAt("Withdraw", 18, 2);
                setTextAt("Enter The Details", 18, 5); // display the title at left , top 
                setTextAt("Account Number:", 3, 7); //  display the subtitle at left , top
                setTextAt("Amount:", 3, 8);
                setTextAt("                   ", 19, 7);

                Console.SetCursorPosition(19, 7);
                string userInput = Console.ReadLine();
                int accountNo;
                bool isNumeric = int.TryParse(userInput, out accountNo);
                bool chooseG = false;

                if (isNumeric && userInput.Length <= 10)
                {
                    AccountG account = new AccountG(accountNo);
                    bool accountFound = account.searchAccount(accountNo);

                    if (accountFound)
                    {
                        setTextAt("Account Found! Plese Enter the amount", 3, 12);
                        Console.SetCursorPosition(11, 8);
                        string userAmount = Console.ReadLine();
                        double amount;
                        bool isNum = double.TryParse(userAmount, out amount);

                        if (!isNum)
                        {
                            setTextAt("invalid user input for amount", 3, 12);
                            setTextAt("                             ", 11, 8);
                            setTextAt("press any key to continue", 3, 14);
                            done = true;
                        }
                        else
                        {
                            bool withdrawn = account.withdrawAmount(amount);

                            if (withdrawn)
                            {
                                done = true;
                                setTextAt("                             ", 3, 13);
                                setTextAt("Amount withdrawn", 3, 13);
                                Console.WriteLine("Press any key to continue");
                            }
                            else
                            {
                                setTextAt("                             ", 3, 13);
                                setTextAt("Invalid amoount entered", 3, 13);
                                Console.WriteLine("Press any key to continue");
                            }
                            done = true;

                        }

                    }
                    else
                    {
                        setTextAt("Invalid account number", 3, 12);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n)?", 3, 13);
                            Console.SetCursorPosition(31, 13);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                Console.WriteLine("press enter to go back to main menu");
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                setTextAt("                                     ", 1, 12);
                                setTextAt("                                     ", 1, 13);
                                done = false;
                                chooseG = true;
                            }

                        }

                    }

                }
                else
                {
                    setTextAt(" Invalid format used", 3, 12);
                    while (!chooseG)
                    {
                        setTextAt("check another account(y/n)?", 3, 13);
                        Console.SetCursorPosition(31, 13);
                        string choose = Console.ReadLine();

                        if (choose.Equals("n") || choose.Equals("N"))
                        {
                            done = true;
                            Console.WriteLine("press enter to go back to main menu");
                            chooseG = true;
                        }

                        if (choose.Equals("y") || choose.Equals("Y"))
                        {
                            setTextAt("                                     ", 1, 12);
                            setTextAt("                                     ", 1, 13);
                            done = false;
                            chooseG = true;
                        }

                    }

                }

            }

        }


        public void accountStatementDis()
        {

            bool done = false;

            while (!done)
            {
                generateForm(8, 50);
                setTextAt("Account statement", 18, 2); // display the title at left , top 
                setTextAt("Enter  the details", 12, 5);
                setTextAt("Account Number:", 3, 7); //  display the subtitle at left , top
                setTextAt("                     ", 19, 7);

                Console.SetCursorPosition(19, 7);
                string userInput = Console.ReadLine();
                int accountNo;
                bool isNumeric = int.TryParse(userInput, out accountNo);
                bool chooseG = false;

                if (isNumeric && userInput.Length <= 10)
                {
                    AccountG account = new AccountG(accountNo);
                    bool accountFound = account.searchAccount(accountNo);

                    if (accountFound)
                    {
                        List<string> textFields = new List<string>();
                        List<String> transactions = new List<string>();
                        displayAccountDetails(account, true);
                        done = true;
                    }
                    else
                    {
                        setTextAt("Invalid account number", 3, 12);

                        while (!chooseG)
                        {
                            setTextAt("check another account(y/n) ", 3, 13);
                            Console.SetCursorPosition(31, 13);
                            string choose = Console.ReadLine();

                            if (choose.Equals("n") || choose.Equals("N"))
                            {
                                done = true;
                                setTextAt("press enter to go back to main menu", 3, 13);
                                chooseG = true;
                            }

                            if (choose.Equals("y") || choose.Equals("Y"))
                            {
                                setTextAt("                                    ", 1, 12);
                                setTextAt("                                    ", 1, 13);
                                done = false;
                                chooseG = true;
                            }

                        }

                    }

                }
                else
                {
                    setTextAt("Invalid format used", 3, 12);
                    while (!chooseG)
                    {
                        setTextAt("check another account(y/n)?", 3, 13);
                        Console.SetCursorPosition(31, 13);
                        string choose = Console.ReadLine();

                        if (choose.Equals("n") || choose.Equals("N"))
                        {
                            done = true;
                            Console.WriteLine("press enter to go back to main menu");
                            chooseG = true;
                        }

                        if (choose.Equals("y") || choose.Equals("Y"))
                        {
                            setTextAt("                                    ", 1, 12);
                            setTextAt("                                    ", 1, 13);
                            done = false;
                            chooseG = true;
                        }

                    }

                }

            }

        }

        public void displayAccountDetails(AccountG account, bool showTransactions)
        {
            List<String> transactions = new List<string>();
            List<String> textFields = new List<string>();
            transactions = account.DataRem;
            account.loadInfo();

            if (showTransactions)
            {
                generateForm(19, 50, 10);
                setTextAt(string.Format("Account Details"), 18, 12); // display the title at left , top 
                setTextAt(string.Format("Account Number:{0}", account.AccountNo), 3, 16); //  display the subtitle at left , top
                setTextAt(string.Format("Account Balance:{0}", account.Balance), 3, 17);
                setTextAt(string.Format("First Name:{0}", account.FirstName), 3, 18);
                setTextAt(string.Format("Last Name:{0}", account.LastName), 3, 19);
                setTextAt(string.Format("Address:{0}", account.Address), 3, 20);
                setTextAt(string.Format("Phone:{0}", account.Phone), 3, 21);
                setTextAt(string.Format("Email:{0}", account.Email), 3, 22);

                if (transactions.Count > 0)
                {
                    if (transactions.Count >= 5)
                    {
                        int i = transactions.Count;
                        setTextAt(string.Format("Transaction details {0}", account.DataRem.Count), 3, 23);
                        setTextAt(string.Format(transactions[i - 5]), 3, 24);
                        setTextAt(string.Format(transactions[i - 4]), 3, 25);
                        setTextAt(string.Format(transactions[i - 3]), 3, 26);
                        setTextAt(string.Format(transactions[i - 2]), 3, 27);
                        setTextAt(string.Format(transactions[i - 1]), 3, 28);
                    }
                    if (transactions.Count < 5)
                    {
                        for (int i = 0; i < transactions.Count; i++)
                        {
                            setTextAt(string.Format("Transaction details1"), 3, 23);
                            setTextAt(string.Format(transactions[i]), 3, 24 + i);
                        }

                    }

                }

                bool chooseG = false;

                while (!chooseG)
                {
                    setTextAt("         ", 25, 31);
                    setTextAt("Email statement(y/n)? ", 3, 31);
                    Console.SetCursorPosition(25, 31);
                    string choose = Console.ReadLine(); // choose for email

                    if (choose.Equals("n") || choose.Equals("N"))
                    {
                        setTextAt("press enter to go back to main menu", 1, 32);
                        chooseG = true;
                        return;
                    }

                    if (choose.Equals("y") || choose.Equals("Y"))
                    {
                        textFields.Clear();
                        textFields.Add(string.Format("Account No: {0} \n ", account.AccountNo));
                        textFields.Add(string.Format("Account Balance: {0}$ \n", account.Balance));
                        textFields.Add(string.Format("First Name: {0} \n", account.FirstName));
                        textFields.Add(string.Format("Last Name: {0} \n", account.LastName));
                        textFields.Add(string.Format("Address: {0} \n", account.Address));
                        textFields.Add(string.Format("Phone: {0} \n", account.Phone));
                        textFields.Add(string.Format("Email: {0} \n", account.Email));

                        if (transactions.Count > 0)  // check if there is any transaction history
                        {
                            if (transactions.Count >= 5)
                            {
                                int i = transactions.Count;

                                textFields.Add(transactions[i - 5]);
                                textFields.Add(transactions[i - 4]);
                                textFields.Add(transactions[i - 3]);
                                textFields.Add(transactions[i - 2]);
                                textFields.Add(transactions[i - 1]);
                            }
                            if (transactions.Count < 5)
                            {
                                textFields.Add(string.Format("transaction no2 {0}\n", transactions.Count));
                                for (int i = 0; i < transactions.Count; i++)
                                {
                                    textFields.Add(transactions[i]);
                                }

                            }

                        }

                        string body = "";

                        for (int i = 0; i < textFields.Count; i++)
                        {
                            body += textFields[i] + "\n"; // appending the list for email body
                        }

                        chooseG = sendEmail(body, account.Email); // confirming email has been sent

                        if (chooseG)
                        {
                            setTextAt("Emailed Successfully!", 5, 33);
                        }
                        else
                        {
                            setTextAt("Email not sent!!!", 5, 33);
                        }

                        return;

                    }

                }

            }
            else
            {
                generateForm(14, 50, 10);
                setTextAt(string.Format("Account Details"), 18, 12); // display the title at left , top 
                setTextAt(string.Format("Account Number:{0}", account.AccountNo), 3, 16); //  display the subtitle at left , top
                setTextAt(string.Format("Account Balance:{0}", account.Balance), 3, 17);
                setTextAt(string.Format("First Name:{0}", account.FirstName), 3, 18);
                setTextAt(string.Format("Last Name:{0}", account.LastName), 3, 19);
                setTextAt(string.Format("Address:{0}", account.Address), 3, 20);
                setTextAt(string.Format("Phone:{0}", account.Phone), 3, 21);
                setTextAt(string.Format("Email:{0}", account.Email), 3, 22);
            }

        }

        public bool sendEmail(string body, string email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("faisaltest.net@gmail.com", "test@net123"),
                EnableSsl = true,
            };

            try
            {
                smtpClient.Send("faisaltest.net@gmail.com", email, "ACCOUNT DETAILS", body);
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

    }

}


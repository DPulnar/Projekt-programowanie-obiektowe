namespace Projekt
{
    internal class Program
    {
        public static bool exit = false;
        private UserPage userPage;
        static void Main(string[] args)
        {
            

            Database.InitializeDatabase();
            do
            {


                Console.WriteLine("1. Logowanie");
                Console.WriteLine("2. Rejestracja");
                Console.WriteLine("3. Exit");
     
                int selection =  Int32.Parse(Console.ReadLine());
                OptionSelection(selection);


            } 
            while (exit == false);

        }
        public static void OptionSelection(int selection)
        {
            switch (selection)
            {
                case 1:
                    Console.WriteLine("Logowanie");
                    LoginUser();


                    break;
                case 2:
                    Console.WriteLine("Rejestracja");
                    RegisterUser();
                    break;
                case 3:
                    Console.WriteLine("Exit");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór");
                    break;
            }
        }
        public static void LoginUser()
        {
            Console.WriteLine("Podaj email");
            string email = Console.ReadLine();
            Console.WriteLine("Podaj hasło");
      
            string password = Console.ReadLine();
          


            bool response = Database.UserLogin(email, password);
            if (response)
            {
                Console.WriteLine("Zalogowano");
                UserPage userPage = new UserPage(Database.GetUserId(email));
                userPage.UserPageMenu();
            }
            else
            {
                Console.WriteLine("Niepoprawne dane logowania");
            }   
        }

   
        public static void RegisterUser()
        {
            Console.WriteLine("Podaj email");
            string email = Console.ReadLine().ToString();
            bool validatedEmail = DataValidation.ValidateEmail(email);
            if (!validatedEmail)
            {
                Console.WriteLine("Niepoprawny email");
                return;
            }
            Console.WriteLine("Podaj hasło");
            string password = Console.ReadLine().ToString();
            Console.WriteLine("Podaj hasło drugi raz");
            string passwordConfirmation = Console.ReadLine().ToString();
            if (password != passwordConfirmation)
            {
                Console.WriteLine("Hasła nie są takie same");
                return;
            }
            bool validated = DataValidation.ValidatePassword(password);
            if (!validated)
            {
                Console.WriteLine("Hasło jest za krótkie,musi mieć conjamniej 8 znaków");
                return;
            }


            bool response = Database.AddUser(email, password);
            if (response)
            {
                Console.WriteLine("Użytkownik został dodany");
            }
            else
            {
                Console.WriteLine("Użytkownik o podanym emailu już istnieje");
            }

        }
    }
}

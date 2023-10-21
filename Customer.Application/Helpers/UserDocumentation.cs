using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Helpers
{
    public class UserDocumentation
    {
        public void DisplayDocumentation()
        {
            Console.WriteLine("1. Walidacja Loginu:");
            DisplayLoginValidationHelp();
            Console.WriteLine("\n");
            Console.WriteLine("2. Walidacja Hasła:");
            DisplayPasswordValidationHelp();
            Console.WriteLine("\n");
            Console.WriteLine("4. Walidacja Adresu Email:");
            DisplayEmailValidationHelp();
            Console.WriteLine("\n");
            Console.WriteLine("5. Walidacja Numeru Telefonu:");
            DisplayPhoneNumberValidationHelp();
            Console.WriteLine("\n");
            Console.WriteLine("3. Walidacja Imienia i Nazwiska:");
            DisplayNameValidationHelp();
            Console.WriteLine("\n");
            Console.WriteLine("6. Walidacja Stanowiska:");
            DisplayPositionValidationHelp();
        }
        public void DisplayLoginValidationHelp()
        {
            Console.WriteLine("- Login musi mieć od 4 do 14 znaków.");
            Console.WriteLine("- Może zawierać tylko litery i cyfry.");
            Console.WriteLine("- Musi zawierać przynajmniej jedną cyfrę.");
            Console.WriteLine("- Nie może już istnieć użytkownik o takim samym loginie.");
        }

        public void DisplayPasswordValidationHelp()
        {
            Console.WriteLine("- Hasło musi mieć od 8 do 16 znaków.");
            Console.WriteLine("- Musi zawierać przynajmniej jedną cyfrę.");
            Console.WriteLine("- Może zawierać tylko jeden znak specjalny.");
        }

        public void DisplayNameValidationHelp()
        {
            Console.WriteLine("- Imię i Nazwisko musi składać się wyłącznie z liter.");
        }

       public void DisplayEmailValidationHelp()
        {
            Console.WriteLine("- Adres email musi być w formacie standardowym.");
        }

        public void DisplayPhoneNumberValidationHelp()
        {
            Console.WriteLine("- Numer telefonu musi składać się z 9 cyfr.");
        }

        public void DisplayPositionValidationHelp()
        {
            Console.WriteLine("- Stanowisko musi być jednym z dostępnych: Manager, Kierownik, Specjalista, Asystent, Pracownik.");
            Console.WriteLine("- Tylko jeden Administrator może być zarejestrowany.");
        }
    }
}

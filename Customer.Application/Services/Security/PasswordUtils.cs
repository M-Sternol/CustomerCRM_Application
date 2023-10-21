using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services.Security
{
    public class PasswordUtils
    {
        public static string GetMaskedPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return password;
        }


        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(inputPassword);
            return StringComparer.OrdinalIgnoreCase.Compare(hashedInputPassword, hashedPassword) == 0;
        }
    }
}

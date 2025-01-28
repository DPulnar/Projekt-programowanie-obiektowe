using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public static class DataValidation
    {
        public static bool ValidateEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidatePassword(string password)
        {
            if (password.Length >= 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarDealer.Models.EntityModels
{
    public partial class User
    {
        private static bool CheckIsUsernameValid(string usernameForChecking)
        {
            string pattern = @"^[A-Za-z]\w+$";
            Regex regex = new Regex(pattern);

            if (usernameForChecking.Length >= 3 && regex.IsMatch(usernameForChecking))
            {
                return true;
            }

            return false;
        }

        private static bool CheckIsPasswordValid(string passwordForChecking)
        {
            string pattern = @"^\w+$";
            Regex regex = new Regex(pattern);

            if (passwordForChecking.Length >= 6 && regex.IsMatch(passwordForChecking))
            {
                return true;
            }

            return false;
        }

        private static bool CheckIsEmailValid(string emailForChecking)
        {
            string pattern = @"^[A-Za-z]{1}[\w.]+@\w+\.\w{2,3}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(emailForChecking);
        }
    }
}

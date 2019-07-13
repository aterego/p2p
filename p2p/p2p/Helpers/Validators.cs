using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace p2p.Helpers
{
    public class Validators
    {

        public static string ServerBaseUrl { get; set; } = "http://192.168.1.10:5002";
        //"http://localhost:8080";

        public static async Task<bool> ConnectionAvailableAsync()
        {
            try
            {
                var get = string.Format("http://google.com");
                var client = new HttpClient();
                var result = await client.GetAsync(get);
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


        public static bool NameIsValid(string name)
        {
            string expresion;
            expresion = @"^[\p{L} \.\-\']+$";
            if (Regex.IsMatch(name, expresion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PhoneIsValid(string name)
        {
            string expresion;
            expresion = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            if (Regex.IsMatch(name, expresion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool EmailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
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


    }
}

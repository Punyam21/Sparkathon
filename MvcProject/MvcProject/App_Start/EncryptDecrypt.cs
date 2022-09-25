using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.App_Start
{
    public class EncryptDecrypt
    {
        public string Encrypt(string str)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            return Convert.ToBase64String(b);


        }

        public string Decrypt(string str)
        {
            byte[] b;
            string decrp;
            try
            {
                b = Convert.FromBase64String(str);
                decrp = System.Text.ASCIIEncoding.ASCII.GetString(b);

            }catch(FormatException e)
            {
                decrp = "";
            }
            return decrp;


        }
    }
}
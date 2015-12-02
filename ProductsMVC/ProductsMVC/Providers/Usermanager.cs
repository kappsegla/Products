using ProductsMVC.Providers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProductsMVCTest
{
    public class Usermanager
    {
        IRandom r;
        
        Dictionary<string, string> accounts = new Dictionary<string, string>()
        {
            { "User1","P@ssw0rd" }
        };

        public Usermanager(IRandom rand)
        {
            r = rand;
        }

        public bool Validate(string v1, string v2)
        {
            string password;
            if( accounts.TryGetValue(v1, out password))
            {
                if (password.Equals(v2))
                    return true;
            }
            return false;
        }

        private static readonly char[] InvalidChars = "@#$ÅÄÖåäö".ToCharArray();
        private bool ValidateUserName(string v1)
        {
            return v1.IndexOfAny(InvalidChars) >= 0;
        }

        public string AddUser(string v1)
        {
            if (ValidateUserName(v1))
                return null;

            string password = CreatePassword(7);

            accounts.Add(v1, password);
            return password;
        }

        private string CreatePassword(int v)
        {
            //Removed Oo0l1 that can be misstaken
            const string passchars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVXYZ123456789";
            StringBuilder pass = new StringBuilder();
         
            while(0 < v--)
            {
                pass.Append(passchars[r.Next(passchars.Length)]);
            }
            return pass.ToString();
        }

        public void ChangePassword(string user, string oldpassword, string newpassword)
        {
            if( Validate(user, oldpassword))
            {
                accounts[user] = newpassword;
                return;
            }
            throw new ArgumentException();
        }

        //SHA512 hash
        public string Get_Base64Encoded_SHA512Hash(string user)
        {
            string concatenated = user + accounts[user];
            byte[] data = System.Text.Encoding.ASCII.GetBytes(concatenated);
            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }
            return Convert.ToBase64String(hash);
        }
    }
}
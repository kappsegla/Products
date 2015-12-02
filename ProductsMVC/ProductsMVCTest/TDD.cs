using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductsMVC.Providers;

namespace ProductsMVCTest
{
    [TestClass]
    public class TDDTest
    {
        IRandom r = new TestRandom();
        [TestMethod]
        public void UserName_Password_Pair_Returns_True_Test()
        {
            Usermanager usermanager = new Usermanager(r);

            bool check = usermanager.Validate("User1", "P@ssw0rd");
            Assert.AreEqual(true, check);
        }

        [TestMethod]
        public void UserName_Password_Pair_Returns_False_Test()
        {
            Usermanager usermanager = new Usermanager(r);

            bool check = usermanager.Validate("User", "Pissw0rd");
            Assert.AreEqual(false, check);
        }
        [TestMethod]
        public void AddUser_Success()
        {
            Usermanager usermanager = new Usermanager(r);

            string check = usermanager.AddUser("Martin");
            Assert.IsNotNull(check);
        }
        [TestMethod]
        public void AddUser_ValidateUserName_Fail()
        {
            Usermanager usermanager = new Usermanager(r);

            string check = usermanager.AddUser("@#$ÅÄÖ");
            Assert.IsNull(check);
        }
        [TestMethod]
        public void AddUser_ReturnsPassword()
        {
            Usermanager usermanager = new Usermanager(r);

            string newpassword = usermanager.AddUser("Martin");

            //How can we verify random output data? Insert our own random generator... Dependency Injection
            Assert.AreEqual("abcdefg", newpassword);
        }
        [TestMethod]
        public void ChangePassword_Success()
        {
            Usermanager usermanager = new Usermanager(r);
            //Create user
            string password = usermanager.AddUser("Martin");
            //Change password
            usermanager.ChangePassword("Martin", password, "OK");
            //Check that we can use the new password
            Assert.AreEqual(true,usermanager.Validate("Martin", "OK"));
        }

        [TestMethod]
        public void GetSHA512()
        {
            Usermanager usermanager = new Usermanager(r);
            //Create user
            string password = usermanager.AddUser("Martin");
            //Change password
            usermanager.ChangePassword("Martin", password, "P@ssw0rd");
            //Check that we can use the new password
            Assert.AreEqual("KrwZWp+QMJzee36albLxYFTn+TQ1oohiI5TivJOU4bF1sNc3V3Ugs8GJN71lLuJV/T0l8XsgfSXKtMuk/KO4/g==", usermanager.Get_Base64Encoded_SHA512Hash("Martin"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongPassword_CastsException()
        {
            Usermanager usermanager = new Usermanager(r);
            //Create user
            string password = usermanager.AddUser("Martin");
            //Change password
            usermanager.ChangePassword("Martin", "OK", "P@ssw0rd");
        }
    }
}

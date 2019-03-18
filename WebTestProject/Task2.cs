using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebTestProject
{

    [TestClass]
    public class Task2
    {

        IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            this.driver = new EdgeDriver("C:\\Users\\Venessa Matlala\\Desktop\\Driver\\");
        }

        [TestMethod]
        public void TestCreateUsers()
        {
            var users = UserCreationLogic.GetUsersFromExcel();
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var addNewUserButtonPath = "/html/body/table/thead/tr[2]/td/button";
            IWebElement addNewUserButton = driver.FindElement(By.XPath(addNewUserButtonPath));
            addNewUserButton.Click();
            for (int i = 0; i < users.Count; i++)
            {
                var item = users[i];
                this.CreateUser(item);
                Thread.Sleep(1000);
                if (i < users.Count - 1)
                {
                   addNewUserButton.Click();
                }
                
            }
         
            //driver.Close();
            //driver.Quit();

        }

        public void CreateUser(User user)
        {
            this.ClearFields();

            String Fname = "FirstName";
            IWebElement FirstName = driver.FindElement(By.Name(Fname));
            FirstName.SendKeys(user.FirstName);

            String Lname = "LastName";
            IWebElement LastName = driver.FindElement(By.Name(Lname));
            LastName.SendKeys(user.LastName);

            String Uname = "UserName";
            IWebElement UserName = driver.FindElement(By.Name(Uname));
            UserName.SendKeys(user.Username);

            String Pword = "Password";
            IWebElement Password = driver.FindElement(By.Name(Pword));
            Password.SendKeys(user.Username);

            
            string optionsRadios = "optionsRadios";
            IWebElement optionsRadiosCntl = driver.FindElement(By.Name(optionsRadios));

            if (user.Customer.Equals("Company AAA"))
            {
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/form/table/tbody/tr[5]/td[2]/label[1]/input")).Click();
            }
            else
            {
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/form/table/tbody/tr[5]/td[2]/label[2]/input")).Click();
            }

            String role = "RoleId";
            IWebElement roles = driver.FindElement(By.Name(role));
            roles.SendKeys(user.Role);
            roles.SendKeys(Keys.Enter);

            driver.FindElement(By.Name("Email")).SendKeys(user.Email);

            driver.FindElement(By.Name("Mobilephone")).SendKeys(user.Cell);

            driver.FindElement(By.XPath("/html/body/div[3]/div[3]/button[2]")).Click();

        }

        public void ClearFields()
        {
            String Uname = "UserName";
            IWebElement UserName = driver.FindElement(By.Name(Uname));
            UserName.Clear();

            String Fname = "FirstName";
            IWebElement FirstName = driver.FindElement(By.Name(Fname));
            FirstName.Clear();

            String Lname = "LastName";
            IWebElement LastName = driver.FindElement(By.Name(Lname));
            LastName.Clear();

            String Pword = "Password";
            IWebElement Password = driver.FindElement(By.Name(Pword));
            Password.Clear();

            //if(user.Customer.Equals("Customer AAA"))
            //{
            //    driver.FindElement(By.XPath("/ html / body / div[3] / div[2] / form / table / tbody / tr[5] / td[2] / label[1] / input")).Click();
            //}
            //else
            //{
            //    driver.FindElement(By.XPath("/html/body/div[3]/div[2]/form/table/tbody/tr[5]/td[2]/label[2]/input")).Click();
            //}

            //String role = "RoleId";
            //IWebElement roles = driver.FindElement(By.Name(role));
            //roles.SendKeys("Admin");
            //roles.Clear();

            driver.FindElement(By.Name("Email")).Clear();

            driver.FindElement(By.Name("Mobilephone")).Clear();

        }
    }
}

using Microsoft.Playwright.MSTest;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace WebTests
{
    

    [TestClass]
    public class UITests : PageTest
    {
        string testsite;
        (string user, string password) admin;
        (string user, string password) user;

        [TestInitialize]
        public void TestSetup()
        {
            
            testsite = ConfigurationManager.AppSettings.Get("Site")!;
            admin.user = ConfigurationManager.AppSettings.Get("Admin_UserName")!;
            admin.password = ConfigurationManager.AppSettings.Get("Admin_Pass")!;
            user.user = ConfigurationManager.AppSettings.Get("User_UserName")!;
            user.password = ConfigurationManager.AppSettings.Get("User_Pass")!;
        }


        [TestMethod]
        public async Task MenuTestPopup()
        {
            try
            {
                await Page.GotoAsync(testsite);

                await Page.GetByRole(AriaRole.Button).First.ClickAsync();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Menu Test Failed");
                Assert.Fail();
            }


        }

        [TestMethod]
        public async Task DrinkCardTest()
        {
            try
            {
                await Page.GotoAsync(testsite + "/Drink/Cuba Libre/4");

                await Page.GetByText("C", new() { Exact = true }).ClickAsync();

                await Page.GetByRole(AriaRole.Heading, new() { Name = "Cuba Libre" }).ClickAsync();

                await Page.GetByRole(AriaRole.Link, new() { Name = "Drink Book" }).ClickAsync();

                await Page.GetByRole(AriaRole.Heading, new() { Name = "Drink List" }).ClickAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DrinkCardTest");
                Assert.Fail();
            }



        }

        [TestMethod]
        public async Task DataTest()
        {
            try
            {
                await Page.GotoAsync(testsite);

                await Page.Locator("div").Filter(new() { HasText = "Drink Book Log in Prototype Build v.1 Drink List Data And Reports Data Dashboard" }).GetByRole(AriaRole.Button).First.ClickAsync();

            }
            catch(Exception ex)
            {
                Console.WriteLine("DataTest Failed");
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task LoginLandingCheck()
        {
            try
            {
                await Page.GotoAsync(testsite);

                await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();

                await Page.GetByRole(AriaRole.Img, new() { Name = "dev-gqxqw0hxudx3tmbe" }).ClickAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Click Test Failure");
                Assert.Fail();
            }


        }

        [TestMethod]
        public async Task  AdminLoginLogoutTest()
        {
            try
            {
                await Page.GotoAsync(testsite);

                await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();

                await Page.GetByLabel("Email address").ClickAsync();

                await Page.GetByLabel("Email address").FillAsync(admin.user);

                await Page.GetByLabel("Password").ClickAsync();

                await Page.GetByLabel("Password").FillAsync(admin.password);

                await Page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();

                await Page.GetByRole(AriaRole.Heading, new() { Name = "Drink List (Mod & Admin View)" }).ClickAsync();

                await Page.GetByRole(AriaRole.Link, new() { Name = "Log out" }).ClickAsync();
            }
            catch (Exception)
            {

                Console.WriteLine("Admin Login Logout Test Failure");
                Assert.Fail();
            }



        }

        [TestMethod]
        public async Task UserLogInPermTest()
        {

            try
            {
                await Page.GotoAsync(testsite);

                await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();

                await Page.GetByLabel("Email address").ClickAsync();

                await Page.GetByLabel("Email address").FillAsync(user.user);

                await Page.GetByLabel("Password").ClickAsync();

                await Page.GetByLabel("Password").FillAsync(user.password);

                await Page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();

                await Page.GetByRole(AriaRole.Heading, new() { Name = "Drink List" }).ClickAsync();

                await Page.GetByRole(AriaRole.Link, new() { Name = "Log out" }).ClickAsync();
            }
            catch (Exception)
            {

                Console.WriteLine("User Login Logout Test Failure");
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task CheckEdit()
        {

            try
            {
                await Page.GotoAsync(testsite + "/drinktools/adddrinkpage");

                await Page.GetByRole(AriaRole.Heading, new() { Name = "Not Authorized" }).ClickAsync();
            }
            catch (Exception)
            {
                Console.WriteLine("Logged out user can see edit");
                Assert.Fail();
            }

        }
    }
}

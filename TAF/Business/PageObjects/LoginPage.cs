using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class LoginPage : BaseDriver
  {
    private IWebElement Login => FindElement(By.CssSelector("input[placeholder=\"Login\"]"));
    private IWebElement Password => FindElement(By.CssSelector("input[placeholder=\"Password\"]"));
    private IWebElement LoginButton => FindElement(By.XPath("//button[@type='submit' and text()='Login']"));
    private static bool IsLogged = false;

    public void PerformLogin()
    {
      if (IsLogged == false)
      {
        Login.TypeText("superadmin");
        Password.TypeText("erebus");
        LoginButton.ClickWithWait();


        IsLogged = true;
        logger.Info("Log in successfull");
      }
    }
  }
}
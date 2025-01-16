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
    public void PerformLogin()
    {
      Login.SendKeys("superadmin");
      Password.SendKeys("erebus");
      LoginButton.ClickWithWait();

      logger.Info("Log in successfull");
    }
  }
}
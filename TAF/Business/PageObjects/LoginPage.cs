using OpenQA.Selenium;
using TAF.Core.BaseClasses;

namespace TAF.Business.PageObjects
{
  public class LoginPage(IWebDriver driver) : BaseDriver(driver)
  {
    private IWebElement Login => FindElement(By.CssSelector("input[placeholder=\"Login\"]"));
    private IWebElement Password => FindElement(By.CssSelector("input[placeholder=\"Password\"]"));
    private IWebElement LoginButton => FindElement(By.XPath("//button[@type='submit' and text()='Login']"));
    public void PerformLogin()
    {
      Login.SendKeys("superadmin");
      Password.SendKeys("erebus");
      LoginButton.Click();

      logger.Info("Log in successfull");
    }
  }
}
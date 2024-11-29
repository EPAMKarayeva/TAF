using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Page_Objects
{
  public class LoginPage(IWebDriver driver) : BasePage(driver)
  {
    public void PerformLogin()
    {
      FindElement(By.CssSelector("input[placeholder=\"Login\"]")).SendKeys("superadmin");
      FindElement(By.CssSelector("input[placeholder=\"Password\"]")).SendKeys("erebus");
      FindElement(By.XPath("//button[@type='submit' and text()='Login']")).Click();

      logger.Info("Log in successfull");
    }
  }
}
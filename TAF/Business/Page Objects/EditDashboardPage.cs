using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Page_Objects
{
  public class EditDashboardPage(IWebDriver driver): BasePage(driver)
  {
    private IWebElement DeleteButton => WaitForVisbility(By.XPath("//button[.//span[text()='Edit']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement UpdateButton => FindElement(By.XPath("//button[@type='button' and text()='Update']"));

    public void PerformEditDashboard(string newName)
    {
      ClickJS(DeleteButton);
      InputName.Click();
      InputName.SendKeys(Keys.Control + "a");
      InputName.SendKeys(Keys.Backspace);
      InputName.SendKeys(newName);
      ClickJS(UpdateButton);
      logger.Info("Dashboard updated");
    }
  }
}

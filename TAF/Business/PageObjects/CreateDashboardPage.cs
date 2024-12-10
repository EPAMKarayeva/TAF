using OpenQA.Selenium;
using TAF.Core.BaseClasses;

namespace TAF.Business.PageObjects
{
  public class CreateDashboardPage(IWebDriver driver): BaseDriver(driver)
  {
    private IWebElement CreateButton => FindElement(By.XPath("//button[.//span[text()='Add New Dashboard']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement AddButton => FindElement(By.XPath("//button[@type='button' and text()='Add']"));

    public void CreateDashboard(string name, string description)
    {
      WaitForVisbility(CreateButton).Click();
      InputName.SendKeys(name);
      logger.Info("Name entered");
      WaitForVisbility(AddButton).Click();
      logger.Info("Dashboard created");
    }
  }
}

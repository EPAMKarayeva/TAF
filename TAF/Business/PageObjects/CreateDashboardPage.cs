using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class CreateDashboardPage: BaseDriver
  {
    private IWebElement CreateButton => FindElement(By.XPath("//button[.//span[text()='Add New Dashboard']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement AddButton => FindElement(By.XPath("//button[@type='button' and text()='Add']"));

    public void CreateDashboard(string name, string description)
    {
      CreateButton.ClickWithWait();
      InputName.TypeText(name);
      logger.Info("Name entered");
      AddButton.ClickWithWait();
      logger.Info("Dashboard created");
    }
  }
}

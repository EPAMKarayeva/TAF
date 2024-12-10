using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class EditDashboardPage(IWebDriver driver): BaseDriver(driver)
  {
    private IWebElement DeleteButton => FindElement(By.XPath("//button[.//span[text()='Edit']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement UpdateButton => FindElement(By.XPath("//button[@type='button' and text()='Update']"));

    public void RenameDashboard(string newName)
    {
      WaitForVisbility(DeleteButton).Click();
      InputHelper.InputText(InputName, newName);
      UpdateButton.Click();
      logger.Info("Dashboard updated");
    }
  }
}

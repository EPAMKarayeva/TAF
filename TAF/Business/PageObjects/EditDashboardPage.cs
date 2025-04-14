using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class EditDashboardPage(): BaseDriver()
  {
    private IWebElement EditButton => FindElement(By.XPath("//button[.//span[text()='Edit']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement UpdateButton => FindElement(By.XPath("//button[@type='button' and text()='Update']"));

    public void RenameDashboard(string newName)
    {
      EditButton.ClickWithWait();
      InputName.TypeText(newName);
      UpdateButton.ClickWithWait();
      logger.Info("Dashboard updated");
    }
  }
}

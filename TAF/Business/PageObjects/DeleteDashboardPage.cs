using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class DeleteDashboardPage : BaseDriver
  {
    private string name;
    private string rowXPath;

    private IWebElement ConfirmDeleteButton => FindElement(By.XPath("//button[@type='button' and text()='Delete']"));
    private IWebElement DeleteByNameButton => FindElement(By.XPath($"{rowXPath}//i[contains(@class, 'icon__icon-delete--')]"));

    public DeleteDashboardPage(string dashboardName)
    {
      name = dashboardName;
      //I'm using assignment in the constructor of the class for initialization here, because fields are initializing in the run time
      rowXPath = $"//div[contains(@class, 'gridRow__grid-row--') and .//a[contains(@class, 'dashboardTable__name--') and text()='{name}']]";
    }

    public void DeleteDashboardByName()
    {
      DeleteByNameButton.ClickWithWait();
      ConfirmDeleteButton.ClickWithWait();
      logger.Info("Dashboard deleted");
    }
  }
}
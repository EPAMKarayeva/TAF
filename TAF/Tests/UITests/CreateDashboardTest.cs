using OpenQA.Selenium;
using TAF.Business.Constants;
using TAF.Business.PageObjects;
using TAF.Core.BaseClasses;

namespace TAF.Tests.UITests
{
  public class CreateDashboardTest : BaseUiTest
  {
    [Test]
    public void CheckCreateDashboard()
    {
      var dashboardName = "NEW BOARD " + DateTime.Now;

      var createDashboardPage = new CreateDashboardPage();
      createDashboardPage.CreateDashboard(dashboardName, "");
      createdDashboards.Add(dashboardName);

      browser.GoToPage(DashboardPages.PersonalDashboard);

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//a[contains(text(), '{dashboardName}')]"));

      Assert.That(dashboardElement, Is.Not.Null);
    }
  }
}
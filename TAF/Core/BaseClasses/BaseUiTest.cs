using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TAF.Business.Constants;
using TAF.Business.PageObjects;
using TAF.Core.Utilities.Helpers;

namespace TAF.Core.BaseClasses
{
  public class BaseUiTest
  {
    protected static IWebDriver driver;
    protected static WebDriverWait wait;
    protected static Browser browser = new Browser();
    protected static List<string> createdDashboards = new List<string>();

    [OneTimeSetUp]
    public static void SetUpWebDriver()
    {
      driver = DriverManager.GetDriver();
      driver.Manage().Window.Maximize();

      browser.GoToPage(DashboardPages.LoginPage);

      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
      var loginPage = new LoginPage();
      loginPage.PerformLogin();
    }

    [SetUp]
    public void NavigateToHomePage()
    {
      browser.GoToPage(DashboardPages.PersonalDashboard);
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    public static IWebElement WaitForElementToBeVisible(By locator)
    {
      return wait.Until(d =>
      {
        var element = d.FindElement(locator);
        return element;
      });
    }

    public static IList<IWebElement> WaitForElementsToBeVisible(By locator)
    {
      return wait.Until(d =>
      {
        var elements = d.FindElements(locator);
        return elements;
      });
    }

    public static void CleanUp(string dashboardName)
    {
      browser.GoToPage(DashboardPages.PersonalDashboard);

      var rowXPath = $"//div[contains(@class, 'gridRow__grid-row--') and .//a[contains(@class, 'dashboardTable__name--') and text()='{dashboardName}']]";

      var deleteButton = driver.FindElement(By.XPath($"{rowXPath}//i[contains(@class, 'icon__icon-delete--')]"));
      deleteButton.Click();

      var submitButton = WaitForElementToBeVisible(By.XPath("//button[@type='button' and text()='Delete']"));
      submitButton.Click();
    }

    [OneTimeTearDown]
    public static void TearDownRestClient()
    {
      foreach (var dashboardName in createdDashboards)
      {
        CleanUp(dashboardName);
      }

      driver.Quit();
      driver.Dispose();
    }
  }
}
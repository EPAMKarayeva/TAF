using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TAF.Business.Constants;
using TAF.Business.PageObjects;
using TAF.Core.Utilities.Helpers;

namespace TAF.Core.BaseClasses
{
  public class BaseUiTest
  {
    protected static WebDriverWait wait;
    protected static Browser browser = new Browser();
    protected static Logger logger = LogManager.GetCurrentClassLogger();
    protected static List<string> createdDashboards = new List<string>();

    [OneTimeSetUp]
    public static void SetUpWebDriver()
    {
      var driver = DriverManager.GetDriver();
      driver.Manage().Window.Maximize();

      browser.GoToPage(DashboardPages.LoginPage);

      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
      var loginPage = new LoginPage();
      loginPage.PerformLogin();
    }

    [SetUp]
    public void NavigateToHomePage()
    {
      var driver = DriverManager.GetDriver();
      browser.GoToPage(DashboardPages.PersonalDashboard);
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public static IWebElement WaitForElementToBeVisible(By locator)
    {
      try
      {
        return wait.Until(d =>
        {
          var element = d.FindElement(locator);
          return element;
        });
      }
      catch(ElementNotVisibleException) 
      {
        logger.Error("Element is not visible.");
        throw;
      }
    }

    public static IList<IWebElement> WaitForElementsToBeVisible(By locator)
    {
      try
      {
        return wait.Until(d =>
        {
          var elements = d.FindElements(locator);
          return elements;
        });
      }
      catch (ElementNotVisibleException)
      {
        logger.Error("Elements are not visible.");
        throw;
      }
    }

    public static void CleanUp(string dashboardName)
    {
      browser.GoToPage(DashboardPages.PersonalDashboard);

      try
      {
        var deletePage = new DeleteDashboardPage(dashboardName);
        deletePage.DeleteDashboardByName();
        logger.Info($"Dashboard '{dashboardName}' successfully deleted");
      }
      catch (Exception ex)
      {
        logger.Info($"Dashboard '{dashboardName}' wasn't successfully deleted. {ex.Message}");
      }
    }

    [OneTimeTearDown]
    public static void TearDownRestClient()
    {
      if (createdDashboards.Count > 0)
      {
        foreach (var dashboardName in createdDashboards)
        {
          CleanUp(dashboardName);
        }
      }
    }
  }
}
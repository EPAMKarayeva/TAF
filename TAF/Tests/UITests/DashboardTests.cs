using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TAF.Business.PageObjects;
using TAF.Core.BaseClasses;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TAF.Tests.UITests
{
  public class DashboardTests
  {
    private static IWebDriver driver;
    protected static WebDriverWait wait;
    private static List<string> createdDashboards = new List<string>();

    [OneTimeSetUp]
    public static void SetUpWebDriver()
    {
      new DriverManager().SetUpDriver(new ChromeConfig());
      driver = new ChromeDriver();
      driver.Manage().Window.Maximize();
      driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");

      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
      var loginPage = new LoginPage(driver);
      loginPage.PerformLogin();
    }

    [SetUp]
    public void NavigateToHomePage()
    {
      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard");
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    [Test]
    public void CheckCreateDashboard()
    {
      var dashboardName = "NEW BOARD " + DateTime.Now;

      var createDashboardPage = new CreateDashboardPage(driver);
      createDashboardPage.CreateDashboard(dashboardName, "");
      createdDashboards.Add(dashboardName);

      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard");

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//a[contains(text(), '{dashboardName}')]"));

      Assert.That(dashboardElement, Is.Not.Null);
    }

    [Test]
    public void CheckDeleteBoard()
    {
      var dashboardName = "BOARD FOR DELETION";

      var createDashboardPage = new CreateDashboardPage(driver);
      createDashboardPage.CreateDashboard(dashboardName, "");

      var deletePage = new DeleteDashboardPage(driver);
      deletePage.Delete();
      driver.Navigate().Refresh();

      var dashboardElements = WaitForElementsToBeVisible(By.XPath($"//a[contains(text(), '{dashboardName}')]"));

      Assert.That(dashboardElements, Is.Empty);
    }

    [Test]
    public void CheckUpdateBoard()
    {
      var newBoardName = "NEW BOARD " + DateTime.Now;

      var createDashboardPage = new CreateDashboardPage(driver);
      createDashboardPage.CreateDashboard(newBoardName, "");
      createdDashboards.Add(newBoardName);

      var editPage = new EditDashboardPage(driver);
      editPage.RenameDashboard(newBoardName);

      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard");

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//a[contains(text(), '{newBoardName}')]"));

      Assert.That(dashboardElement, Is.Not.Null);
    }

    [Test]
    public void CheckAddWidget()
    {
      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard/74");

      var widgetName = "Widget " + DateTime.Now;


      var addWidgetPage = new AddWidgetPage(driver);
      addWidgetPage.AddWidget(widgetName);

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='{widgetName}']"));

      Assert.That(dashboardElement, Is.Not.Null);
    }


    [Test]
    public void CheckJSActions()
    {
      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard/14");

      var baseDriver = new BaseDriver(driver);

      baseDriver.ScrollToElement();

      var isElementntInView = baseDriver.IsElementInView();

      Assert.That(isElementntInView, Is.True);
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


    public static void DeleteCreatedDashboard(string dashboardName)
    {
      driver.Navigate().GoToUrl("http://localhost:8080/ui/#superadmin_personal/dashboard");

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
        DeleteCreatedDashboard(dashboardName);
      }

      driver.Quit();
      driver.Dispose();
    }
  }
}
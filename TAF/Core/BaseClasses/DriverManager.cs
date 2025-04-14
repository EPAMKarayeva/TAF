using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TAF.Core.BaseClasses
{
  public class DriverManager
  {
    private static IWebDriver driver;
    private static readonly object lockObject = new object();

    public static IWebDriver GetDriver()
    {
      if (driver == null)
      {
        lock (lockObject)
        {
          if (driver == null)
          {
            driver = new ChromeDriver();
          }
        }
      }
      return driver;
    }
  }
}
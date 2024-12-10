using RestSharp;
using TAF.Business.Constants;

namespace TAF.Core.BaseClasses
{
  public class BaseTestClass
  {
    protected static IRestClient _client;

    [OneTimeSetUp]
    public static void InitializeRestClient()
    {
      _client = new RestClient("http://localhost:8080");
    }

    protected RestRequest RequestWithAuth(string url, Method method)
    {
      return RequestWithoutAuth(url, method)
        .AddHeader("Authorization", $"Bearer {DashboardUrl.ApiKey}")
        .AddUrlSegment("user", DashboardUrl.AdminUserName);
    }

    protected RestRequest RequestWithoutAuth(string url, Method method)
    {
      return new RestRequest(url, method);
    }

    [OneTimeTearDown]
    public static void TearDownRestClient()
    {
      _client?.Dispose();
    }
  }
}

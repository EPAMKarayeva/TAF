using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataParsers;

namespace TAF.Tests.API_tests.PUT
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class PutValidationTests :BaseTestClass
  {
    string newName = "NEW NAME " + DateTime.Now;

    [Test]
    public void CheckUpdateDashboardsWithOutAuth()
    {
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } }); 

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetWrongApi))]
    public void CheckUpdateDashboardWithWrongApiKey(string apiKey)
    {
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    .AddHeader("Authorization", apiKey)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetWrongId))]
    public void CheckUpdateDashboardWithWrongId(string id)
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
        .AddUrlSegment("id", id)
        .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }


    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParamsParser), nameof(TestCaseDataParamsParser.PostWithWrongParams))]
    public void CheckUpdateDashboardWithWrongParams(string description, string name)
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetAnotherId))]
    public void CheckUpdateDashboardWithAnotherId(string id)
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", id)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      var response = _client.Execute(request);

      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}

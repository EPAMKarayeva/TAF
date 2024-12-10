using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataParsers;


[assembly: LevelOfParallelism(5)]

namespace TAF.Tests.API_tests.GET
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  internal class GetValidationTest : BaseTestClass
  {
    [Test]
    public void CheckGetAllDashboardsWithOutAuth()
    {
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      var response =  _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content,Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetWrongApi))]
    public void CheckGetAllDashboardsWithWrongApiKey(string apiKey)
    {
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get)
        .AddHeader("Authorization", apiKey);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetWrongId))]
    public void CheckGetDashboardWithWrongId(string id)
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParser), nameof(TestCaseDataParser.GetAnotherId))]
    public void CheckGetDashboardWithAnotherId(string id)
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

      var response = _client.Execute(request);

      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}
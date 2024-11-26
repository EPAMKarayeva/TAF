using RestSharp;
using System.Net;
using TAF.Business.Models;
using TAF.Core.Utilities.Contants;
using TAF.Core.Utilities.TestData.TestDataProviders;
using TAF.Tests.TestClasses;

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
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl, Method.Get);

      var response =  _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content,Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetWrongApi))]
    public void CheckGetAllDashboardsWithWrongApiKey(string apiKey)
    {
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl, Method.Get)
        .AddHeader("Authorization", apiKey);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetWrongId))]
    public void CheckGetDashboardWithWrongId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.GetDashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetAnotherId))]
    public void CheckGetDashboardWithAnotherId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.GetDashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

      RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}
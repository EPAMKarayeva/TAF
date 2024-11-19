using RestSharp;
using System.Net;
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
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl);

      var response = Assert.Throws<HttpRequestException>(() => _client.Get(request));

      Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
      Assert.That("Request failed with status code Unauthorized", Is.EqualTo(response.Message));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetWrongApi))]
    public void CheckGetAllDashboardsWithWrongApiKey(string apiKey)
    {
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl)
        .AddHeader("Authorization", apiKey);

      var response = Assert.Throws<HttpRequestException>(() => _client.Get(request));

      Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
      Assert.That("Request failed with status code Unauthorized", Is.EqualTo(response.Message));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetWrongId))]
    public void CheckGetDashboardWithWrongId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.GetDashboardUrl)
        .AddUrlSegment("id", id);

      var response = Assert.Throws<HttpRequestException>(() => _client.Get(request));

      Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
      Assert.That("Request failed with status code BadRequest", Is.EqualTo(response.Message));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataGetProvider), nameof(TestCaseDataGetProvider.GetAnotherId))]
    public void CheckGetDashboardWithAnotherId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.GetDashboardUrl)
        .AddUrlSegment("id", id);

      var response = _client.Get(request);

      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(HttpStatusCode.NotFound, Is.EqualTo(response.StatusCode));
    }
  }
}
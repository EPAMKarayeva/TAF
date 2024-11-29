using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;
using TAF.Core.Utilities.TestData.TestDataProviders;
using TAF.Tests.TestClasses;

namespace TAF.Tests.API_tests.DELETE
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class DeleteValidationTests : BaseTestClass
  {
    [Test]
    public void CheckDeleteDashboardsWithOutAuth()
    {
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl, Method.Delete)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetWrongApi))]
    public void CheckDeleteWithWrongApiKey(string apiKey)
    {
      var request = RequestWithoutAuth(DashboardEnpoints.GetAllDashboardsUrl, Method.Delete)
        .AddHeader("Authorization", apiKey);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetWrongId))]
    public void CheckDeleteDashboardWithWrongId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", id);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetAnotherId))]
    public void CheckDeleteDashboardWithAnotherId(string id)
    {
      var request = RequestWithAuth(DashboardEnpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", id);

      var response = _client.Execute(request);

      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}

using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataParsers;

namespace TAF.Tests.APITests.DeleteDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class DeleteDashboardNegativeScenarios : BaseAPITest
  {
    [Test]
    public void CheckDeleteDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Delete)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard);

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataWithWrongApiKey.json" })]
    public void CheckDeleteWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Delete)
        .AddHeader("Authorization", apiKey);

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] {"TestDataWithInvalidId.json" })]
    public void CheckDeleteDashboardWithWrongId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", id);

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataWithValidId.json" })]
    public void CheckDeleteDashboardWithAnotherId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", id);

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}

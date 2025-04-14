using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataManager;

namespace TAF.Tests.APITests.PutDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class PutDashboardNegativeScenarios : BaseAPITest
  {
    string newName = "NEW NAME " + DateTime.Now;

    [Test]
    public void CheckUpdateDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataWithWrongApiKey.json" })]
    public void CheckUpdateDashboardWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    .AddHeader("Authorization", apiKey)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataWithInvalidId.json" })]
    public void CheckUpdateDashboardWithWrongId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
        .AddUrlSegment("id", id)
        .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }


    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataInvalidValues.json" })]
    public void CheckUpdateDashboardWithWrongParams(string description, string name)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataWithValidId.json" })]
    public void CheckUpdateDashboardWithAnotherId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", id)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.Content, Does.Contain($"Dashboard with ID '{id}' not found on project"));
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
  }
}

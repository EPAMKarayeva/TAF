using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF.Business.Models;
using TAF.Core.Utilities.Contants;
using TAF.Tests.TestClasses;

namespace TAF.Tests.API_tests.GET
{
  internal class GetTests : BaseTestClass
  {

    [Test]
    public void CheckGetAllDashboardsTest()
    {
      var request = RequestWithAuth(DashboardEnpoints.GetAllDashboardsUrl, Method.Get);

      var response = _client.Execute(request);
      var responseContent = JToken.Parse(response.Content);

      bool dashboardsExists = responseContent["content"]
                           .Children<JObject>()
                           .Any();

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      Assert.That(dashboardsExists, Is.True);

    }

    [Test]
    public void CheckGetDashboardTest()
    {
      var request = RequestWithAuth(DashboardEnpoints.DashboardUrl, Method.Get)
          .AddQueryParameter("fields", "id,name")
          .AddUrlSegment("id", DashboardUrl.ExistingDashboardId);

      RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      Assert.That(response.Data.Name, Is.EqualTo("DEMO DASHBOARD"));
    }
  }
}
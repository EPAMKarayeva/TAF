using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;
using TechTalk.SpecFlow;

namespace TAF.Tests.BDD.Steps
{
  [Binding]
  public class ReportPortalApiSteps
  {
    private static IRestClient _client = new RestClient("http://localhost:8080");
    private RestRequest request;
    private RestResponse response;

    protected RestRequest RequestWithAuth()
    {
      return RequestWithoutAuth()
      .AddHeader("Authorization", $"Bearer {DashboardUrl.ApiKey}");
    }

    protected RestRequest RequestWithoutAuth()
    {
      return new RestRequest();
    }

    [Given("a request with authorization")]
    public void ARequestWithAuthorization()
    {
      request = RequestWithAuth();
    }

    [Given("a request without authorization")]
    public void ARequesteWithoutAuthorization()
    {
      request = RequestWithoutAuth();
    }

    [Given("the request has header:")]
    public void TheRequestHasQueryParams(Table table)
    {
      foreach (var row in table.Rows)
      {
        request = request.AddHeader(row[0], row[1]);
      }
    }

    [Given("the request has path params:")]
    public void TheRequestHasPathParams(Table table)
    {
      foreach (var row in table.Rows)
      {
        request = request.AddUrlSegment(row["name"], row["value"]);
      }
    }

    [When("the '{}' request is sent to {string} endpoint")]
    public void TheRequestIsSentToEndpoint(Method method, string endpoint)
    {
      request.Method = method;
      request.Resource = endpoint;
      response = _client.ExecuteAsync(request).Result;
    }

    [Then("the response status code is {}")]
    public void TheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
    {
      Assert.That(expectedStatusCode, Is.EqualTo(response.StatusCode));
    }

    [Then("the response contains the following values with params:")]
    public void TheResponseContainsValues(Table table)
    {
      foreach (var row in table.Rows)
      {
        Assert.That(row["expected_value"], Is.EqualTo(JToken.Parse(response.Content).SelectToken(row["param"]).ToString()));
      }
    }

    [Then("the response body is equal to {string}")]
    public void TheResponseBodyIsEqualTo(string expected_value)
    {
      Assert.That(response.Content, Does.Contain(expected_value));

    }
  }
}
using Newtonsoft.Json;

namespace TAF.Business.Models
{
  public class ErrorResponse
  {
    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }
  }
}
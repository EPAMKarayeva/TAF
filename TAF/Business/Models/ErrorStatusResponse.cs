using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Models
{
  public class ErrorStatusResponse
  {
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
  }
}
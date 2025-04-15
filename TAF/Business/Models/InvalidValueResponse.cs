using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Models
{
    public class InvalidValueResponse
    {
    [JsonProperty("errorCode")]
    public int ErrorCode { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
  }
}

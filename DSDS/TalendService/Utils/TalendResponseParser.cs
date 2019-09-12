using System.Collections.Generic;
using System.Linq;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TalendService.Utils
{
    public static class TalendResponseParser
    {
        public static (List<TalendResponseObject> successfulRows, List<string> failedRows) ParseTalendResponse(string content)
        {
            var successfulTokens = new List<TalendResponseObject>();
            var failedTokens = new List<string>();

            foreach (var arrayElement in JArray.Parse(content))
            {
                var token = arrayElement.SelectToken("rows");

                if (arrayElement.Value<string>("key") == "success")
                {
                    if (token is JArray)
                    {
                        successfulTokens.AddRange(token.ToObject<List<TalendResponseObject>>());
                    }
                    else
                    {
                        successfulTokens.Add(token.SelectToken("success").ToObject<TalendResponseObject>());
                    }
                }
                else
                {
                    failedTokens.AddRange(token.ToObject<List<JObject>>().Select(obj => obj.ToString(Formatting.None)).ToList());
                }
            }

            return (successfulTokens, failedTokens);
        }
    }
}
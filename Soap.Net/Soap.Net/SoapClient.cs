using System;
using System.Collections.Generic;
using System.Net.Http;
using NameBright.DomainApi.Soap.NameBrightDomainPurchasing;
using NameBright.DomainApi.Soap.OAuth2Helpers;
using Newtonsoft.Json;
namespace NameBright.DomainApi.Soap
{
    class SoapClient
    {
        static void Main(string[] args)
        {
            //api.namebright.com apis require OAuth2 bearer tokens for all requests.
            RetrieveAccessToken();

            DomainPurchasingClient purchasingClient = new DomainPurchasingClient();
            
            //Test domain availability
            var result = purchasingClient.GetDomainAvailability("namebright.com");
            Console.WriteLine("namebright.com: " + JsonConvert.SerializeObject(result, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter()));

            result = purchasingClient.GetDomainAvailability("thismustbeavailabile1234.com");
            Console.WriteLine("thismustbeavailabile1234.com: " + JsonConvert.SerializeObject(result, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter()));

            Console.ReadKey();
        }

        static void RetrieveAccessToken()
        {
            string accessToken = null;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.namebright.com/");
                
                //TODO: fill in client_id and client_secret below.
                // Your application name will be in the form of "account name:application name". e.g. "MyAccount:MyApp"
                // Go here to manage api applications for your account: https://www.namebright.com/Settings#Api
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", /* FILL ME IN */ ""),
                    new KeyValuePair<string, string>("client_secret", /* FILL ME IN */ "")
                });
                var result = httpClient.PostAsync("/auth/token", content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
                var anonType = new { access_token = string.Empty };
                var tokenObject = JsonConvert.DeserializeAnonymousType(resultContent, anonType);
                accessToken = tokenObject.access_token;
            }

            //Set the access token in the inspector so that all of our client requests pass our credentials.
            OAuth2HeaderInspector.AccessToken = accessToken; //not thread safe.
        }
    }
}

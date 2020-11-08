using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Globalization;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Security.Cryptography;

namespace DashboardMaterias
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            GetAuthorizationToken();
        }



        private static async Task<string> GetAuthorizationToken()
        {
            string jwt = CreateJwt();

            var dic = new Dictionary<string, string>
       {
           { "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" },
           { "assertion", jwt }
       };
            var content = new FormUrlEncodedContent(dic);

            var httpClient = new HttpClient { BaseAddress = new Uri("https://accounts.google.com") };
            var response = await httpClient.PostAsync("/o/oauth2/token", content);
            response.EnsureSuccessStatusCode();

            dynamic dyn = await response.Content.ReadAsAsync<dynamic>();
            return dyn.access_token;
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        private static string CreateJwt()
        {
            string url = "https://www.googleapis.com/robot/v1/metadata/x509/googlemeetpruebacuenta%40projectmeet-293616.iam.gserviceaccount.com";
            var certificate = new X509Certificate2(Convert.FromBase64String(url), "136bb241dc85a27a6a6f8089d7cb4eddd0fca05f");

            DateTime now = DateTime.UtcNow;
            var claimset = new
            {
                iss = "googlemeetpruebacuenta@projectmeet-293616.iam.gserviceaccount.com",
                scope = "https://www.googleapis.com/auth/calendar",
                aud = "https://oauth2.googleapis.com/token",
                iat = ((int)now.Subtract(UnixEpoch).TotalSeconds).ToString(CultureInfo.InvariantCulture),
                exp = ((int)now.AddMinutes(55).Subtract(UnixEpoch).TotalSeconds).ToString(CultureInfo.InvariantCulture)
            };

            // header
            var header = new { typ = "JWT", alg = "RS256" };

            // encoded header
            var headerSerialized = JsonConvert.SerializeObject(header);
            var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
            var headerEncoded = TextEncodings.Base64Url.Encode(headerBytes);

            // encoded claimset
            var claimsetSerialized = JsonConvert.SerializeObject(claimset);
            var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
            var claimsetEncoded = TextEncodings.Base64Url.Encode(claimsetBytes);

            // input
            var input = String.Join(".", headerEncoded, claimsetEncoded);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // signiture
            var rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
            var cspParam = new CspParameters
            {
                KeyContainerName = rsa.CspKeyContainerInfo.KeyContainerName,
                KeyNumber = rsa.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2
            };
            var cryptoServiceProvider = new RSACryptoServiceProvider(cspParam) { PersistKeyInCsp = false };
            var signatureBytes = cryptoServiceProvider.SignData(inputBytes, "SHA256");
            var signatureEncoded = TextEncodings.Base64Url.Encode(signatureBytes);

            // jwt
            return String.Join(".", headerEncoded, claimsetEncoded, signatureEncoded);
        }


        private void CreateToken()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var apiSecret = "adMvFxlEqySjPJQnzcTWm4I97oQz65khAlji";
            byte[] symmetricKey = System.Text.Encoding.ASCII.GetBytes(apiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "googlemeetpruebacuenta@projectmeet-293616.iam.gserviceaccount.com",


                Expires = now.AddSeconds(3600),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.RsaSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);
        }

        private void postMetting()
        {

            //var client = new RestClient("https://api.zoom.us/v2/users/jtorrstorrs@gmail.com/meetings");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("content-type", "application/json");
            //request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDA5MjA0MTUsImV4cCI6MTYwMDkyNDAxMSwiaWF0IjoxNjAwOTIwNDE1LCJpc3MiOiIyc0VqRkQ3elJIV3RESmNDdkRfZHVBIn0.xrrX-rkQYODrujyaiLS_LbAaapDagM__RAa0-eunuMo");
            //request.AddParameter("application/json", "{\"duration\":60,\"host_id\":\"jtorrstorrs@gmail.com \",\"id\":5073480908,\"join_url\":\"https://zoom.us/j/5073480908\",\"settings\":{\"alternative_hosts\":\"\",\"approval_type\":2,\"audio\":\"both\",\"auto_recording\":\"local\",\"close_registration\":false,\"cn_meeting\":false,\"enforce_login\":false,\"enforce_login_domains\":\"\",\"host_video\":false,\"in_meeting\":false,\"join_before_host\":true,\"mute_upon_entry\":false,\"participant_video\":false,\"registrants_confirmation_email\":true,\"use_pmi\":false,\"waiting_room\":false,\"watermark\":false,\"registrants_email_notification\":true},\"start_time\":\"2020-09-24T22:00:00\",\"start_url\":\"https://us04web.zoom.us/j/5073480908?pwd=UWx6cEswUTZCQkJEWmoveHd1aFQ2QT09 \",\"status\":\"waiting\",\"timezone\":\"America/Mexico_City\",\"topic\":\"API token jwt\",\"type\":2}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);


            //var client = new RestClient("https://api.zoom.us/v2/users?status=active&page_size=30&page_number=1");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("content-type", "application/json");
            //request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));

            //IRestResponse response = client.Execute(request);
        }
    }
}
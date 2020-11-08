using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace example
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CreateToken();
            this.postMetting();
        }


        private void CreateToken()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var apiSecret = "adMvFxlEqySjPJQnzcTWm4I97oQz65khAlji";
            byte[] symmetricKey = System.Text.Encoding.ASCII.GetBytes(apiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "2sEjFD7zRHWtDJcCvD_duA",
                Expires = now.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);
        }

        private void postMetting()
        {
            try
            {
                var client = new RestClient("https://api.zoom.us/v2/users/jtorrstorrs@gmail.com/meetings");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDA5MjA0MTUsImV4cCI6MTYwMDkyNDAxMSwiaWF0IjoxNjAwOTIwNDE1LCJpc3MiOiIyc0VqRkQ3elJIV3RESmNDdkRfZHVBIn0.xrrX-rkQYODrujyaiLS_LbAaapDagM__RAa0-eunuMo");
                request.AddParameter("application/json", "{\"duration\":60,\"host_id\":\"jtorrstorrs@gmail.com \",\"id\":5073480908,\"join_url\":\"https://zoom.us/j/5073480908\",\"settings\":{\"alternative_hosts\":\"\",\"approval_type\":2,\"audio\":\"both\",\"auto_recording\":\"local\",\"close_registration\":false,\"cn_meeting\":false,\"enforce_login\":false,\"enforce_login_domains\":\"\",\"host_video\":false,\"in_meeting\":false,\"join_before_host\":true,\"mute_upon_entry\":false,\"participant_video\":false,\"registrants_confirmation_email\":true,\"use_pmi\":false,\"waiting_room\":false,\"watermark\":false,\"registrants_email_notification\":true},\"start_time\":\"2020-09-24T22:00:00\",\"start_url\":\"https://us04web.zoom.us/j/5073480908?pwd=UWx6cEswUTZCQkJEWmoveHd1aFQ2QT09 \",\"status\":\"waiting\",\"timezone\":\"America/Mexico_City\",\"topic\":\"API token jwt2\",\"type\":2}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

            }
            catch (Exception ex)
            {

            }
        }

    }
}
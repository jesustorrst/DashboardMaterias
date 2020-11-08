using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace MeetWF
{
    public partial class Contact : Page
    {
        protected string googleplus_client_id = "838174723617-mjuo3m55f38nuer9k2jpfur71tfda320.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_secret = "xQ2aNp8rWs2r5Fh-cuzmrZh6";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "urn:ietf:wg:oauth:2.0:oob, http://localhost";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string Parameters;

        public static string[] Scopes =
        {
                                CalendarService.Scope.Calendar
                                
                               //,CalendarService.Scope.CalendarReadonly
                            };
        public static UserCredential GetUserCredential(out string error)
        {
            UserCredential credential = null;
            error = string.Empty;

            try
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "295167635687-lbtrijb7k56ekuodqjdd4dgiieoo8o9g.apps.googleusercontent.com",
                    ClientSecret = "F8HzhD38w2HWFJd6nhl2BSEk"
                },
                Scopes,
                Environment.UserName,
                CancellationToken.None,
                null).Result;
            }
            catch (Exception ex)
            {
                credential = null;
                error = "Failed to UserCredential Initialization: " + ex.ToString();
            }

            return credential;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string scopes = "https://www.googleapis.com/auth/calendar";

            //if (string.IsNullOrEmpty(redirectUri))
            //{
            //    redirectUri = "urn:ietf:wg:oauth:2.0:oob";
            //}
            string oauth = string.Format("https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code", googleplus_client_id, googleplus_redirect_url, scopes);
          


            try
            {
                var url = oauth;
                if (url != "")
                {
                    string queryString = url.ToString();
                    char[] delimiterChars = { '=' };
                    string[] words = queryString.Split(delimiterChars);
                    string code = words[1];

                    if (code != null)
                    {
                        //get the access token 
                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                        webRequest.Method = "POST";
                        Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_secret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(Parameters);
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                        webRequest.ContentLength = byteArray.Length;
                        Stream postStream = webRequest.GetRequestStream();
                        // Add the post data to the web request
                        postStream.Write(byteArray, 0, byteArray.Length);
                        postStream.Close();

                        WebResponse response = webRequest.GetResponse();
                        postStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(postStream);
                        string responseFromServer = reader.ReadToEnd();

                        GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                        if (serStatus != null)
                        {
                            string accessToken = string.Empty;
                            accessToken = serStatus.access_token;

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                // This is where you want to add the code if login is successful.
                                // getgoogleplususerdataSer(accessToken);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message, ex);
                Response.Redirect("index.aspx");
            }

        }

        public static string AddCalenderEvents(string refreshToken, string emailAddress, string summary, DateTime? start, DateTime? end, out string error)
        {
            string eventId = string.Empty;
            error = string.Empty;
            string serviceError;

            UserCredential credential = GetUserCredential(out error);
            try
            {
                var calendarService = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "MeetWF"
                });


                //if (calendarService != null && string.IsNullOrWhiteSpace(serviceError))
                //{
                //    var list = calendarService.CalendarList.List().Execute();
                //    var calendar = list.Items.SingleOrDefault(c => c.Summary == emailAddress);
                //    if (calendar != null)
                //    {
                Google.Apis.Calendar.v3.Data.Event calenderEvent = new Google.Apis.Calendar.v3.Data.Event();

                calenderEvent.Summary = summary;
                //calenderEvent.Description = summary;
                //calenderEvent.Location = summary;
                calenderEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    //DateTime = new DateTime(2018, 1, 20, 19, 00, 0)
                    DateTime = start//,
                                    //TimeZone = "Europe/Istanbul"
                };
                calenderEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    //DateTime = new DateTime(2018, 4, 30, 23, 59, 0)
                    DateTime = start.Value.AddHours(12)//,
                                                       //TimeZone = "Europe/Istanbul"
                };
                calenderEvent.Recurrence = new List<string>();

                //Set Remainder
                //calenderEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData()
                //{
                //    UseDefault = false,
                //    Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                //    {
                //                            new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 24 * 60 },
                //                            new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "popup", Minutes = 24 * 60 }
                //    }
                //};

                //#region Attendees
                ////Set Attendees
                //calenderEvent.Attendees = new Google.Apis.Calendar.v3.Data.EventAttendee[] {
                //                        new Google.Apis.Calendar.v3.Data.EventAttendee() { Email = "kaptan.cse@gmail.com" },
                //                        new Google.Apis.Calendar.v3.Data.EventAttendee() { Email = emailAddress }
                //                    };
                //#endregion

                var newEventRequest = calendarService.Events.Insert(calenderEvent, "primary");
                newEventRequest.SendNotifications = true;
                var eventResult = newEventRequest.Execute();
                eventId = eventResult.Id;
                //}
            }

            catch (Exception ex)
            {
                eventId = string.Empty;
                error = ex.Message;
            }
            return eventId;
        }





        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
    }
}
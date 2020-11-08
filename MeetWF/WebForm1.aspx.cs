using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

using Newtonsoft.Json;
using System.Threading;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Responses;

namespace MeetWF
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["code"] != null)
                GetAccessToken();
        }

        protected void googleButton_Click(object sender, EventArgs e)
        {
            /*https://developers.google.com/accounts/docs/OAuth2InstalledApp
              https://developers.google.com/google-apps/contacts/v3/
              https://developers.google.com/accounts/docs/OAuth2WebServer
              https://developers.google.com/oauthplayground/
            */
            string clientId = "295167635687-bhugb57e9u7tg46ieolrbs636silvljj.apps.googleusercontent.com";
            string redirectUrl = "https://localhost:44339/Webform1";
            string scope = "https://www.googleapis.com/auth/calendar";
            //https://accounts.google.com/o/oauth2/auth?scope=https://www.googleapis.com/auth/drive&response_type=code&access_type=offline&redirect_uri=redirect_url&client_id=client_id
            Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=" + scope + "&approval_prompt=force&access_type=offline");
        }
        public void GetAccessToken()
        {
            string code = Request.QueryString["code"]; //"4/0AfDhmrhkTNFgMEWC5kDhKko63t8KlhYSXhNE6Jo-5AdOGxQawkYnwG0rqRtKoodQFGWW4A";
            string google_client_id = "295167635687-bhugb57e9u7tg46ieolrbs636silvljj.apps.googleusercontent.com";
            string google_client_sceret = "CwUAg3ydCCE1dnA6OshbOJfX";
            string google_redirect_url = "https://localhost:44339/Webform1";

            /*Get Access Token and Refresh Token*/
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            webRequest.Method = "POST";
            string parameters = "code=" + code + "&client_id=" + google_client_id + "&client_secret=" + google_client_sceret + "&redirect_uri=" + google_redirect_url + "&grant_type=authorization_code";
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
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
            /*End*/
            Run(serStatus);
            //CreateEvent(serStatus);
        }

        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }
        public void Run(GooglePlusAccessToken model)
        {
            //string refreshToken = string.Empty;
            //string credentialError;
            //var credential = GetUserCredential(out credentialError);
            //if (credential != null && string.IsNullOrWhiteSpace(credentialError))
            //{
            //    //Save RefreshToken into Database 
            //    refreshToken = credential.Token.RefreshToken;
            //}

            string addEventError;
            string calendarEventId = string.Empty;

            calendarEventId = AddCalenderEvents(model.refresh_token, "jtorrstorrs@gmail.com", "My Calendar Event", DateTime.Now, DateTime.Now.AddHours(1), out addEventError);

        }

        public static string ApplicationName = "CW1";
        public static string ClientId1 = "295167635687-bhugb57e9u7tg46ieolrbs636silvljj.apps.googleusercontent.com";
        public static string ClientSecret1 = "CwUAg3ydCCE1dnA6OshbOJfX";
        public static string RedirectURL1 = "https://localhost:44339/Webform1";

        public static string[] Scopes =
        {
                                CalendarService.Scope.Calendar
                                
                               //,CalendarService.Scope.CalendarReadonly
        };

        private static readonly IAuthorizationCodeFlow flow =
        new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = ClientId1,
                ClientSecret = ClientSecret1
            },
            Scopes = new[] { CalendarService.Scope.Calendar },
            DataStore = new FileDataStore("Drive.Api.Auth.Store")
        });

      

        public static IAuthorizationCodeFlow GoogleAuthorizationCodeFlow(out string error)
        {
            IAuthorizationCodeFlow flow = null;
            error = string.Empty;

            try
            {
                flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets =  new ClientSecrets 
                    {
                        ClientId = ClientId1,
                        ClientSecret = ClientSecret1
                    },
                    Scopes = Scopes
                });
            }
            catch (Exception ex)
            {
                flow = null;
                error = "Failed to AuthorizationCodeFlow Initialization: " + ex.ToString();
            }

            return flow;
        }

        public static UserCredential GetGoogleUserCredentialByRefreshToken(string refreshToken, out string error)
        {
            TokenResponse respnseToken = null;
            UserCredential credential = null;
            string flowError;
            error = string.Empty;
            try
            {
                // Get a new IAuthorizationCodeFlow instance
                IAuthorizationCodeFlow flow = GoogleAuthorizationCodeFlow(out flowError);


                respnseToken = new TokenResponse() { RefreshToken = refreshToken };

                // Get a new Credential instance                
                if ((flow != null && string.IsNullOrWhiteSpace(flowError)) && respnseToken != null)
                {
                    credential = new UserCredential(flow, "user", respnseToken);
                }

                // Get a new Token instance
                if (credential != null)
                {
                    bool success = credential.RefreshTokenAsync(CancellationToken.None).Result;
                }

                // Set the new Token instance
                if (credential.Token != null)
                {
                    string newRefreshToken = credential.Token.RefreshToken;
                }
            }
            catch (Exception ex)
            {
                credential = null;
                error = "UserCredential failed: " + ex.ToString();
            }
            return credential;
        }

        public static CalendarService GetCalendarService(string refreshToken, out string error)
        {
            CalendarService calendarService = null;
            string credentialError;
            error = string.Empty;
            try
            {
                var credential = GetGoogleUserCredentialByRefreshToken(refreshToken, out credentialError);
                if (credential != null && string.IsNullOrWhiteSpace(credentialError))
                {
                    calendarService = new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName
                    });
                }
            }
            catch (Exception ex)
            {
                calendarService = null;
                error = "Calendar service failed: " + ex.ToString();
            }
            return calendarService;
        }

        public static string AddCalenderEvents(string refreshToken, string emailAddress, string summary, DateTime? start, DateTime? end, out string error)
        {
            string eventId = string.Empty;
            error = string.Empty;
            string serviceError;

            try
            {
                var calendarService = GetCalendarService(refreshToken, out serviceError);

                if (calendarService != null && string.IsNullOrWhiteSpace(serviceError))
                {
                    var list = calendarService.CalendarList.List().Execute();
                    var calendar = list.Items.SingleOrDefault(c => c.Summary == emailAddress);
                    if (calendar != null)
                    {

                        

                        Google.Apis.Calendar.v3.Data.Event calenderEvent = new Google.Apis.Calendar.v3.Data.Event();


                        calenderEvent.Summary = "prueba";

                        //calenderEvent.Description = summary;

                        //calenderEvent.Location = summary;

                        calenderEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime

                        {

                            //DateTime = new DateTime(2018, 1, 20, 19, 00, 0)

                            DateTime = start,

                            TimeZone = "America/Mexico_City"

                        };

                        calenderEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime

                        {

                            //DateTime = new DateTime(2018, 4, 30, 23, 59, 0)

                            DateTime = start.Value.AddHours(1),

                            TimeZone = "America/Mexico_City"

                        };

                        calenderEvent.Creator = new Google.Apis.Calendar.v3.Data.Event.CreatorData();

                        calenderEvent.Creator.DisplayName = "Jesús Torres";

                        calenderEvent.Creator.Email = "jtorrstorrs@gmail.com";

                        EventAttendee a = new EventAttendee();
                        a.Email = "jesustorrst@gmail.com";
                        List<EventAttendee> attendes = new List<EventAttendee>();
                        attendes.Add(a);
                        calenderEvent.Attendees = attendes;


                        calenderEvent.Status = "confirmed";

                        calenderEvent.Visibility = "public";

                        calenderEvent.Description = "EjemploDescripcion";

                        var Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" };

                        calenderEvent.Recurrence = Recurrence;

                        

                        calenderEvent.ConferenceData = new Google.Apis.Calendar.v3.Data.ConferenceData
                        {
                            CreateRequest = new CreateConferenceRequest
                            {
                                ConferenceSolutionKey = new ConferenceSolutionKey
                                {
                                    Type = "hangoutsMeet"
                                },
                                Status = new ConferenceRequestStatus
                                {
                                    StatusCode = "success"
                                },
                                RequestId = "123456789",                                
                            },
                            ConferenceSolution= new ConferenceSolution
                            {
                                Key= new ConferenceSolutionKey
                                {
                                    Type= "hangoutsMeet"
                                }
                            },
                            EntryPoints = new List<EntryPoint>
                            {
                                  new EntryPoint
                                  {
                                       EntryPointType="video"
                                  }
                            }

                        };


                        //calenderEvent.ConferenceData.CreateRequest = new Google.Apis.Calendar.v3.Data.CreateConferenceRequest();

                        //calenderEvent.ConferenceData.CreateRequest.ConferenceSolutionKey = new Google.Apis.Calendar.v3.Data.ConferenceSolutionKey();



                        //calenderEvent.ConferenceData.CreateRequest.ConferenceSolutionKey.Type = "hangoutsMeet";

                        //calenderEvent.ConferenceData.CreateRequest.Status = new Google.Apis.Calendar.v3.Data.ConferenceRequestStatus();

                        //calenderEvent.ConferenceData.CreateRequest.Status.StatusCode = "success";

                        //calenderEvent.ConferenceData.CreateRequest.RequestId = "123456789100";

                        //calenderEvent.ConferenceData.ConferenceSolution = new ConferenceSolution();
                        //calenderEvent.ConferenceData.ConferenceSolution.Key = new ConferenceSolutionKey();
                        //calenderEvent.ConferenceData.ConferenceSolution.Key.Type = "hangoutsMeet";



                        IDictionary<string, string> pm =new Dictionary<string, string> { { "conferenceDataVersion", "1" } };


                        calenderEvent.ConferenceData.Parameters = new ConferenceParameters();
                        calenderEvent.ConferenceData.Parameters.AddOnParameters = new ConferenceParametersAddOnParameters();
                        calenderEvent.ConferenceData.Parameters.AddOnParameters.Parameters = pm;
                        calenderEvent.ConferenceData.Parameters.AddOnParameters.ETag = "1";

                        //calenderEvent.ConferenceData.ConferenceSolution = new Google.Apis.Calendar.v3.Data.ConferenceSolution();

                        //calenderEvent.ConferenceData.ConferenceSolution.Key = new Google.Apis.Calendar.v3.Data.ConferenceSolutionKey();

                        //calenderEvent.ConferenceData.ConferenceSolution.Key.Type = "hangoutsMeet";

                        //List<EntryPoint> list1 = new System.Collections.Generic.List<Google.Apis.Calendar.v3.Data.EntryPoint>();
                        //EntryPoint entry = new Google.Apis.Calendar.v3.Data.EntryPoint();
                        //entry.EntryPointType = "video";
                        //entry.AccessCode = "123456";
                        //list1.Add(entry);
                        //calenderEvent.ConferenceData.EntryPoints = list1;


                        //calenderEvent.ExtendedProperties = new Event.ExtendedPropertiesData { Private__ = pm, Shared = pm };
                        //calenderEvent.ExtendedProperties=


                        try

                        {

                            String calendarId = "primary";
                           // EventsResource.InsertRequest request = new EventsResource.InsertRequest(calendarService, calenderEvent, calendarId);
                            

                             EventsResource.InsertRequest request = calendarService.Events.Insert(calenderEvent, calendarId);
                            request.ConferenceDataVersion = 1;
                            Event createdEvent = request.Execute();

                            Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

                        }

                        catch (Exception ex)

                        {



                        }




















                    }
                }
            }
            catch (Exception ex)
            {
                eventId = string.Empty;
                error = ex.Message;
            }
            return eventId;
        }



        public void CreateEvent(GooglePlusAccessToken serStatus)
        {
            UserCredential credential =
            GoogleWebAuthorizationBroker.AuthorizeAsync(
           new ClientSecrets
           {
               ClientId = "295167635687-bhugb57e9u7tg46ieolrbs636silvljj.apps.googleusercontent.com",
               ClientSecret = "CwUAg3ydCCE1dnA6OshbOJfX",

           },
           new[] { CalendarService.Scope.Calendar }, "user",
           CancellationToken.None).Result;

            credential.Token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse();
            credential.Token.AccessToken = serStatus.access_token;
            credential.Token.ExpiresInSeconds = serStatus.expires_in;
            credential.Token.RefreshToken = serStatus.refresh_token;
            credential.Token.TokenType = serStatus.token_type;

            var service = new CalendarService(new BaseClientService.Initializer()

            {

                HttpClientInitializer = credential,

                ApplicationName = "ProjectMeet",

            });











            DateTime start = DateTime.Now;





            Google.Apis.Calendar.v3.Data.Event calenderEvent = new Google.Apis.Calendar.v3.Data.Event();







            calenderEvent.Summary = "prueba";

            //calenderEvent.Description = summary;

            //calenderEvent.Location = summary;

            calenderEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime

            {

                //DateTime = new DateTime(2018, 1, 20, 19, 00, 0)

                DateTime = start,

                TimeZone = "America/Mexico_City"

            };

            calenderEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime

            {

                //DateTime = new DateTime(2018, 4, 30, 23, 59, 0)

                DateTime = start.AddHours(1),

                TimeZone = "America/Mexico_City"

            };

            //calenderEvent.Creator = new Google.Apis.Calendar.v3.Data.Event.CreatorData();

            //calenderEvent.Creator.DisplayName = "Jesús Torres";

            //calenderEvent.Creator.Email = "jtorrstorrs@gmail.com";



            calenderEvent.Status = "confirmed";

            calenderEvent.Visibility = "public";

            calenderEvent.Description = "EjemploDescripcion";

            var Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" };

            calenderEvent.Recurrence = Recurrence;



            calenderEvent.ConferenceData = new Google.Apis.Calendar.v3.Data.ConferenceData();


            calenderEvent.ConferenceData.CreateRequest = new Google.Apis.Calendar.v3.Data.CreateConferenceRequest();

            calenderEvent.ConferenceData.CreateRequest.ConferenceSolutionKey = new Google.Apis.Calendar.v3.Data.ConferenceSolutionKey();



            calenderEvent.ConferenceData.CreateRequest.ConferenceSolutionKey.Type = "hangoutsMeet";

            calenderEvent.ConferenceData.CreateRequest.Status = new Google.Apis.Calendar.v3.Data.ConferenceRequestStatus();

            calenderEvent.ConferenceData.CreateRequest.Status.StatusCode = "success";

            calenderEvent.ConferenceData.CreateRequest.RequestId = "123456789100";

            var pm = new Dictionary<string, string>
            {
                {"conferenceDataVersion", "1"}
            };


            List<ConferenceParametersAddOnParameters> listParameters = new List<ConferenceParametersAddOnParameters>();
            //listParameters.Add(new ConferenceParametersAddOnParameters { Parameters = {   }, ETag = "1" });
            ConferenceParameters objParam = new ConferenceParameters();
            objParam.AddOnParameters = new ConferenceParametersAddOnParameters();
            objParam.AddOnParameters.Parameters = pm;
            objParam.ETag = "1";





            calenderEvent.ConferenceData.Parameters = new ConferenceParameters();
            calenderEvent.ConferenceData.Parameters.AddOnParameters = new ConferenceParametersAddOnParameters();
            calenderEvent.ConferenceData.Parameters.AddOnParameters.Parameters = pm;
            calenderEvent.ConferenceData.Parameters.AddOnParameters.ETag = "1";

            calenderEvent.ConferenceData.ConferenceSolution = new Google.Apis.Calendar.v3.Data.ConferenceSolution();

            calenderEvent.ConferenceData.ConferenceSolution.Key = new Google.Apis.Calendar.v3.Data.ConferenceSolutionKey();

            calenderEvent.ConferenceData.ConferenceSolution.Key.Type = "hangoutsMeet";



            List<EntryPoint> list = new System.Collections.Generic.List<Google.Apis.Calendar.v3.Data.EntryPoint>();

            EntryPoint entry = new Google.Apis.Calendar.v3.Data.EntryPoint();

            entry.EntryPointType = "video";
            entry.AccessCode = "123456";

            //list.Add(new Google.Apis.Calendar.v3.Data.EntryPoint { EntryPointType = "video" });

            list.Add(entry);

            //EntryPoint entry = new Google.Apis.Calendar.v3.Data.EntryPoint();

            //entry.EntryPointType = "video";

            calenderEvent.ConferenceData.EntryPoints = list;





            try

            {

                String calendarId = "primary";

                EventsResource.InsertRequest request = service.Events.Insert(calenderEvent, calendarId);

                Event createdEvent = request.Execute();

                Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

            }

            catch (Exception ex)

            {



            }
        }
        //public void GetContacts(GooglePlusAccessToken serStatus)
        //{
        //    string refreshToken = serStatus.refresh_token;
        //    string accessToken = serStatus.access_token;
        //    string scopes = "https://www.google.com/m8/feeds/contacts/default/full/";
        //    OAuth2Parameters oAuthparameters = new OAuth2Parameters()
        //    {
        //        RedirectUri = "http://www.demo.yogihosting.com/aspnet/Google-Contacts-API/index.aspx",
        //        Scope = scopes,
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken
        //    };


        //    RequestSettings settings = new RequestSettings("<var>YOUR_APPLICATION_NAME</var>", oAuthparameters);
        //    ContactsRequest cr = new ContactsRequest(settings);
        //    ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
        //    query.NumberToRetrieve = 5000;
        //    Feed<Contact> feed = cr.Get<Contact>(query);

        //    StringBuilder sb = new StringBuilder();
        //    int i = 1;
        //    foreach (Contact entry in feed.Entries)
        //    {
        //        foreach (EMail email in entry.Emails)
        //        {
        //            sb.Append(i + "&nbsp;").Append(email.Address).Append("<br/>");
        //            i++;
        //        }
        //    }
        //    /*End*/
        //    dataDiv.InnerHtml = sb.ToString();
        //}


    }
}
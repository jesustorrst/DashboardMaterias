using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleMeetBien
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Credentials();  
        }

        public Google.Apis.Calendar.v3.Data.Event CreateObject(DateTime start)
        {
            Google.Apis.Calendar.v3.Data.Event calenderEvent = new Google.Apis.Calendar.v3.Data.Event();


            calenderEvent.Summary = "prueba";
            calenderEvent.Description = "Descripción";
            calenderEvent.Status = "confirmed";
            calenderEvent.Visibility = "public";

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

            calenderEvent.Organizer = new Event.OrganizerData
            {
                Email = "jtorrstorrs@gmail.com",
                Self = true
            };
            calenderEvent.Creator = new Google.Apis.Calendar.v3.Data.Event.CreatorData
            {
                DisplayName = "",//nombre del docente
                Email= "jtorrstorrs@gmail.com"
            };

            //calenderEvent.Attendees = new List<EventAttendee>
            //{
            //    new EventAttendee
            //    {
            //        Email="jesustorrst@gmail.com",
            //        DisplayName="",
            //        ResponseStatus="accepted"
            //    }
            //};

            //var Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" };
            calenderEvent.Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" }; 

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
                ConferenceSolution = new ConferenceSolution
                {
                    Key = new ConferenceSolutionKey
                    {
                        Type = "hangoutsMeet"
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


            return calenderEvent;
        }

        public void Credentials()
        {
            string[] scopes = new string[] { CalendarService.Scope.Calendar };

            GoogleCredential credential;

            using (var stream = new FileStream(@"C:\Users\52922\source\repos\DashboardMaterias\Meet\credentials2.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                                 .CreateScoped(scopes);
            }

            //credential = credential.CreateScoped(new[] { "https://www.googleapis.com/auth/calendar" });
            //try
            //{
            //    Task<string> task = ((ITokenAccess)credential).GetAccessTokenForRequestAsync();
            //    task.Wait();
            //    string bearer = task.Result;

            //}
            //catch (AggregateException ex)
            //{
            //    throw ex.InnerException;
            //}
            

            var service = new CalendarService(new BaseClientService.Initializer()

            {

                HttpClientInitializer = credential,

                ApplicationName = "",

            });

            var calenderEvent = CreateObject(DateTime.Now);

            try

            {
                String calendarId = "primary";
                EventsResource.InsertRequest request = service.Events.Insert(calenderEvent, "jtorrstorrs@gmail.com");
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
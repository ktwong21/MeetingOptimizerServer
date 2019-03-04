using MeetingOptimizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Routing;

namespace MeetingOptimizer.Controllers
{
    public class MeetingsController : ApiController
    {
        private MeetingOptimizerData meetingOptimizerData;

        public MeetingsController()
        {
            meetingOptimizerData = new MeetingOptimizerData();
        }

        //
        // GET: /Meeting/
        [Route("/list")]
        [HttpGet]        
        public string List()
        {
            var meetings = meetingOptimizerData.Meetings.ToList();
            return JsonConvert.SerializeObject(meetings);
        }

        //To allow OPTIONS in Cors
        public string OptionsMeetings()
        {
            return null;
        }

        [Route("/update")]
        [HttpPost]
        public HttpResponseMessage UpdateMeetings(List<Meeting> meetings)
        {
            HttpResponseMessage response;
            try
            {
                //Delete Unused meetings
                meetingOptimizerData.Meetings.RemoveRange(meetingOptimizerData.Meetings.Where(x => x.Id == x.Id));

                int meetingIndex = 1;
                //Reset IDs for meetings
                foreach (var m in meetings)
                {
                    m.Id = meetingIndex;
                    meetingOptimizerData.Meetings.Add(m);
                    meetingIndex++;
                }

                meetingOptimizerData.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.OK, "Update succeeded");
                //response.Content = new StringContent("hello", Encoding.Unicode);

            }catch(Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, "Update failed: "+ex.Message);
            }
            return response;
        }

    }
}

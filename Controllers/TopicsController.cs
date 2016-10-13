using MessageBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessageBoard.Controllers
{
    public class TopicsController : ApiController
    {
        private IMessageBoardRepository _repo;
        public TopicsController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Topic> Get()
        {
            var topics = _repo.GetTopicsIncludingReplies()
                              .OrderByDescending(t => t.CreatedDate)
                              .Take(25)
                              .ToList();
            return topics;
        }

        public HttpResponseMessage Post([FromBody]Topic newTopic)
        {
            if (newTopic.CreatedDate == default(DateTime))
            {
                newTopic.CreatedDate = DateTime.UtcNow;
            }

            if (_repo.AddTopic(newTopic) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newTopic);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}

using Domain;
using System.Web.Http;
using System.Collections.Generic;

namespace WebJobs.Controllers
{
    public class EventsController<T> : ApiController
    {
        // GET: Events
        public virtual ICollection<T> Get()
        {
            return new EventRepository<T>().Get();
        }
    }
}
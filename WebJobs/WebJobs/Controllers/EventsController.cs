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

        public virtual T GetLatest()
        {
            return new EventRepository<T>().GetLatest();
        }

        public virtual ICollection<T> GetEntity(string Id)
        {
            var d = new EventRepository<T>().Get(Id);
            return d;
        }

        public virtual T GetLatestEntity(string Id)
        {
            return new EventRepository<T>().GetLatestEntity(Id);
        }

        public virtual T GetEvent(int Id)
        {
            return new EventRepository<T>().Get(Id);
        }
    }
}
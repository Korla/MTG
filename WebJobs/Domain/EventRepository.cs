using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class EventRepository<T>
    {
        private string Type = typeof(T).ToString();

        public Event Create(string entity, T model)
        {
            var e = new Event
            {
                Entity = entity,
                Type = this.Type,
                Json = JsonConvert.SerializeObject(model)
            };
            using (var context = new EventContext())
            {
                context.Events.Add(e);
                    
                context.SaveChanges();
            }

            return e;
        }

        public ICollection<T> Get()
        {
            var retVal = new List<T>();
            using (var context = new EventContext())
            {
                var jsonObjects =
                    (from e in context.Events
                     orderby e.Id
                     where e.Type == this.Type
                     select e.Json).ToList();

                retVal = (from json in jsonObjects
                          select JsonConvert.DeserializeObject<T>(json)).ToList();
            }

            return retVal;
        }

        public ICollection<T> Get(string entity)
        {
            var retVal = new List<T>();
            using (var context = new EventContext())
            {
                var jsonObjects =
                    (from e in context.Events
                     orderby e.Id
                     where e.Type == this.Type && e.Entity == entity
                     select e.Json).ToList();

                retVal = (from json in jsonObjects
                          select JsonConvert.DeserializeObject<T>(json)).ToList();
            }

            return retVal;
        }

        public T Get(int id)
        {
            T retVal = default(T);
            using (var context = new EventContext())
            {
                var json = context.Events.First(e => e.Id == id).Json;

                retVal = JsonConvert.DeserializeObject<T>(json);
            }

            return retVal;
        }
    }
}
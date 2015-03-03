using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class EventRepository<T>
    {
        private string Type = typeof(T).ToString();

        public void Create(string entity, T model)
        {
            using (var context = new EventContext())
            {
                context.Events.Add(
                    new Event
                    {
                        Entity = entity,
                        Type = this.Type,
                        Json = JsonConvert.SerializeObject(model)
                    });
                context.SaveChanges();
            }
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
    }
}
using Newtonsoft.Json;
using System;
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

        public IEnumerable<Event> Create(IEnumerable<T> entities, Func<T, string> keySelector)
        {
            var events = entities.Select(model => new Event { Type = this.Type, Json = JsonConvert.SerializeObject(model), Entity = keySelector.Invoke(model) });
            using (var context = new EventContext())
            {
                context.Events.AddRange(events);

                context.SaveChanges();
            }

            return events;
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

        public T GetLatest()
        {
            var retVal = default(T);
            using (var context = new EventContext())
            {
                var json =
                    (from e in context.Events
                     orderby e.Id descending
                     where e.Type == this.Type
                     select e.Json).First();

                retVal = JsonConvert.DeserializeObject<T>(json);
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

        public T GetLatestEntity(string entity)
        {
            var retVal = default(T);
            using (var context = new EventContext())
            {
                var json =
                    (from e in context.Events
                     orderby e.Id descending
                     where e.Type == this.Type && e.Entity == entity
                     select e.Json).First();

                retVal = JsonConvert.DeserializeObject<T>(json);
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
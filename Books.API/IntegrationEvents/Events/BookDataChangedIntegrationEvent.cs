using BookStore.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.IntegrationEvents.Events
{
    public class BookDataChangedIntegrationEvent : IntegrationEvent
    {
        public string BookId { get; set; }
    }
}

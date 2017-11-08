using BookStore.EventBus.Abstractions;
using BookStore.Resiliance.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using Search.API.DataModel;
using Search.API.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.IntegrationEvents.EventHandling
{
    public class BookDataChangedIntegrationEventHandler : IIntegrationEventHandler<BookDataChangedIntegrationEvent>
    {
        private readonly SearchContext context;
        private readonly IHttpClient httpClient;

        public BookDataChangedIntegrationEventHandler(
            SearchContext context,
            IHttpClient httpClient)
        {
            this.context = context;
            this.httpClient = httpClient;
        }

        public async Task Handle(BookDataChangedIntegrationEvent @event)
        {
            var resp = await httpClient.GetStringAsync($"http://localhost:6500/api/books/{@event.BookId}");
            var book = JsonConvert.DeserializeObject<BookDetailsViewModel>(resp);
            
            await context.Books.ReplaceOneAsync(
                b => b.Id == @event.BookId,
                new BookSearchModel
                {
                    Id = book.Id,
                    Author = book.Author,
                    Title = book.Title,
                    FreeItemCount = book.BookItems.Where(i => i.State == "Free").Count()
                },
                new UpdateOptions { IsUpsert = true });
        }

        class BookDetailsViewModel
        {
            public string Id { get; set; }

            public string Author { get; set; }

            public string Title { get; set; }

            public string Isbn { get; set; }

            public List<BookItemListViewModel> BookItems { get; set; } = new List<BookItemListViewModel>();
        }

        class BookItemListViewModel
        {
            public string Id { get; set; }

            public string Barcode { get; set; }

            public string Condition { get; set; }

            public string State { get; set; }
        }
    }
}

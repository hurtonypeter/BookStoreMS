using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KonyvtarMVC.Web.Models.Books;
using Books.API.DataModel;
using Microsoft.EntityFrameworkCore;
using Books.API.DataModel.Entities;
using Books.API.IntegrationEvents.Events;
using BookStore.EventBus.Abstractions;

namespace Books.API.Controllers
{
    [Produces("application/json")]
    [Route("api/bookItems")]
    public class BookItemsController : Controller
    {
        private readonly BookContext context;
        private readonly IEventBus eventBus;

        public BookItemsController(
            BookContext context,
            IEventBus eventBus)
        {
            this.context = context;
            this.eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IActionResult> List(string bookId)
        {
            var list = await context.BookItems
                .Where(x => x.BookId == bookId)
                .Select(i => new BookItemListViewModel
                {
                    Id = i.Id,
                    Barcode = i.Barcode,
                    State = i.State
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{bookItemId}")]
        public async Task<IActionResult> Get(string bookItemId)
        {
            var item = await context.BookItems
                .SingleOrDefaultAsync(i => i.Id == bookItemId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookItemEditViewModel model)
        {
            var bookItem = new BookItem
            {
                Id = Guid.NewGuid().ToString(),
                BookId = model.BookId,
                Barcode = model.Barcode,
                Condition = model.Condition,
                State = "Free"
            };
            context.BookItems.Add(bookItem);
            await context.SaveChangesAsync();

            eventBus.Publish(new BookDataChangedIntegrationEvent { BookId = model.BookId });

            return Ok(bookItem.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] BookItemEditViewModel model)
        {
            var current = await context.BookItems.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.Entry<BookItem>(current).CurrentValues.SetValues(model);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var current = await context.BookItems.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.BookItems.Remove(current);
            await context.SaveChangesAsync();

            eventBus.Publish(new BookDataChangedIntegrationEvent { BookId = current.BookId });
            
            return Ok();
        }
    }
}
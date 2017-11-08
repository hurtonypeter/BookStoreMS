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
using BookStore.EventBus.Abstractions;
using Books.API.IntegrationEvents.Events;

namespace Books.API.Controllers
{
    [Produces("application/json")]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly BookContext context;
        private readonly IEventBus eventBus;

        public BooksController(
            BookContext context,
            IEventBus eventBus)
        {
            this.context = context;
            this.eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await context.Books
                .Select(b => new BookListViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    Isbn = b.Isbn
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var book = await context.Books
                .Include(b => b.BookItems)
                .SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookDetailsViewModel
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                Isbn = book.Isbn,
                BookItems = book.BookItems.Select(i => new BookItemListViewModel
                {
                    Id = i.Id,
                    Barcode = i.Barcode,
                    State = i.State,
                    Condition = i.Condition.ToString()
                }).ToList()
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookEditViewModel model)
        {
            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                Author = model.Author,
                Title = model.Title,
                Isbn = model.Isbn
            };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            eventBus.Publish(new BookDataChangedIntegrationEvent { BookId = book.Id });

            return Ok(book.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] BookEditViewModel model)
        {
            var current = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.Entry<Book>(current).CurrentValues.SetValues(model);

            await context.SaveChangesAsync();

            eventBus.Publish(new BookDataChangedIntegrationEvent { BookId = id });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var current = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.Books.Remove(current);
            await context.SaveChangesAsync();

            eventBus.Publish(new BookDataChangedIntegrationEvent { BookId = id });

            return Ok();
        }
    }
}
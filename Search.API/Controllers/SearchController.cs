using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search.API.DataModel;
using MongoDB.Driver;

namespace Search.API.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly SearchContext context;

        public SearchController(SearchContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            await context.Books.InsertOneAsync(new BookSearchModel
            {
                Id = "07396388-f614-4970-bdd8-1f4af7bd12e8",
                Author = "Agatha Christie",
                Title = "Tíz kicsi néger"
            });

            return Ok();
        }

        [HttpGet("{term}")]
        public async Task<IActionResult> Get(string term)
        {
            term = term.ToLower();
            var filter = new FilterDefinitionBuilder<BookSearchModel>()
                .Where(x => x.Author.ToLower().Contains(term) || x.Title.ToLower().Contains(term));
            var books = await context.Books.Find(filter).ToListAsync();

            return Ok(books);
        }
    }
}
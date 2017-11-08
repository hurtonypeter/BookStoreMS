using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Books.API.DataModel;
using Microsoft.EntityFrameworkCore;
using Books.API.Models;
using Newtonsoft.Json.Linq;
using BookStore.Resiliance.Http;
using Newtonsoft.Json;
using Books.API.DataModel.Entities;

namespace Books.API.Controllers
{
    [Produces("application/json")]
    [Route("api/rents")]
    public class RentsController : Controller
    {
        private readonly BookContext context;
        private readonly IHttpClient httpClient;

        public RentsController(
            BookContext context,
            IHttpClient httpClient)
        {
            this.context = context;
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> List(string memberId = null, string bookItemId = null)
        {
            if (string.IsNullOrWhiteSpace(memberId) && string.IsNullOrWhiteSpace(bookItemId))
            {
                return BadRequest();
            }

            var rents = context.Rents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(memberId))
            {
                rents = rents.Where(x => x.MemberId == memberId);
            }
            if (!string.IsNullOrWhiteSpace(bookItemId))
            {
                rents = rents.Where(x => x.BookItemId == bookItemId);
            }

            var resp = await rents.Select(x => new RentModel
            {
                Id = x.Id,
                End = x.End,
                Start = x.Start,
                ReturnDate = x.ReturnDate,
                Barcode = x.BookItem.Barcode,
                Author = x.BookItem.Book.Author,
                Title = x.BookItem.Book.Title
            }).ToListAsync();

            return Ok(resp);
        }

        [HttpPost("rent")]
        public async Task<IActionResult> Rent([FromBody]JObject model)
        {
            var bookBarcode = model["bookBarcode"].ToString();
            var userCardNumber = int.Parse(model["userCardNumber"].ToString());

            var bookItem = await context.BookItems.SingleOrDefaultAsync(i => i.Barcode == bookBarcode);
            if (bookItem == null || bookItem.Rents.Any(i => i.End == null))
            {
                return BadRequest();
            }

            var resp = await httpClient.GetStringAsync($"http://localhost:6502/api/members/search/{userCardNumber}");
            var member = JsonConvert.DeserializeObject<Member>(resp);
            if (member == null)
            {
                return BadRequest();
            }

            bookItem.State = "Rented";
            var rent = new Rent
            {
                Id = Guid.NewGuid().ToString(),
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(14),
                BookItemId = bookItem.Id,
                MemberId = member.Id
            };

            context.Rents.Add(rent);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return([FromBody]JObject model)
        {
            var bookBarcode = model["bookBarcode"].ToString();

            var bookItem = await context.BookItems
                .Include(i => i.Rents)
                .SingleOrDefaultAsync(i => i.Barcode == bookBarcode);
            if (bookItem == null)
            {
                return BadRequest();
            }

            var lastRent = bookItem.Rents.SingleOrDefault(r => r.ReturnDate == null);
            if (lastRent == null)
            {
                return BadRequest();
            }

            bookItem.State = "Free";
            lastRent.ReturnDate = DateTime.Now;

            await context.SaveChangesAsync();

            return Ok();
        }
        
        class Member
        {
            public string Id { get; set; }

            public int CardNumber { get; set; }
        }
    }
}
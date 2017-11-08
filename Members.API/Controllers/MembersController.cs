using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KonyvtarMVC.Web.Models.Member;
using Members.API.DataModel;
using Microsoft.EntityFrameworkCore;
using BookStore.Resiliance.Http;
using Newtonsoft.Json;
using Members.API.DataModel.Entities;

namespace Members.API.Controllers
{
    [Produces("application/json")]
    [Route("api/members")]
    public class MembersController : Controller
    {
        private readonly MemberContext context;
        private readonly IHttpClient httpClient;

        public MembersController(
            MemberContext context,
            IHttpClient httpClient)
        {
            this.context = context;
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await context.Members.ToListAsync();
            return Ok(list);
        }

        [HttpGet("search/{cardNumber}")]
        public async Task<IActionResult> Search(int cardNumber)
        {
            var member = await context.Members
                .SingleOrDefaultAsync(x => x.CardNumber == cardNumber);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id, 
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var member = await context.Members.SingleOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            var resp = await httpClient.GetStringAsync($"http://localhost:6500/api/rents?memberId={id}");
            var rents = JsonConvert.DeserializeObject<List<MemberRentListViewModel>>(resp);

            var model = new MemberDetailsViewModel
            {
                Id = member.Id,
                Address = member.Address,
                BirthDate = member.BirthDate,
                BirthPlace = member.BirthPlace,
                CardNumber = member.CardNumber,
                MothersName = member.MothersName,
                Name = member.Name,
                Rents = rents
            };
            
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] MemberCreateViewModel model, 
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var member = new Member
            {
                Id = Guid.NewGuid().ToString(),
                Address = model.Address,
                BirthDate = model.BirthDate,
                BirthPlace = model.BirthPlace,
                CardNumber = model.CardNumber,
                MothersName = model.MothersName,
                Name = model.Name
            };
            context.Members.Add(member);
            await context.SaveChangesAsync();

            return Ok(member.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] MemberEditViewModel model, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var current = await context.Members.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.Entry<Member>(current).CurrentValues.SetValues(model);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var current = await context.Members.SingleOrDefaultAsync(b => b.Id == id);
            if (current == null)
            {
                return BadRequest();
            }

            context.Members.Remove(current);
            await context.SaveChangesAsync();

            return Ok();
        }
        
    }
}
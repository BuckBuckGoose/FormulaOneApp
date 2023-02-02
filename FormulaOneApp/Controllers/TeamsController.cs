﻿using FormulaOneApp.Data;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private static AppDbContext _context;
        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // "In-Memory DB"
        //private static List<Team> teams = new List<Team>()
        //{
        //    new Team()
        //    {
        //        Country = "Germany",
        //        Id= 1,
        //        Name = "Mercedes AMG F1",
        //        TeamPrincipal = "Toto Wolf"
        //    },
        //    new Team()
        //    {
        //        Country = "Italy",
        //        Id= 2,
        //        Name = "Ferrari",
        //        TeamPrincipal = "Mattia Binotto"
        //    },
        //    new Team()
        //    {
        //        Country = "Swiss",
        //        Id= 3,
        //        Name = "Alfa Romeo",
        //        TeamPrincipal = "Frederic Vasseur"
        //    }

        //};

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teams = await _context.Teams.ToListAsync<Team>();
            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
            {
                return BadRequest("Invalid Id");
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", team.Id, team);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, string country)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
                return BadRequest("Invalid Id");

            team.Country = country;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
                return BadRequest("Invalid Id");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

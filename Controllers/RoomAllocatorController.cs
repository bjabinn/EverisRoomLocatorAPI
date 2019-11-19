using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalasEveris.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomAllocatorController : ControllerBase
    {
        private readonly ILogger<RoomAllocatorController> _logger;
        private readonly RoomContext _context;

        public RoomAllocatorController(ILogger<RoomAllocatorController> logger, RoomContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> Get()
        {
            try {
                var results = _context.Room.ToListAsync();
                return await results;
            } catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Get: " + ex.Message);
            }                        
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            try
            {
                var room = await _context.Room.FirstOrDefaultAsync(m => m.Id == id);
                if (room == null)
                {
                    return NotFound();
                }

                return room;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Get{id}: " + ex.Message);
            }

        }

        [HttpPost]        
        public async Task<ActionResult<Room>> Post([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var objetoInsertado = _context.Room.Add(room);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Post{room}: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete (long id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        private bool RoomExists(long id)
        {
            return _context.Room.Any(e => e.Id == id);
        }

    } //end of class
} //end of namespace

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            try {
                var results = _context.Room.ToList();
                return Ok(results);
            } catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Get: " + ex.Message);
            }                        
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var room = _context.Room.FirstOrDefault(m => m.Id == id);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Get{id}: " + ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] Room room)
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
                _context.SaveChanges();

                return Ok(objetoInsertado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "RoomAllocatorController - Post{room}: " + ex.Message);
            }
        }

        [HttpPut]
        public Room Put(Room room)
        {
            return null;
        }

        [HttpDelete]
        public Room Delete (Room room)
        {
            return null;
        }
    }
}

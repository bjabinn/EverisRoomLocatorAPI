using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SalasEveris
{
    public class RoomContext : DbContext
    {
        public DbSet<Room> Room { get; set; }

        public RoomContext(DbContextOptions<RoomContext> options) : base(options)
        {            
        }               
    }
}

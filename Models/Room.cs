using System;
using System.ComponentModel.DataAnnotations;

namespace SalasEveris
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Floor { get; set; }

        public string Number { get; set; }

        public string Explanation { get; set; }
    }
}

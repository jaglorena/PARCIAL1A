﻿using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.Models
{
    public class AutorLibro
    {
        [Key]
        public int AutorId { get; set; }
        public int LibroId { get; set; }
        public int Orden { get; set; }
    }
}

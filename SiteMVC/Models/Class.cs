﻿using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public int? Name { get; set; }
    }
}

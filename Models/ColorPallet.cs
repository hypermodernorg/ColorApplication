using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using ColorApplication.Areas.Identity.Data;

namespace ColorApplication.Models
{
    public class ColorPallet
    {
        public Guid Id { get; set; } // Auto primary key by convention.
        public Guid UId { get; set; } // Key to User
        public string Name { get; set; }
        public string Description { get; set; }

        public string Pallet { get; set; } // Store as JSON

        [NotMapped]
        public List<SingleColor> Colors { get; set; }

        public bool IsPublic {get; set;}
        public bool IsCopy { get; set; }
    }

    [NotMapped]
    public class SingleColor
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string CHexA { get; set; }
    }
}

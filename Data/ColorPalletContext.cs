using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ColorApplication.Models;

namespace ColorApplication.Data
{
    public class ColorPalletContext : DbContext
    {
        public ColorPalletContext (DbContextOptions<ColorPalletContext> options)
            : base(options)
        {
        }

        public DbSet<ColorApplication.Models.ColorPallet> ColorPallet { get; set; }
    }
}

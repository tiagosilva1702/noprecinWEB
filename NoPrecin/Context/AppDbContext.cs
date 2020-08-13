using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoPrecin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NoPrecin.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        { }
    }
}

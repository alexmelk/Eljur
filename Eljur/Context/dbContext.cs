using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Eljur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context.Tables;

namespace Eljur.Context
{
    public class dbContext : IdentityDbContext<User>
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }

        public DbSet<Student> Student { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<Visit> Visit { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Eljur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context.Tables;
using Eljur.EF.Custom.Entities;

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
        public DbSet<GroupVisit> GroupVisit { get; set; }
        public DbSet<StudentVisit> StudentVisit { get; set; }
        public DbSet<ThemeGroup> ThemeGroup { get; set; }
        public DbSet<ThemeVisit> ThemeVisit { get; set; }

        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<EducationDepartment> EducationDepartments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterStudent> SemesterStudents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Check> Checks { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<BatchFee> BatchFee { get; set; }
        public DbSet<CourseSubject> CourseSubject { get; set; }
        public DbSet<BatchStudent> BatchStudent { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Assessment> Assessment { get; set; }
        public DbSet<AssessmentSchedule> AssessmentSchedule { get; set; }
        public DbSet<Institute> Institute { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<SubjectOutline> SubjectOutline { get; set; }
        public DbSet<StudentDetail> StudentDetail { get; set; }
        public DbSet<SubjectDetail> SubjectDetail { get; set; }
        public DbSet<Submission> Submission { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Message> Message { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().HasData(
            //     new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            //      new Category { Id = 2, Name = "Comedy", DisplayOrder = 2 },
            //      new Category { Id = 3, Name = "Romance", DisplayOrder = 3 },
            //    new Category { Id = 4, Name = "History", DisplayOrder = 4 });
            modelBuilder.Entity<Student>()
            .HasIndex(s => s.RegistrationNo)
            .IsUnique();

            modelBuilder.Entity<BatchStudent>()
            .HasIndex(s => s.EnrollmentNo)
            .IsUnique();
        }
    }
}
